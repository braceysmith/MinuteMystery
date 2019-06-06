using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalGrow : MonoBehaviour
{
    Vector3 endScale = new Vector3(15,15,15);
    bool hit = false;

    public delegate void Magic();
    public static event Magic WandEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (hit == false)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<SphereCollider>());
            StartCoroutine(Grow());
            hit = true;
        }
    }

    private IEnumerator Grow()
    {
        while (transform.lossyScale != endScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, endScale, .2f);

            
            yield return null;
        }
        WandEvent();
        Destroy(gameObject);
    }
}
