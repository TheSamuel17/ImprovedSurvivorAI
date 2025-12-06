using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class MercenaryAI
    {
        // Asset references
        public static SkillDef eviscerateSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Merc/MercBodyEvis.asset").WaitForCompletion();
        public static SkillDef slicingWindsSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Merc/MercBodyEvisProjectile.asset").WaitForCompletion();

        public MercenaryAI(GameObject masterObject)
        {
            // Better targeting
            BaseAI baseAI = masterObject.GetComponent<BaseAI>();
            if (baseAI)
            {
                baseAI.fullVision = true;
                baseAI.neverRetaliateFriendlies = true;
                baseAI.aimVectorDampTime = .05f;
                baseAI.aimVectorMaxSpeed = 3600;
            }


            // Use Utility on the target off cooldown
            AISkillDriver utility = masterObject.AddComponent<AISkillDriver>();
            utility.skillSlot = SkillSlot.Utility;
            utility.requireSkillReady = true;
            utility.requireEquipmentReady = false;
            utility.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utility.minDistance = 0f;
            utility.maxDistance = 35f;
            utility.selectionRequiresTargetLoS = true;
            utility.activationRequiresTargetLoS = true;
            utility.activationRequiresAimConfirmation = true;
            utility.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utility.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utility.ignoreNodeGraph = true;
            utility.noRepeat = false;
            utility.shouldSprint = true;
            utility.shouldFireEquipment = false;
            utility.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Eviscerate on the target off cooldown
            AISkillDriver eviscerate = masterObject.AddComponent<AISkillDriver>();
            eviscerate.skillSlot = SkillSlot.Special;
            eviscerate.requiredSkill = eviscerateSkill;
            eviscerate.requireSkillReady = true;
            eviscerate.requireEquipmentReady = false;
            eviscerate.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            eviscerate.minDistance = 0f;
            eviscerate.maxDistance = 30f;
            eviscerate.selectionRequiresTargetLoS = true;
            eviscerate.activationRequiresTargetLoS = true;
            eviscerate.activationRequiresAimConfirmation = true;
            eviscerate.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            eviscerate.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            eviscerate.ignoreNodeGraph = true;
            eviscerate.noRepeat = false;
            eviscerate.shouldSprint = true;
            eviscerate.shouldFireEquipment = false;
            eviscerate.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Slicing Winds off cooldown
            AISkillDriver slicingWinds = masterObject.AddComponent<AISkillDriver>();
            slicingWinds.skillSlot = SkillSlot.Special;
            slicingWinds.requiredSkill = slicingWindsSkill;
            slicingWinds.requireSkillReady = true;
            slicingWinds.requireEquipmentReady = false;
            slicingWinds.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            slicingWinds.minDistance = 0f;
            slicingWinds.maxDistance = 120f;
            slicingWinds.selectionRequiresTargetLoS = true;
            slicingWinds.activationRequiresTargetLoS = true;
            slicingWinds.activationRequiresAimConfirmation = true;
            slicingWinds.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            slicingWinds.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            slicingWinds.ignoreNodeGraph = false;
            slicingWinds.noRepeat = false;
            slicingWinds.shouldSprint = true;
            slicingWinds.shouldFireEquipment = false;
            slicingWinds.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use secondary on the target off cooldown
            AISkillDriver secondary = masterObject.AddComponent<AISkillDriver>();
            secondary.skillSlot = SkillSlot.Secondary;
            secondary.requireSkillReady = true;
            secondary.requireEquipmentReady = false;
            secondary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            secondary.minDistance = 0f;
            secondary.maxDistance = 10f;
            secondary.selectionRequiresTargetLoS = false;
            secondary.activationRequiresTargetLoS = false;
            secondary.activationRequiresAimConfirmation = true;
            secondary.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            secondary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            secondary.ignoreNodeGraph = true;
            secondary.noRepeat = false;
            secondary.shouldSprint = true;
            secondary.shouldFireEquipment = false;
            secondary.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use primary while retreating (point-blank)
            AISkillDriver primaryFlee = masterObject.AddComponent<AISkillDriver>();
            primaryFlee.skillSlot = SkillSlot.Primary;
            primaryFlee.requireSkillReady = false;
            primaryFlee.requireEquipmentReady = false;
            primaryFlee.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryFlee.minDistance = 0f;
            primaryFlee.maxDistance = 5f;
            primaryFlee.selectionRequiresTargetLoS = false;
            primaryFlee.activationRequiresTargetLoS = false;
            primaryFlee.activationRequiresAimConfirmation = false;
            primaryFlee.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            primaryFlee.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryFlee.ignoreNodeGraph = false;
            primaryFlee.noRepeat = false;
            primaryFlee.shouldSprint = true;
            primaryFlee.shouldFireEquipment = false;
            primaryFlee.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryFlee.driverUpdateTimerOverride = .25f;


            // Use primary while strafing (very close)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requireSkillReady = false;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 10f;
            primaryStrafe.selectionRequiresTargetLoS = false;
            primaryStrafe.activationRequiresTargetLoS = false;
            primaryStrafe.activationRequiresAimConfirmation = false;
            primaryStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primaryStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryStrafe.ignoreNodeGraph = false;
            primaryStrafe.noRepeat = false;
            primaryStrafe.shouldSprint = true;
            primaryStrafe.shouldFireEquipment = false;
            primaryStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryStrafe.driverUpdateTimerOverride = .25f;


            // Use primary while approaching (close)
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requireSkillReady = false;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 15f;
            primaryChase.selectionRequiresTargetLoS = false;
            primaryChase.activationRequiresTargetLoS = false;
            primaryChase.activationRequiresAimConfirmation = false;
            primaryChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            primaryChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryChase.ignoreNodeGraph = true;
            primaryChase.noRepeat = false;
            primaryChase.shouldSprint = true;
            primaryChase.shouldFireEquipment = false;
            primaryChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryChase.driverUpdateTimerOverride = .25f;


            // Use utility off cooldown to path
            AISkillDriver utilityFar = masterObject.AddComponent<AISkillDriver>();
            utilityFar.skillSlot = SkillSlot.Utility;
            utilityFar.requireSkillReady = true;
            utilityFar.requireEquipmentReady = false;
            utilityFar.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utilityFar.minDistance = 115f;
            utilityFar.maxDistance = float.PositiveInfinity;
            utilityFar.selectionRequiresTargetLoS = false;
            utilityFar.activationRequiresTargetLoS = false;
            utilityFar.activationRequiresAimConfirmation = false;
            utilityFar.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityFar.aimType = AISkillDriver.AimType.MoveDirection;
            utilityFar.ignoreNodeGraph = false;
            utilityFar.noRepeat = true;
            utilityFar.shouldSprint = true;
            utilityFar.shouldFireEquipment = false;
            utilityFar.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            utilityFar.resetCurrentEnemyOnNextDriverSelection = true;
            utilityFar.selectionRequiresOnGround = true;
            utilityFar.driverUpdateTimerOverride = .5f;


            // Use Eviscerate off cooldown to path
            AISkillDriver eviscerateFar = masterObject.AddComponent<AISkillDriver>();
            eviscerateFar.skillSlot = SkillSlot.Special;
            eviscerateFar.requiredSkill = eviscerateSkill;
            eviscerateFar.requireSkillReady = true;
            eviscerateFar.requireEquipmentReady = false;
            eviscerateFar.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            eviscerateFar.minDistance = 90f;
            eviscerateFar.maxDistance = float.PositiveInfinity;
            eviscerateFar.selectionRequiresTargetLoS = false;
            eviscerateFar.activationRequiresTargetLoS = false;
            eviscerateFar.activationRequiresAimConfirmation = false;
            eviscerateFar.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            eviscerateFar.aimType = AISkillDriver.AimType.MoveDirection;
            eviscerateFar.ignoreNodeGraph = false;
            eviscerateFar.noRepeat = true;
            eviscerateFar.shouldSprint = true;
            eviscerateFar.shouldFireEquipment = false;
            eviscerateFar.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            eviscerateFar.resetCurrentEnemyOnNextDriverSelection = true;
            eviscerateFar.selectionRequiresOnGround = true;
            eviscerateFar.driverUpdateTimerOverride = .5f;


            // Occasional strafing
            AISkillDriver strafing = masterObject.AddComponent<AISkillDriver>();
            strafing.skillSlot = SkillSlot.None;
            strafing.requireSkillReady = false;
            strafing.requireEquipmentReady = false;
            strafing.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            strafing.minDistance = 30f;
            strafing.maxDistance = 60f;
            strafing.selectionRequiresTargetLoS = true;
            strafing.activationRequiresTargetLoS = true;
            strafing.activationRequiresAimConfirmation = false;
            strafing.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            strafing.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            strafing.ignoreNodeGraph = false;
            strafing.noRepeat = true;
            strafing.shouldSprint = true;
            strafing.shouldFireEquipment = false;
            strafing.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            strafing.resetCurrentEnemyOnNextDriverSelection = true;
            strafing.driverUpdateTimerOverride = 1f;


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
            sprintChase.driverUpdateTimerOverride = .5f;


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


            // Force Mercenary to delay his Utility a little bit
            AISkillDriver forceDelay = masterObject.AddComponent<AISkillDriver>();
            forceDelay.skillSlot = SkillSlot.None;
            forceDelay.requireSkillReady = false;
            forceDelay.requireEquipmentReady = false;
            forceDelay.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            forceDelay.minDistance = 0f;
            forceDelay.maxDistance = float.PositiveInfinity;
            forceDelay.selectionRequiresTargetLoS = false;
            forceDelay.activationRequiresTargetLoS = false;
            forceDelay.activationRequiresAimConfirmation = false;
            forceDelay.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            forceDelay.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            forceDelay.ignoreNodeGraph = false;
            forceDelay.noRepeat = true;
            forceDelay.shouldSprint = true;
            forceDelay.shouldFireEquipment = false;
            forceDelay.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            forceDelay.driverUpdateTimerOverride = .6f;


            // Overrides
            utility.nextHighPriorityOverride = forceDelay;
        }
    }
}
