using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unforgibbable;
using UnityEngine;
using Object = UnityEngine.Object;

public class PrefabGrabber : MonoBehaviour
{
   public List<string> prefabNames = new List<string>();

   private void OnEnable()
   {

      foreach (var VARIABLE in UnforgibbableMod.gorefabs)
      {
         var t =Instantiate(VARIABLE, gameObject.transform, false);
         var z = t.GetComponent<ZNetView>();
         if (z)
         {
            z.m_distant = false;
            z.m_persistent = false;
         }
      }

   }
}
