using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;



public class VisualizeBones : MonoBehaviour
{
   public List<Transform> _bones = new List<Transform>();
   private Vector3 lastpos;

   private void Awake()
   {
      lastpos = new Vector3(0,0,0);
   }

   private void OnDrawGizmos()
   {
      foreach (var bone in _bones.Where(bone => bone != null))
      {
         if (lastpos.Equals(Vector3.zero))
         {
            lastpos = bone.position;
            continue;
         }
         Gizmos.DrawLine(lastpos, bone.position);
      }
   }
}
