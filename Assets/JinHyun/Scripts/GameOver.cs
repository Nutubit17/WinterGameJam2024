using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tier;
    [SerializeField] private TextMeshProUGUI _time;
   [SerializeField] private Image _upImage, _downImage;

   private float _enteredTime;
   private bool _isActive = true;

    void Start()
    {
         _enteredTime = Time.time;
        _tier.text = $"{string.Format("{0:F1}", (float)ScoreManager.Instance.CurrentScore / 1000)}\nGB";
        _time.text = ((int)ScoreManager.Instance.CurrentTime).ToString() + "\n SEC";

      _isActive = true;

      SetUpGameOverScene();
    }


   private void Update()
   {
      if (!_isActive) return;

      if(Input.anyKeyDown && _enteredTime + 1.5f < Time.time) // 1초이상 지나간 상황에서 아무 키가 눌리면
      {
         ShutDownGameOverScene();
         _isActive = true;
      }
   }

   private void SetUpGameOverScene()
   {
      float duration = 1f;
      _upImage.rectTransform.DOAnchorMin(new(0, 1), duration).SetEase(Ease.InOutFlash);
      _downImage.rectTransform.DOAnchorMax(new(1, 0), duration).SetEase(Ease.InOutFlash);
   }

   private void ShutDownGameOverScene()
   {
      float duration = 1f;
      _upImage.rectTransform.DOAnchorMin(new(0, 0.5f), duration).SetEase(Ease.InOutFlash);
      _downImage.rectTransform.DOAnchorMax(new(1, 0.5f), duration).SetEase(Ease.InOutFlash)
         .OnComplete(() => DOVirtual.DelayedCall(1f, () => SceneManager.LoadScene(CONST.GAME_START_SCENE_NAME)));
   }

   public string GetCapacityTier(long score)
    {
        if(score / (1000*1000*1000) > 0)
        {
            return "PB";
        }
        else if(score / (1000*1000) > 0)
        {
            return "TB";
        }
        else if(score / 1000 > 0)
        {
            return "GB";
        }
        return "MB";
    }
}
