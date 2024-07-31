using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PopupUtils
{
    public static void BubbleItem(Transform item)
    {
        float duration = 0.5f;
        item.localScale = new Vector3 (1f, 1f, 1f);
        float originalScale = item.localScale.x;
        DOTween.Sequence(item).Append(item.DOScale(new Vector3(originalScale * 1.2f, originalScale * 1.2f, originalScale * 1.2f), duration))
            .Append(item.DOScale(new Vector3(originalScale, originalScale, originalScale), duration))
            .SetLoops(-1);
    }
}
