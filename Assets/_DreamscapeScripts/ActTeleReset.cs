using System.Collections;
using UnityEngine;
using DreamscapeAssets.Utils;

public class ActTeleReset : MonoBehaviour {

	[SerializeField] private DreamerInteractiveItem interactiveItem;
	[SerializeField] private GameObject teleTarget;
	[SerializeField] private GameObject[] activateList;
	[SerializeField] private Vector3 teleTo;
	[SerializeField] private GameObject[] resetList;

	private void OnEnable()
	{
		interactiveItem.OnOver += DreamerOver;
		interactiveItem.OnOut += DreamerOut;
	}

	private void DreamerOver() {

		//activate this list
		for(int i = 0; i <activateList.Length; i++)
		{
			activateList[i].SetActive(true);
		}
		//teleport target
		teleTarget.transform.position = teleTo;

		// reset this list
		for(int j = 0; j <resetList.Length; j++)
		{
			if (resetList [j].activeSelf) {
				resetList [j].SetActive (false);
			} 
			else {
				resetList [j].SetActive (true);
			}
		}
	}

	private void OnDisable()
	{
		interactiveItem.OnOver -= DreamerOver;
		interactiveItem.OnOut -= DreamerOut;
	}

	private void DreamerOut() {
 
	}
}
