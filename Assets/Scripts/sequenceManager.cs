using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sequenceManager : MonoBehaviour {

	private Text _tvText;
	private Text _timerText;
	private string _timeString;
	public Transform _cameraToShake;
	public bool _shakeCamera; // set to private after testing
	private float _shakeStartTime;
	public float _shakeDuration;
	public float _shakeMaxMovement;

	void Start () {
		_tvText = GameObject.Find("Dynamic GUI/TV Text").GetComponent<Text>();
		_timerText = GameObject.Find("Dynamic GUI/Timer Text").GetComponent<Text>();
		StartCoroutine(PackRedCube());
	}
	

	void Update () {
		// update time on TV screen
		_timeString = string.Format("{0:0}:{1:00}", Mathf.Floor(Time.time/60), Time.time % 60);
		_timerText.text = _timeString;

		if (_shakeCamera) {
			ShakeCamera();
		}
	}

	public void NewItemCollected (string itemName) {
		if (_tvText.text == "red cube" && itemName == "red cube") {
			StartCoroutine(PackBlueCube());
		}

		if (_tvText.text == "blue cube" && itemName == "blue cube") {
			StartCoroutine(PackGreenCube());
		}

		if (_tvText.text == "green cube" && itemName == "green cube") {
			_tvText.text = "go under table";
			_shakeCamera = true;
			_shakeStartTime = Time.time;
			gameObject.GetComponent<AudioSource>().Play();
		}
	}

	IEnumerator PackRedCube () {
		_tvText.text = "red cube";
		yield return null;
	}

	IEnumerator PackBlueCube () {
		_tvText.text = "blue cube";
		yield return null;
	}

	IEnumerator PackGreenCube () {
		_tvText.text = "green cube";
		yield return null;
	}

	private void ShakeCamera () {

		// add some equation that converts shake magnitude to a parabola

		float _newX = _cameraToShake.position.x + Random.Range(-0.01f, 0.01f);
		_cameraToShake.position = new Vector3 (_newX, _cameraToShake.position.y, _cameraToShake.position.z);

		if (Time.time > _shakeStartTime + _shakeDuration) {
			_shakeCamera = false;
		}
	}
}
