using System;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;
using Unity.VisualScripting;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _announceTMP;
    [SerializeField] private string _gameScene;
    private List<KeyControl> _baseKeys;
    private StringBuilder _announceString;
    private string _baseKeyString;
    private bool _canStart = false;


    void Start()
    {
        _baseKeys = new List<KeyControl>{Keyboard.current.wKey, Keyboard.current.aKey, Keyboard.current.sKey, Keyboard.current.dKey, Keyboard.current.spaceKey};
        foreach(var key in _baseKeys)
        {
            _baseKeyString += key.name.ToString();
        }

        _announceString = new StringBuilder($"Press '{_baseKeyString}' to Start", 50);
        _announceTMP.DOFade(0, 1.2f).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        for(int i = 0; i < _baseKeys.Count; i++)
        {
            if(_baseKeys[i] == null) continue;

            if(_baseKeys[i].wasPressedThisFrame)
            {
                print(_baseKeys[i].name.ToString());
                print(_baseKeyString);
                _baseKeyString.Replace(_baseKeys[i].name.ToString(), " ");
                _baseKeys[i] = null;
            }
        }
        
        _announceString = new StringBuilder($"Press '{_baseKeyString}' to Start", 50);
        _announceTMP.text = _announceString.ToString();

        if(_baseKeys.Count <= 0)
        {
            _canStart = true;
        }


        if(_canStart)
        {
            SceneManager.LoadScene(_gameScene);
        }
    }
}
