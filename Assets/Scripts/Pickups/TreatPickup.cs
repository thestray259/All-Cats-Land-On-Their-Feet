using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatPickup : Pickup, IDestructable
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroyed();
        gameObject.SetActive(false); 
    }

    public void Destroyed()
    {
        Game.Instance.OnTreatFound(); 
    }
}
