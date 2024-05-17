using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    [SerializeField] private Image _imageLevel;
    [SerializeField] private TextMeshProUGUI _textLevel;
    [SerializeField] private TextMeshProUGUI _name;
    private Transform _camera;
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
    void LateUpdate()
    {
        if(_camera != null)
        {
            TF.LookAt(TF.position + _camera.forward);

        }
        // Ensure the object is always facing the main camera
        
    }
    public void OnInit(Color color, string levelIndex, Transform cam, string nameText)
    {
        _imageLevel.color = color;
        _camera = cam;
        _name.text = nameText;
        _name.color = color;
        UpdateLevel(levelIndex);
    }

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void UpdateLevel(string levelIndex)
    {
        _textLevel.text = levelIndex;
    }
}
