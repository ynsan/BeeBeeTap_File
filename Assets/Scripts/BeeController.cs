using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

	// ハチの制御

	[Header("isEnemy の扱いでスコアの加算方法が変わります")]
	public int Score_point = 100;

	public float speed_min = 0.8f;
	public float speed_max = 2.4f;
	private float Speed = 2f;

	[Header("0なら効果なし")]
	public float creepSpeed_x = 0;

	public GameObject Target;

	public GameObject break_particle;

	public GameObject GetSpilit;

	private Rigidbody2D RB;

	// 良いハチも悪いハチもAnimatorの扱いは同じ
	public Animator animator;
	public AudioSource audio;

	public bool notTap = false;

	[HideInInspector]
	public GUIScript _GUIScript_;

	//今までいた位置を保持
	public Vector2 prev;


	private float in_tl = 0;

	void Start () {
		// GUIScriptの自動取得
		_GUIScript_ = GameObject.Find ("Canvas").gameObject.GetComponent<GUIScript>();
		RB = this.GetComponent<Rigidbody2D>();
		animator = this.GetComponent<Animator>();
		audio = this.GetComponent<AudioSource>();

		Target = GameObject.Find ("House").gameObject;

		Speed = Random.Range (speed_min, speed_max);

		prev = transform.position;
	}

	void Update () {
		//ユーザの場所を特定
		Vector2 Predetor = Target.transform.position;

		float x = Predetor.x;
		float y = Predetor.y;
		//追跡方向の決定
		Vector2 direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;
		//ターゲット方向に力を加える
		RB.velocity = (direction * Speed);

		//本体の向きを調整
		Vector2 Position = transform.position;  
		Vector2 diff = Position - prev;
		if (diff.magnitude > 0.01) {
			float angle = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}

		if (notTap == true) {
			in_tl += Time.deltaTime;
			if (in_tl >= 1f) {
				Debug.Log ("きえた");
				Destroy (this.gameObject);
			}
		} else {
			RB.AddForce (transform.right * creepSpeed_x);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag("Hole")){
			notTap = true;
			animator.SetBool("Bool_Inhouse", notTap);
			// ブレ防止のスピードダウン
			Speed = 0.8f;
			if (_GUIScript_.GameEnd_bool == false) {
				if (this.gameObject.CompareTag ("Bee")) {
					Debug.Log ("ハチ入りポイント加算");
					_GUIScript_.Score_have += Score_point;
					GameObject inst = Instantiate (GetSpilit, this.gameObject.transform.position, Quaternion.identity);
				} else if (this.gameObject.CompareTag ("Enemy")) {
					_GUIScript_.life_val--;
				}
				audio.Play ();
			}
		}
	}


	public void BeeBroken(){
		Instantiate (break_particle, this.transform.position, Quaternion.identity);
	}
}
