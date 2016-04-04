using UnityEngine;
using System.Collections;

public class bullseye : MonoBehaviour {

	private Transform ring2;
	public float delay;
	private sequenceManager sequenceManager;
	public int nextTarget;
	//public AudioClip hitAudio;

	// Use this for initialization
	void Start () {
		ring2 = transform.Find("rings/red ring (1)");
		ring2.gameObject.SetActive(false);
		StartCoroutine(Activate2ndRing());
		//Debug.Log("ring2 = " + ring2);
		sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Activate2ndRing () {
		yield return new WaitForSeconds(delay);
		ring2.gameObject.SetActive(true);
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.transform.name == "Hammer") {
			collision.transform.GetComponent<AudioSource>().Play();
			sequenceManager.NextHammerTarget(nextTarget);
		}
	}
}
