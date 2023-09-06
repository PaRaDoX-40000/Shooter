using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootingInput : MonoBehaviour
{
   
    [SerializeField] private GunController _gunController;
    private bool _fire;
    private bool _reload;
    public UnityEvent FireInputDown;
    public UnityEvent FireInputUp;
    public UnityEvent ReloadInputDown;
    public UnityEvent ReloadInputUp;
    public UnityEvent<int> MouseScrollWheelInput;








    private void Start()
    {
        
    }

    private void Update()
    {
        int fireInput = (int)Input.GetAxis("Fire");
        int reloadInput = (int)Input.GetAxis("Reload");


        if (fireInput>0)
        {
            FireInputDown?.Invoke();
            _fire = true;
        }

        if (fireInput < 0 && _fire==true)
        {
            FireInputUp?.Invoke();
        }

        if (reloadInput>0)
        {
            ReloadInputDown?.Invoke();
            _reload = true;
        }

        if (reloadInput < 0 && _reload == true)
        {
            ReloadInputUp?.Invoke();
        }

        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollWheel > 0.1)
        {
            //_gunController.SwitchGun(1);
            MouseScrollWheelInput?.Invoke(1);
        }
        if (mouseScrollWheel < -0.1)
        {
           // _gunController.SwitchGun(-1);
            MouseScrollWheelInput?.Invoke(-1);
        }
    }
}
