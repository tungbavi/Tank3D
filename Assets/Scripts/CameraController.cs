using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,target.position + new Vector3(-15, 20, 0),Time.deltaTime*5);
    }
}
