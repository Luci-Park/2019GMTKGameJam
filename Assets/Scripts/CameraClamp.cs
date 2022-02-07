using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, 23f, -22.7f),
            Mathf.Clamp(targetToFollow.position.y, 15.2f, -15.2f),
            transform.position.z);
    }
}
