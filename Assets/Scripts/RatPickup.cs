using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPickup : Pickup, IDestructable
{
    private void OnTriggerEnter(Collider collider)
    {
        Destroyed();
        gameObject.SetActive(false);
    }

    public void Destroyed()
    {
        Game.Instance.OnRatFound(); 
    }
}
