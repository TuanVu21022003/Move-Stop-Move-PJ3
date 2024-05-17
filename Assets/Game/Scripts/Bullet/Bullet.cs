using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private MeshRenderer bulletVisual;
    private Vector3 _destination;
    protected float _speed;
    protected float _rotationSpeed;
    protected float currentRotation = 0f;
    protected Character attacker;
    private Vector3 direction;
    private Action<Character, Character> _onHit;
    protected float timeExist;
    public Vector3 initialScale;
    protected float finalScale;
    protected float currentSize = 1;
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        OnDespawn();
    }

    public virtual void OnInit(Vector3 destination, Action<Character, Character> onHit, float timer)
    {
        this._destination = destination;
        direction = this._destination - TF.position;
        this._onHit = onHit;
        timeExist = timer;
        currentSize = 1;
    }

    public virtual void OnDespawn()
    {
        
        TF.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
        currentSize += (finalScale - 1f) * Time.deltaTime * 3f / timeExist;
        transform.localScale = initialScale * currentSize;
        if (_rotationSpeed == 0)
        {
            direction.Normalize();

            // Xác định hướng forward mới của viên đạn
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Cập nhật góc quay của viên đạn
            TF.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }
        else
        {
            currentRotation += _rotationSpeed * Time.deltaTime;
            TF.rotation = Quaternion.Euler(0f, currentRotation, 0f);

        }
    }

    public virtual void DestroyBullet()
    {
        
        StartCoroutine(DisableAfterDelay(timeExist));

        
    }

    public IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(gameObject.activeSelf)
        {
            SimplePool.Despawn(this);

        }
    }

    public void SetBullet(float speed, Character playerOwn, float scale, float rotation, Material[] materialsBullet)
    {
        this._speed = speed;
        this._rotationSpeed = rotation;
        this.attacker = playerOwn;
        TF.localScale = new Vector3(scale, scale, scale);
        initialScale = TF.localScale;
        ChangeMaterial(materialsBullet);
        if(playerOwn.isPower)
        {
            finalScale = 2;
        }
        else
        {
            finalScale = 1;
        }
    }

    public void ChangeMaterial(Material[] materials)
    {
        bulletVisual.materials = materials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(KeyConstants.Tag_Player))
        {
            SimplePool.Despawn(this);
            Character victim = Cache.GetCharacter(other);
            if(!victim.isDead && victim != attacker && PlayManager.Instance.win == false)
            {
                _onHit?.Invoke(attacker, victim);

            }
        }
        if(other.CompareTag(KeyConstants.Tag_Platform))
        {
            StartCoroutine(DisableAfterDelay(2f));
            _speed = 0;
        }
    }
}



