using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wandManager : MonoBehaviour
{
    //gameobjects
    GameObject projectile1;
    GameObject projectile2;
    GameObject projectile3;
    GameObject projectile4;
    GameObject trail1;
    GameObject trail2;
    GameObject trail3;
    GameObject trail4;
    GameObject portal1;
    GameObject portal2;
    GameObject portal3;
    GameObject portal4;
    GameObject newTrail;
    GameObject newDisk;

    public GameObject portalPoint;
    public GameObject wandtip;
    public GameObject[] rip;

    //bools
    bool fired = false;
    bool diskOn = false;

    //inta
    int worldNum = 0;
    int wandCycle = 0;

    void Start()
    {
        projectile1 = Resources.Load("projectile1") as GameObject;
        projectile2 = Resources.Load("projectile2") as GameObject;
        projectile3 = Resources.Load("projectile3") as GameObject;
        projectile4 = Resources.Load("projectile4") as GameObject;
        trail1 = Resources.Load("Mask01Trail") as GameObject;
        trail2 = Resources.Load("Mask02Trail") as GameObject;
        trail3 = Resources.Load("Mask03Trail") as GameObject;
        trail4 = Resources.Load("Mask04Trail") as GameObject;
        portal1 = Resources.Load("PortalDisk1") as GameObject;
        portal2 = Resources.Load("PortalDisk2") as GameObject;
        portal3 = Resources.Load("PortalDisk3") as GameObject;
        portal4 = Resources.Load("PortalDisk4") as GameObject;
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

    void PortalDisk(GameObject d)
    {
        newDisk = Instantiate(d) as GameObject;
        newDisk.transform.position = portalPoint.transform.position;
        newDisk.transform.rotation = portalPoint.transform.rotation;
    }

    void EndDisk()
    {
        if (newDisk != null)
        {
            StartCoroutine("ShinkDisk");
        }
    }

    IEnumerator ShinkDisk()
    {
        while(newDisk.transform.localScale != Vector3.zero)
        {
            newDisk.transform.localScale = Vector3.MoveTowards(newDisk.transform.localScale, Vector3.zero, .2f);
            yield return null;
        }

        diskOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //speed = (transform.position - lastPosition).magnitude/Time.deltaTime;
        //lastPosition = transform.position;

        /*if (fired == true) {
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
                EndDisk();
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

        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
        {
            if (diskOn == false)
            {
                if (worldNum == 0)
                {
                    PortalDisk(portal2);
                }
                else if (worldNum == 1)
                {
                    PortalDisk(portal3);
                }
                else if (worldNum == 2)
                {
                    PortalDisk(portal4);
                }
                else if (worldNum == 3)
                {
                    PortalDisk(portal1);
                }
                diskOn = true;
            }else if(diskOn == true)
            {
                EndDisk();
            }
        }*/
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (fired == false && wandCycle == 0)
            {
                if (worldNum == 0)
                {
                    PortalDisk(portal2);
                }
                else if (worldNum == 1)
                {
                    PortalDisk(portal3);
                }
                else if (worldNum == 2)
                {
                    PortalDisk(portal4);
                }
                else if (worldNum == 3)
                {
                    PortalDisk(portal1);
                }
                //diskOn = true;
                fired = true;
                wandCycle++;
            }else if (fired == true && wandCycle == 1)
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
                wandCycle++;
            }else if (wandCycle == 2)
            {
                Destroy(newDisk);
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
                wandCycle = 0;
            }
        }

    }
}
