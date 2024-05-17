using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightAttack : MonoBehaviour
{
    [SerializeField] private Character _playerOwn;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(KeyConstants.Tag_Player))
        {
            Character target = Cache.GetCharacter(other);
            _playerOwn.AddTarget(target);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(KeyConstants.Tag_Player))
        {
            Character target = Cache.GetCharacter(other);
            _playerOwn.RemoveTarget(target);
        }

    }
}
