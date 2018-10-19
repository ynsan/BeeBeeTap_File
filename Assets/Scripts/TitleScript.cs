using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

	public Transform point;
	public GameObject Bee;

	public GameObject Buttons;
	public GameObject Description;

	public Text firstGuide;

	private bool gotoCan_bool = false;
	private bool desc_bool = false;

	public AudioSource[] audios;

	private float gotoCan_tl = 0.5f;	// シーン移行許可用タイムレート

	void Awake () {
		switch (Screen.orientation) {
		// 縦画面のとき
		case ScreenOrientation.Portrait:
			// 左回転して左向きの横画面にする
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			break;
		// 上下反転の縦画面のとき
		case ScreenOrientation.PortraitUpsideDown:
			// 右回転して左向きの横画面にする
			Screen.orientation = ScreenOrientation.LandscapeRight;
			break;
		}
	}

	void Start () {
		gotoCan_bool = false;
		desc_bool = false;
		firstGuide.enabled = true;

		Buttons.gameObject.SetActive(false);
		Description.gameObject.SetActive(false);

		gotoCan_tl = 0.5f;
	}

	void Update() {

		if (Bee) {
			if (Input.GetMouseButtonDown (0) && gotoCan_bool == true && firstGuide.enabled == true) {

				point.position = Input.mousePosition;

				gotoCan_tl = 1f;
				firstGuide.enabled = false;
				if (gotoCan_tl == 1f) {
					audios [1].Play ();
				}
			}

			if (firstGuide.enabled == false) {
				Bee.transform.Translate (0, 0.24f, 0);

				if (gotoCan_tl <= 0) {
					Destroy (Bee);
					Buttons.gameObject.SetActive (true);
				}
			}
		}

		if (desc_bool == true && Input.GetMouseButtonDown (0)) {
			Description.gameObject.SetActive(false);
			desc_bool = false;
		}


		// タイム
		if (gotoCan_tl >= 0) {
			gotoCan_tl -= Time.deltaTime;
		} else {
			gotoCan_bool = true;
		}
	}

	public void GoGame() {
		audios [0].Play();
		SceneManager.LoadScene("Game");
	}

	public void show_Description() {
		audios [0].Play();
		Description.gameObject.SetActive(true);
		desc_bool = true;
	}
}
