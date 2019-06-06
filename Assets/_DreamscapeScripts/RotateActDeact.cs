using System.Collections;
using UnityEngine;
using DreamscapeAssets.Utils;
[RequireComponent(typeof(DreamerInteractiveItem))]

public class RotateActDeact : MonoBehaviour {

	DreamerInteractiveItem interactiveItem;
	[SerializeField] private GameObject rotateTo;
	[SerializeField] private float speed = 1f;
	[SerializeField] private Behaviour[] activateScript;
	[SerializeField] private Behaviour[] deactivateScript;
	[SerializeField] private GameObject[] activateObject;
	[SerializeField] private GameObject[] deactivateObject;

	public bool deactivateThisScript = false;
	private bool isMoving;
	private bool isReturning;
	private Quaternion startRotation;
	private Quaternion endRotation;
	private float ratio;
	private float gap;
	private float startGap;

	void Awake(){
		interactiveItem = GetComponent<DreamerInteractiveItem> () as DreamerInteractiveItem;
	}

	void Start () {
		startRotation = transform.rotation;
		isMoving = false;
		isReturning = false;
		endRotation = rotateTo.transform.rotation;
		//startGap = Quaternion.Angle (endRotation, startRotation);
	}

	private void Update(){
		//gap = Quaternion.Angle (transform.rotation, startRotation);
		//ratio = 1 - (gap / startGap);
		if (isMoving == true && isReturning == false) {
			
			transform.rotation = Quaternion.RotateTowards (transform.rotation, endRotation, speed);
			if (transform.rotation == endRotation) { 
					RotationComplete ();
			}
		}

		//return target to original location within remaing time
		if (isReturning == true && isMoving == false) { 
			transform.rotation = Quaternion.RotateTowards (transform.rotation, startRotation, speed*2);

			//has it reach the beginning rotation 
			if (transform.rotation == startRotation) { 
				ReturnComplete ();
			}
		}
	}

	private void OnEnable()
	{
		interactiveItem.OnOver += MoveToPlayer;
		interactiveItem.OnOut += MoveToOrigin;
	}

	private void MoveToPlayer() {
		isMoving = true;
		isReturning = false;
	}

	private void MoveToOrigin() {
		isMoving = false;
		isReturning = true;
	}

	private void OnDisable()
	{
		interactiveItem.OnOver -= MoveToPlayer;
		interactiveItem.OnOut -= MoveToOrigin;
	}
		
	private void RotationComplete(){
		isMoving = false;
		isReturning = false;
	
		for(int k = 0; k <activateObject.Length; k++)
		{
			activateObject[k].SetActive(true);
		}
		//activate this list
		for(int i = 0; i <activateScript.Length; i++)
		{
			activateScript[i].enabled = true;
		}
		//deactivate this list
		for(int ds = 0; ds <deactivateScript.Length; ds++)
		{
			deactivateScript[ds].enabled = false;
		}
		// deactivate this list
		for(int j = 0; j <deactivateObject.Length; j++)
		{
			deactivateObject[j].SetActive(false);
		}

		isReturning = true;

		if (deactivateThisScript == true) {
			enabled = false;
		}

	

	}

	private void ReturnComplete(){
		isMoving = false;
		isReturning = false;
	}
}