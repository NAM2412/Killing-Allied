using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] AimComponent aimComp;
    [SerializeField] float damage = 5f;
    [SerializeField] ParticleSystem bulletVFX;
    public override void Attack()
    {
        GameObject target = aimComp.GetAimTarget(out Vector3 aimDir);
        DamageGameObject(target, damage);

        bulletVFX.transform.rotation = Quaternion.LookRotation(aimDir);
        bulletVFX.Emit(bulletVFX.emission.GetBurst(0).maxCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
