using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Gun : MonoBehaviour
{      
    public UnityEvent GunRemoved;
    [SerializeField] private int idGun;

    public int IdGun => idGun;

    abstract public void Fire();
    abstract public void Reload();
    abstract public void RemoveGun();
    abstract public void GetGun();


}
