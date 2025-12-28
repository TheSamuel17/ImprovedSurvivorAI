using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class EngineerAI
    {
        // Asset references
        public static SkillDef pressureMineSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Engi/EngiBodyPlaceMine.asset").WaitForCompletion();
        public static SkillDef spiderMineSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Engi/EngiBodyPlaceSpiderMine.asset").WaitForCompletion();
        public static SkillDef bubbleShieldSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Engi/EngiBodyPlaceBubbleShield.asset").WaitForCompletion();
        public static SkillDef harpoonSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Engi/EngiHarpoons.asset").WaitForCompletion();
        public static SkillDef harpoonTargetSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Engi/EngiConfirmTargetDummy.asset").WaitForCompletion();
        public static SkillDef turretSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Engi/EngiBodyPlaceTurret.asset").WaitForCompletion();
        public static SkillDef walkerTurretSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Engi/EngiBodyPlaceWalkerTurret.asset").WaitForCompletion();

        public EngineerAI(GameObject masterObject)
        {
            // Better targeting
            BaseAI baseAI = masterObject.GetComponent<BaseAI>();
            if (baseAI)
            {
                baseAI.fullVision = true;
                baseAI.neverRetaliateFriendlies = true;
                baseAI.aimVectorDampTime = .05f;
                baseAI.aimVectorMaxSpeed = 720;
            }


            // Misc
            CharacterMaster.onCharacterMasterDiscovered += CharacterMaster_onCharacterMasterDiscovered;


            // Paint targets with Thermal Harpoons
            AISkillDriver targetHarpoons = masterObject.AddComponent<AISkillDriver>();
            targetHarpoons.skillSlot = SkillSlot.Primary;
            targetHarpoons.requiredSkill = harpoonTargetSkill;
            targetHarpoons.requireSkillReady = true;
            targetHarpoons.requireEquipmentReady = false;
            targetHarpoons.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            targetHarpoons.minDistance = 0f;
            targetHarpoons.maxDistance = 150f;
            targetHarpoons.selectionRequiresTargetLoS = true;
            targetHarpoons.activationRequiresTargetLoS = true;
            targetHarpoons.activationRequiresAimConfirmation = false;
            targetHarpoons.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            targetHarpoons.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            targetHarpoons.ignoreNodeGraph = false;
            targetHarpoons.noRepeat = false;
            targetHarpoons.shouldSprint = false;
            targetHarpoons.shouldFireEquipment = false;
            targetHarpoons.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            targetHarpoons.driverUpdateTimerOverride = 1.2f;


            // Place stationary turrets off cooldown
            AISkillDriver turret = masterObject.AddComponent<AISkillDriver>();
            turret.skillSlot = SkillSlot.Special;
            turret.requiredSkill = turretSkill;
            turret.requireSkillReady = true;
            turret.requireEquipmentReady = false;
            turret.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            turret.minDistance = 0f;
            turret.maxDistance = 60f;
            turret.selectionRequiresTargetLoS = true;
            turret.activationRequiresTargetLoS = true;
            turret.activationRequiresAimConfirmation = false;
            turret.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            turret.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            turret.ignoreNodeGraph = false;
            turret.noRepeat = false;
            turret.shouldSprint = false;
            turret.shouldFireEquipment = false;
            turret.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            turret.selectionRequiresOnGround = true;


            // Place mobile turrets off cooldown
            AISkillDriver walkerTurret = masterObject.AddComponent<AISkillDriver>();
            walkerTurret.skillSlot = SkillSlot.Special;
            walkerTurret.requiredSkill = walkerTurretSkill;
            walkerTurret.requireSkillReady = true;
            walkerTurret.requireEquipmentReady = false;
            walkerTurret.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            walkerTurret.minDistance = 0f;
            walkerTurret.maxDistance = float.PositiveInfinity;
            walkerTurret.selectionRequiresTargetLoS = false;
            walkerTurret.activationRequiresTargetLoS = false;
            walkerTurret.activationRequiresAimConfirmation = false;
            walkerTurret.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            walkerTurret.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            walkerTurret.ignoreNodeGraph = false;
            walkerTurret.noRepeat = false;
            walkerTurret.shouldSprint = false;
            walkerTurret.shouldFireEquipment = false;
            walkerTurret.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            walkerTurret.selectionRequiresOnGround = true;


            // Place bubble shield on nearby allies off cooldown
            AISkillDriver bubbleShieldAlly = masterObject.AddComponent<AISkillDriver>();
            bubbleShieldAlly.skillSlot = SkillSlot.Utility;
            bubbleShieldAlly.requiredSkill = bubbleShieldSkill;
            bubbleShieldAlly.requireSkillReady = true;
            bubbleShieldAlly.requireEquipmentReady = false;
            bubbleShieldAlly.moveTargetType = AISkillDriver.TargetType.NearestFriendlyInSkillRange;
            bubbleShieldAlly.minDistance = 0f;
            bubbleShieldAlly.maxDistance = 10f;
            bubbleShieldAlly.selectionRequiresTargetLoS = false;
            bubbleShieldAlly.activationRequiresTargetLoS = false;
            bubbleShieldAlly.activationRequiresAimConfirmation = true;
            bubbleShieldAlly.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            bubbleShieldAlly.aimType = AISkillDriver.AimType.AtMoveTarget;
            bubbleShieldAlly.ignoreNodeGraph = false;
            bubbleShieldAlly.noRepeat = true;
            bubbleShieldAlly.shouldSprint = true;
            bubbleShieldAlly.shouldFireEquipment = false;
            bubbleShieldAlly.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Throw out Spider Mines off cooldown
            AISkillDriver spiderMines = masterObject.AddComponent<AISkillDriver>();
            spiderMines.skillSlot = SkillSlot.Secondary;
            spiderMines.requiredSkill = spiderMineSkill;
            spiderMines.requireSkillReady = true;
            spiderMines.requireEquipmentReady = false;
            spiderMines.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            spiderMines.minDistance = 0f;
            spiderMines.maxDistance = 40f;
            spiderMines.selectionRequiresTargetLoS = true;
            spiderMines.activationRequiresTargetLoS = true;
            spiderMines.activationRequiresAimConfirmation = true;
            spiderMines.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            spiderMines.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            spiderMines.ignoreNodeGraph = false;
            spiderMines.noRepeat = false;
            spiderMines.shouldSprint = false;
            spiderMines.shouldFireEquipment = false;
            spiderMines.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Place Pressure Mines on nearby allies off cooldown
            AISkillDriver pressureMinesAlly = masterObject.AddComponent<AISkillDriver>();
            pressureMinesAlly.skillSlot = SkillSlot.Secondary;
            pressureMinesAlly.requiredSkill = pressureMineSkill;
            pressureMinesAlly.requireSkillReady = true;
            pressureMinesAlly.requireEquipmentReady = false;
            pressureMinesAlly.moveTargetType = AISkillDriver.TargetType.NearestFriendlyInSkillRange;
            pressureMinesAlly.minDistance = 0f;
            pressureMinesAlly.maxDistance = 10f;
            pressureMinesAlly.selectionRequiresTargetLoS = false;
            pressureMinesAlly.activationRequiresTargetLoS = false;
            pressureMinesAlly.activationRequiresAimConfirmation = true;
            pressureMinesAlly.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            pressureMinesAlly.aimType = AISkillDriver.AimType.AtMoveTarget;
            pressureMinesAlly.ignoreNodeGraph = false;
            pressureMinesAlly.noRepeat = false;
            pressureMinesAlly.shouldSprint = false;
            pressureMinesAlly.shouldFireEquipment = false;
            pressureMinesAlly.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Throw out Pressure Mines on targets off cooldown
            AISkillDriver pressureMines = masterObject.AddComponent<AISkillDriver>();
            pressureMines.skillSlot = SkillSlot.Secondary;
            pressureMines.requiredSkill = pressureMineSkill;
            pressureMines.requireSkillReady = true;
            pressureMines.requireEquipmentReady = false;
            pressureMines.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            pressureMines.minDistance = 0f;
            pressureMines.maxDistance = 40f;
            pressureMines.selectionRequiresTargetLoS = true;
            pressureMines.activationRequiresTargetLoS = true;
            pressureMines.activationRequiresAimConfirmation = true;
            pressureMines.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            pressureMines.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            pressureMines.ignoreNodeGraph = false;
            pressureMines.noRepeat = false;
            pressureMines.shouldSprint = false;
            pressureMines.shouldFireEquipment = false;
            pressureMines.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Activate Thermal Harpoons off cooldown
            AISkillDriver activateHarpoons = masterObject.AddComponent<AISkillDriver>();
            activateHarpoons.skillSlot = SkillSlot.Utility;
            activateHarpoons.requiredSkill = harpoonSkill;
            activateHarpoons.requireSkillReady = true;
            activateHarpoons.requireEquipmentReady = false;
            activateHarpoons.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            activateHarpoons.minDistance = 0f;
            activateHarpoons.maxDistance = 150f;
            activateHarpoons.selectionRequiresTargetLoS = true;
            activateHarpoons.activationRequiresTargetLoS = true;
            activateHarpoons.activationRequiresAimConfirmation = true;
            activateHarpoons.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            activateHarpoons.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            activateHarpoons.ignoreNodeGraph = false;
            activateHarpoons.noRepeat = false;
            activateHarpoons.shouldSprint = false;
            activateHarpoons.shouldFireEquipment = false;
            activateHarpoons.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Shoot primary
            AISkillDriver primary = masterObject.AddComponent<AISkillDriver>();
            primary.skillSlot = SkillSlot.Primary;
            primary.requireSkillReady = true;
            primary.requireEquipmentReady = false;
            primary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primary.minDistance = 0f;
            primary.maxDistance = 50f;
            primary.selectionRequiresTargetLoS = true;
            primary.activationRequiresTargetLoS = true;
            primary.activationRequiresAimConfirmation = false;
            primary.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primary.ignoreNodeGraph = false;
            primary.noRepeat = false;
            primary.shouldSprint = false;
            primary.shouldFireEquipment = false;
            primary.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primary.driverUpdateTimerOverride = 2.5f;


            // Sprint towards the target
            AISkillDriver sprintChase = masterObject.AddComponent<AISkillDriver>();
            sprintChase.skillSlot = SkillSlot.None;
            sprintChase.requireSkillReady = false;
            sprintChase.requireEquipmentReady = false;
            sprintChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sprintChase.minDistance = 50f;
            sprintChase.maxDistance = 400f;
            sprintChase.selectionRequiresTargetLoS = false;
            sprintChase.activationRequiresTargetLoS = false;
            sprintChase.activationRequiresAimConfirmation = false;
            sprintChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            sprintChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sprintChase.ignoreNodeGraph = false;
            sprintChase.noRepeat = false;
            sprintChase.shouldSprint = true;
            sprintChase.shouldFireEquipment = false;
            sprintChase.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            sprintChase.resetCurrentEnemyOnNextDriverSelection = true;


            // Sprint towards the nearest ally
            AISkillDriver followAlly = masterObject.AddComponent<AISkillDriver>();
            followAlly.skillSlot = SkillSlot.None;
            followAlly.requireSkillReady = false;
            followAlly.requireEquipmentReady = false;
            followAlly.moveTargetType = AISkillDriver.TargetType.NearestFriendlyInSkillRange;
            followAlly.minDistance = 15f;
            followAlly.maxDistance = 100f;
            followAlly.selectionRequiresTargetLoS = false;
            followAlly.activationRequiresTargetLoS = false;
            followAlly.activationRequiresAimConfirmation = false;
            followAlly.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            followAlly.aimType = AISkillDriver.AimType.AtCurrentLeader;
            followAlly.ignoreNodeGraph = false;
            followAlly.noRepeat = false;
            followAlly.shouldSprint = true;
            followAlly.shouldFireEquipment = false;
            followAlly.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            followAlly.resetCurrentEnemyOnNextDriverSelection = true;


            // Sprint towards the owner
            AISkillDriver followOwner = masterObject.AddComponent<AISkillDriver>();
            followOwner.skillSlot = SkillSlot.None;
            followOwner.requireSkillReady = false;
            followOwner.requireEquipmentReady = false;
            followOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            followOwner.minDistance = 15f;
            followOwner.maxDistance = float.PositiveInfinity;
            followOwner.selectionRequiresTargetLoS = false;
            followOwner.activationRequiresTargetLoS = false;
            followOwner.activationRequiresAimConfirmation = false;
            followOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            followOwner.aimType = AISkillDriver.AimType.AtCurrentLeader;
            followOwner.ignoreNodeGraph = false;
            followOwner.noRepeat = false;
            followOwner.shouldSprint = true;
            followOwner.shouldFireEquipment = false;
            followOwner.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            followOwner.resetCurrentEnemyOnNextDriverSelection = true;


            // Stop near owner
            AISkillDriver idleNearOwner = masterObject.AddComponent<AISkillDriver>();
            idleNearOwner.skillSlot = SkillSlot.None;
            idleNearOwner.requireSkillReady = false;
            idleNearOwner.requireEquipmentReady = false;
            idleNearOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            idleNearOwner.minDistance = 0f;
            idleNearOwner.maxDistance = 15f;
            idleNearOwner.selectionRequiresTargetLoS = false;
            idleNearOwner.activationRequiresTargetLoS = false;
            idleNearOwner.activationRequiresAimConfirmation = false;
            idleNearOwner.movementType = AISkillDriver.MovementType.Stop;
            idleNearOwner.aimType = AISkillDriver.AimType.AtCurrentLeader;
            idleNearOwner.ignoreNodeGraph = false;
            idleNearOwner.noRepeat = false;
            idleNearOwner.shouldSprint = false;
            idleNearOwner.shouldFireEquipment = false;
            idleNearOwner.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            idleNearOwner.resetCurrentEnemyOnNextDriverSelection = true;


            // Sprint towards the target
            AISkillDriver sprintChaseLowPriority = masterObject.AddComponent<AISkillDriver>();
            sprintChaseLowPriority.skillSlot = SkillSlot.None;
            sprintChaseLowPriority.requireSkillReady = false;
            sprintChaseLowPriority.requireEquipmentReady = false;
            sprintChaseLowPriority.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sprintChaseLowPriority.minDistance = 0f;
            sprintChaseLowPriority.maxDistance = float.PositiveInfinity;
            sprintChaseLowPriority.selectionRequiresTargetLoS = false;
            sprintChaseLowPriority.activationRequiresTargetLoS = false;
            sprintChaseLowPriority.activationRequiresAimConfirmation = false;
            sprintChaseLowPriority.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            sprintChaseLowPriority.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sprintChaseLowPriority.ignoreNodeGraph = false;
            sprintChaseLowPriority.noRepeat = false;
            sprintChaseLowPriority.shouldSprint = true;
            sprintChaseLowPriority.shouldFireEquipment = false;
            sprintChaseLowPriority.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            sprintChaseLowPriority.resetCurrentEnemyOnNextDriverSelection = true;


            // Confirm turret placement
            AISkillDriver placeTurret = masterObject.AddComponent<AISkillDriver>();
            placeTurret.skillSlot = SkillSlot.Primary;
            placeTurret.requireSkillReady = false;
            placeTurret.requireEquipmentReady = false;
            placeTurret.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            placeTurret.minDistance = 0f;
            placeTurret.maxDistance = float.PositiveInfinity;
            placeTurret.selectionRequiresTargetLoS = false;
            placeTurret.activationRequiresTargetLoS = false;
            placeTurret.activationRequiresAimConfirmation = false;
            placeTurret.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            placeTurret.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            placeTurret.ignoreNodeGraph = false;
            placeTurret.noRepeat = false;
            placeTurret.shouldSprint = false;
            placeTurret.shouldFireEquipment = false;
            placeTurret.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            placeTurret.selectionRequiresOnGround = true;
            placeTurret.driverUpdateTimerOverride = .5f;


            // Release primary fire
            AISkillDriver releasePrimary = masterObject.AddComponent<AISkillDriver>();
            releasePrimary.skillSlot = SkillSlot.None;
            releasePrimary.requireSkillReady = false;
            releasePrimary.requireEquipmentReady = false;
            releasePrimary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            releasePrimary.minDistance = 0f;
            releasePrimary.maxDistance = float.PositiveInfinity;
            releasePrimary.selectionRequiresTargetLoS = false;
            releasePrimary.activationRequiresTargetLoS = false;
            releasePrimary.activationRequiresAimConfirmation = false;
            releasePrimary.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            releasePrimary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            releasePrimary.ignoreNodeGraph = false;
            releasePrimary.noRepeat = false;
            releasePrimary.shouldSprint = false;
            releasePrimary.shouldFireEquipment = false;
            releasePrimary.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            releasePrimary.driverUpdateTimerOverride = .2f;


            // Overrides
            turret.nextHighPriorityOverride = placeTurret;
            walkerTurret.nextHighPriorityOverride = placeTurret;
            targetHarpoons.nextHighPriorityOverride = releasePrimary;
            primary.nextHighPriorityOverride = releasePrimary;
        }

        private void CharacterMaster_onCharacterMasterDiscovered(CharacterMaster master)
        {
            if (master.masterIndex == MasterCatalog.FindMasterIndex("EngiMonsterMaster"))
            {
                var component = master.GetComponent<EngiHarpoonsController>();
                if (!component && master.gameObject)
                {
                    component = master.gameObject.AddComponent<EngiHarpoonsController>();
                }
            }
        }
    }

    public class EngiHarpoonsController : MonoBehaviour
    {
        public static float updateInterval = .5f; // Seconds
        public static float timer = 0f;

        public static SkillDef harpoonSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Engi/EngiHarpoons.asset").WaitForCompletion();
        public static AISkillDriver harpoonSkillDriver;

        private void Start()
        {
            AISkillDriver[] skillDrivers = gameObject.GetComponents<AISkillDriver>();
            foreach (AISkillDriver skillDriver in skillDrivers)
            {
                if (skillDriver.requiredSkill == harpoonSkill)
                {
                    harpoonSkillDriver = skillDriver;
                    break;
                };
            }
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer < updateInterval) return;
            timer -= updateInterval;

            if (harpoonSkillDriver)
            {
                BaseAI ai = gameObject.GetComponent<BaseAI>();
                if (ai && ai.body)
                {
                    if (ai.body.skillLocator && ai.body.skillLocator.utility && ai.body.skillLocator.utility.skillDef == harpoonSkill)
                    {
                        if (ai.body.skillLocator.utility.stock >= ai.body.skillLocator.utility.maxStock)
                        {
                            harpoonSkillDriver.enabled = true; // Enable at max stocks
                        }
                        else
                        {
                            harpoonSkillDriver.enabled = false; // Disable below max stocks
                        }
                    }
                }
            }
        }
    }
}
