using UnityEngine;
using System.Collections;

public class firstAidBag : MonoBehaviour {

	private sequenceManager _sequenceManager;

	// Use this for initialization
	void Start () {
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
		Debug.Log("sequenceManager = " + _sequenceManager);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "FirstAidItem") {
			Destroy (collision.gameObject);
			_sequenceManager.NewItemCollected(collision.gameObject.name);
		}
	}
}
