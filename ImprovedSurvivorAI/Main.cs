using BepInEx;
using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace ImprovedSurvivorAI
{
    // Metadata
    [BepInPlugin("Samuel17.ImprovedSurvivorAI", "ImprovedSurvivorAI", "1.4.0")]

    public class Main : BaseUnityPlugin
    {
        public static List<GameObject> survivorMasterPrefabs = new();
        public static List<CharacterMaster> activeSurvivorMasters = new();

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
            AdjustVanillaSurvivors();

            // Modify Engineer mobile turrets
            AdjustWalkerTurrets();
        }

        private void AdjustVanillaSurvivors()
        {
            //  Commando  //
            GameObject commandoMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(commandoMaster);
            new CommandoAI(commandoMaster);

            //  Huntress  //
            GameObject huntressMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Huntress/HuntressMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(huntressMaster);
            new HuntressAI(huntressMaster);

            //  Bandit  //
            GameObject banditMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Bandit2/Bandit2MonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(banditMaster);
            new BanditAI(banditMaster);

            //  MUL-T  //
            GameObject multMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Toolbot/ToolbotMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(multMaster);
            new MultAI(multMaster);

            //  Engineer  //
            GameObject engiMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Engi/EngiMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(engiMaster);
            new EngineerAI(engiMaster);

            //  Artificer  //
            GameObject artiMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MageMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(artiMaster);
            new ArtificerAI(artiMaster);

            //  Mercenary  //
            GameObject mercMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Merc/MercMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(mercMaster);
            new MercenaryAI(mercMaster);

            //  REX  //
            GameObject rexMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Treebot/TreebotMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(rexMaster);
            new RexAI(rexMaster);

            //  Loader  //
            GameObject loaderMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Loader/LoaderMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(loaderMaster);
            new LoaderAI(loaderMaster);

            //  Acrid  //
            GameObject acridMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Croco/CrocoMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(acridMaster);
            new AcridAI(acridMaster);

            //  Captain  //
            GameObject captainMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Captain/CaptainMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(captainMaster);
            new CaptainAI(captainMaster, enableCaptainBeacons);

            //  Railgunner  //
            GameObject railgunnerMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/RailgunnerMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(railgunnerMaster);
            new RailgunnerAI(railgunnerMaster);

            //  Void Fiend  //
            GameObject voidFiendMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidSurvivorMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(voidFiendMaster);
            new VoidFiendAI(voidFiendMaster);

            //  Seeker  //
            GameObject seekerMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Seeker/SeekerMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(seekerMaster);
            new SeekerAI(seekerMaster);

            //  False Son  //
            GameObject falseSonMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/FalseSon/FalseSonMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(falseSonMaster);
            new FalseSonAI(falseSonMaster);

            //  CHEF  //
            GameObject chefMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Chef/ChefMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(chefMaster);
            new ChefAI(chefMaster);

            //  Operator  //
            GameObject operatorMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/Drone Tech/DroneTechMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(operatorMaster);
            new OperatorAI(operatorMaster);

            //  Drifter  //
            GameObject drifterMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/Drifter/DrifterMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(drifterMaster);
            new DrifterAI(drifterMaster);

            //  Heretic  //
            GameObject hereticMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Heretic/HereticMonsterMaster.prefab").WaitForCompletion();
            ClearSkillDrivers(hereticMaster);
            new HereticAI(hereticMaster);
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
        private void ClearSkillDrivers(GameObject survivorMaster)
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
