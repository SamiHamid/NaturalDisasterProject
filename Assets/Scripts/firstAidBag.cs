using UnityEngine;
using System.Collections;

public class firstAidBag : MonoBehaviour {

	private sequenceManager _sequenceManager;


	void Start () {
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
	}
	

	void Update () {
	
	}


	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "FirstAidItem") {
			Destroy (collision.gameObject);
			_sequenceManager.NewItemCollected(collision.gameObject.name);
		}
	}
}
