using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject Camera;
    void Update()
    {
        this.transform.LookAt(Camera.transform);
    }
}
