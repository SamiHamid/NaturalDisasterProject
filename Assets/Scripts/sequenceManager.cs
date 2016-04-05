using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sequenceManager : MonoBehaviour {

	private Text _tvText;
	private Text _timerText;
	private string _timeString;
	public Transform _cameraToShake;
	private bool _shakeCamera;
	private float _shakeStartTime;
	public float _shakeDuration;
	private Renderer _tvImage;
	private int _itemsTotal = 8;				// these 2 will not be necessary
	private int _itemsCollected;			// if timer is used to trigger next part of sequence (instead of completion of packing all items)
	//public float _shakeMaxMovement;
	private bool checkItem;
	private string itemName;
    
    public GameObject earthquake_controller;

	// special FX during the quake
	private ParticleSystem _ceilingDustPfx;
	private GameObject _lightSparks1;
	private GameObject _lightSparks2;
	private GameObject _ceilingLight1;
	private bool _light1dead;

	// Audio for the TV
	private AudioSource _tvAudioSource;
	public AudioClip warning;
	public AudioClip intro;
	public AudioClip alcoholWipes;
	public AudioClip bandages;
	public AudioClip firstAidBook;
	public AudioClip gasMask;
	public AudioClip rollBandage;
	public AudioClip safetyPin;
	public AudioClip scissors;
	public AudioClip triangularBandage;

	// Textures for the TV
	public Material alcoholWipesImg;
	public Material bandagesImg;
	public Material firstAidBookImg;
	public Material gasMaskImg;
	public Material rollBandageImg;
	public Material safetyPinImg;
	public Material scissorsImg;
	public Material triangularBandageImg;
	public Material dropCoverHoldImg;

	// Hammer Targets
	private GameObject _hammerTarget1;
	private GameObject _hammerTarget2;
	private GameObject _hammerTarget3;
	private GameObject _hammerTarget4;

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
		_tvImage = GameObject.Find("Dynamic GUI/Image").GetComponent<Renderer>();

		// Find and deactivate all hammer targets.  Each will be activated later during the sequence.
		_hammerTarget1 = GameObject.Find("Hammer Target 1");
		_hammerTarget2 = GameObject.Find("Hammer Target 2");
		_hammerTarget3 = GameObject.Find("Hammer Target 3");
		_hammerTarget4 = GameObject.Find("Hammer Target 4");
		//_hammerTarget1.SetActive(false);						leave this coded out until hammering is properly part of the sequence
		_hammerTarget2.SetActive(false);
		_hammerTarget3.SetActive(false);
		_hammerTarget4.SetActive(false);

		StartCoroutine(Intro());
	} // end of Start()
	

	void Update () {
		// update time on TV screen
		_timeString = string.Format("{0:0}:{1:00}", Mathf.Floor(Time.time/60), Time.time % 60);
		_timerText.text = _timeString;

		if (_shakeCamera) {
			ShakeCamera();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
            StopAllCoroutines();
            StartCoroutine(DropCoverHold());
			Debug.Log("EARTHQUAKE");
		}

		/*if (Input.GetKeyDown("space")) {
			StartCoroutine(DropCoverHold());
		}*/
	}

	void LateUpdate () {
		if (checkItem) {
			if (_tvText.text == itemName) {
				if (GameObject.Find("bandages")) {
					Debug.Log("found the bandages = " + GameObject.Find("bandages"));
					StartCoroutine(PackBandages());
				} else if (GameObject.Find("first aid book")) {
					StartCoroutine(PackFirstAidBook());
				} else if (GameObject.Find("gas mask")) {
					StartCoroutine(PackGasMask());
				} else if (GameObject.Find("roll bandage")) {
					StartCoroutine(PackRollBandage());
				} else if (GameObject.Find("safety pin")) {
					StartCoroutine(PackSafetyPin());
				} else if (GameObject.Find("scissors")) {
					StartCoroutine(PackScissors());
				} else if (GameObject.Find("triangular bandage")) {
					StartCoroutine(PackTriangularBandage());
				}
			}

			checkItem = false;
		}
	}

	public void NewItemCollected (string itemNameImported) {
		itemName = itemNameImported;
		_itemsCollected ++;
		if (_itemsCollected >= _itemsTotal) {
			StartCoroutine(DropCoverHold());
			return;
		}

		checkItem = true;



		/*
		DO NOT NEED THIS SEQUENCE ANY MORE.  FUNCTION HAS MOVED TO LATEUPDATE()

		if (_tvText.text == "alcohol wipes" && itemName == "alcohol wipes") {
			StartCoroutine(PackBandages());
		}

		if (_tvText.text == "bandages" && itemName == "bandages") {
			if (GameObject.Find("first aid book")) {
				StartCoroutine(PackFirstAidBook());
			} else {
				StartCoroutine(PackGasMask());
			}
		}

		if (_tvText.text == "first aid book" && itemName == "first aid book") {
			if (GameObject.Find("gas mask")) {
				StartCoroutine(PackGasMask());
			} else {
				StartCoroutine(PackRollBandage());
			}
		}

		if (_tvText.text == "gas mask" && itemName == "gas mask") {
			StartCoroutine(PackRollBandage());
		}

		if (_tvText.text == "roll bandage" && itemName == "roll bandage") {
			StartCoroutine(PackSafetyPin());
		}

		if (_tvText.text == "safety pin" && itemName == "safety pin") {
			StartCoroutine(PackScissors());
		}

		if (_tvText.text == "scissors" && itemName == "scissors") {
			StartCoroutine(PackTriangularBandage());
		}
			

		if (_tvText.text == "triangular bandage" && itemName == "triangular bandage") {
			StartCoroutine(DropCoverHold());
		}
		*/

	}

	IEnumerator DropCoverHold () {
        earthquake_controller.SetActive(true);
		_tvText.text = "";
		_tvImage.material = dropCoverHoldImg;
		_shakeCamera = true;
		_shakeStartTime = Time.time;
		//_tvAudioSource.Play();
		gameObject.GetComponent<AudioSource>().Play();
		_ceilingDustPfx.Play();
		yield return new WaitForSeconds(1);
	}

	IEnumerator Intro () {
		_tvText.text = "WARNING!";
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = warning;
		_tvAudioSource.Play();
		yield return new WaitForSeconds(warning.length);
		_tvAudioSource.clip = intro;
		_tvAudioSource.Play();
		yield return new WaitForSeconds(intro.length);
		StartCoroutine(PackAlcoholWipes());
	}



	IEnumerator PackAlcoholWipes () {
		_tvText.text = "alcohol wipes";
		_tvImage.material = alcoholWipesImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = alcoholWipes;
		_tvAudioSource.Play();
	}

	IEnumerator PackBandages () {
		_tvText.text = "bandages";
		_tvImage.material = bandagesImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = bandages;
		_tvAudioSource.Play();
	}

	IEnumerator PackFirstAidBook () {
		_tvText.text = "first aid book";
		_tvImage.material = firstAidBookImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = firstAidBook;
		_tvAudioSource.Play();
	}

	IEnumerator PackGasMask () {
		_tvText.text = "gas mask";
		_tvImage.material = gasMaskImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = gasMask;
		_tvAudioSource.Play();
	}

	IEnumerator PackRollBandage () {
		_tvText.text = "roll bandage";
		_tvImage.material = rollBandageImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = rollBandage;
		_tvAudioSource.Play();
	}

	IEnumerator PackSafetyPin () {
		_tvText.text = "safety pin";
		_tvImage.material = safetyPinImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = safetyPin;
		_tvAudioSource.Play();
	}

	IEnumerator PackScissors () {
		_tvText.text = "scissors";
		_tvImage.material = scissorsImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = scissors;
		_tvAudioSource.Play();
	}

	IEnumerator PackTriangularBandage () {
		_tvText.text = "triangular bandage";
		_tvImage.material = triangularBandageImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = triangularBandage;
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

	public void NextHammerTarget (int nextTarget) {
		if (nextTarget == 2) {
			_hammerTarget1.SetActive(false);
			_hammerTarget2.SetActive(true);
		} else if (nextTarget == 3) {
			_hammerTarget2.SetActive(false);
			_hammerTarget3.SetActive(true);
		} else if (nextTarget == 4) {
			_hammerTarget3.SetActive(false);
			_hammerTarget4.SetActive(true);
		} else if (nextTarget == 5) {
			_hammerTarget4.SetActive(false);
		}
	}
}
