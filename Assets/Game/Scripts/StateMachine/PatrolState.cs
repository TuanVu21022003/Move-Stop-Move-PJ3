using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float duration;
    public void OnEnter(Enemy enemy)
    {
        enemy.SetIsGoToDes(false);
        timer = 0f;
        duration = Random.Range(3.0f, 6.0f);
    }

    public void OnExcute(Enemy enemy)
    {
        if(enemy.isGotoDes == false)
        {
            enemy.Moving();
        }

        else if (enemy.CheckTarget() && enemy.isRun ==false)
        {
            enemy.ChangeState(new AttackState());
        }


        if (timer > duration)
        {
            enemy.ChangeState(new IdleState());
        }
        timer += Time.deltaTime;
    }

    public void OnExit(Enemy enemy)
    {

    }
}
