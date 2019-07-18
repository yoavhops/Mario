using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Vector3 _target;
    // Start is called before the first frame update
    void Start()
    {
        _target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        _target += Vector3.up * Configuration.Singleton.CameraSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, _target, 0.1f);
         
    }
}
