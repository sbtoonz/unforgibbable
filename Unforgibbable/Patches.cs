using HarmonyLib;
using UnityEngine;

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
                __instance.m_prefabs.Add(UnforgibbableMod.devuix);
            }
            
            public static void Postfix(ZNetScene __instance)
            {
                if(__instance.m_prefabs.Count<=0 || __instance.GetPrefab("Amber") == null)return;
                __instance.m_prefabs.Find(x => x.name == "Deer").gameObject.GetComponent<Character>().m_deathEffects
                    .m_effectPrefabs[1].m_prefab = UnforgibbableMod._gibObject;


                if (UnforgibbableMod._gibObject == null) return;
                var gibber = UnforgibbableMod._gibObject.gameObject.GetComponent<Gibber>();
                var deer_hit = __instance.GetPrefab("vfx_deer_hit");
                var deer_death = __instance.GetPrefab("vfx_deer_death");
                var burst = new ParticleSystem.Burst(0, new ParticleSystem.MinMaxCurve(3500, 5000), 2, 0.01f);
                burst.probability = 1;
                deer_hit.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                deer_hit.transform.Find("blood_drops").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                deer_hit.transform.Find("bloodchunks").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                deer_death.transform.Find("vfx_BloodHit 1").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                gibber.m_gibHitEffect = __instance.GetPrefab("vfx_deer_hit");
                gibber.m_gibDestroyEffect = deer_death;
                gibber.m_punchEffector.m_effectPrefabs[0].m_prefab =  __instance.GetPrefab("vfx_deer_hit");
            }
        }

        [HarmonyPatch(typeof(StoreGui), nameof(StoreGui.Awake))]
        public static class HudPatch
        {
            public static void Postfix(StoreGui __instance)
            {
                UnforgibbableMod.ingameDevUIX = (GameObject)Object.Instantiate(UnforgibbableMod.devuix, __instance.gameObject.GetComponentInParent<Transform>(), false)!;
            }
        }
    }
}