
using System;
using UnityEngine;

public class BoundaryBox : Weapon
{
    public static event Action RespawnPlayer;
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        RespawnPlayer?.Invoke();

    }

}