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
	//public float _shakeMaxMovement;

	// special FX during the quake
	private ParticleSystem _ceilingDustPfx;
	private GameObject _lightSparks1;
	private GameObject _lightSparks2;
	private GameObject _ceilingLight1;
	private bool _light1dead;

	// Audio for the TV
	private AudioSource _tvAudioSource;
	public AudioClip getRedCube;
	public AudioClip getBlueCube;
	public AudioClip getGreenCube;
	public AudioClip goodJob;
	public AudioClip niceWork;
	public AudioClip wow;

	void Start () {
		_tvText = GameObject.Find("Dynamic GUI/TV Text").GetComponent<Text>();
		_timerText = GameObject.Find("Dynamic GUI/Timer Text").GetComponent<Text>();
		_tvAudioSource = GameObject.Find("Sequence Manager/TV Audio Source").GetComponent<AudioSource>();
		_ceilingDustPfx = GameObject.Find("Ceiling Dust PFX").GetComponent<ParticleSystem>();
		_lightSparks1 = GameObject.Find("Light Sparks 1");
		_lightSparks1.SetActive(false);
		_lightSparks2 = GameObject.Find("Light Sparks 2");
		_lightSparks2.SetActive(false);
		_ceilingLight1 = GameObject.Find("Spotlight 1");

		StartCoroutine(PackRedCube());
	} // end of Start()
	

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
			_tvAudioSource.clip = goodJob;
			_tvAudioSource.Play();
			StartCoroutine(PackBlueCube());
		}

		if (_tvText.text == "blue cube" && itemName == "blue cube") {
			_tvAudioSource.clip = niceWork;
			_tvAudioSource.Play();
			StartCoroutine(PackGreenCube());
		}

		if (_tvText.text == "green cube" && itemName == "green cube") {
			_tvText.text = "go under table";
			_shakeCamera = true;
			_shakeStartTime = Time.time;
			_tvAudioSource.clip = wow;
			_tvAudioSource.Play();
			gameObject.GetComponent<AudioSource>().Play();
			_ceilingDustPfx.Play();
		}
	}

	IEnumerator PackRedCube () {
		_tvText.text = "red cube";
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = getRedCube;
		_tvAudioSource.Play();
	}

	IEnumerator PackBlueCube () {
		_tvText.text = "blue cube";
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = getBlueCube;
		_tvAudioSource.Play();
	}

	IEnumerator PackGreenCube () {
		_tvText.text = "green cube";
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = getGreenCube;
		_tvAudioSource.Play();
	}

	IEnumerator KillLights1 () {
		_light1dead = true;
		_lightSparks1.SetActive(true);
		_ceilingLight1.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		_lightSparks2.SetActive(true);
	}

	private void ShakeCamera () {

		// add some equation that converts shake magnitude to a parabola
		float _newX = _cameraToShake.position.x + Random.Range(-0.01f, 0.01f);
		_cameraToShake.position = new Vector3 (_newX, _cameraToShake.position.y, _cameraToShake.position.z);

		if (Time.time > _shakeStartTime + 4 && _light1dead == false) {
			StartCoroutine(KillLights1());
		}

		if (Time.time > _shakeStartTime + _shakeDuration) {
			_shakeCamera = false;
			//_ceilingDustPfx.Stop();												// technically not necessary since PFX only last 8 seconds
		}
	}
}
