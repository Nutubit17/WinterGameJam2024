using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManagerHelper
{
   public static void PlayBGM(this SoundManager soundManager, string clipName)
   {
      soundManager.Play(soundManager.AudioClips[clipName], SoundType.BGM);
   }

   public static void PlayEffect(this SoundManager soundManager, string clipName)
   {
      soundManager.Play(soundManager.AudioClips[clipName], SoundType.SFX);
   }

}
