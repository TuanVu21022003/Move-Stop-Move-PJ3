using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayManager : Singleton<PlayManager>
{
    [SerializeField] private LevelCharacterOS _levelCharacterData;
    [SerializeField] private LevelUpOS _levelUpData;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] public LevelMapOS levelMapData;
    [SerializeField] public VariableJoystick _joystick;
    [SerializeField] private int countEnemyPerInstantiate;
    [SerializeField] private int limitSpawnEnemy;
    [SerializeField] private int countEnemyToInstantiate;
    [SerializeField] private int maxLevel = 0, minLevel = 50;
    [SerializeField] private float timeSpawnGift = 8f;
    
    private Player _player;
    public Player player => _player;
    public List<Enemy> _listEnemy = new List<Enemy>();
    private int countEnemyDead = 0;
    private int countEnemySpawned = 0;
    private int _countEnemyByLevel;
    public int countEnemyByLevel => _countEnemyByLevel;

    private int _countEnemyExist ;
    public int countEnemyExist => _countEnemyExist;
    private float timerGift = 0f;
    private bool _play = true;
    public bool play => _play;

    private bool _win = false;
    public bool win => _win;

    private int _zoneCurrent;
    public int zoneCurrent => _zoneCurrent;
    public CameraPlayer cameraPlayer;
    private GameObject map;
    private int _countPerPlay = 0;
    public int countPerPlay => _countPerPlay;
    private void Start()
    {
        SetCameraFocus(true);
        _play = false;
        _joystick.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BakeMap();
        }
        if (_play == false)
        {
            return;
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SpawnLevel();
        //    SetPlayGame(false);
        //}
        
        timerGift += Time.deltaTime;
        if (timerGift > timeSpawnGift)
        {
            SpawnGift();
            timerGift = 0;
        }

    }

    public void SetWinPlayer(bool check)
    {
        _win = check;
    }

    public void BakeMap()
    {
        _surface.BuildNavMesh();
    }

    public void SetMap()
    {
        Invoke(nameof(BakeMap), 0.5f);
    }
    public void SetMinMaxLevel()
    {
        minLevel = Math.Min(minLevel, _player.levelCurrent);
        maxLevel = Math.Max(maxLevel, _player.levelCurrent);
        for (int i = 0; i < _listEnemy.Count; i++)
        {
            maxLevel = Math.Max(maxLevel, _listEnemy[i].levelCurrent);
            minLevel = Math.Min(minLevel, _listEnemy[i].levelCurrent);
        }
    }

    public void SpawnEnemy(int countEnemy)
    {
        SetMinMaxLevel();
        for (int i = 0; i < countEnemy; i++)
        {
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.ENEMY, RandomDestinationSpawn(), Quaternion.identity);
            string nameEnemy = "Enemy " + (countEnemySpawned + 1 + i);
            WeaponType weaponType = VisualManager.Instance.RandomWeapon();
            SkinType skinType = VisualManager.Instance.RandomSkin();
            enemy.OnInit(Random.Range(minLevel + Random.Range(0, _zoneCurrent + 1), maxLevel + Random.Range(1, _zoneCurrent + 2)), weaponType , VisualManager.Instance.RandomColor(), _camera.TF, VisualManager.Instance.RandomPant(), VisualManager.Instance.RandomHair(), nameEnemy, VisualManager.Instance.GetListMaterialWeapon(VisualManager.Instance.GetMaterialAvailable((int)weaponType, Random.Range(0, 4))), skinType);
            _listEnemy.Add(enemy);
        }
        countEnemySpawned += countEnemy;
        Debug.Log(countEnemySpawned);
    }

    public void SpawnGift()
    {
        Gift gift = SimplePool.Spawn<Gift>(PoolType.GIFT, RandomDestinationSpawn(), Quaternion.identity);
    }

    public void SpawnPlayer(ColorType colorType, PlayerData dataPlayer)
    {
        _player = SimplePool.Spawn<Player>(PoolType.PLAYER, Vector3.zero + new Vector3(0, 1, 0), Quaternion.identity);
        _player.OnInit(0, dataPlayer.WeaponPlayer, colorType, _camera.TF, dataPlayer.PantPlayer, dataPlayer.HairPlayer, dataPlayer.NamePlayer, dataPlayer.GetMaterialsWeapon(), dataPlayer.skinPlayer);
        _camera.OnInit(_player);
    }

    public void SetPlayGame(bool check)
    {
        _play = check;
        SetCameraFocus(check);
        _player.PrepareGame(check);
        _joystick.gameObject.SetActive(check);
        for(int i = 0; i < _listEnemy.Count; i ++)
        {
            if (_listEnemy[i] != null )
            {
                _listEnemy[i].PrepareGame(check);

            }
        }
    }

    public void SetCameraFocus(bool check)
    {
        _camera.gameObject.SetActive(check);
        cameraPlayer.gameObject.SetActive(!check);
    }

    public void SpawnLevel(string textIndex)
    {
        LevelMapData levelMapInfo = levelMapData.list[int.Parse(textIndex) -1];
        if(map == null)
        {
            GameObject mapPrefab = levelMapInfo.levelMap;
            map = Instantiate(mapPrefab);
            StartCoroutine(DelayTime(0.1f, BakeMap));
        }
        else
        {
            if(int.Parse(textIndex) != _zoneCurrent)
            {
                Destroy(map);
                GameObject mapPrefab = levelMapInfo.levelMap;
                map = Instantiate(mapPrefab);
                StartCoroutine(DelayTime(0.1f, BakeMap));
            }
        }
        _zoneCurrent = levelMapInfo.levelIndex;
        _countEnemyByLevel = levelMapInfo.countCharacter;
        _countEnemyExist = _countEnemyByLevel;
        ResetGame();
        SpawnPlayer(ColorType.GOLD, LoadDataPlayer.Instance.LoadData());
        Invoke(nameof(SpawnEnemyDelay), 0.2f);
        UIManager.Instance.OpenUI<UIShop>();
        MapController1.Instance.ResetlistPos();
        SetPlayGame(false);
    }

    public void SetCountPerPlay()
    {
        _countPerPlay = _countEnemyByLevel - _countEnemyExist;
    }

    public void ResetGame()
    {
        _win = false;
        SimplePool.CollectAll();
        _listEnemy = new List<Enemy>();
        minLevel = 50;
        maxLevel = 0;
        countEnemyToInstantiate = 5;
        countEnemyDead = 0;
        countEnemySpawned = 0;
        _countPerPlay = 0;
    }

    public void StopGame()
    {
        _joystick.ResetJoystick();
        _joystick.gameObject.SetActive(false);
        
    }

    public void SpawnEnemyDelay()
    {
        SpawnEnemy(countEnemyPerInstantiate);
    }

    public void SetCountDead()
    {
        countEnemyDead += 1;
        _countEnemyExist = _countEnemyByLevel - countEnemyDead;
        UIManager.Instance.GetUI<UIGamePlay>().UpdateTextCount(_countEnemyExist);
        countEnemyToInstantiate--;
        if(countEnemyToInstantiate == 0 && countEnemySpawned < _countEnemyByLevel)
        {
            int tmp1 = Random.Range(_zoneCurrent, limitSpawnEnemy + _zoneCurrent);
            int tmp2 = _countEnemyByLevel - countEnemySpawned - 1;
            int tmp = tmp2 < tmp1 ? tmp2 : tmp1;
            SpawnEnemy(tmp);
            countEnemyToInstantiate = tmp;


        }
    }



    public Vector3 RandomDestinationSpawn()
    {
        return MapController1.Instance.RandomPos();
    }


    public int GetLevelUp(int levelIndexEnemy)
    {
        return _levelUpData.list[(int)levelIndexEnemy] ;
    }

    public float GetScaleCharacter(int levelCharacter)
    {
        return _levelCharacterData.list[levelCharacter].scaleBody;
    }


    public void ChangeCamera(int levelUp)
    {
        _camera.SetCameraLevelUp(levelUp);
    }

    private IEnumerator DelayTime(float time, Action actionNext)
    {
        yield return new WaitForSeconds(time);
        // Thực hiện các hành động sau khi chờ đợi
        actionNext?.Invoke();
    }


    public void SetDelayTime(float time, Action actionNext)
    {
        StartCoroutine(DelayTime(time, actionNext));
    }

    
}


public static class Cache
{

    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    }
}


