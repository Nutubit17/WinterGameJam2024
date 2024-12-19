using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
   [SerializeField] private Image _backgroundImage;
   [SerializeField] private TextMeshProUGUI _titleText;

   [SerializeField] private TextMeshProUGUI _announceTMP;
   [SerializeField] private ParticleSystem _particleSystem;

   [SerializeField] private Image _upImage, _downImage;

   private List<KeyControl> _baseKeys;
   private StringBuilder _announceString;
   private string _baseKeyString;
   private bool _canStart = false;

   private readonly int _blockCntHash = Shader.PropertyToID("_BlankCnt");
   private readonly int _erasePowerHash = Shader.PropertyToID("_ErasePower");


   void Start()
   {
      SetUpGameStartScene();
      _backgroundImage.material = Instantiate(_backgroundImage.material);

      SoundManager.Instance.PlayBGM(CONST.TITLE_BGM);
      _baseKeys = new List<KeyControl> { Keyboard.current.wKey, Keyboard.current.aKey, Keyboard.current.sKey, Keyboard.current.dKey, Keyboard.current.spaceKey };
      foreach (var key in _baseKeys)
      {
         _baseKeyString += key.name.ToString();
      }

      _announceString = new StringBuilder($"Press '{_baseKeyString}' to Start", 50);
      _announceTMP.DOFade(0, 1.2f).SetLoops(-1, LoopType.Yoyo);
      _titleText.DOFade(1, 0.2f);
   }

   private void SetUpGameStartScene()
   {
      float duration = 1f;
      _upImage.rectTransform.DOAnchorMin(new(0, 1), duration).SetEase(Ease.InOutFlash);
      _downImage.rectTransform.DOAnchorMax(new(1, 0), duration).SetEase(Ease.InOutFlash);
   }


   void Update()
   {
      if (_canStart) return;

      for (int i = 0; i < _baseKeys.Count; i++)
      {
         if (_baseKeys[i] == null) continue;

         if (_baseKeys[i].wasPressedThisFrame)
            _baseKeys[i] = null;
      }
      _baseKeyString = "";
      foreach (var key in _baseKeys)
      {
         if (key == null) continue;
         _baseKeyString += key.name.ToString();
      }

      _announceString = new StringBuilder($"Press '{_baseKeyString}' to Start", 50);
      _announceTMP.text = _announceString.ToString();

      if (_baseKeyString == "")
      {
         _canStart = true;
      }


      if (_canStart)
      {
         DOTween.To(() => _particleSystem.main.startColor.color, x => {
               var main =_particleSystem.main;
               main.startColor = x;
            }, Color.white, 2f);

         var emission = _particleSystem.emission;
         emission.rateOverTime = 200;

         var main = _particleSystem.main;
         main.startSpeed = 20;
         
         VolumeManager.Instance.DOFilmGain(1, 2f);
         VolumeManager.Instance.DOLensDistortion(1f, 4f);
         _titleText.transform.DOScaleY(5, 6f);


         DOVirtual.DelayedCall(1.5f, () =>
         {
            DOTween.To(() => _backgroundImage.material.GetFloat(_blockCntHash),
               x => _backgroundImage.material.SetFloat(_blockCntHash, x), 0, 3.5f);

            DOTween.To(() => _backgroundImage.material.GetFloat(_erasePowerHash),
               x => _backgroundImage.material.SetFloat(_erasePowerHash, x), -4f, 3f);
            VolumeManager.Instance.DOBloomIntensity(10, 4f).SetEase(Ease.InQuint)
               .OnComplete(() =>
               {
                  Debug.Log("abc");
                  VolumeManager.Instance.DOFilmGain(0, 2f);
                  VolumeManager.Instance.DOBloomIntensity(1f, 4f).SetEase(Ease.OutQuint);
                  VolumeManager.Instance.DOLensDistortion(0f, 4f).SetEase(Ease.OutQuint);
                  SceneManager.LoadScene(CONST.GAME_SCENE_NAME);
               });
         });
      }
   }

   
}
