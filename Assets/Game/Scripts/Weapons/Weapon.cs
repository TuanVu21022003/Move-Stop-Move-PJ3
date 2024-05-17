using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System;

public class Weapon : MonoBehaviour
{
    protected WeaponType _weaponCurrent;
    protected GameObject _weaponVisual;
    protected WeaponData _dataWeapon;
    protected float timeOrigin = 1.5f;
    protected Material[] materialsWeapon;
    public void SetActiveWeaponVisual()
    {
        _weaponVisual.SetActive(true);
    }

    public void Throw(Character player, Transform _posBulletStart, Vector3 _target, float scale, Action<Character, Character> actionCharacter)
    {
        Bullet bullet = SimplePool.Spawn<Bullet>(_dataWeapon.weaponType, _posBulletStart.position, Quaternion.identity);
        if(!player.isPower)
        {
            bullet.OnInit(_target, actionCharacter, timeOrigin * scale);
            bullet.SetBullet(_dataWeapon.weaponSpeed, player, scale, _dataWeapon.weaponSpeedRotation, materialsWeapon);
            bullet.DestroyBullet();

        }
        else
        {
            bullet.OnInit(_target, actionCharacter, timeOrigin * scale * 2);
            bullet.SetBullet(_dataWeapon.weaponSpeed * 2f, player, scale * 2, _dataWeapon.weaponSpeedRotation, materialsWeapon);
            bullet.DestroyBullet();
            player.SetIsPower(false);
            player.SetSightAttack(false);
        }
    }

    public void ChangeWeapon(WeaponType weaponType, Transform weaponPosition, Material[] materialsWeapon)
    {
        if (_weaponVisual != null)
        {
            Destroy(_weaponVisual);
        }
        _weaponCurrent = weaponType;
        _weaponVisual = Instantiate(VisualManager.Instance.GetWeaponVisual(weaponType), weaponPosition);
        _weaponVisual.GetComponent<MeshRenderer>().materials = materialsWeapon;
        _dataWeapon = VisualManager.Instance.GetWeaponData(weaponType);
        this.materialsWeapon = materialsWeapon;
    }

    public void ActiveVisual(bool check)
    {
        _weaponVisual?.SetActive(check);
    }
}
