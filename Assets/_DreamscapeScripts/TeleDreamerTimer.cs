//using System;
using UnityEngine;
using System.Collections;
using DreamscapeAssets.Utils;

//namespace VRStandardAssets.Utils
 //{
[RequireComponent(typeof(DreamerInteractiveItem))]

	public class TeleDreamerTimer : MonoBehaviour {
		DreamerInteractiveItem interactiveItem;
		GameObject dreamer;

		public GameObject[] activateList;
		[SerializeField] private Vector3 teleportTo;
		public GameObject[] deactivateList;

		[SerializeField] private float m_Duration = 2f;                     // The length of time it takes for the bar to fill.
		//[SerializeField] private AudioSource m_Audio;                       // Reference to the audio source that will play effects when the user looks at it and when it fills.
		//[SerializeField] private AudioClip m_OnOverClip;                    // The clip to play when the user looks at the bar.
		//[SerializeField] private AudioClip m_OnFilledClip;                  // The clip to play when the bar finishes filling.
	void Awake(){
		interactiveItem = GetComponent<DreamerInteractiveItem> () as DreamerInteractiveItem;
		dreamer = GameObject.FindGameObjectWithTag ("Dreamer");
	}
		private bool m_GazeOver;                                            // Whether the user is currently looking at the bar.
		private float m_Timer;                                              // Used to determine how much of the bar should be filled.
		private Coroutine m_FillBarRoutine;                                 // Reference to the coroutine that controls the bar filling up, used to stop it if required.
		//private bool teled = false;

		private void OnEnable ()
		{
	        interactiveItem.OnOver += HandleOver;
			interactiveItem.OnOut += HandleOut;
		}


		private void OnDisable ()
		{
			interactiveItem.OnOver -= HandleOver;
			interactiveItem.OnOut -= HandleOut;
		}

		private IEnumerator FillBar ()
		{
			// When the bar starts to fill, reset the timer.
			m_Timer = 0f;

			// Until the timer is greater than the fill time...
			while (m_Timer < m_Duration)
			{
				// ... add to the timer the difference between frames.
				m_Timer += Time.deltaTime;

				// Wait until next frame.
			    yield return null;

				//If the user is still looking at the bar, go on to the next iteration of the loop.
				if (m_GazeOver)
					continue;

				// If the user is no longer looking at the bar, reset the timer and bar and leave the function.
				m_Timer = 0f;

				yield break;
			}

			//Activate list
			for(int i = 0; i <activateList.Length; i++)
			{
				activateList[i].SetActive(true);
			}

			// Teleport Dreamer
			dreamer.transform.position = teleportTo;
			//teled = true;

			// Deactivate list
			for(int j = 0; j <deactivateList.Length; j++)
			{
				deactivateList[j].SetActive(false);
			}
			// Play the clip for when the bar is filled.
			//m_Audio.clip = m_OnFilledClip;
			//m_Audio.Play ();
		}

		private void HandleOver ()
		{
			// The user is now looking at the bar.
			m_GazeOver = true;
			//tcStartTime = Time.time;
			m_FillBarRoutine = StartCoroutine(FillBar());

			// Play the clip appropriate for when the user starts looking at the bar.
			//m_Audio.clip = m_OnOverClip;
			//m_Audio.Play();
		}


		private void HandleOut ()
		{
			// The user is no longer looking at the bar.
			m_GazeOver = false;
			/*if (teled = false) {
				m_Audio.Stop ();
			}*/
			StopCoroutine (m_FillBarRoutine);
			// Reset the timer and bar values.
			m_Timer = 0f;
			//tcStartTime = -1;
		}
	}
//}
