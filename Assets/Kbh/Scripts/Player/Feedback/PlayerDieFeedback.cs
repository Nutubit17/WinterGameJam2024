using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlayerDieFeedback : EntityFeedback<Player>
{
    [SerializeField] private Image _upImage, _downImage;


    public override void Init(Player instance)
    {
        base.Init(instance);
        _instance.EnemyDetector.OnHpChangeEvent += HandleHpChangeEvent;
    }

    private void HandleHpChangeEvent(int currentHp)
    {
        if (currentHp <= 0)
            Execute();
    }

    public override void Execute()
    {
        ShutDownGameOverScene();
    }

    private void ShutDownGameOverScene()
    {
        float duration = 1f;
        _upImage.rectTransform.DOAnchorMin(new(0, 0.5f), duration).SetEase(Ease.InOutFlash).SetUpdate(true);
        _downImage.rectTransform.DOAnchorMax(new(1, 0.5f), duration).SetEase(Ease.InOutFlash)
           .OnComplete(() => DOVirtual.DelayedCall(1f, () => SceneManager.LoadScene(CONST.GAME_OVER_SCENE_NAME)).SetUpdate(true)).SetUpdate(true);
    }
}
