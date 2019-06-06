using UnityEngine;
using UnityEngine.Audio;
using System;
using DreamscapeAssets.Utils;

public class TransitionSound2 : MonoBehaviour {

	[SerializeField] private DreamerInteractiveItem m_Item; // The block that should pop out.
	[SerializeField] private AudioSource m_Audio;    // Reference to the audio source that will play effects when the user looks at it and when it fills.
	[SerializeField] private AudioClip m_OnOverClip; 
	[SerializeField] private GameObject transitionObject;
	[SerializeField] private GameObject endObject;
	[SerializeField] private GameObject soundObject;
	private Vector3 startPosition, endPosition, currentPosition;
	private float ratio = 0, gap, startGap;
	private bool restart = true;
	private bool teleported = false;
	private bool detatched = false;

	private void Start(){
		endPosition = endObject.transform.position;
		startPosition = transitionObject.transform.position;
		currentPosition = transitionObject.transform.position;
		soundObject.transform.position = currentPosition;
		startGap = Vector3.Distance (startPosition, endPosition);
		//teleported = true;
		restart = true;
	}

	private void Update()
	{
		if (detatched == false) {
			currentPosition = transitionObject.transform.position;
		
			if (currentPosition == endPosition) {
				TeleportActions ();
			}

			if (currentPosition == startPosition) {
				restart = true;
				//teleported = true;
			} else {
				restart = false;
			}

			if (teleported == false && detatched == false) {
				WhileAudioPlays ();
			}
		}
	}

	private void TeleportActions(){
		m_Audio.pitch = 1;
		m_Audio.volume = 1;
		soundObject.transform.position = endPosition;
		teleported = true;
		detatched = true;
	}

	private void OnEnable ()
	{
		m_Item.OnOver += HandleOver;
		m_Item.OnOver += HandleOut;
	}
		
	private void HandleOver ()
	{
		m_Audio.clip = m_OnOverClip;
		//teleported = false;
		detatched = false;

		if (restart == true) {
			m_Audio.Play ();
		}
	}
		
	private void OnDisable ()
	{
		m_Item.OnOver -= HandleOver;
		m_Item.OnOver -= HandleOut;
	}

	private void HandleOut ()
	{

	}

	private void WhileAudioPlays ()
	{
		gap = Vector3.Distance (currentPosition, endPosition);
		ratio = 1 - 1 / (startGap / gap);
		soundObject.transform.position = currentPosition;

		m_Audio.pitch = ratio;
		m_Audio.volume = ratio;
		Debug.Log ("hello computer");
	}
}
