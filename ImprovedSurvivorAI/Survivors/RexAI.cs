using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class RexAI
    {
        // Asset references
        public static SkillDef directiveDrillSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Treebot/TreebotBodyAimMortarRain.asset").WaitForCompletion();
        public static SkillDef seedBarrageSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Treebot/TreebotBodyAimMortar2.asset").WaitForCompletion();
        public static SkillDef directiveHarvestSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Treebot/TreebotBodyFireFruitSeed.asset").WaitForCompletion();
        public static SkillDef tanglingGrowthSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Treebot/TreebotBodyFireFlower2.asset").WaitForCompletion();

        public RexAI(GameObject masterObject)
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


            // Use utility off cooldown at point-blank
            AISkillDriver utility = masterObject.AddComponent<AISkillDriver>();
            utility.skillSlot = SkillSlot.Utility;
            utility.requireSkillReady = true;
            utility.requireEquipmentReady = false;
            utility.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utility.minDistance = 0f;
            utility.maxDistance = 15f;
            utility.selectionRequiresTargetLoS = false;
            utility.activationRequiresTargetLoS = false;
            utility.activationRequiresAimConfirmation = true;
            utility.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            utility.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utility.ignoreNodeGraph = false;
            utility.noRepeat = false;
            utility.shouldSprint = false;
            utility.shouldFireEquipment = false;
            utility.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Directive: Harvest off cooldown
            AISkillDriver directiveHarvest = masterObject.AddComponent<AISkillDriver>();
            directiveHarvest.skillSlot = SkillSlot.Special;
            directiveHarvest.requiredSkill = directiveHarvestSkill;
            directiveHarvest.requireSkillReady = true;
            directiveHarvest.requireEquipmentReady = false;
            directiveHarvest.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            directiveHarvest.minDistance = 0f;
            directiveHarvest.maxDistance = 150f;
            directiveHarvest.selectionRequiresTargetLoS = true;
            directiveHarvest.activationRequiresTargetLoS = true;
            directiveHarvest.activationRequiresAimConfirmation = true;
            directiveHarvest.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            directiveHarvest.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            directiveHarvest.ignoreNodeGraph = false;
            directiveHarvest.noRepeat = false;
            directiveHarvest.shouldSprint = false;
            directiveHarvest.shouldFireEquipment = false;
            directiveHarvest.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Tangling Growth off cooldown when healthy enough
            AISkillDriver tanglingGrowth = masterObject.AddComponent<AISkillDriver>();
            tanglingGrowth.skillSlot = SkillSlot.Special;
            tanglingGrowth.requiredSkill = tanglingGrowthSkill;
            tanglingGrowth.requireSkillReady = true;
            tanglingGrowth.requireEquipmentReady = false;
            tanglingGrowth.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            tanglingGrowth.minDistance = 0f;
            tanglingGrowth.maxDistance = 100f;
            tanglingGrowth.selectionRequiresTargetLoS = true;
            tanglingGrowth.activationRequiresTargetLoS = true;
            tanglingGrowth.activationRequiresAimConfirmation = true;
            tanglingGrowth.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            tanglingGrowth.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            tanglingGrowth.ignoreNodeGraph = false;
            tanglingGrowth.noRepeat = false;
            tanglingGrowth.shouldSprint = false;
            tanglingGrowth.shouldFireEquipment = false;
            tanglingGrowth.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            tanglingGrowth.minUserHealthFraction = .6f;


            // Use Directive: Drill off cooldown
            AISkillDriver directiveDrill = masterObject.AddComponent<AISkillDriver>();
            directiveDrill.skillSlot = SkillSlot.Secondary;
            directiveDrill.requiredSkill = directiveDrillSkill;
            directiveDrill.requireSkillReady = true;
            directiveDrill.requireEquipmentReady = false;
            directiveDrill.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            directiveDrill.minDistance = 0f;
            directiveDrill.maxDistance = 300f;
            directiveDrill.selectionRequiresTargetLoS = true;
            directiveDrill.activationRequiresTargetLoS = true;
            directiveDrill.activationRequiresAimConfirmation = true;
            directiveDrill.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            directiveDrill.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            directiveDrill.ignoreNodeGraph = false;
            directiveDrill.noRepeat = false;
            directiveDrill.shouldSprint = false;
            directiveDrill.shouldFireEquipment = false;
            directiveDrill.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Seed Barrage off cooldown when healthy enough
            AISkillDriver seedBarrage = masterObject.AddComponent<AISkillDriver>();
            seedBarrage.skillSlot = SkillSlot.Secondary;
            seedBarrage.requiredSkill = seedBarrageSkill;
            seedBarrage.requireSkillReady = true;
            seedBarrage.requireEquipmentReady = false;
            seedBarrage.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            seedBarrage.minDistance = 0f;
            seedBarrage.maxDistance = 300f;
            seedBarrage.selectionRequiresTargetLoS = true;
            seedBarrage.activationRequiresTargetLoS = true;
            seedBarrage.activationRequiresAimConfirmation = true;
            seedBarrage.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            seedBarrage.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            seedBarrage.ignoreNodeGraph = false;
            seedBarrage.noRepeat = false;
            seedBarrage.shouldSprint = false;
            seedBarrage.shouldFireEquipment = false;
            seedBarrage.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            seedBarrage.minUserHealthFraction = .6f;


            // Shoot primary while retreating (point-blank)
            AISkillDriver primaryRetreat = masterObject.AddComponent<AISkillDriver>();
            primaryRetreat.skillSlot = SkillSlot.Primary;
            primaryRetreat.requireSkillReady = true;
            primaryRetreat.requireEquipmentReady = false;
            primaryRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryRetreat.minDistance = 0f;
            primaryRetreat.maxDistance = 10f;
            primaryRetreat.selectionRequiresTargetLoS = true;
            primaryRetreat.activationRequiresTargetLoS = true;
            primaryRetreat.activationRequiresAimConfirmation = false;
            primaryRetreat.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            primaryRetreat.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryRetreat.ignoreNodeGraph = false;
            primaryRetreat.noRepeat = false;
            primaryRetreat.shouldSprint = false;
            primaryRetreat.shouldFireEquipment = false;
            primaryRetreat.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryRetreat.driverUpdateTimerOverride = .5f;


            // Shoot primary while strafing (close)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 50f;
            primaryStrafe.selectionRequiresTargetLoS = true;
            primaryStrafe.activationRequiresTargetLoS = true;
            primaryStrafe.activationRequiresAimConfirmation = false;
            primaryStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primaryStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryStrafe.ignoreNodeGraph = false;
            primaryStrafe.noRepeat = false;
            primaryStrafe.shouldSprint = false;
            primaryStrafe.shouldFireEquipment = false;
            primaryStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryStrafe.driverUpdateTimerOverride = .5f;


            // Shoot primary while approaching (far)
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requireSkillReady = true;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 150f;
            primaryChase.selectionRequiresTargetLoS = true;
            primaryChase.activationRequiresTargetLoS = true;
            primaryChase.activationRequiresAimConfirmation = false;
            primaryChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            primaryChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryChase.ignoreNodeGraph = false;
            primaryChase.noRepeat = false;
            primaryChase.shouldSprint = false;
            primaryChase.shouldFireEquipment = false;
            primaryChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryChase.driverUpdateTimerOverride = .5f;


            // Sprint towards the target
            AISkillDriver sprintChase = masterObject.AddComponent<AISkillDriver>();
            sprintChase.skillSlot = SkillSlot.None;
            sprintChase.requireSkillReady = false;
            sprintChase.requireEquipmentReady = false;
            sprintChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sprintChase.minDistance = 0f;
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
        }
    }
}
