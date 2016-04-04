using UnityEngine;
using System.Collections;

public class AttachHand : MonoBehaviour {

	public Transform controller;
	private bool hammerIsAttached = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!hammerIsAttached && controller.gameObject.activeInHierarchy) {
			//transform.position = controller.position;
			transform.rotation	= controller.rotation;
			transform.SetParent(controller);
			transform.Rotate(0,0,0);
			hammerIsAttached = true;
		}
	}
}
