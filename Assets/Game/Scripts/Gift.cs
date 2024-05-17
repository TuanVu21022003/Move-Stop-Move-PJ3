using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gift : GameUnit
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(KeyConstants.Tag_Player))
        {
            Character player = Cache.GetCharacter(other);
            if (player.isPower == false)
            {
                player.SetIsPower(true);
                player.SetSightAttack(true);
                SimplePool.Despawn(this);
            }

        }
    }
}
