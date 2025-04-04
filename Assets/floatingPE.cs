using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingPE : MonoBehaviour
{
    GameObject Jeff;
    // Start is called before the first frame update
    void Start()
    {
        Jeff = GameObject.Find("Jeff");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Jeff.transform.position;
    }
}
