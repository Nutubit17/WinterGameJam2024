using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TextAnimation : MonoBehaviour
{
   private enum TextAnimationType
   {
      Vertice,
      Char,
      Word
   }

   private enum VertexPosition
   {
      LeftDown = 0,
      LeftUp = 1,
      RightUp = 2,
      RightDown = 3
   }

   [Header("Default setting")]
   [SerializeField] private TMP_Text _text;

   private Mesh _mesh;
   private Vector3[] _vertices;
   private Color[] _colors;
   private bool _isCanvasText;

   [Header("Vertice Setting")]
   [SerializeField] private TextAnimationType _verticeAnimationType;
   [SerializeField] private float _verticeMoveSpeed = 1f;
   [SerializeField] private float _verticeMoveAmount = 1f;
   [SerializeField] private float _verticeMoveCycle = 1f;
   [Space]
   [SerializeField] private float _verticeScalingSpeed = 1f;
   [SerializeField] private float _verticeScalingAmount = 1f;
   [SerializeField] private float _verticeScalingCycle = 1f;

   [Header("Color Setting")]
   [SerializeField] private TextAnimationType _colorAnimationType;
   [SerializeField] private AnimationCurve _emissionPower;
   [SerializeField] private Gradient _gradient;
   [SerializeField] private float _colorMoveSpeed = 1f;
   [SerializeField] private float _colorMoveCycle = 1f;

   private void Awake()
      => _isCanvasText = _text is TextMeshProUGUI;


   private void FixedUpdate()
   {
      _text.ForceMeshUpdate();

      _mesh = _text.mesh;
      _vertices = _mesh.vertices;
      _colors = _mesh.colors;
      
      {
         int countedBlank = 0;
         int previousCharIdx = 0;
         int currentWordIdx = 0;
         int previousWordIdx = 0;

         for (int i = 0; i < _vertices.Length; ++i)
         {
            int targetWeight = 0;
            int currentCharIdx = i / 4;
            VertexPosition vtxPosition = (VertexPosition)(i % 4);

            if (currentCharIdx > previousCharIdx)
            {
               previousCharIdx = currentCharIdx;
               while ( _text.text.Length > currentCharIdx + countedBlank
                  && (_text.text[currentCharIdx + countedBlank] == ' '
                  || _text.text[currentCharIdx + countedBlank] == '\n'))
               {
                  currentWordIdx = Mathf.Min(currentWordIdx + 1, previousWordIdx + 1);
                  ++countedBlank;
               }

               previousWordIdx = currentWordIdx;
            }

            switch (_verticeAnimationType)
            {
               case TextAnimationType.Vertice:
                  targetWeight = i;
                  break;
               case TextAnimationType.Char:
                  targetWeight = currentCharIdx;
                  break;
               case TextAnimationType.Word:
                  targetWeight = currentWordIdx;
                  break;
            }

            // Position
            _vertices[i] += Vector3.up
               * (Mathf.Sin((Time.time / _verticeMoveCycle + targetWeight) * _verticeMoveSpeed) * _verticeMoveAmount);

            // Color
            float colorPercent = (Mathf.Sin((Time.time / _colorMoveCycle + targetWeight) * _colorMoveSpeed) + 1) / 2;
            _colors[i] = _gradient.Evaluate(colorPercent) + Color.white * _emissionPower.Evaluate(colorPercent);



            // Scaling Y
            float moveAmount = ((Mathf.Sin((Time.time / _verticeScalingCycle + currentCharIdx) + 1) / 2 * _verticeScalingSpeed) * _verticeScalingAmount);

            switch (vtxPosition)
            {
               case VertexPosition.LeftUp:
               case VertexPosition.RightUp:
                  _vertices[i] += Vector3.up * moveAmount;

                  break;
               case VertexPosition.LeftDown:
               case VertexPosition.RightDown:
                  _vertices[i] += Vector3.down * moveAmount;
                  break;
            }

         }
      }


      _mesh.vertices = _vertices;
      _mesh.colors = _colors;

      _text.mesh.SetVertices(_vertices);
      _text.mesh.SetColors(_colors);

      if (_isCanvasText)
         _text.canvasRenderer.SetMesh(_mesh);
   }

}
