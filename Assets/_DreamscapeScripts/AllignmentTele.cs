//using System.Collections;
using UnityEngine;
using DreamscapeAssets.Utils;
[RequireComponent(typeof(DreamerInteractiveItem))]

public class AllignmentTele : MonoBehaviour {

	DreamerInteractiveItem interactiveItem;
	[SerializeField] private GameObject rotateTo;
	[SerializeField] private float speed = 1f;
	[SerializeField] private GameObject top;
	[SerializeField] private GameObject bottom;
	[SerializeField] private Vector3 teleportTo;
	private GameObject dreamer;

	public bool totalAllignment = false;
	private bool topAlligned = false;
	private bool bottomMoving = false;
	private bool isMoving = false;
	private bool isReturning = false;
	private Quaternion topStartRotation;
	private Quaternion bottomStartRotation;
	private Quaternion endRotation;
	private float ratio;
	private float gap;
	private float startGap;

	void Awake(){
		interactiveItem = GetComponent<DreamerInteractiveItem> () as DreamerInteractiveItem;
	}

	void Start () {
		dreamer = GameObject.FindGameObjectWithTag ("Dreamer");
		topStartRotation = top.transform.rotation;
		bottomStartRotation = bottom.transform.rotation;
		endRotation = rotateTo.transform.rotation;
		//startGap = Quaternion.Angle (endRotation, startRotation);
	}

	private void Update(){
		//gap = Quaternion.Angle (transform.rotation, topStartRotation);
		//ratio = 1 - (gap / startGap);

		//top rotation
		if (bottomMoving == false) {
			if (isMoving == true && isReturning == false) {
			
				top.transform.rotation = Quaternion.RotateTowards (top.transform.rotation, endRotation, speed);
				topAlligned = false;
				if (top.transform.rotation == endRotation) { 
					topAlligned = true;
				}
			}

			//return target to original location within remaing time
			if (isReturning == true && isMoving == false) { 
				top.transform.rotation = Quaternion.RotateTowards (top.transform.rotation, topStartRotation, speed * 2);

				//has it reach the beginning rotation 
				if (top.transform.rotation == topStartRotation) { 

					ReturnComplete();
				}
			}
		}

		//bottom rotation
		if (topAlligned == true) {
			if (isMoving == true && isReturning == false) {

				bottom.transform.rotation = Quaternion.RotateTowards (bottom.transform.rotation, endRotation, speed);
				bottomMoving = true;
				if (bottom.transform.rotation == endRotation) { 
					RotationComplete ();
				}
			}

			//return target to original location within remaing time
			if (isReturning == true && isMoving == false) { 
				bottom.transform.rotation = Quaternion.RotateTowards (bottom.transform.rotation, bottomStartRotation, speed * 2);

				//has it reach the beginning rotation 
				if (bottom.transform.rotation == bottomStartRotation) { 
					bottomMoving = false;
				}
			}
		}
	}

	private void OnEnable()
	{
		interactiveItem.OnOver += MoveToAllign;
		interactiveItem.OnOut += MoveToOrigin;
	}

	private void MoveToAllign() {
		isMoving = true;
		isReturning = false;
	}

	private void MoveToOrigin() {
		isMoving = false;
		isReturning = true;
	}

	private void OnDisable()
	{
		interactiveItem.OnOver -= MoveToAllign;
		interactiveItem.OnOut -= MoveToOrigin;
	}
		
	private void RotationComplete(){
		isMoving = false;
		isReturning = false;
		totalAllignment = true;
		dreamer.transform.position = teleportTo;

		GameObject d = GameObject.FindGameObjectWithTag ("APU");
		AudioSource audio = d.GetComponent<AudioSource> ();
		audio.clip = Resources.Load ("FlipCard") as AudioClip;
		audio.volume = .25f;
		audio.spatialBlend = 1f;
		audio.Play ();


	}

	private void ReturnComplete(){
		isMoving = false;
		isReturning = false;
		//top.transform.rotation = topStartRotation;
		//bottom.transform.rotation = bottomStartRotation;
	}
}