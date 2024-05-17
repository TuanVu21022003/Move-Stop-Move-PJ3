using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float duration;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0f;
        duration = Random.Range(1f, 2.5f);
    }

    public void OnExcute(Enemy enemy)
    {
        if(timer < duration)
        {
            timer += Time.deltaTime;
            if (enemy.CheckTarget() && !enemy.isAttack)
            {
                if (enemy.RandomState(PlayManager.Instance.zoneCurrent))
                {
                    enemy.ChangeState(new AttackState());
                }
                else
                {
                    enemy.ChangeState(new PatrolState());
                }
            }
        }
        else
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
