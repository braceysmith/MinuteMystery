using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unactiveTele : MonoBehaviour {
	[SerializeField] private Vector3 teleportTo;
	// Use this for initialization
	private GameObject dreamer;

	void Update () {
		dreamer = GameObject.FindGameObjectWithTag ("Dreamer");
		dreamer.transform.position = teleportTo;
		enabled = false;
	}

}
