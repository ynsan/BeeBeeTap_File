using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ほぼプレイヤー用のScript

public class GUIScript : MonoBehaviour {

	public int Score_have;

	[Header("--------------------")]
	public Text score_text;
	public Text time_text;
	public int life_val = 3;
	public Image[] Life;

	public Text stanbyCount_text;

	// ゲームクリアとゲームオーバーの黒BG
	public GameObject Black_layer;
	public Text GameClear;
	public Text GameOver;
	public Text lastGetScore_text;
	public Text lifeBounus_text;

	public GameObject GetSpilit;

	public int intTime_yl = 60;
	private float Time_tl = 1;
	public float standby_tl = 3;
	public float gotoCan_tl = 4;	// シーン移行許可用タイムレート

	[HideInInspector]
	public bool GameEnd_bool = false;

	private bool GameClear_bool = false;
	private bool gotoCleck_bool = false;

	public AudioSource SE_point;
	public AudioSource SE_timeLimit;

	void Start () {
		Score_have = 0;
		intTime_yl = 60;
		Time_tl = 1;
		standby_tl = 4;
		GameEnd_bool = false;
		GameClear_bool = false;
		gotoCleck_bool = false;

		score_text.text = "とくてん\n" + Score_have;
		time_text.text = intTime_yl.ToString (); //((int)Time_tl).ToString ();
	}
	void FixedUpdate (){
		if (standby_tl <= 0) {
			// 強制的に0以下にならないように修正
			if (Score_have <= 0) {
				Score_have = 0;
			}

			// タイム
			if (GameEnd_bool != true) {
				Time_tl -= Time.deltaTime;
				if (Time_tl <= 0) {
					intTime_yl--;
					time_text.text = intTime_yl.ToString ();
					Time_tl = 1;
					if (intTime_yl < 10 && intTime_yl > 0) {
						time_text.color = new Color32(255, 0, 0, 255);
						SE_timeLimit.Play ();
					} 
				} else if (intTime_yl <= 0) {
					Time_tl = 0;
					if (GameEnd_bool == false) {
						GameEnd_bool = true;
						GameClear_bool = true;
					}
				}
			}


			if (life_val == 3) {
				Life [0].enabled = true;
				Life [1].enabled = true;
				Life [2].enabled = true;
			} else if (life_val == 2) {
				Life [0].enabled = true;
				Life [1].enabled = true;
				Life [2].enabled = false;
			} else if (life_val == 1) {
				Life [0].enabled = true;
				Life [1].enabled = false;
				Life [2].enabled = false;
			} else if (life_val == 0) {
				Life [0].enabled = false;
				Life [1].enabled = false;
				Life [2].enabled = false;
				GameEnd_bool = true;
			}
		}
	}

	void Update (){
		
		score_text.text = "とくてん\n" + Score_have;

		// タイム
		if (standby_tl >= 0) {
			standby_tl -= Time.deltaTime;
			stanbyCount_text.text = ((int)standby_tl).ToString ();
			if (standby_tl <= 1) {
				stanbyCount_text.text = "スタート！";
			}
			if (standby_tl <= 0 && stanbyCount_text.enabled == true) {
				stanbyCount_text.enabled = false;
			}
		} else if (standby_tl <= 0) {
			if (Input.GetMouseButtonDown (0)) {
			
				Vector3 TapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Collider2D col = Physics2D.OverlapPoint (TapPoint);

				if (col) {
					if (GameEnd_bool == false) {
						//GameObject obj = col.transform.gameObject;
						// rayが当たったオブジェクトの名前を取得
						if (col.transform.gameObject.CompareTag ("Enemy")) {
							BeeController _BeeController_ = col.transform.gameObject.GetComponent<BeeController> ();
							Score_have += _BeeController_.Score_point;
							Debug.Log ("敵を倒した！");
							SE_point.Play ();
							GameObject inst = Instantiate (GetSpilit, col.gameObject.transform.position, Quaternion.identity);
							_BeeController_.BeeBroken ();
							Destroy (col.transform.gameObject);
						}
						if (col.transform.gameObject.CompareTag ("Bee")) {
							BeeController _BeeController_ = col.transform.gameObject.GetComponent<BeeController> ();
							Score_have -= _BeeController_.Score_point % 2;
							Debug.Log ("ミツバチを誤つぶし…");
							_BeeController_.BeeBroken ();
							Destroy (col.transform.gameObject);
						}
					}
				}

				if (gotoCleck_bool == true) {
					gotoTitle ();
				}
			}

			// デバッグキー
			if (Input.GetKeyDown ("1")) {
				intTime_yl = 5;
			}

			if (GameEnd_bool == true) {
				int life_plus = life_val * 1000;
				Black_layer.SetActive (true);
				lastGetScore_text.text = "とくてん：" + (Score_have + life_plus);
				lifeBounus_text.text = "ライフボーナス：+" + life_plus;

				// ゲームクリアorゲームオーバー
				if (GameClear_bool == true) {
					GameClear.enabled = true;
					GameOver.enabled = false;
				}
				if (GameClear_bool == false) {
					GameOver.enabled = true;
					GameClear.enabled = false;
				}

				// タイム
				if (gotoCan_tl >= 0) {
					gotoCan_tl -= Time.deltaTime;
				} else {
					gotoCleck_bool = true;
				}
			}
		}
	}

	public void gotoTitle() {
		Destroy (GameObject.Find ("Player_").gameObject);
		SceneManager.LoadScene("Title");
	}
}
