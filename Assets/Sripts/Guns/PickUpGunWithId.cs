using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGunWithId: MonoBehaviour
{
    [SerializeField] private int _idGun;
    private void OnTriggerEnter(Collider other)
    {
        GunController gunController = other.GetComponentInChildren<GunController>();
        AllListGun allListGun = other.GetComponentInChildren<AllListGun>();

        if(gunController != null && allListGun != null)
        {
            gunController.AddGun(allListGun.GetGunWithID(_idGun));
            Destroy(this.gameObject);
        }

    }
}
