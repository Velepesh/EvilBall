using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 51)]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<Gun> _guns;

    public void AddGun(Gun gun)
    {
        _guns.Add(gun);
    }

    public Gun GetGun(int gunNumber)
    {
        return _guns[gunNumber];
    }

    public int GetCountOfWeapon()
    {
        return _guns.Count;
    }
}
