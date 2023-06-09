﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Unforgibbable
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class UnforgibbableMod : BaseUnityPlugin
    {
        private const string ModName = "UnforgibbableMod";
        internal const string ModVersion = "0.0.1";
        private const string ModGUID = "com.zarboz.UnforgibbableMod";
        private static Harmony harmony = null!;

        internal static AssetBundle? _bundle;
        internal static GameObject? deer_gibs = null!;
        internal static GameObject? boar_gibs = null!;
        internal static GameObject? neck_gibs = null!;
        internal static GameObject? troll_gibs = null!;
        internal static GameObject? devuix = null!;
        internal static GameObject? ingameDevUIX = null!;
        internal static List<GameObject> gorefabs = new List<GameObject>();
        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            harmony = new(ModGUID);
            harmony.PatchAll(assembly);

            _bundle = Utilities.LoadAssetBundle("gibmeplz");
            if (_bundle == null) return;
            deer_gibs = _bundle.LoadAsset<GameObject>("deer_gibs");
            boar_gibs = _bundle.LoadAsset<GameObject>("boar_gibs");
            neck_gibs = _bundle.LoadAsset<GameObject>("neck_gibs");
            troll_gibs = _bundle.LoadAsset<GameObject>("troll_gibs");
            devuix = _bundle.LoadAsset<GameObject>("DevUI");
        }
    }
}
