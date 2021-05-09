using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
  public Transform hammerPosition;
  public Transform fenceModelRoot;
  private bool isWork = false;
  public static int count = 0;

  private void Update()
  {
    if (fenceModelRoot.localPosition.y <= -0.8f && !isWork)
    {
      gameObject.GetComponent<Collider>().enabled = false;
      count++;
      isWork = true;
    }
  }
}
