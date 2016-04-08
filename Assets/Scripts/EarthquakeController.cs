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
	private AudioSource CitySiren;

	// camera shake effect
	private Transform _cameraToShake;
	private bool _shakeCamera;
	private float _shakeStartTime;
	public float _shakeDuration;

	// Particle Effects (Sami)

	public GameObject DustBits;
	public GameObject DustThick;
	public GameObject BuildingSmoke1;
	public GameObject BuildingSmoke2;
	public GameObject BuildingSmoke3;
	public GameObject BuildingSmoke4;


	void Start () {
		// Find all of the objects we need for Special FX.
		_ceilingDustPfx = GameObject.Find("Ceiling Dust PFX").GetComponent<ParticleSystem>();
		_lightSparks1 = GameObject.Find("Light Sparks 1");
		_lightSparks2 = GameObject.Find("Light Sparks 2");
		_ceilingLight1 = GameObject.Find("Spotlight 1");
		_rumbleAudiosource = GameObject.Find("Rumble AudioSource").GetComponent<AudioSource>();
		CitySiren = GameObject.Find("CitySiren").GetComponent<AudioSource>();

		// Deactivate some objects. (Activating these objects triggers their effect.)
		_lightSparks1.SetActive(false);
		_lightSparks2.SetActive(false);

		// Find the active camera in the scene.
		if (GameObject.Find("[CameraRig]")) {
			_cameraToShake = GameObject.Find("[CameraRig]").transform;
		} else {
			_cameraToShake = GameObject.Find("FPSController/FirstPersonCharacter").transform;
		}
	}
	

	void Update () {
		if (_shakeCamera) 
		{
			ShakeCamera();
			//Debug.Log("CAMERA SHAKING");
		}
	}


	public void StartQuake () {						// This function is called by sequenceManager.cs at the end of packing sequence, and also by spacebar.

		CitySiren.Play();
		Invoke ("QuakeStarter", 10);				// Delayed Quake for 10 seconds so player can find table & hear siren
	}


	private void ShakeCamera () {
		float _newX = _cameraToShake.position.x + Random.Range(-0.01f, 0.01f);
		_cameraToShake.position = new Vector3 (_newX, _cameraToShake.position.y, _cameraToShake.position.z);

		if (Time.time > _shakeStartTime + _shakeDuration) {
			_shakeCamera = false;

		}
	}

	void QuakeStarter()
	{
		StartCoroutine(QuakeSequence());
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


		// Particles
		DustBits.SetActive(true);
		DustThick.SetActive(true);
		BuildingSmoke1.SetActive(true);
		BuildingSmoke2.SetActive(true);
		BuildingSmoke3.SetActive(true);
		BuildingSmoke4.SetActive(true);

		// Audio


		// 
	}


	IEnumerator KillLights1 () {
		_light1dead = true;
		_lightSparks1.SetActive(true);
		_ceilingLight1.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		_lightSparks2.SetActive(true);
	}
}
