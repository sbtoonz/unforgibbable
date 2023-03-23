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
        internal static GameObject? _gibObject = null!;
        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            harmony = new(ModGUID);
            harmony.PatchAll(assembly);

            _bundle = Utilities.LoadAssetBundle("gibmeplz");
            if (_bundle != null) _gibObject = _bundle.LoadAsset<GameObject>("deer_gibs");
        }
    }
}
