using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionPhysicsForce : MonoBehaviour
{
    public float explosionForce = 4;
    public float blastRadius = 10;


    private IEnumerator Start()
    {
        // wait one frame because some explosions instantiate debris which should then
        // be pushed by physics force
        yield return null;

        var cols = Physics.OverlapSphere(transform.position, blastRadius);
        var rigidbodies = new List<Rigidbody>();
        foreach (var col in cols)
        {
            if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
            {
                rigidbodies.Add(col.attachedRigidbody);
            }
        }
        foreach (var rb in rigidbodies)
        {
            rb.AddExplosionForce(explosionForce, transform.position, blastRadius, 0, ForceMode.Impulse);
        }
    }
}

