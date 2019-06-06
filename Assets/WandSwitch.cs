using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandSwitch : MonoBehaviour
{
    public GameObject TrailMask1;
    public GameObject wandTip;
    bool switchedOn = false;

    private void OnTriggerEnter(Collider wand)
    {
        // if (collision.collider.name == "wand")
        // {
        //TrailMask1.SetActive(true);
        if (switchedOn == false)
        {
            GameObject wandMask;
            wandMask = Instantiate(TrailMask1, wandTip.transform.position, wandTip.transform.rotation);
            wandMask.transform.SetParent(wandTip.transform);
            switchedOn = true;
        }
       // }
    }
}
