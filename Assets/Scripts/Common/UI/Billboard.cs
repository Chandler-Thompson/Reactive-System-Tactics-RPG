using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;
	public Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        cam = Camera.main.transform; //comment out if wrong
        transform.LookAt(transform.position + cam.forward);
    }
}
