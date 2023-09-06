using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _cameraConnector;    

    private void Start()
    {
        
    }

   

    void Update()
    {
        transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0);
        _cameraConnector.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

    }
}
