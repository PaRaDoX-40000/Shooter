using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllListGun : MonoBehaviour
{
    [SerializeField] private List<Gun> _allGun = new List<Gun>();

    public Gun GetGunWithID (int id)
    {
        return _allGun.FirstOrDefault(q=>q.IdGun==id);
    }

}
