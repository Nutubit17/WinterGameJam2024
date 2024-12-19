using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SettingUI : MonoBehaviour, InputControls.IUIActions
{
    [SerializeField] private InputControls _inputControls;

    [SerializeField] private CanvasGroup settingUI;

    private bool isActive = false;

    [SerializeField] private Slider SFXslider;
    [SerializeField] private Slider BGMslider;

    private void Start()
    {
        _inputControls = new InputControls();
        _inputControls.Enable();
        _inputControls.UI.Enable();
        _inputControls.UI.SetCallbacks(this);

        settingUI.alpha = 0;
        settingUI.interactable = false;
        settingUI.blocksRaycasts = false;

        SFXslider.onValueChanged.AddListener(HandleSfxValueChange);
        BGMslider.onValueChanged.AddListener(HandleBGMValueChange);
    }

    private void OnDestroy()
    {
        _inputControls.UI.Disable();
        settingUI.alpha = 0;
        settingUI.interactable = false;
        settingUI.blocksRaycasts = false;
        VolumeManager.Instance.ResetDepthOfField();
        Time.timeScale = 1;
    }

    private void HandleBGMValueChange(float value)
    {
        SoundManager.Instance.SetAudioValue("BGM", value);
    }

    private void HandleSfxValueChange(float value)
    {
        SoundManager.Instance.SetAudioValue("SFX", value);
        Debug.Log(value);
    }

    private void HandleOpenUI()
    {
        float duration = 0.3f;
        if (isActive)
        {
            settingUI.DOFade(0, duration).SetEase(Ease.InCirc).OnComplete(() => Time.timeScale = 1).SetUpdate(true);
            VolumeManager.Instance.DODepthOfField(false, duration);
            settingUI.interactable = false;
            settingUI.blocksRaycasts = false;
            isActive = false;
        }
        else
        {
            settingUI.DOFade(1, duration).SetEase(Ease.InCirc).OnComplete(() => Time.timeScale = 0).SetUpdate(true);
            VolumeManager.Instance.DODepthOfField(true, duration);
            settingUI.interactable = true;
            settingUI.blocksRaycasts = true;
            isActive = true;
        }
    }



    public void OnOpenSetting(InputAction.CallbackContext context)
    {
        HandleOpenUI();
    }
}
