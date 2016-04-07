using UnityEngine;
using System.Collections;
using NewtonVR;

public class interactableSfx : MonoBehaviour {

	private AudioSource _sfx;
	private bool _isAttached;


	// Use this for initialization
	void Start () {
	
		_sfx = this.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {


		if (!_isAttached) {
		
			if (this.GetComponent<NVRAttachPoint> ().IsAttached) {
			
				_sfx.Play ();
				_isAttached = true;

			}

		}

		if (!this.GetComponent<NVRAttachPoint> ().IsAttached) {


			_isAttached = false;

		}



	}	



}
