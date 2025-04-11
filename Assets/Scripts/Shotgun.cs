using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        b.GetComponent<Projectile>().Initialize(10, 5, 1, 20, null); // version without special effect
        

        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }
}
