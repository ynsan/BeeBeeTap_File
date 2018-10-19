using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	public Transform SpawnPoint;

	public GameObject[] Character;
	public GameObject[] LevelUP_Character;
	private int char_rnd;

	private float spawnTL_min = 0.65f;
	private float spawnTL_max = 0.8f;

	private float timeEl;	// タイム用
	private float spawnTime;
	private float time_rnd;
	private float levelUp_time;

	private int level = 0;

	public GUIScript _GUIScript_;

	void Start () {
		char_rnd = Random.Range (0, Character.Length);
		time_rnd = Random.Range (spawnTL_min, spawnTL_max);
		spawnTime = time_rnd;

		level = 0;
	}

	void Update () {
		if (_GUIScript_.standby_tl <= 0) {
			if (_GUIScript_.GameEnd_bool == false) {
				this.gameObject.transform.Rotate (0, 0, 5.4f);

				timeEl += Time.deltaTime;
				if (timeEl >= spawnTime) {
					// スポーン実行
					GameObject instChar = Instantiate (Character [char_rnd], SpawnPoint.position, Quaternion.identity);
					timeEl = 0f;
					char_rnd = Random.Range (0, Character.Length);
					time_rnd = Random.Range (spawnTL_min, spawnTL_max);
					spawnTime = time_rnd;
				}

				levelUp_time += Time.deltaTime;
				if (levelUp_time >= 10) {
					GameObject instChar = Instantiate (Character [1], SpawnPoint.position, Quaternion.identity);
					spawnTL_max -= 0.1f;
					// レベルアップ
					Debug.Log ("" + level);
					level++;
					if (level == 2) {
						Debug.Log ("残り" + _GUIScript_.intTime_yl + "秒");
						Character [2] = LevelUP_Character [0];
					} else if (level == 4) {
						Debug.Log ("残り" + _GUIScript_.intTime_yl + "秒");
						Character[3] = LevelUP_Character[0];
						Character[4] = LevelUP_Character[1];
					} else if (level == 5) {
						Debug.Log ("残り" + _GUIScript_.intTime_yl + "秒");
						Character[5] = LevelUP_Character[1];
					}
					levelUp_time = 0;
				}
			}
		}
	}
}
