using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : Character
{
    [SerializeField] NavMeshAgent _navMeshAgent;
    private IState currentState;
    private bool _isGotoDes = false;
    public bool isGotoDes => _isGotoDes;

    private bool _isRun = false;
    public bool isRun => _isRun;

    [Tooltip("Select if box indicator is required for this target")]
    [SerializeField] private bool needBoxIndicator = true;

    [Tooltip("Select if arrow indicator is required for this target")]
    [SerializeField] private bool needArrowIndicator = true;

    [Tooltip("Select if distance text is required for this target")]
    [SerializeField] private bool needDistanceText = true;

    /// <summary>
    /// Please do not assign its value yourself without understanding its use.
    /// A reference to the target's indicator, 
    /// its value is assigned at runtime by the offscreen indicator script.
    /// </summary>
    [HideInInspector] public Indicator indicator;

    /// <summary>
    /// Gets the color for the target indicator.
    /// </summary>

    // Update is called once per frame
    void Update()
    {
        if(PlayManager.Instance.play == false)
        {
            return;
        }
        if (_isDead)
        {
            ChangeAnim(KeyConstants.Anim_Dead);
            return;
        }
        //if (_isAttack)
        //{
        //    return;
        //}
        ResetList();
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            // Đặt _isGotoDes thành false khi enemy đến gần điểm đích
            SetIsGoToDes(false);
        }
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
    }

    public override void PrepareGame(bool check)
    {
        base.PrepareGame(check);
        //indicator.gameObject.SetActive(false);
    }

    public override void Attack()
    {
        base.Attack();
        _isRun = true;
        Invoke(nameof(SetIsRun), 3f);
    }

    public override void ResetIdle()
    {
        if (RandomState(PlayManager.Instance.zoneCurrent))
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
        //ChangeState(new IdleState());
        //Debug.Log("da vao day");
    }

    public void SetIsRun()
    {
        _isRun = false;
    }

    public void GoToDes()
    {
        Vector3 pos = SetRandomDestination();
        _navMeshAgent.isStopped = false;
        _isStop = false;
        SetIsGoToDes(true);
        if (_navMeshAgent == null)
        {
            Debug.Log("ko");
        }
        else
        {
            SetDestination(pos);
        }
        
    }

    public override void OnInit(int levelIndex, WeaponType weaponType, ColorType colorType, Transform cam, PantType pantType, HairType hairType, string nameText, Material[] materials, SkinType skinType)
    {
        ChangeState(new IdleState());
        base.OnInit(levelIndex, weaponType, colorType, cam, pantType, hairType, nameText, materials, skinType);
    }

    public void StopMoving()
    {
        _isStop = true;
        _navMeshAgent.velocity = Vector3.zero;
        _navMeshAgent.isStopped = true;
        SetIsGoToDes(false);
        ChangeAnim(KeyConstants.Anim_Idle);
    }

    public void Moving()
    {
        GoToDes();
        Vector3 movement = transform.forward;
        MoveChangeAnim();
    }

    public void SetIsGoToDes(bool isGoToDes)
    {
        this._isGotoDes = isGoToDes;
    }

    public void SetDestination(Vector3 destination)
    {
        if (_navMeshAgent != null)
        {
            _navMeshAgent.SetDestination(destination);
        }
        else
        {
            Debug.LogError("NavMeshAgent component not found!");
        }
    }

    public Vector3 SetRandomDestination()
    {

        NavMeshHit hit;

        // Thử lấy một vị trí ngẫu nhiên trên NavMesh
        while (true)
        {
            if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * 100f, out hit, 100f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public bool RandomState(int levelMap)
    {
        int randomPos;
        switch (levelMap)
        {
            case 1:
                randomPos = 1;
                break;
            case 2:
                randomPos = 3; break;
            case 3:
                randomPos= 5; break;
            case 4:
                randomPos = 7; break;
            case 5:
                randomPos = 9;
                break;
            default:
                randomPos = 9;
                break;
        }
        int randomIndex = Random.Range(0, 11);
        if(randomIndex > randomPos)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void DoDead()
    {
        ChangeState(new DeadState());
        PlayManager.Instance._listEnemy.Remove(this);
        PlayManager.Instance.SetCountDead();
        base.DoDead();
    }

    public override void OnHitVictim(Character attacker, Character victim)
    {
        base.OnHitVictim(attacker, victim);
        if(victim is Player && PlayManager.Instance.win == false)
        {
            PlayManager.Instance.SetCountPerPlay();
            UIManager.Instance.OpenUI<UIFail>();
            UIManager.Instance.GetUI<UIFail>().UpdateFail(PlayManager.Instance.countEnemyExist, attacker.nameCharacter);
            UIManager.Instance.CloseUI<UIGamePlay>(0);
            PlayManager.Instance.StopGame();
        }
        
    }

    /// <summary>
    /// Gets if box indicator is required for the target.
    /// </summary>
    public bool NeedBoxIndicator
    {
        get
        {
            return needBoxIndicator;
        }
    }

    /// <summary>
    /// Gets if arrow indicator is required for the target.
    /// </summary>
    public bool NeedArrowIndicator
    {
        get
        {
            return needArrowIndicator;
        }
    }

    /// <summary>
    /// Gets if the distance text is required for the target.
    /// </summary>
    public bool NeedDistanceText
    {
        get
        {
            return needDistanceText;
        }
    }

    /// <summary>
    /// On enable add this target object to the targets list.
    /// </summary>
    private void OnEnable()
    {
        if (OffScreenIndicator.TargetStateChanged != null)
        {
            OffScreenIndicator.TargetStateChanged.Invoke(this, true);
        }
    }

    /// <summary>
    /// On disable remove this target object from the targets list.
    /// </summary>
    private void OnDisable()
    {
        if (OffScreenIndicator.TargetStateChanged != null)
        {
            OffScreenIndicator.TargetStateChanged.Invoke(this, false);
        }
    }

    /// <summary>
    /// Gets the distance between the camera and the target.
    /// </summary>
    /// <param name="cameraPosition">Camera position</param>
    /// <returns></returns>
    public float GetDistanceFromCamera(Vector3 cameraPosition)
    {
        float distanceFromCamera = Vector3.Distance(cameraPosition, transform.position);
        return distanceFromCamera;
    }
}
