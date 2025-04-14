using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class Trigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
    }
}
