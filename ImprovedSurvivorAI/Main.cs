using BepInEx;
using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace ImprovedSurvivorAI
{
    // Metadata
    [BepInPlugin("Samuel17.ImprovedSurvivorAI", "ImprovedSurvivorAI", "1.5.2")]

    public class Main : BaseUnityPlugin
    {
        public static List<GameObject> survivorMasterPrefabs = new();
        public static List<CharacterMaster> activeSurvivorMasters = new();
        public static SkillDef[] listSkillDefs;

        // Config fields
        public static bool enableCaptainBeacons = true;
        public static bool walkerTurretChanges = true;
        public static bool walkerTurretSprint = true;
        public static float walkerTurretRange = 35f;

        // Load addressables
        public static EntityStateConfiguration walkerTurretBeam = Addressables.LoadAssetAsync<EntityStateConfiguration>("RoR2/Base/Engi/EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.asset").WaitForCompletion();

        public void Awake()
        {
            // Logging!
            Log.Init(Logger);

            // Configs
            enableCaptainBeacons = Config.Bind("Captain", "Enable Supply Beacons", true, "Minion Captains will drop both Supply Beacons as soon as they spawn.").Value;
            walkerTurretChanges = Config.Bind("Mobile Turrets", "Enable Mobile Turret Changes", true, "Enable the configs pertaining to Engineer's mobile turrets.").Value;
            walkerTurretSprint = Config.Bind("Mobile Turrets", "Enable Mobile Turret Sprint", true, "Mobile turrets sprint when following their owner.").Value;
            walkerTurretRange = Config.Bind("Mobile Turrets", "Mobile Turret Range", 35f, "Mobile turrets start attacking from this far away. Set to a negative value for no change. Vanilla is 25.").Value;

            // Improved awareness for Survivor AI
            On.RoR2.CharacterAI.BaseAI.EvaluateSkillDrivers += InfiniVisionLite;

            // Spend less time shooting at corpses, will you?
            On.RoR2.BullseyeSearch.GetResults += IgnoreTheDead;
            GlobalEventManager.onCharacterDeathGlobal += GlobalEventManager_onCharacterDeathGlobal;
            CharacterMaster.onCharacterMasterDiscovered += CharacterMaster_onCharacterMasterDiscovered;
            CharacterMaster.onCharacterMasterLost += CharacterMaster_onCharacterMasterLost;

            // Gummy clone targeting
            On.RoR2.CharacterMaster.SetUpGummyClone += GummyCloneTargeting;

            // Modify vanilla survivors
            InitSurvivors.AdjustVanillaSurvivors();

            // Modify Engineer mobile turrets
            AdjustWalkerTurrets();

            RoR2Application.onLoad += () =>
            {
                // SkillDef reference
                listSkillDefs = SkillCatalog._allSkillDefs;

                // Modify modded survivors
                InitSurvivors.AdjustModdedSurvivors();
            };
        }

        private void AdjustWalkerTurrets()
        {
            if (!walkerTurretChanges) return;

            GameObject walkerTurretMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Engi/EngiWalkerTurretMaster.prefab").WaitForCompletion();

            AISkillDriver[] skillDrivers = walkerTurretMaster.GetComponents<AISkillDriver>();
            foreach (AISkillDriver skillDriver in skillDrivers)
            {
                switch (skillDriver.customName)
                {
                    case "ReturnToLeader": // Enable sprinting on the second "return to owner" driver
                        if (skillDriver.minDistance == 6f && walkerTurretSprint)
                        {
                            skillDriver.shouldSprint = true;
                        }
                        break;

                    case "ChaseAndFireAtEnemy": // Improve range on the attacking driver
                        if (walkerTurretRange < 0) return;

                        skillDriver.maxDistance = Mathf.Max(walkerTurretRange, 0f);
                        for (int i = 0; i < walkerTurretBeam.serializedFieldsCollection.serializedFields.Length; i++)
                        {
                            if (walkerTurretBeam.serializedFieldsCollection.serializedFields[i].fieldName == "maxDistance")
                            {
                                walkerTurretBeam.serializedFieldsCollection.serializedFields[i].fieldValue.stringValue = skillDriver.maxDistance.ToString(); // Adjust the actual laser's length
                            }
                        }
   
                        break;
                };
            }
        }

        // Clear all existing skill dirvers
        public static void ClearSkillDrivers(GameObject survivorMaster)
        {
            AISkillDriver[] skillDrivers = survivorMaster.GetComponents<AISkillDriver>();
            foreach (AISkillDriver skillDriver in skillDrivers)
            {
                Destroy(skillDriver);
            }

            // Assign them to the index list
            survivorMasterPrefabs.Add(survivorMaster);
            Log.Message("Added " + survivorMaster.name + " to the list of Survivor masters.");
        }

        // If nothing is found with the initial targeting, force another one with no LoS restriction
        private BaseAI.SkillDriverEvaluation InfiniVisionLite(On.RoR2.CharacterAI.BaseAI.orig_EvaluateSkillDrivers orig, BaseAI self)
        {
            if (IsSurvivorMaster(self) && self.body && (!self.currentEnemy.gameObject || !self.currentEnemy.healthComponent || self.currentEnemy.healthComponent.alive))
            {
                self.ForceAcquireNearestEnemyIfNoCurrentEnemy();
            }

            return orig(self);
        }

        // Blacklist dead entities from being targeted in general
        private IEnumerable<HurtBox> IgnoreTheDead(On.RoR2.BullseyeSearch.orig_GetResults orig, BullseyeSearch self)
        {
            _ = self.candidatesEnumerable;

            self.candidatesEnumerable.RemoveAll((BullseyeSearch.CandidateInfo candidateInfo) => !candidateInfo.hurtBox.healthComponent.alive);

            return orig(self);
        }

        // Upon killing an enemy, the Survivor is forced into losing track of it entirely
        private void GlobalEventManager_onCharacterDeathGlobal(DamageReport damageReport)
        {
            foreach (CharacterMaster survivorMaster in activeSurvivorMasters)
            {
                CharacterBody victimBody = damageReport.victimBody;
                BaseAI survivorAI = survivorMaster.GetComponent<BaseAI>();
                if (survivorAI && survivorAI.currentEnemy != null && survivorAI.currentEnemy.characterBody == victimBody)
                {
                    survivorAI.currentEnemy.Reset();
                }
            }
        }

        private void CharacterMaster_onCharacterMasterDiscovered(CharacterMaster master)
        {
            if (IsSurvivorMasterAlt(master))
            {
                activeSurvivorMasters.Add(master);
            }
        }

        private void CharacterMaster_onCharacterMasterLost(CharacterMaster master)
        {
            activeSurvivorMasters.Remove(master);
        }

        // Gummy clones copy their owner's target, similar to drones
        private void GummyCloneTargeting(On.RoR2.CharacterMaster.orig_SetUpGummyClone orig, CharacterMaster self)
        {
            orig(self);

            BaseAI gummyAI = self.GetComponent<BaseAI>();
            if (gummyAI && IsSurvivorMaster(gummyAI))
            {
                gummyAI.copyLeaderTarget = true;
            }
        }

        // Check for matching names
        public bool IsSurvivorMaster(BaseAI self)
        {
            if (self && self.master)
            {
                GameObject masterPrefab = MasterCatalog.GetMasterPrefab(self.master.masterIndex);
                if (masterPrefab && survivorMasterPrefabs.Contains(masterPrefab))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsSurvivorMasterAlt(CharacterMaster self)
        {
            GameObject masterPrefab = MasterCatalog.GetMasterPrefab(self.masterIndex);
            if (masterPrefab && survivorMasterPrefabs.Contains(masterPrefab))
            {
                return true;
            }

            return false;
        }
    }
}
