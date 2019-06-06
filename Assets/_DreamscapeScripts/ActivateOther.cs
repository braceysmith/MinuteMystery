using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Utility
{
	public class ActivateOther : MonoBehaviour
	{
		// Causes an object that you specify to activate when a trigger is collided with

		public enum Mode
		{
			Trigger = 0,    // Just broadcast the action on to the target
			Replace = 1,    // replace target with source
			Activate = 2,   // Activate the target GameObject
			Enable = 3,     // Enable a component
			Animate = 4,    // Start animation on target
			Deactivate = 5  // Decativate target GameObject
		}

		public Mode action = Mode.Activate;         // The action to accomplish
		public Object target;                       // The game object to affect. If none, the trigger work on this game object
		public GameObject source;
		public int triggerCount = 1;
		public bool repeatTrigger = false;


		private void DoActivateTrigger()
		{
			triggerCount--;

			if (triggerCount == 0 || repeatTrigger)
			{
				Object currentTarget = target ?? gameObject;
				Behaviour targetBehaviour = currentTarget as Behaviour;
				GameObject targetGameObject = currentTarget as GameObject;
				if (targetBehaviour != null)
				{
					targetGameObject = targetBehaviour.gameObject;
				}

				switch (action)
				{
				case Mode.Trigger:
					if (targetGameObject != null)
					{
						targetGameObject.BroadcastMessage("DoActivateTrigger");
					}
					break;
				case Mode.Replace:
					if (source != null)
					{
						if (targetGameObject != null)
						{
							Instantiate(source, targetGameObject.transform.position,
								targetGameObject.transform.rotation);
							DestroyObject(targetGameObject);
						}
					}
					break;
				case Mode.Activate:
					if (targetGameObject != null)
					{
						targetGameObject.SetActive(true);
					}
					break;
				case Mode.Enable:
					if (targetBehaviour != null)
					{
						targetBehaviour.enabled = true;
					}
					break;
				case Mode.Animate:
					if (targetGameObject != null)
					{
						targetGameObject.GetComponent<Animator> ().enabled = true;
					}
					break;
				case Mode.Deactivate:
					if (targetGameObject != null)
					{
						targetGameObject.SetActive(false);
					}
					break;
				}
			}
		}

		private void CancelTrigger () {
			Object currentTarget = target ?? gameObject;
			Behaviour targetBehaviour = currentTarget as Behaviour;
			GameObject targetGameObject = currentTarget as GameObject;
			if (targetBehaviour != null)
			{
				targetGameObject = targetBehaviour.gameObject;
			}

			if (targetGameObject != null)
			{
				targetGameObject.SetActive(false);
			}
		}


		private void OnTriggerEnter(Collider other)
		{
			DoActivateTrigger();
		}

		private void OnTriggerExit (Collider other)
		{
			CancelTrigger ();
		}
	}
}