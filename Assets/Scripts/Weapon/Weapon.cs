using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string attachSlotTag;

    public string GetAttachSlotTag()
    {
        return attachSlotTag;
    }    
    public GameObject Owner
    {
        get;
        private set;
    }    

    public void Init(GameObject owner)
    {
        Owner = owner;
        Unequip();
    }    

    public void Equip()
    {
        gameObject.SetActive(true);
    }    

    public void Unequip()
    {
        gameObject.SetActive(false);
    }    
}
