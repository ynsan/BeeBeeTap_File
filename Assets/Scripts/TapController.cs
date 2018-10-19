using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour {

	// クリックした位置座標
	private Vector3 clickPosition;

	public GameObject TapGuideEffect;

	void Start () {
		this.gameObject.name = "Player_";

		// 消えないゲームオブジェクト化
		DontDestroyOnLoad(this.gameObject);
	}

	void FixedUpdate () {

		if (Input.GetMouseButtonDown (0)) {
			clickPosition = Input.mousePosition;
			clickPosition.z = 1f;
			GameObject c_p = Instantiate (TapGuideEffect,  Camera.main.ScreenToWorldPoint(clickPosition), Quaternion.identity);
		}
	}
}
