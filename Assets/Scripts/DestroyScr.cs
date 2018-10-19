using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScr : MonoBehaviour {

	// 削除処理を簡単にするためのスクリプト

	public bool Is_Destroy_On = true;
	public bool Is_Destroy_time_On = true;

	public float Destroy_tl = 5f;
	//public float Destroy_time = 500f;

	void Start () {
		if(Is_Destroy_time_On == false){
			Destroy_tl = 0f;
		}
	}
	

	void Update () {

		if (Is_Destroy_On == true) {

			Destroy_tl -= Time.deltaTime;
			if (Destroy_tl <= 0.0) {						// 消滅時間
				//Destroy_tl += Destroy_tl;
				Destroy(this.gameObject);
			}
		}

	}
}
