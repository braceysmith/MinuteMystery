using System.Collections;
using System.Collections.Generic;
using DreamscapeAssets.Utils;
using UnityEngine;
[RequireComponent(typeof(DreamerInteractiveItem))]

public class MushroomFloat : MonoBehaviour
{

    DreamerInteractiveItem interactiveItem;
    GameObject target;
    //Vector3 lastPoint;
    bool there = false;
    bool seen = false;

    void Awake()
    {
        interactiveItem = GetComponent<DreamerInteractiveItem>() as DreamerInteractiveItem;
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

    private void HandleOver()
    {
        seen = true;
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
        if(gameObject.GetComponent<MeshCollider>() != null)
        {
            Destroy(gameObject.GetComponent<MeshCollider>());
        }
        GetComponent<SphereCollider>().radius = GetComponent<SphereCollider>().radius * 2;
        target = GameObject.Find("MushroomTarget");
        StartCoroutine("FloatToTarget");
    }

    private void HandleOut()
    {
        StopCoroutine("FloatToTarget");
        GetComponent<SphereCollider>().radius = GetComponent<SphereCollider>().radius /2;
        FallToGround();
    }

    IEnumerator FloatToTarget()
    {
        if (seen == false)
        {
            yield return new WaitForSeconds(1);
            seen = true;
        }

        while (transform.position != target.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime / 4);
            transform.Rotate(.5f, 1, 1);

            yield return null;
        }
        there = true;
        FallToGround();
    }

    void FallToGround()
    {
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.mass = 1;
            rb.drag = 1;
        }
        if (gameObject.GetComponent<MeshCollider>() == null)
        {
            MeshCollider mc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
            mc.convex = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (there == true)
        {
            Destroy(gameObject);
        }
    }
}
