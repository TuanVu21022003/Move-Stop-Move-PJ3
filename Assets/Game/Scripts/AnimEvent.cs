using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    [SerializeField] Character character;

    public void Throw()
    {
        character.ThrowAttack();
        DeactiveVisual();
    }

    public void ResetIdle()
    {
        character.ResetIdle();
    }

    public void ActiveVisual()
    {
        character.ActiveWeaponVisual(true);
        character.ResetAttack();
    }

    public void DeactiveVisual()
    {
        character.ActiveWeaponVisual(false);
    }
}
