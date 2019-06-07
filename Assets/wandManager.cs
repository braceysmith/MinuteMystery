using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wandManager : MonoBehaviour
{
    GameObject projectile1;
    GameObject projectile2;
    GameObject projectile3;
    GameObject projectile4;
    GameObject trail1;
    GameObject trail2;
    GameObject trail3;
    GameObject trail4;
    GameObject newTrail;
    public GameObject wandtip;
    public GameObject[] rip;
    bool fired = false;
    int worldNum = 0;
    //public float speed;
    //Rigidbody wtrb;
    //Vector3 lastPosition;
    //int swish = 0;
    // Start is called before the first frame update
    void Start()
    {
        //wtrb = wandtip.GetComponent<Rigidbody>();
        //RipsFalse();
        projectile1 = Resources.Load("projectile1") as GameObject;
        projectile2 = Resources.Load("projectile2") as GameObject;
        projectile3 = Resources.Load("projectile3") as GameObject;
        projectile4 = Resources.Load("projectile4") as GameObject;
        trail1 = Resources.Load("Mask01Trail") as GameObject;
        trail2 = Resources.Load("Mask02Trail") as GameObject;
        trail3 = Resources.Load("Mask03Trail") as GameObject;
        trail4 = Resources.Load("Mask04Trail") as GameObject;
    }

    private void OnEnable()
    {
        portalGrow.WandEvent += SwitchWorlds;
    }

    private void OnDisable()
    {
        portalGrow.WandEvent -= SwitchWorlds;
    }
    // Start is called before the first frame update
    void SwitchWorlds()
    {
        if (worldNum < 3)
        {
            worldNum++;
        }
        else if (worldNum == 3)
        {
            worldNum = 0;
        }
        fired = false;
    }

    void Projectile(GameObject p)
    {
        GameObject warp = Instantiate(p) as GameObject;
        warp.transform.position = transform.position + wandtip.transform.forward;
        if (newTrail != null)
        {
            newTrail.transform.position = transform.position + wandtip.transform.forward;
            newTrail.transform.SetParent(warp.transform);
        }
        Rigidbody rb = warp.GetComponent<Rigidbody>();
        rb.velocity = wandtip.transform.forward * 4;
    }
    void Trail(GameObject t)
    {
        newTrail = Instantiate(t) as GameObject;
        newTrail.transform.position = wandtip.transform.position;
        newTrail.transform.SetParent(wandtip.transform);
        fired = true;
    }

    // Update is called once per frame
    void Update()
    {
        //speed = (transform.position - lastPosition).magnitude/Time.deltaTime;
        //lastPosition = transform.position;

        if (fired == true) {
            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (worldNum == 0)
                {
                    Projectile(projectile2);
                }
                else if (worldNum == 1)
                {
                    Projectile(projectile3);
                }
                else if (worldNum == 2)
                {
                    Projectile(projectile4);
                }
                else if (worldNum == 3)
                {
                    Projectile(projectile1);
                }
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (fired == false)
            {
                if (worldNum == 0)
                {
                    Trail(trail2);
                }
                else if (worldNum == 1)
                {
                    Trail(trail3);
                }
                else if (worldNum == 2)
                {
                    Trail(trail4);
                }
                else if (worldNum == 3)
                {
                    Trail(trail1);
                }
            }
        }

    }
}
