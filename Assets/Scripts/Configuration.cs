using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{

    public static Configuration Singleton;

    public float CameraSpeed;
    
    void Awake()
    {
        Singleton = this;
    }

   

}
