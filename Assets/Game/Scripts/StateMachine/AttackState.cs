using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
    }

    public void OnExcute(Enemy enemy)
    {
        enemy.PrepareAttack();
        //if(!enemy.CheckTarget() && !enemy.isAttack)
        //{
        //    enemy.ChangeState(new PatrolState());
        //}
    }

    public void OnExit(Enemy enemy)
    {

    }
}
