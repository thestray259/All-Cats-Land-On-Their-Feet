using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatPickup : Pickup, IDestructable
{
    private void OnCollisionEnter(Collision collision)
    {
        sfx.Play();
        Destroyed();

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
    }

    public void Destroyed()
    {
        Game.Instance.OnTreatFound();
    }
}
