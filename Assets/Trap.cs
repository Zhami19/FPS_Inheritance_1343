using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Gun
{
    [SerializeField] GameObject particleEffect;

    [SerializeField] GameObject ice;
    public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        b.GetComponent<Projectile>().Initialize(1, 10, 2, 20, HitUpward); // version without special effect


        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }

    void HitUpward(HitData data)
    {
        Vector3 impactLocation = data.location;

        

        var colliders = Physics.OverlapSphere(impactLocation, 5);
        foreach (var c in colliders)
        {
            if (c.GetComponent<Rigidbody>())
            {
                c.GetComponent<Rigidbody>().AddForce(Vector3.up * 20, ForceMode.Impulse);
                StartCoroutine(Freeze(c));
            }
        }
    }

    IEnumerator Freeze(Collider c)
    {
        yield return new WaitForSeconds(1);
        if (c.GetComponent <FixedJoint>() == null)
        {
            c.gameObject.AddComponent<FixedJoint>();
            Instantiate(ice, c.transform.position, Quaternion.Euler(27, 45, 25));
        }
        yield return new WaitForSeconds(3);
        if (c.GetComponent <FixedJoint>() != null)
        {
            Instantiate(particleEffect, c.transform.position, Quaternion.identity);
            Destroy(c.gameObject.GetComponent<FixedJoint>());
        }
    }
}
