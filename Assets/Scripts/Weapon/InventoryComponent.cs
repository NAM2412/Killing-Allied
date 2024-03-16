using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initialWeaponPrefabs;

    [SerializeField] Transform defaultWeaponSlot;
    [SerializeField] Transform[] weaponSlot;

    List<Weapon> weapons;
    int currentWeaponIndex = -1;
    private void Start()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        weapons = new List<Weapon>();
        foreach (Weapon weapon in initialWeaponPrefabs)
        {
            Transform weaponSlot = defaultWeaponSlot;

            foreach (Transform slot in weaponSlot)
            {
                if (slot.gameObject.tag == weapon.GetAttachSlotTag())
                {
                    weaponSlot = slot; 
                }
            }
            Weapon newWeapon = Instantiate(weapon, weaponSlot);
            newWeapon.Init(gameObject);

            weapons.Add(newWeapon);
        }

        NextWeapon();
    }

    public void NextWeapon()
    {
        int nextWeaponIndex = currentWeaponIndex + 1;

        if (nextWeaponIndex >= weapons.Count)
        {
            nextWeaponIndex = 0;
        }

        EquipWeapon(nextWeaponIndex);

    }

    private void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
            return;

        
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count) //Unequip current weapon
        {
            weapons[currentWeaponIndex].Unequip();
        }

        weapons[weaponIndex].Equip();

        currentWeaponIndex = weaponIndex;
    }
}