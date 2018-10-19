using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpilitTarget : MonoBehaviour {

	public GameObject Target;
	public float Speed = 4f;

	public GameObject DestroyParticle;

	void Start () {
		Target = GameObject.Find ("score_pos").gameObject;
	}

	void Update () {
		//ユーザの場所を特定
		Vector2 Predetor = Target.transform.position;
		float x = Predetor.x;
		float y = Predetor.y;
		//追跡方向の決定
		Vector2 direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;
		//ターゲット方向に力を加える
		GetComponent<Rigidbody2D> ().velocity = (direction * Speed);
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("S_p")){
			GameObject inst = Instantiate (DestroyParticle, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
