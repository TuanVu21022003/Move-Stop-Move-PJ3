using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player playerPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    private Vector3 posStart;
    private Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        posStart = TF.position;

    }

    // Update is called once per frame
    void Update()
    {
        //if (PlayManager.instance.endGame)
        //{
        //    transform.position = Vector3.Lerp(transform.position, offset + PlayManager.instance.characterWin.transform.position, speed * Time.deltaTime);
        //    return;
        //}
        if (playerPrefab != null)
        {
            TF.position = Vector3.Lerp(TF.position, offset + playerPrefab.TF.position, speed * Time.deltaTime);

        }
    }

    public void OnInit(Player playerPrefab)
    {
        this.playerPrefab = playerPrefab;
        offset = posStart - new Vector3(0, 2, 0);
    }

    public void SetCameraLevelUp(int levelUp)
    {
        offset += new Vector3(0, levelUp * 0.5f, -(levelUp * 0.5f));
    }
}
