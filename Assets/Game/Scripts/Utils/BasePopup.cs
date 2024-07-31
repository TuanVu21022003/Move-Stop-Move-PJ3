using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePopup : MonoBehaviour
{

    [SerializeField] private Image backGround;
    [SerializeField] private Transform content;

    public void Show()
    {
        if(backGround != null)
        {
            SetOpacityBackGround(0.5f);

        }
        content.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Sequence mySequence = DOTween.Sequence();
        if(content != null )
        {
            mySequence.Append(content.DOScale(new Vector3(1f, 1f, 1f), 2f).SetEase(Ease.OutElastic));
        }
        if(backGround != null )
        {
            mySequence.Join(backGround.DOFade(1, 0.8f));
        }
    }

    public void Hide()
    {
        Sequence mySequence = DOTween.Sequence();
        if (content != null)
        {
            mySequence.Append(content.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).SetEase(Ease.InBack));
        }
        if (backGround != null)
        {
            mySequence.Join(backGround.DOFade(0.5f, 0.5f));
        }
        mySequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void SetOpacityBackGround(float amount)
    {
        Color color = backGround.color;
        color.a = amount;
        backGround.color = color;
    }
}
