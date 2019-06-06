using UnityEngine;
using UnityEngine.Audio;
using System;
using DreamscapeAssets.Utils;

public class RotationSound : MonoBehaviour {

	[SerializeField] private GameObject rotateTo;
	[SerializeField] private GameObject soundObject;
	private Quaternion startRotation;
	private Quaternion finalRotation;
	private float ratio;
	private float gap;
	private float startGap;

	private void Start(){
		startRotation = transform.rotation;
		finalRotation = rotateTo.transform.rotation;
		startGap = Quaternion.Angle (finalRotation, startRotation);
	}
		
	private void Update()
	{
		gap = Quaternion.Angle (transform.rotation, startRotation);
		ratio = 1 - (gap / startGap);
		soundObject.GetComponent<AudioSource>().pitch = ratio;
		//soundObject.GetComponent<AudioSource>().volume = ratio;
	}
}
