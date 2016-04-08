using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sequenceManager : MonoBehaviour {

	private Text _tvText;
	private Text _timerText;
	private string _timeString;
	private Renderer _tvImage;
	private int _itemsTotal = 8;				// these 2 will not be necessary
	private int _itemsCollected;			    // if timer is used to trigger next part of sequence (instead of completion of packing all items)
	private bool _checkItem;
	private string _itemName;
    private EarthquakeController _earthquakeController;

	// Audio for the TV
	private AudioSource _tvAudioSource;
	public AudioClip warning;
	public AudioClip intro;
	public AudioClip rollBandage;
	public AudioClip alcoholWipes;
	public AudioClip bandages;
	public AudioClip firstAidBook;
	public AudioClip gasMask;
	public AudioClip safetyPin;
	public AudioClip scissors;
	public AudioClip triangularBandage;

	public AudioClip hammerIntro;
	public AudioClip target1done;
	public AudioClip target2done;
	public AudioClip getUnderTable;


	// New Order: roll, alc, cpr v, manual, band, tri, pins, scissors


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
		_tvImage = GameObject.Find("Dynamic GUI/Image").GetComponent<Renderer>();
		_earthquakeController = GameObject.Find("Earthquake Controller").GetComponent<EarthquakeController>();

		// Find and deactivate all hammer targets.  Each will be activated later during the sequence.
		_hammerTarget1 = GameObject.Find("Hammer Target 1");
		_hammerTarget2 = GameObject.Find("Hammer Target 2");
		_hammerTarget3 = GameObject.Find("Hammer Target 3");
		_hammerTarget4 = GameObject.Find("Hammer Target 4");
		_hammerTarget1.SetActive(false);			
		_hammerTarget2.SetActive(false);
		_hammerTarget3.SetActive(false);
		_hammerTarget4.SetActive(false);

		// Begin the game sequence
		StartCoroutine(Intro());
	} // end of Start()
	

	void Update () {
		// update time on TV screen
		_timeString = string.Format("{0:0}:{1:00}", Mathf.Floor(Time.time/60), Time.time % 60);
		_timerText.text = _timeString;

		if (Input.GetKeyDown(KeyCode.Space))
		{
            StopAllCoroutines();		//not sure if we need this or not
			_earthquakeController.StartQuake();
		}

		if (Input.GetKeyDown(KeyCode.H)) {
			StartCoroutine(HammerIntro());

		}
	} // end of Update()


	void LateUpdate () {
		if (_checkItem) {
			if (_tvText.text == _itemName) {
				if (GameObject.Find("alcohol wipes")) {
					StartCoroutine(PackAlcoholWipes());
				} else if (GameObject.Find("first aid book")) {
					StartCoroutine(PackFirstAidBook());
				} else if (GameObject.Find("gas mask")) {
					StartCoroutine(PackGasMask());
				} else if (GameObject.Find("bandages")) {
					StartCoroutine(PackBandages());
				} else if (GameObject.Find("triangular bandage")) {
					StartCoroutine(PackTriangularBandage());
				} else if (GameObject.Find("safety pin")) {
					StartCoroutine(PackSafetyPin());
				} else if (GameObject.Find("scissors")) {
					StartCoroutine(PackScissors());
				}
			}
			_checkItem = false;
		}
	} // end of LateUpdate()


	public void NewItemCollected (string _itemNameImported) {
		_itemName = _itemNameImported;
		_itemsCollected ++;
		if (_itemsCollected >= _itemsTotal) {
			//StartCoroutine(DropCoverHold());

			// start hammer sequence
			StartCoroutine(HammerIntro());
			return;
		}
		_checkItem = true;
	}



	IEnumerator Intro () {
		//_tvText.text = "WARNING!";
		/*
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = warning;
		_tvAudioSource.Play();
		yield return new WaitForSeconds(warning.length);
		*/
		yield return new WaitForSeconds(2); //just a pause at the beginning
		_tvAudioSource.clip = intro;
		_tvAudioSource.Play();
		yield return new WaitForSeconds(intro.length);
		StartCoroutine(PackRollBandage());
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
		_tvText.text = "ventilator";
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
		_tvText.text = "safety pins";
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

	IEnumerator DropCoverHold () {
		_tvText.text = "";
		_tvImage.material = dropCoverHoldImg;
		_tvAudioSource.clip = getUnderTable;  // use the longer clip with "get under... hold on... hold on..."
		_tvAudioSource.Play();
		yield return new WaitForSeconds(5);
		_earthquakeController.StartQuake();
		yield return null;
	}
		
	public void NextHammerTarget (int nextTarget) {
		if (nextTarget == 2) {
			_hammerTarget1.SetActive(false);
			_hammerTarget2.SetActive(true);
			_tvAudioSource.clip = target1done;
			_tvAudioSource.Play();
		} else if (nextTarget == 3) {
			_hammerTarget2.SetActive(false);
			_hammerTarget3.SetActive(true);
			_tvAudioSource.clip = target2done;
			_tvAudioSource.Play();
		} else if (nextTarget == 4) {
			_hammerTarget3.SetActive(false);
			_hammerTarget4.SetActive(true);
		} else if (nextTarget == 5) {
			_hammerTarget4.SetActive(false);
		}
	}

	IEnumerator HammerIntro () {
		_tvAudioSource.clip = hammerIntro;
		_tvAudioSource.Play();
		_hammerTarget1.SetActive(true);
		yield return null;
	}


}
