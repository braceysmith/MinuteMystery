using UnityEngine;
using UnityEngine.Audio;
using System;
using DreamscapeAssets.Utils;

public class TransitionSound1 : MonoBehaviour {

	[SerializeField] private DreamerInteractiveItem m_Item; // The block that should pop out.
	private AudioSource m_Audio;    // Reference to the audio source that will play effects when the user looks at it and when it fills.
	[SerializeField] private AudioClip m_OnOverClip; 
	[SerializeField] private GameObject transitionObject;
	[SerializeField] private GameObject moveTo;
	[SerializeField] private GameObject soundObject;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private Vector3 currentPosition;
	private float ratio = 0;
	private float gap;
	private float startGap;
	//private bool restart = true;
	private bool teleported = false;
	private bool detatched = false;

	private void Start(){
		m_Audio = soundObject.GetComponent<AudioSource> ();
		endPosition = moveTo.transform.position;
		startPosition = transitionObject.transform.position;
		soundObject.transform.position = transitionObject.transform.position;
		startGap = Vector3.Distance (startPosition, endPosition);
		//teleported = true;
		ratio = 0;

		//restart = true;
	}

	private void Update()
	{
		if (detatched == false) {
			
			//ratio = 1 - 1 / (startGap / gap);
			if (ratio >= .8f) {
				TeleportActions ();
			}

		

			/*if (transitionObject.transform.position == startPosition) {
				restart = true;
				//teleported = true;
			} else {
				restart = false;
			}*/

			if (teleported == false) {
				WhileAudioPlays ();
			}
			
		}
	}

	private void TeleportActions(){
		//m_Audio.pitch = ratio;
		//m_Audio.volume = ratio;
		soundObject.transform.position = endPosition;
		teleported = true;
		detatched = true;

		Debug.Log ("hello computer");
	}

	private void OnEnable ()
	{
		m_Item.OnOver += HandleOver;
		m_Item.OnOver += HandleOut;
	}
		
	private void HandleOver ()
	{
		m_Audio.clip = m_OnOverClip;
		teleported = false;
		detatched = false;

		if (transitionObject.transform.position==startPosition) {
			WhileAudioPlays ();
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
		gap = Vector3.Distance (transitionObject.transform.position, endPosition);
		ratio = 1 - 1 / (startGap / gap);
		soundObject.transform.position = transitionObject.transform.position;

		m_Audio.pitch = ratio + .1f;
		m_Audio.volume = ratio + .1f;
		//Debug.Log ("WORKING");
	}
}
