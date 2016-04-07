using UnityEngine;
using System.Collections;

public class firstAidBag : MonoBehaviour {

	private sequenceManager _sequenceManager;
	public AudioSource _sfx;
	public GameObject ParticleSuccess;
	public GameObject ParticleSpawn;
	public GameObject ParticleFailure;

	//Audio
	private AudioSource Success;
	private AudioSource Failure;


	void Start () {
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
		Success = GameObject.Find("SuccessSound").GetComponent<AudioSource>();
		Failure = GameObject.Find("FailureSound").GetComponent<AudioSource>();
	}
	

	void Update () {
	
	}


	void OnCollisionEnter (Collision collision) 
	{
		if (collision.gameObject.tag == "FirstAidItem") {
			Destroy (collision.gameObject);
			_sequenceManager.NewItemCollected(collision.gameObject.name);
			Instantiate (ParticleSuccess,ParticleSpawn.transform.position, Quaternion.identity);
			Success.Play();
		}

		// DROPPING WRONG ITEM IN BAG
		if (collision.gameObject.tag == "WrongItem")
		{
			Destroy (collision.gameObject);
			Instantiate (ParticleFailure,ParticleSpawn.transform.position, Quaternion.identity);
			Failure.Play();
		}
	}
}
