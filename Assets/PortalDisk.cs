using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDisk : MonoBehaviour
{

    Vector3 fullSize = new Vector3(1, 1, .1f);

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine("Grow");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1);
    }

    IEnumerator Grow()
    {
        while (transform.localScale != fullSize)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, fullSize, .1f);
            yield return null;
        }
    }
}
