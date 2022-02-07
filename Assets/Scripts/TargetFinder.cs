using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetFinder : MonoBehaviour
{

    public CinemachineVirtualCamera VirtualCamera;


    void Start()
    {
        VirtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        //VirtualCamera.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
