using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParticleToTextMeshConnector : MonoBehaviour
{
   [SerializeField] private ParticleSystem _particleSys;
   [SerializeField] private TMP_Text _tmptext;

   public Mesh TextMesh => _tmptext.mesh;

   private void FixedUpdate()
   {
      var shape = _particleSys.shape;
      shape.enabled = true;
      shape.mesh = TextMesh;
   }

}
