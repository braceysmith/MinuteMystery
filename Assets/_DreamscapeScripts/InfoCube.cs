using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DreamscapeAssets.Utils;

public class InfoCube : MonoBehaviour {

	[SerializeField] private DreamerInteractiveItem interactiveItem;
	[SerializeField] private GameObject endTarget;
	[SerializeField] private float floatSpeed = .012f;

	[SerializeField] private Behaviour imageSwitch;

	private GameObject player;

	private float interval = .1f;
	private float playerDistance = 0;
	private float nextTime = 1;
	private float rotationSpeed = .8f;

	private Vector3 floatStart;
	private Vector3 floatOn;

	private Quaternion startRotation;
	private Quaternion floatRotation;

	private bool seen = false;
	private bool done = false;
	public Behaviour thisScript;

	private void Start (){
		floatStart = transform.position; // starting position
		startRotation = transform.rotation; // starting rotation
		floatRotation = startRotation;	
		transform.rotation = floatRotation; //objects rotation is set to to this 
		player = GameObject.Find("Player");
		imageSwitch.enabled = false;

	}

	private void OnEnable()
	{
		interactiveItem.OnOver += HandleOver;
		//interactiveItem.OnOut += HandleOut;
	}

	private void OnDisable()
	{
		interactiveItem.OnOver -= HandleOver;
		//interactiveItem.OnOut -= HandleOut;
	}

	private void HandleOver() {
		seen = true; //user sees trigger object
		rotationSpeed = .9f;

	}

	private void Update() {
		playerDistance = Vector3.Distance (player.transform.position, transform.position);// how far the user is from the object

		if (seen == true || playerDistance <= 1.5f) { // if the user sees the oject or is close enough



			if (Time.time >= nextTime) {
				floatOn = endTarget.transform.position; // the target position for this object will now be
				floatRotation = endTarget.transform.rotation;// the target rotation will now be
				nextTime += interval;
				if (transform.rotation == endTarget.transform.rotation) {
					done = true;
				}
			}
		} else {

			if(done == false){
			floatOn = floatStart; // set target position to start point
			floatRotation = startRotation; //  set target rotation to start rotation
			}
		}

		transform.position = Vector3.MoveTowards (transform.position, floatOn, floatSpeed); // this object will now move to targeted position
		transform.rotation = Quaternion.RotateTowards (transform.rotation, floatRotation, rotationSpeed); // this object will now turm to set rotation

		if (transform.rotation == endTarget.transform.rotation && transform.position == endTarget.transform.position) {
			thisScript.enabled = false;
			imageSwitch.enabled = true;
		}
	}

	/*private void HandleOut() {
		seen = false; // the user no longer sees this object
		rotationSpeed = .8f;
	}*/

}

