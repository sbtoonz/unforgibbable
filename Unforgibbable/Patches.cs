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
                __instance.m_prefabs.Add(UnforgibbableMod.neck_gibs);
                __instance.m_prefabs.Add(UnforgibbableMod.troll_gibs);
            }
            
            public static void Postfix(ZNetScene __instance)
            {
                if (UnforgibbableMod.deer_gibs == null) return;
                if (UnforgibbableMod.boar_gibs == null) return;
                if (UnforgibbableMod.neck_gibs == null) return;
                if (UnforgibbableMod.troll_gibs == null) return;
                
                
                if(__instance.m_prefabs.Count<=0 || __instance.GetPrefab("Amber") == null)return;
                __instance.m_prefabs.Find(x => x.name == "Deer").gameObject.GetComponent<Character>().m_deathEffects
                    .m_effectPrefabs[1].m_prefab = UnforgibbableMod.deer_gibs;
                
                __instance.m_prefabs.Find(x => x.name == "Boar").gameObject.GetComponent<Character>().m_deathEffects
                    .m_effectPrefabs[1].m_prefab = UnforgibbableMod.boar_gibs;
                
                __instance.m_prefabs.Find(x => x.name == "Neck").gameObject.GetComponent<Character>().m_deathEffects
                    .m_effectPrefabs[2].m_prefab = UnforgibbableMod.neck_gibs;

                __instance.m_prefabs.Find(x => x.name == "Troll").gameObject.GetComponent<Character>().m_deathEffects
                    .m_effectPrefabs[1].m_prefab = UnforgibbableMod.troll_gibs;

                var deerGibber = UnforgibbableMod.deer_gibs.gameObject.GetComponent<Gibber>();
                var boarGibber = UnforgibbableMod.boar_gibs.gameObject.GetComponent<Gibber>();
                var neckGibber = UnforgibbableMod.neck_gibs.gameObject.GetComponent<Gibber>();
                var trollgibber = UnforgibbableMod.troll_gibs.gameObject.GetComponent<Gibber>();
                var deerHit = __instance.GetPrefab("vfx_deer_hit");
                var deerDeath = __instance.GetPrefab("vfx_deer_death");
                var boarHit = __instance.GetPrefab("vfx_boar_hit");
                var boarDeath = __instance.GetPrefab("vfx_boar_death");
                var neckDeath = __instance.GetPrefab("vfx_neck_death");
                var neckHit = __instance.GetPrefab("vfx_neck_hit");
                var trollhit = __instance.GetPrefab("vfx_foresttroll_hit");
                var trolldeath = __instance.GetPrefab("vfx_troll_death");
                
                var burst = new ParticleSystem.Burst(0, new ParticleSystem.MinMaxCurve(500, 1500), 1, 0.01f);
                burst.probability = 1;
                
                
                deerHit.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                deerHit.transform.Find("blood_drops").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                deerHit.transform.Find("bloodchunks").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                deerDeath.transform.Find("vfx_BloodHit 1").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                
                boarHit.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                boarHit.transform.Find("blood_drops").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                boarHit.transform.Find("bloodchunks").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                boarDeath.transform.Find("vfx_BloodHit 1").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                
                trollhit.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                trollhit.transform.Find("vfx_BloodHit 1").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                trollhit.transform.Find("cunks").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                trollhit.transform.Find("cunks/blob_splat").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                trolldeath.transform.Find("vfx_BloodHit 1").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                   
                neckHit.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                neckHit.transform.Find("blood_drips").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0, burst);
                neckHit.transform.Find("vfx_BloodHit_decals").transform.Find("blob_splat").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                neckDeath.transform.Find("vfx_BloodHit 1").gameObject.GetComponent<ParticleSystem>().emission.SetBurst(0,  burst);
                
                
                deerGibber.m_gibHitEffect = deerHit;
                deerGibber.m_gibDestroyEffect = deerDeath;
                deerGibber.m_punchEffector.m_effectPrefabs[0].m_prefab =  deerHit;
                
                
                boarGibber.m_gibHitEffect = boarHit;
                boarGibber.m_gibDestroyEffect = boarDeath;
                boarGibber.m_punchEffector.m_effectPrefabs[0].m_prefab =  boarHit;
                
                neckGibber.m_gibHitEffect = neckHit;
                neckGibber.m_gibDestroyEffect = neckDeath;
                neckGibber.m_punchEffector.m_effectPrefabs[0].m_prefab =  neckHit;
                
                trollgibber.m_gibHitEffect = trollhit;
                trollgibber.m_gibDestroyEffect = trolldeath;
                trollgibber.m_punchEffector.m_effectPrefabs[0].m_prefab =  trollhit;
                
                
                
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

        [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.Start))]
        public static class zonesyspatch
        {
            public static void Postfix(ZoneSystem __instance)
            {
                foreach (var variable in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (variable.gameObject.name is "fx_TickBloodHit" or "BloodDrops")
                    {
                        UnforgibbableMod.gorefabs.Add(variable);
                    }
                }
            }
        }
    }
}