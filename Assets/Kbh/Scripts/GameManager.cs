using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public void Awake()
   {
      SoundManager.Instance.PlayBGM(CONST.IN_GAME_BGM);
   }


}
