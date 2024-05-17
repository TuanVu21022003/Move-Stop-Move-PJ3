using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : Bullet
{
    private bool returning;
    private float flyingTime;
    public override void OnInit(Vector3 destination, Action<Character, Character> onHit, float timer)
    {
        base.OnInit(destination, onHit, timer);
        returning = false;
        flyingTime = 0;
    }

    public override void DestroyBullet()
    {
        
    }

    public override void OnDespawn()
    {
        if(!returning)
        {
            flyingTime += Time.deltaTime;
            if (flyingTime >= timeExist) 
            {
                returning = true;
            }
            base.OnDespawn();

        }
        else // If bullet is returning
        {
            // Move bullet back to its initial position
            float returnSpeed = _speed * 1.5f; // Change this value to adjust the return speed
            TF.position = Vector3.MoveTowards(TF.position, attacker.TF.position, returnSpeed * Time.deltaTime);
            currentSize -= (finalScale - 1f) * Time.deltaTime * 3f / timeExist;
            transform.localScale = initialScale * currentSize;
            currentRotation += _rotationSpeed * Time.deltaTime;
            TF.rotation = Quaternion.Euler(0f, currentRotation, 0f);

            // If bullet is close enough to its initial position, stop returning
            if (Vector3.Distance(TF.position, attacker.TF.position) < 0.5f)
            {
                SimplePool.Despawn(this);
            }
        }
    }
}
