using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Character : GameUnit
{
    [SerializeField] protected float _speed;
    [SerializeField] Animator _anim;
    [SerializeField] private Weapon _weapon = null;
    [SerializeField] private Transform _weaponPosition;
    [SerializeField] private Transform _posBulletStart;
    [SerializeField] protected GameObject _sightAttack;
    [SerializeField] private GameObject _sightTarget;
    [SerializeField] protected LevelBar _levelBar;
    [SerializeField] private SkinnedMeshRenderer _characterVisual;
    [SerializeField] private SkinnedMeshRenderer _pantCharacter;
    [SerializeField] private Transform _hairPosition;
    [SerializeField] private Transform _backPosition;
    [SerializeField] private Transform _leftPosition;

    private GameObject _backCharacter;
    private GameObject _leftCharacter;
    private GameObject _hairCharacter;
    private Character _target = null;
    public Character target => _target;
    private List<Character> _listTarget = new List<Character>();
    private string _currentAnim = KeyConstants.Anim_Idle;
    protected bool _isAttack = false;
    public bool isAttack => _isAttack;
    private bool _isPrepareAttack = false;
    protected bool _isStop = false;
    protected bool _isDead = false;
    public bool isDead => _isDead;
    private int _levelCurrent = 0;
    public int levelCurrent => _levelCurrent;

    private Color _colorCharacter;
    public Color colorCharacter => _colorCharacter;

    private bool _isPower;
    public bool isPower => _isPower;

    private string _nameCharacter;
    public string nameCharacter => _nameCharacter;  
    public virtual void OnInit(int levelIndex, WeaponType weaponType, ColorType colorType, Transform cam, PantType pantType, HairType hairType, string nameText, Material[] materials, SkinType skinType)
    {
        ChangeAnim(KeyConstants.Anim_Idle);
        _colorCharacter = VisualManager.Instance.ToColor(colorType);
        ChangeEquipment(skinType);
        if(skinType == SkinType.NORMAL)
        {
            ChangeEquipment(pantType);
            ChangeEquipment(hairType);

        }
        
        SetLevel0();
        SetSightTarget(false);
        _isDead = false;
        _isPower = false;
        SelectWeapon(weaponType, materials);
        SetStateCharacterLevel(levelIndex);
        _listTarget.Clear();
        ChangeName(nameText);
        DelayedOnInit(cam);
    }

    public virtual void PrepareGame(bool check)
    {
        _levelBar.gameObject.SetActive(check);
    }

    // Coroutine để chờ 0.5 giây trước khi gọi DelayedOnInit
    private IEnumerator DelayedOnInitCoroutine(Transform cam)
    {
        yield return new WaitForSeconds(1f);
        DelayedOnInit(cam);
    }



    // Phương thức được gọi sau 0.5 giây
    private void DelayedOnInit(Transform cam)
    {
        _levelBar.OnInit(_colorCharacter, _levelCurrent.ToString(), cam, _nameCharacter);
    }

    public void ChangeName(string nameText)
    {
        _nameCharacter = nameText;
        _levelBar.SetName(nameText);
    }

    public void ChangeEquipment(HairType hairType)
    {
        if (_hairCharacter != null)
        {
            Destroy(_hairCharacter);
        }
        _hairCharacter = Instantiate(VisualManager.Instance.GetHair(hairType), _hairPosition);
    }

    public void ChangeEquipment(PantType pantType)
    {
        _pantCharacter.material = VisualManager.Instance.GetPant(pantType);
    }

    public void ChangeEquipment(SkinType skinType)
    {
        SkinData skin = VisualManager.Instance.GetSkinData(skinType);
        if(skin.skinVisual != null)
        {
            if(skinType == SkinType.NORMAL)
            {
                if(this is Player)
                {
                    _characterVisual.material = skin.skinVisual;
                }
                else
                {
                    _characterVisual.material = VisualManager.Instance.GetColorCharacter(VisualManager.Instance.RandomColor());
                }
            }
            else
            {
                _characterVisual.material = skin.skinVisual;
            }
        }
        if(skin.skinBack != null)
        {
            if (_backCharacter != null)
            {
                Destroy(_backCharacter);
            }
            _backCharacter = Instantiate(skin.skinBack, _backPosition);
        }
        else
        {
            Destroy(_backCharacter);
        }
        if (skin.skinHair != null)
        {
            if (_hairCharacter != null)
            {
                Destroy(_hairCharacter);
            }
            _hairCharacter = Instantiate(skin.skinHair, _hairPosition);
        }
        else
        {
            Destroy(_hairCharacter);
        }
        if (skin.skinLeft != null)
        {
            if (_leftCharacter != null)
            {
                Destroy(_leftCharacter);
            }
            _leftCharacter = Instantiate(skin.skinLeft, _leftPosition);
        }
        else
        {
            Destroy(_leftCharacter);
        }
    }

    public void ChangeEquipmentType<T>(T equiType)
    {
        if(equiType is HairType)
        {
            ChangeEquipment((HairType)(object)(equiType));
        }
        if (equiType is PantType)
        {
            ChangeEquipment((PantType)(object)(equiType));
        }
        if (equiType is SkinType)
        {
            ChangeEquipment((SkinType)(object)(equiType));
        }
    }

    public void ResetList()
    {
        _listTarget.RemoveAll(c => c.isDead);
    }

    public Character SetTarget()
    {
        for(int i = 0; i<  _listTarget.Count; i++)
        {
            if (!_listTarget[i].isDead) {
                return _listTarget[i];
            }
        }
        return null;
    }

    public virtual void Attack()
    {
        if(_target != null)
        {
            TF.rotation = Quaternion.LookRotation(new Vector3((_target.transform.position - TF.position).normalized.x, 0, (_target.transform.position - TF.position).normalized.z));
            _isAttack = true;
            if(!_isPower)
            {
                ChangeAnim(KeyConstants.Anim_Attack);
                //Invoke(nameof(ThrowAttack), 0.2f);
                //Invoke(nameof(ResetIdle), 0.3f);
            }
            else
            {
                ChangeAnim(KeyConstants.Anim_Ulti);
                //Invoke(nameof(ThrowAttack), 0.3f);
                //Invoke(nameof(ResetIdle), 0.7f);
            }
            //Invoke(nameof(ResetAttack), 0.3f);
            

        }
        
    }

    public void ThrowAttack()
    {
        if(_target != null)
        {
           _weapon.Throw(this, _posBulletStart, _target.transform.position, _anim.transform.localScale.x, OnHitVictim);
        }
    }

    public void ActiveWeaponVisual(bool check)
    {
        _weapon.ActiveVisual(check);
    }

    public void ResetAttack()
    {
        _isAttack = false;
    }

    public virtual void ResetIdle()
    {
        ChangeAnim(KeyConstants.Anim_Idle);
    }

    public void SetSightTarget(bool check)
    {
        _sightTarget.SetActive(check);
    }

    public void PrepareAttack()
    {
        if (_isStop)
        {
            if (!_isPrepareAttack && CheckTarget())
            {
                _target = SetTarget();
                if( _target != null )
                {
                    if(this is Player)
                    {
                        _target.SetSightTarget(true);
                    }
                    Attack();
                    _isPrepareAttack = true;
                    Invoke(nameof(ResetPrepareAttack), 1.5f);

                }
            }
        }
    }
    public void ResetPrepareAttack()
    {
        _isPrepareAttack = false;
    }

    public void AddTarget(Character enemy)
    {
        _listTarget.Add(enemy);
    }

    public void RemoveTarget(Character enemy)
    {
        _listTarget.Remove(enemy);
        enemy.SetSightTarget(false);
    }

    

    public void MoveChangeAnim()
    {
        if (!_isStop)
        {
            ChangeAnim(KeyConstants.Anim_Run);
        }
        else
        {
            ChangeAnim(KeyConstants.Anim_Idle);
        }
    }

    

    public void ChangeAnim(string animName)
    {
        if (_currentAnim != animName)
        {
            _anim.ResetTrigger(_currentAnim);
            _currentAnim = animName;
            _anim.SetTrigger(_currentAnim);
        }
    }


    public void ChangeUpLevel(int levelIndexEnemy)
    {
        int levelUp = PlayManager.Instance.GetLevelUp(levelIndexEnemy);
        _levelCurrent += levelUp;
        float scaleBody = PlayManager.Instance.GetScaleCharacter(_levelCurrent);
        _anim.transform.localScale = new Vector3(scaleBody, scaleBody, scaleBody);
        _sightAttack.transform.localScale += new Vector3(levelUp * 0.5f, 0, levelUp * 0.5f);
        _posBulletStart.localPosition += new Vector3(0, 0, 0.1f * levelUp);
        _levelBar.UpdateLevel(_levelCurrent.ToString());
    }

    public void SetStateCharacterLevel(int characterLevel)
    {
        int levelUp = characterLevel - _levelCurrent;
        _levelCurrent = characterLevel;
        _sightAttack.transform.localScale += new Vector3(levelUp * 0.5f, 0, levelUp * 0.5f);
        _posBulletStart.localPosition += new Vector3(0, 0, 0.1f * levelUp);
        float scaleBody = PlayManager.Instance.GetScaleCharacter(_levelCurrent);
        _anim.transform.localScale = new Vector3(scaleBody, scaleBody, scaleBody);
    }

    public void SetSightAttack(bool check)
    {
        if(check == true)
        {
            _sightAttack.transform.localScale *= 2;
            _posBulletStart.localPosition += new Vector3(0, 0, 0.5f);
        }
        else
        {
            _sightAttack.transform.localScale /= 2;
            _posBulletStart.localPosition -= new Vector3(0, 0, 0.5f);

        }
    }

    public void SetLevel0()
    {
        _sightAttack.transform.localScale = new Vector3(18.5f, 0.11f, 18.5f);
        _posBulletStart.localPosition = new Vector3(0, 0, 1.17f);
        _anim.transform.localScale = new Vector3(1, 1, 1);
        _levelCurrent = 0;
    }

    public void SelectWeapon(WeaponType weaponType, Material[] materialsWeapon)
    {
        _weapon.ChangeWeapon(weaponType, _weaponPosition, materialsWeapon);
    }


    public bool CheckTarget()
    {
        return _listTarget.Count > 0;
    }

    public virtual void DoDead()
    {
        ChangeAnim(KeyConstants.Anim_Dead);
        _listTarget.Clear();
        Invoke(nameof(SetActiveCharacter), 0.8f);
        _isDead = true;
    }

    public void SetActiveCharacter()
    {
        SimplePool.Despawn(this);
    }

    public virtual void SetDestinationTransform(bool check)
    {

    }

    public virtual void OnHitVictim(Character attacker, Character victim)
    {
        attacker.ChangeUpLevel(victim.levelCurrent);
        attacker.RemoveTarget(victim);
        victim.DoDead();
        victim.SetDestinationTransform(false);
        if (PlayManager.Instance.countEnemyExist == 1 && PlayManager.Instance.player.gameObject.activeSelf == true)
        {
            PlayManager.Instance.SetCountPerPlay();
            UIManager.Instance.CloseUI<UIGamePlay>(0);
            UIManager.Instance.OpenUI<UIVictory>();
            PlayManager.Instance.SetWinPlayer(true);
            PlayManager.Instance.player.PrepareGame(false);
            PlayManager.Instance.StopGame();
            PlayManager.Instance.player.ChangeAnim(KeyConstants.Anim_Win);
        }
    }

    public void SetIsDead(bool check)
    {
        _isDead=check;
    }

    public void SetIsPower(bool check)
    {
        _isPower=check;
    }

}


