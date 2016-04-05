using UnityEngine;
using System.Collections;

public class EarthquakeController : MonoBehaviour {

	// special FX during the quake
	private ParticleSystem _ceilingDustPfx;
	private GameObject _lightSparks1;
	private GameObject _lightSparks2;
	private GameObject _ceilingLight1;
	private bool _light1dead;
	private AudioSource _rumbleAudiosource;

	// camera shake effect
	public Transform _cameraToShake;		// define this camera in the inspector
	private bool _shakeCamera;
	private float _shakeStartTime;
	public float _shakeDuration;



	void Start () {
		// Find all of the objects we need for Special FX.
		_ceilingDustPfx = GameObject.Find("Ceiling Dust PFX").GetComponent<ParticleSystem>();
		_lightSparks1 = GameObject.Find("Light Sparks 1");
		_lightSparks2 = GameObject.Find("Light Sparks 2");
		_ceilingLight1 = GameObject.Find("Spotlight 1");
		_rumbleAudiosource = GameObject.Find("Rumble AudioSource").GetComponent<AudioSource>();

		// Deactivate some objects. (Activating these objects triggers their effect.)
		_lightSparks1.SetActive(false);
		_lightSparks2.SetActive(false);

		// Find the ACTIVE camera in the scene.
		if (GameObject.Find("[CameraRig]")) {
			_cameraToShake = GameObject.Find("[CameraRig]/Camera (head)").transform;
		} else {
			_cameraToShake = GameObject.Find("FPSController/FirstPersonCharacter").transform;
		}
	}
	

	void Update () {
		if (_shakeCamera) {
			ShakeCamera();
		}
	}


	public void StartQuake () {
		StartCoroutine(QuakeSequence());
	}


	private void ShakeCamera () {
		float _newX = _cameraToShake.position.x + Random.Range(-0.01f, 0.01f);
		_cameraToShake.position = new Vector3 (_newX, _cameraToShake.position.y, _cameraToShake.position.z);

		if (Time.time > _shakeStartTime + _shakeDuration) {
			_shakeCamera = false;
		}
	}


	IEnumerator QuakeSequence () {
		_rumbleAudiosource.Play();
		yield return new WaitForSeconds(0.5f);
		_shakeCamera = true;
		_shakeStartTime = Time.time;
		yield return new WaitForSeconds(0.1f);
		_ceilingDustPfx.Play();
		yield return new WaitForSeconds(4.0f);
		StartCoroutine(KillLights1());
	}


	IEnumerator KillLights1 () {
		_light1dead = true;
		_lightSparks1.SetActive(true);
		_ceilingLight1.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		_lightSparks2.SetActive(true);
	}
}
