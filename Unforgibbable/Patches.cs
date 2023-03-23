using HarmonyLib;

namespace Unforgibbable
{
    public class Patches
    {
        [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))]
        public static class ZnetPatcher
        {
            public static void Prefix(ZNetScene __instance)
            {
                if(__instance.m_prefabs.Count<=0 )return;
                __instance.m_prefabs.Add(UnforgibbableMod._gibObject);
            }
            
            public static void Postfix(ZNetScene __instance)
            {
                if(__instance.m_prefabs.Count<=0 || __instance.GetPrefab("Amber") == null)return;
                __instance.m_prefabs.Find(x => x.name == "Deer").gameObject.GetComponent<Character>().m_deathEffects
                    .m_effectPrefabs[1].m_prefab = UnforgibbableMod._gibObject;
            }
        }
    }
}