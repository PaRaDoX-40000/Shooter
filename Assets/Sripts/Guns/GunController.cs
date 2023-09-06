using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GunController : MonoBehaviour
{
    [SerializeField] private List<Gun> _guns = new List<Gun>();
    [SerializeField] private PlayerShootingInput _playerShootingInput;


    private int _numberSelectedGun = 0;

    private Gun _activeGun;

    public Gun ActiveGun => _activeGun; 

    private void Start()
    {
        _activeGun = _guns[0];
        _playerShootingInput.MouseScrollWheelInput.AddListener(SwitchGun);
    }

    public void SwitchGun(int step)
    {
        if (_guns.Count < 2)
        {
            return;
        }
        step = step % _guns.Count;
        step += _guns.IndexOf(_activeGun);
        if (step > 0)
        {
            step = step % _guns.Count;
        }
        else
        {
            step = _guns.Count + step;
        }
        
        ÑhooseGun(step);
    }

    public void ÑhooseGun(int step)
    {
        _numberSelectedGun = step % _guns.Count;
        _activeGun.RemoveGun();
        _activeGun.GunRemoved.AddListener(GetSelectedGun);        
    }

    private void GetSelectedGun()
    {
        _guns[_numberSelectedGun].GetGun();
        _activeGun.GunRemoved.RemoveListener(GetSelectedGun);
        _activeGun = _guns[_numberSelectedGun];
        
    }

    public void AddGun (Gun newGun)
    {

        if(_guns.FirstOrDefault(q=> q.IdGun== newGun.IdGun) == null)
        {
            _guns.Add(newGun);
        }
        
    }





}
