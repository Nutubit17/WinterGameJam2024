using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerStaminaUI
{
   [SerializeField] private Image _gauge;

   public void SetGauge(float gauge)
   {
      _gauge.fillAmount = gauge;
   }
}
