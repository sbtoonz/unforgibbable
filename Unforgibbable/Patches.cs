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
                __instance.m_prefabs.Add(UnforgibbableMod.deer_gibs);
                __instance.m_prefabs.Add(UnforgibbableMod.devuix);
                __instance.m_prefabs.Add(UnforgibbableMod.boar_gibs);
            }
            
            public static void Postfix(ZNetScene __instance)
            {
                if(__instance.m_prefabs.Count<=0 || __instance.GetPrefab("Amber") == null)return;
                __instance.m_prefabs.Find(x => x.name == "Deer").gameObject.GetComponent<Character>().m_deathEffects
                    .m_effectPrefabs[1].m_prefab = UnforgibbableMod.deer_gibs;
                
                __instance.m_prefabs.Find(x => x.name == "Boar").gameObject.GetComponent<Character>().m_deathEffects
                    .m_effectPrefabs[1].m_prefab = UnforgibbableMod.boar_gibs;

                if (UnforgibbableMod.deer_gibs == null) return;
                if (UnforgibbableMod.boar_gibs == null) return;
                var deerGibber = UnforgibbableMod.deer_gibs.gameObject.GetComponent<Gibber>();
                var boarGibber = UnforgibbableMod.boar_gibs.gameObject.GetComponent<Gibber>();
                var deerHit = __instance.GetPrefab("vfx_deer_hit");
                var deerDeath = __instance.GetPrefab("vfx_deer_death");
                var boarHit = __instance.GetPrefab("vfx_boar_hit");
                var boarDeath = __instance.GetPrefab("vfx_boar_death");
                var burst = new ParticleSystem.Burst(0, new ParticleSystem.MinMaxCurve(3500, 5000), 2, 0.01f);
                burst.probability = 1;
                
                
                deerHit.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                deerHit.transform.Find("blood_drops").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                deerHit.transform.Find("bloodchunks").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                deerDeath.transform.Find("vfx_BloodHit 1").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                
                boarHit.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                boarHit.transform.Find("blood_drops").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                boarHit.transform.Find("bloodchunks").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                boarDeath.transform.Find("vfx_BloodHit 1").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                
                
                deerGibber.m_gibHitEffect = deerHit;
                deerGibber.m_gibDestroyEffect = deerDeath;
                deerGibber.m_punchEffector.m_effectPrefabs[0].m_prefab =  deerHit;
                
                
                boarGibber.m_gibHitEffect = boarHit;
                boarGibber.m_gibDestroyEffect = boarDeath;
                boarGibber.m_punchEffector.m_effectPrefabs[0].m_prefab =  boarHit;
                
                
                
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