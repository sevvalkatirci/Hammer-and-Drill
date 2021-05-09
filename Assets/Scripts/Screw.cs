using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    public Transform drillPosition;
    public Transform screwModelRoot;
    private bool isWorked = false;
    public static int count = 0;

    private void Update()
    {
        if (screwModelRoot.localPosition.y <= -0.6f && !isWorked)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            count++;
            isWorked = true;
            Debug.Log((count));
        }
    }
}
