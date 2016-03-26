using UnityEngine;
using System.Collections;

public class sceneDirector : MonoBehaviour {

	private GameObject smokeInKitchen;  
	public string sceneStep;
	public GameObject[] allUi;

	// all of the UI buttons
	private GameObject uiYellFire;
	private GameObject uiGoInvestigate;
	private GameObject uiActivateAlarm;
	private GameObject uiActivateAlarm2;
	private GameObject ui2;
	private GameObject ui3;
	private GameObject ui4;
	private GameObject uiTakeTheStairs;
	private GameObject uiTakeTheElevator;
	private GameObject uiTouchTheDoor;
	private GameObject uiOpenTheDoor;
	private GameObject uiTouchTheDoor2;
	private GameObject uiOpenTheDoor2;

	private GameObject player;
	private Animator transporterAnim;
	private GameObject smokeBlindPlayer;
	private GameObject smokeAlongCeiling;
	private GameObject fireInKitchen;
	private GameObject fireBehindDoor1;
	private GameObject fireOnAna;
	private GameObject fireUnderPlayer;

	public float transporterTime = 3f;




	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		transporterAnim = player.transform.Find("Transporter").GetComponent<Animator>();


		// Store all ui buttons in an array so that they can be easily disabled after use.  See ClearActiveUI().
		allUi = GameObject.FindGameObjectsWithTag("ui");

		// Find and Disable all of the ui buttons.  (Enable them later when needed.)
		uiYellFire = GameObject.Find ("UI - Yell Fire");
		uiYellFire.SetActive (false);
		uiGoInvestigate = GameObject.Find ("UI - Go Investigate");
		uiGoInvestigate.SetActive (false);
		uiActivateAlarm = GameObject.Find ("UI - Activate Alarm");
		uiActivateAlarm.SetActive (false);
		uiActivateAlarm2 = GameObject.Find ("UI - Activate Alarm 2");
		uiActivateAlarm2.SetActive (false);
		ui2 = GameObject.Find ("UI - 2");
		ui2.SetActive (false);
		ui3 = GameObject.Find ("UI - 3");
		ui3.SetActive (false);
		ui4 = GameObject.Find ("UI - 4");
		ui4.SetActive (false);
		uiTakeTheStairs = GameObject.Find ("UI - Take The Stairs");
		uiTakeTheStairs.SetActive (false);
		uiTakeTheElevator = GameObject.Find ("UI - Take The Elevator");
		uiTakeTheElevator.SetActive (false);
		uiTouchTheDoor = GameObject.Find ("UI - Touch The Door");
		uiTouchTheDoor.SetActive (false);
		uiOpenTheDoor = GameObject.Find ("UI - Open The Door");
		uiOpenTheDoor.SetActive (false);
		uiTouchTheDoor2 = GameObject.Find ("UI - Touch The Door 2");
		uiTouchTheDoor2.SetActive (false);
		uiOpenTheDoor2 = GameObject.Find ("UI - Open The Door 2");
		uiOpenTheDoor2.SetActive (false);


		// Disable various props.  (Enable them later when needed.)
		smokeInKitchen = GameObject.Find("Smoke - In Kitchen");
		Debug.Log("smoke = " + smokeInKitchen);
		//smokeInKitchen.enableEmission = false;
		smokeInKitchen.SetActive (false);
		smokeBlindPlayer = GameObject.Find ("Smoke - Blind Player");
		smokeBlindPlayer.SetActive (false);
		fireInKitchen = GameObject.Find("Fire - In Kitchen");
		fireInKitchen.SetActive (false);
		smokeAlongCeiling = GameObject.Find ("Smoke - Along Ceiling");
		smokeAlongCeiling.SetActive (false);
		fireBehindDoor1 = GameObject.Find ("Fire - Behind Door 1");
		fireBehindDoor1.SetActive (false);
		fireOnAna = GameObject.Find ("Fire - On Ana");
		fireOnAna.SetActive (false);
		fireUnderPlayer = GameObject.Find ("Fire - Under Player");
		fireUnderPlayer.SetActive (false);






