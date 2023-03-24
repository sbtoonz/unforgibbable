using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class PrefabGrabber : MonoBehaviour
{
   public List<string> prefabNames = new List<string>();

   private static List<GameObject> prefab = new List<GameObject>();
   private static List<GameObject> instantied_prefab = new List<GameObject>();
   private void Awake()
   {
      foreach (var obj in from prefabName in prefabNames where ZNetScene.instance != null select ZNetScene.instance.GetPrefab(prefabName) into obj where obj != null select obj)
      {
         prefab.Add(obj);
      }
   }

   private void OnEnable()
   {
      foreach (var instantiate in prefab.Select(o => (GameObject)Object.Instantiate(o, transform, false)))
      {
         instantied_prefab.Add(instantiate);
      }
   }
   
}
