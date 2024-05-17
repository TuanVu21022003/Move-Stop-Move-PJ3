using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        enemy.ChangeAnim(KeyConstants.Anim_Dead);
    }

    public void OnExcute(Enemy enemy)
    {
        
    }

    public void OnExit(Enemy enemy)
    {

    }
}
