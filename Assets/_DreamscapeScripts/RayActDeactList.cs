
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamscapeAssets.Utils;


public class RayActDeactList : MonoBehaviour {

	[SerializeField] private DreamerInteractiveItem interactiveItem;
	public GameObject[] activateList;
	public GameObject[] deactivateList;
	public bool repeatTrigger = true;
	public int triggerCount = 1;

	private void OnEnable()
	{
		interactiveItem.OnOver += DoActivateTrigger;
	}

	private void OnDisable()
	{
		interactiveItem.OnOver -= DoActivateTrigger;;
	}
		
	private void DoActivateTrigger()
	{
		triggerCount--;

		if (triggerCount == 0 || repeatTrigger)
		{
			for(int i = 0; i <activateList.Length; i++)
				{
				activateList[i].SetActive(true);
				}

			for(int j = 0; j <deactivateList.Length; j++)
				{
				deactivateList[j].SetActive(false);
				}
		}
	}
}