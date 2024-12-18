using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;


public enum SoundType
{
    SFX,
    BGM,
    Max
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject soundManager = new GameObject("SoundManager");
                    instance = soundManager.AddComponent<SoundManager>();
                    DontDestroyOnLoad(soundManager);
                }
            }
            return instance;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            Init();
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private AudioSource[] _audioSources = new AudioSource[(int)SoundType.Max];
    public Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    [SerializeField] private List<string> str = new List<string>();
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();

    private void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            DontDestroyOnLoad(root);

            string[] soundNames = Enum.GetNames(typeof(SoundType));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)SoundType.BGM].loop = true; // 배경음은 무한 반복 재생
        }


    }

    private void Start()
    {
       
        for (int i = 0; i < audioClips.Count; i++)
        {
            _audioClips.Add(str[i], audioClips[i]);

        }
       //Play(_audioClips["TitleBGM"], SoundType.BGM, 1f); 
    }

    public void Play(AudioClip clip, SoundType type = SoundType.SFX, float volume = 1.0f)
    {
        if (clip == null)
            return;
        if (type == SoundType.SFX)
        {
            AudioSource audioSource = _audioSources[(int)SoundType.SFX];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip); // 한 번만 재생
        }
        else // Sound.Bgm
        {
            AudioSource audioSource = _audioSources[(int)SoundType.BGM];
            audioSource.volume = volume;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void SetAudioValue(string v, float value)
    {
        if (v == "SFX")
            _audioSources[(int)SoundType.SFX].volume = value;
        else
            _audioSources[(int)SoundType.BGM].volume = value;

    }
}


