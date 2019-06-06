using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public GameObject headMask;
    public GameObject[] world;
    public Material[] masks;
    public int worldNum = 0;


    //public delegate void Magic();
    //public static event Magic WandEvent;

    private void OnEnable()
    {
        portalGrow.WandEvent += SwitchWorlds;
    }

    private void OnDisable()
    {
        portalGrow.WandEvent -= SwitchWorlds;
    }

    private void Start()
    {
        world[0].SetActive(true);
        world[1].SetActive(true);
        world[2].SetActive(false);
        world[3].SetActive(false);
    }
    void SwitchWorlds()
    {

        if(worldNum == 0)
        {
            headMask.GetComponent<Renderer>().material = masks[1];
            world[0].SetActive(false);
            world[2].SetActive(true);
            worldNum++;
        }else if(worldNum == 1)
        {
           headMask.GetComponent<Renderer>().material = masks[2];
            world[1].SetActive(false);
            world[3].SetActive(true);
            worldNum++;
        }
        else if (worldNum == 2)
        {
            headMask.GetComponent<Renderer>().material = masks[3];
            world[2].SetActive(false);
            world[0].SetActive(true);
            worldNum++;
        }
        else if (worldNum == 3)
        {
            headMask.GetComponent<Renderer>().material = masks[0];
            world[3].SetActive(false);
            world[1].SetActive(true);
            worldNum =0;
        }
    }
}
