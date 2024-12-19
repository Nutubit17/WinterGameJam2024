using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoSingleton<VolumeManager>
{
   private Volume _volume;
   private VolumeProfile _profile;
   
   private Bloom _bloom;
   private LensDistortion _lensDistortion;
   private FilmGrain _filmGain;
   private DepthOfField _depthOfField;

   protected override void Awake()
   {
      base.Awake();
      _volume = transform.Find("Global Volume").GetComponent<Volume>();
      _profile = _volume.sharedProfile;

      _profile.TryGet(out _bloom);
      _profile.TryGet(out _lensDistortion);
      _profile.TryGet(out _filmGain);
      _profile.TryGet(out _depthOfField);
   }

   public Tween DOBloomIntensity(float intensity, float time)
   {
      return DOTween.To(() => _bloom.intensity.GetValue<float>(), x => _bloom.intensity.value = x, intensity, time);
   }

   public Tween DOFilmGain(float intensity, float time)
   {
      return DOTween.To(() => _filmGain.intensity.GetValue<float>(), x => _filmGain.intensity.value = x, intensity, time);
   }

   public Tween LensDistortion(float intensity, float time)
   {
      return DOTween.To(() => _lensDistortion.intensity.GetValue<float>(), x => _lensDistortion.intensity.value = x, intensity, time);
   }

   public Tween DODepthOfField(bool gain, float time = 0.2f)
   {
      return DOTween.To(() => _depthOfField.gaussianStart.GetValue<float>(), x => _depthOfField.gaussianStart.value = x, gain ? 0 : 1000, time);
   }

}
