using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icegun : Gun
{
    [SerializeField] GameObject impactPE;
    [SerializeField] GameObject explosionPE;
    [SerializeField] GameObject floatingPE;
    [SerializeField] GameObject shotPE;

    [SerializeField] GameObject ice;
    public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        b.GetComponent<Projectile>().Initialize(10, 10, 2, 20, HitUpward); // version without special effect

        Instantiate(shotPE, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);

        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }

    void HitUpward(HitData data)
    {
        Vector3 impactLocation = data.location;

        Instantiate(impactPE, impactLocation, Quaternion.identity);
        Instantiate(floatingPE, impactLocation, Quaternion.identity);

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
        yield return new WaitForSeconds(2);
        if (c.GetComponent <FixedJoint>() != null)
        {
            Instantiate(explosionPE, c.transform.position, Quaternion.identity);
            Destroy(c.gameObject.GetComponent<FixedJoint>());
        }
    }
}
