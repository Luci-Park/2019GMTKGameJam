using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    private void Awake()
    {
        if (GameObject.Find(this.gameObject.name) == null)
        {
        DontDestroyOnLoad(this.gameObject);

        }
    }
}
