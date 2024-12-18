using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShaderKeyEnum
{
   Float,
   BoolAndInt,
}

public class ShaderPropKey
{
   public ShaderPropKey(Material material, string name, ShaderKeyEnum keyType)
   {
      _material = material;

      PropName = name;
      _hashValue = Shader.PropertyToID(name);
   }

   public Tween DOValue(float value, float duration)
      => DOTween.To(() => GetValue(), x => SetValue(x), value, duration);

   public void SetValue(float value)
   {
      if(!IsInvalid())
      {
         Debug.LogWarning("The hash allocated in shaderpropkey instance is invalid.");
         return;
      }

      switch (_keyType)
      {
         case ShaderKeyEnum.Float:
            _material.SetFloat(_hashValue, value);
            break;

         case ShaderKeyEnum.BoolAndInt:
            _material.SetInt(_hashValue, (int)value);
            break;
      }

   }

   public float GetValue()
   {
      if(!IsInvalid())
      {
         Debug.LogWarning("The hash allocated in shaderpropkey instance is invalid.");
         return 0;
      }

      switch (_keyType)
      {
         case ShaderKeyEnum.Float:
            return _material.GetFloat(_hashValue);

         case ShaderKeyEnum.BoolAndInt:
            return _material.GetInt(_hashValue);
      }

      return 0;
   }

   public bool IsInvalid()
   {
      switch (_keyType)
      {
         case ShaderKeyEnum.Float:
            return _material.HasFloat(_hashValue);

         case ShaderKeyEnum.BoolAndInt:
            return _material.HasInt(_hashValue);
      }

      return false;
   }

   private ShaderKeyEnum _keyType;
   private Material _material;
   public string PropName { get; private set; }
   private int _hashValue;


}

[System.Serializable]
public class EntityVisual
{
   [SerializeField] private SpriteRenderer _renderer;
   
   public ShaderPropKey GenerateKey(ShaderKeyEnum keyType, string name)
      => new ShaderPropKey(_renderer.material, name, keyType);


   public void SetColor(Color color)
   {
      _renderer.color = color;
   }

   public Sprite GetCurrentsprite() => _renderer.sprite;
   public void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
}
