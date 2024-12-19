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
   private float _bloomStartIntensity;

   private LensDistortion _lensDistortion;
   private float _lensStartIntensity;

   private FilmGrain _filmGain;
   private float _filmStartIntensity;

   private ChromaticAberration _chromatic;
   private float _chromaticIntensity;

   private DepthOfField _depthOfField;

   protected override void Awake()
   {
      base.Awake();

      _volume = transform.Find("Global Volume").GetComponent<Volume>();
      _profile = _volume.profile;

      _profile.TryGet(out _bloom);
      _profile.TryGet(out _lensDistortion);
      _profile.TryGet(out _filmGain);
      _profile.TryGet(out _depthOfField);
      _profile.TryGet(out _chromatic);

      _bloomStartIntensity = _bloom.intensity.value;
      _lensStartIntensity = _lensDistortion.intensity.value;
      _filmStartIntensity = _filmGain.intensity.value;
      _chromaticIntensity = _chromatic.intensity.value;
   }

   private void OnDestroy()
   {
      _bloom.intensity.value = _bloomStartIntensity;
      _lensDistortion.intensity.value = _lensStartIntensity;
      _filmGain.intensity.value = _filmStartIntensity;
      _chromatic.intensity.value = _chromaticIntensity;
   }

   public Tween DOBloomIntensity(float intensity, float time)
   {
      return DOTween.To(() => _bloom.intensity.GetValue<float>(), x => _bloom.intensity.value = x, intensity, time);
   }

   public Tween DOFilmGain(float intensity, float time)
   {
      return DOTween.To(() => _filmGain.intensity.GetValue<float>(), x => _filmGain.intensity.value = x, intensity, time);
   }

   public Tween DOLensDistortion(float intensity, float time)
   {
      return DOTween.To(() => _lensDistortion.intensity.GetValue<float>(), x => _lensDistortion.intensity.value = x, intensity, time);
   }

   public Tween DOChromatic(float intensity, float time = 0.2f)
   {
      return DOTween.To(() => _chromatic.intensity.GetValue<float>(), x => _chromatic.intensity.value = x, intensity, time);
   }

   public Tween DODepthOfField(bool gain, float time = 0.2f)
   {
      return DOTween.To(() => _depthOfField.gaussianStart.GetValue<float>(), x => _depthOfField.gaussianStart.value = x, gain ? 0 : 1000, time);
   }

}