		StartCoroutine(SceneStepLookAround()); // initiate the first step of this experience
	} // end of Start()

	void Update () {
		if (sceneStep == "See Smoke") {
			StartCoroutine(SceneStepSeeSmoke());
		}
		if (sceneStep == "Yell Fire") {
			StartCoroutine(SceneStepYellFire());
		}
		if (sceneStep == "Go Investigate") {
			StartCoroutine(SceneStepGoInvestigate());
		}
		if (sceneStep == "Activate Alarm") {
			StartCoroutine(SceneStepActivateAlarm());
		}
		if (sceneStep == "Moved 2 Desks") {
			StartCoroutine(SceneStepMoved2Desks());
		}
		if (sceneStep == "Moved 3 Desks") {
			StartCoroutine(SceneStepMoved3Desks());
		}
		if (sceneStep == "Moved 4 Desks") {
			StartCoroutine(SceneStepMoved4Desks());
		}
		if (sceneStep == "Door Test 1") {
			StartCoroutine(SceneStepDoorTest1());
		}
		if (sceneStep == "Open The Door 1") {
			StartCoroutine(SceneStepOpenTheDoor1());
		}
		if (sceneStep == "Door Test 2") {
			StartCoroutine(SceneStepDoorTest2());
		}
	} // end of Update()

	public void ClearActiveUI () {
		for (int i = 0; i < allUi.Length; i++) {
			allUi[i].SetActive(false);
		}
	}

	IEnumerator SceneStepLookAround () {
		// audio: "Welcome to another day at the office!"
		// "Locate the nearest fire alarm.  Count the number of desks between yourself and the exit."
		yield return new WaitForSeconds(5);
		sceneStep = "See Smoke";
	}

	IEnumerator SceneStepSeeSmoke () {
		sceneStep = "null";
		smokeInKitchen.SetActive (true);
		yield return new WaitForSeconds(5);
		uiYellFire.SetActive(true);
		uiGoInvestigate.SetActive(true);
	}

	IEnumerator SceneStepYellFire () {
		sceneStep = "null";
		GameObject.Find("Alfred").GetComponent<Animator>().Play("alfred walk out");
		yield return new WaitForSeconds(5);
		uiGoInvestigate.SetActive (true);
		uiActivateAlarm.SetActive (true);

	}

	IEnumerator SceneStepGoInvestigate () {
		sceneStep = "null";
		transporterAnim.Play("transporter up");
		yield return new WaitForSeconds(transporterTime);
		player.transform.position = new Vector3(4.5f, 1.7f, 0.0f);
		transporterAnim.Play("transporter down");
		yield return new WaitForSeconds(5);
		uiActivateAlarm2.SetActive (true);
	}

	IEnumerator SceneStepActivateAlarm () {
		sceneStep = "null";
		smokeBlindPlayer.SetActive (true);
		yield return new WaitForSeconds(3);
		smokeInKitchen.SetActive (false);
		ui2.SetActive (true);
		ui3.SetActive (true);
		ui4.SetActive (true);
	}

	IEnumerator SceneStepMoved2Desks () {
		sceneStep = "null";
		transporterAnim.Play("transporter up");
		yield return new WaitForSeconds(transporterTime);
		player.transform.position = new Vector3(1.9f, 1.7f, 5.1f);
		transporterAnim.Play("transporter down");
		yield return new WaitForSeconds(5);
	}

	IEnumerator SceneStepMoved3Desks () {
		sceneStep = "null";
		smokeInKitchen.SetActive (false);
		smokeAlongCeiling.SetActive (true);
		smokeBlindPlayer.SetActive (false);
		transporterAnim.Play("transporter up");
		yield return new WaitForSeconds(transporterTime);
		player.transform.position = new Vector3(4.3f, 1.7f, 6.1f);  // move player next to elevator and stairwell
		transporterAnim.Play("transporter down");
		yield return new WaitForSeconds(5);
		uiTakeTheStairs.SetActive (true);
		uiTakeTheElevator.SetActive (true);

	}

	IEnumerator SceneStepMoved4Desks () {
		sceneStep = "null";
		transporterAnim.Play("transporter up");
		yield return new WaitForSeconds(transporterTime);
		player.transform.position = new Vector3(1.6f, 1.7f, 8.7f); // move player outside the window (too far)
		transporterAnim.Play("transporter down");
		yield return new WaitForSeconds(5);
	}

	IEnumerator SceneStepDoorTest1 () {
		sceneStep = "null";
		transporterAnim.Play("transporter up");
		yield return new WaitForSeconds(transporterTime);
		player.transform.position = new Vector3(4.5f, 1.7f, -9.2f); // move player to bottom of stairwell
		transporterAnim.Play("transporter down");
		yield return new WaitForSeconds(5);
		uiTouchTheDoor.SetActive (true);
		uiOpenTheDoor.SetActive (true);
	}

	IEnumerator SceneStepOpenTheDoor1 () {
		sceneStep = "null";
		GameObject.Find("Door 1").GetComponent<Animator>().Play("door open");
		fireBehindDoor1.SetActive (true);
		fireOnAna.SetActive (true);
		yield return new WaitForSeconds(2);
		fireUnderPlayer.SetActive (true);
	}
		
	IEnumerator SceneStepDoorTest2 () {
		sceneStep = "null";
		uiTouchTheDoor2.SetActive (true);
		uiOpenTheDoor2.SetActive (true);
		yield return new WaitForSeconds(1);
	}

	IEnumerator SceneStepTouchDoor2 () {
		sceneStep = "null";
		yield return new WaitForSeconds(1);
		uiOpenTheDoor2.SetActive (true);
	}

	IEnumerator SceneStepOpenDoor2 () {
		GameObject.Find("Door 2").GetComponent<Animator>().Play("door open");
		yield return new WaitForSeconds(1);

	}
}
