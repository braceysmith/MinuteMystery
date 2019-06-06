using System.Collections;
using DreamscapeAssets.Utils;
using UnityEngine;
[RequireComponent(typeof(DreamerInteractiveItem))]	


public class DreamSwitch : MonoBehaviour {
	
	DreamerInteractiveItem interactiveItem;
	public bool onOnly = false;
	[SerializeField] private GameObject[] resetList;
	[SerializeField] private GameObject[] activate;
	[SerializeField] private GameObject[] deactivate;
	[SerializeField] private Behaviour[] activateScript;
	[SerializeField] private Behaviour[] deactivateScript;


	void Awake(){
		interactiveItem = GetComponent<DreamerInteractiveItem> () as DreamerInteractiveItem;
	}

	private void OnEnable()
	{
		interactiveItem.OnOver += HandleOver;
		interactiveItem.OnOut += HandleOut;
	}

	private void OnDisable()
	{
		interactiveItem.OnOver -= HandleOver;
		interactiveItem.OnOut -= HandleOut;
	}

	private void HandleOver() {
		for(int j = 0; j <resetList.Length; j++)
		{
			resetList [j].SetActive (true);
		}

		for (int i = 0; i < deactivate.Length; i++) {
			deactivate [i].SetActive (false);
		}

		for (int k = 0; k < activate.Length; k++) {
			activate [k].SetActive (true);
		}

		for (int aS = 0; aS < activateScript.Length; aS++) {
			activateScript [aS].enabled = true;
		}

		for (int daS = 0; daS < deactivateScript.Length; daS++) {
			deactivateScript [daS].enabled = false;
		}
	}

	private void HandleOut() {

		if (onOnly == false) {
			for (int j = 0; j < resetList.Length; j++) {
				resetList [j].SetActive (false);
			}
		}
	}

}
