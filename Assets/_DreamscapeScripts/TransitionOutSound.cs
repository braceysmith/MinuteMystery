using UnityEngine;
using UnityEngine.Audio;
using System;
using DreamscapeAssets.Utils;

public class TransitionOutSound : MonoBehaviour {

	[SerializeField] private GameObject transitionObject;
	[SerializeField] private GameObject endObject;
	[SerializeField] private GameObject soundObject;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float ratio;
	private float gap;
	private float startGap;

	private void Start(){
		startGap = Vector3.Distance (transitionObject.transform.position, endObject.transform.position);
		startPosition = transitionObject.transform.position;
		endPosition = endObject.transform.position;
	}

	private void Update()
	{
		gap = Vector3.Distance (transitionObject.transform.position, startPosition);
			ratio = .9f-1 / (startGap / gap);
			soundObject.transform.position = transitionObject.transform.position;

			soundObject.GetComponent<AudioSource>().pitch = ratio;
			soundObject.GetComponent<AudioSource>().volume = ratio;
			Debug.Log ("hello bug bug");

			if (ratio <= .2f) {
				TeleportActions();
			}
	}
	private void TeleportActions()
		{
		soundObject.GetComponent<AudioSource>().Stop();
		soundObject.transform.position = endPosition;
		//seen = false;
		}
		
}

