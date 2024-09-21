using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera target_camera;

    // Start is called before the first frame update
    void Start()
    {
        if(target_camera == null)
        {
            target_camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + target_camera.transform.rotation * Vector3.forward,
            target_camera.transform.rotation * Vector3.up);
    }
}
