using System.Collections;
using UnityEngine;
using DreamscapeAssets.Utils;
[RequireComponent(typeof(DreamerInteractiveItem))]

public class TimedMoveActTele : MonoBehaviour {

	[SerializeField] private DreamerInteractiveItem interactiveItem;
	[SerializeField] private GameObject target;
	[SerializeField] private GameObject moveTo;
	[SerializeField] private GameObject teleTarget; 
	[SerializeField] private float speed = .02f;
	[SerializeField] private GameObject[] activateList;
	[SerializeField] private Vector3 tele;
	[SerializeField] private GameObject[] deactivateList;
	[SerializeField] private GameObject[] resetList;

	private bool isMoving;
	private bool isReturning;
	Vector3 startPosition;
	Vector3 endPosition;

	void Start () {
		startPosition = target.transform.position;
		isMoving = false;
		isReturning = false;
	}

	private void Update(){
		//move to player location at set speed
		if (isMoving == true && isReturning == false) {
			//move towards dreamer at set speed
				endPosition = moveTo.transform.position;
				target.transform.position = Vector3.MoveTowards (target.transform.position, endPosition, speed);
				if (target.transform.position == endPosition) { 
					MoveComplete ();
			}
		}

		//return target to original location within remaing time
		if (isReturning == true && isMoving == false) { 
			target.transform.position = Vector3.MoveTowards (target.transform.position, startPosition, speed);

			//is the time up yet? If not, keep returning
			if (target.transform.position == interactiveItem.transform.position) { 
				ReturnComplete ();
			}
		}
	}

	private void OnEnable()
	{
		interactiveItem.OnOver += MoveToPlayer;
		interactiveItem.OnOut += MoveToOrigin;
	}

	//private void HandleOver() {
		//MoveToPlayer();
	//}

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

	//private void HandleOut() {
	//	MoveToOrigin ();
	//}

	private void MoveComplete(){
		isMoving = false;
		isReturning = false;
		// Teleport Dreamer
		teleTarget.transform.position = tele;
		//return target to start position
		target.transform.position = startPosition;
		//activate this list
		for(int i = 0; i <activateList.Length; i++)
		{
			activateList[i].SetActive(true);
		}

		// deactivate this list
		for(int j = 0; j <deactivateList.Length; j++)
		{
			deactivateList[j].SetActive(false);
		}

		// reset this list
		for(int k = 0; k <resetList.Length; k++)
		{
			resetList[k].SetActive(false);
		}
	}

	private void ReturnComplete(){
		isMoving = false;
		isReturning = false;
	}
}