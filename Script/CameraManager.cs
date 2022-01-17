using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate(){
        transform.position = playerTransform.position + offset; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
