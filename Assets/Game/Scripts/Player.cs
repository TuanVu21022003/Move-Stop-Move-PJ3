using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    private VariableJoystick _joystick;

    public override void OnInit(int levelIndex, WeaponType weaponType, ColorType colorType, Transform cam, PantType pantType, HairType hairType, string nameText, Material[] materials, SkinType skinType)
    {
        base.OnInit(levelIndex, weaponType, colorType, cam, pantType, hairType, nameText, materials, skinType);
        _joystick = PlayManager.Instance._joystick;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayManager.Instance.win)
        {
            return;
        }
        if (PlayManager.Instance.play == false)
        {
            TF.rotation = Quaternion.Euler(0, 180, 0);
            return;
        }
        if (_isDead)
        {
            ChangeAnim(KeyConstants.Anim_Dead);
            return;
        }
        ResetList();    
        if(_joystick != null)
        {
            Vector3 movement = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            PrepareAttack();
            if (_isAttack)
            {
                return;
            }
            MoveController(movement);

        }

    }

    public override void PrepareGame(bool check)
    {
        base.PrepareGame(check);
        _sightAttack.SetActive(check);
    }

    public override void Attack()
    {
        base.Attack();
    }

    public void MoveController(Vector3 movement)
    {
        StopMove(movement);
        MoveChangeAnim();
        TF.position += movement * _speed * Time.deltaTime;
    }

    public void StopMove(Vector3 movement)
    {
        if (movement.x != 0 || movement.z != 0)
        {
            _isStop = false;
            TF.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            _isStop = true;
        }
    }

    public override void OnHitVictim(Character attacker, Character victim)
    {
        PlayManager.Instance.ChangeCamera(PlayManager.Instance.GetLevelUp(victim.levelCurrent));
        base.OnHitVictim(attacker, victim);
    }

    


}
