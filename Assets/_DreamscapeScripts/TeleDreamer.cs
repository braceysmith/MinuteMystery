using System;
using System.Resources;
using UnityEngine;
using DreamscapeAssets.Utils;
[RequireComponent(typeof(DreamerInteractiveItem))]

//namespace Dreamscape {
	public class TeleDreamer : MonoBehaviour {
	DreamerInteractiveItem it;
		[SerializeField] private Vector3 teleportTo;
		private GameObject dreamer;
	void Awake() {
		it = GetComponent<DreamerInteractiveItem> () as DreamerInteractiveItem;
	}
		void Start () {
			dreamer = GameObject.FindGameObjectWithTag ("Dreamer");

		}

		private void OnEnable()
		{
			it.OnOver += HandleOver;
			it.OnOut += HandleOut;
		}

		private void OnDisable()
		{
			it.OnOver -= HandleOver;
			it.OnOut -= HandleOut;
		}

		private void HandleOver() {
			dreamer.transform.position = teleportTo;
		}

		private void HandleOut() {
	
		}
	}
//}