using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class HereticAI
    {
        // Asset references
        public static LunarPrimaryReplacementSkill hungeringGazeSkill = Addressables.LoadAssetAsync<LunarPrimaryReplacementSkill>("RoR2/Base/LunarSkillReplacements/LunarPrimaryReplacement.asset").WaitForCompletion();
        public static LunarSecondaryReplacementSkill maelstromSkill = Addressables.LoadAssetAsync<LunarSecondaryReplacementSkill>("RoR2/Base/LunarSkillReplacements/LunarSecondaryReplacement.asset").WaitForCompletion();
        public static SkillDef shadowfadeSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/LunarSkillReplacements/LunarUtilityReplacement.asset").WaitForCompletion();
        public static LunarDetonatorSkill ruinSkill = Addressables.LoadAssetAsync<LunarDetonatorSkill>("RoR2/Base/LunarSkillReplacements/LunarDetonatorSpecialReplacement.asset").WaitForCompletion();

        public HereticAI(GameObject masterObject)
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


            // Shadowfade off cooldown
            AISkillDriver shadowfade = masterObject.AddComponent<AISkillDriver>();
            shadowfade.skillSlot = SkillSlot.Utility;
            shadowfade.requiredSkill = shadowfadeSkill;
            shadowfade.requireSkillReady = true;
            shadowfade.requireEquipmentReady = false;
            shadowfade.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            shadowfade.minDistance = 0f;
            shadowfade.maxDistance = float.PositiveInfinity;
            shadowfade.selectionRequiresTargetLoS = false;
            shadowfade.activationRequiresTargetLoS = false;
            shadowfade.activationRequiresAimConfirmation = false;
            //shadowfade.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            shadowfade.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            shadowfade.ignoreNodeGraph = false;
            shadowfade.noRepeat = true;
            shadowfade.shouldSprint = true;
            shadowfade.shouldFireEquipment = false;
            shadowfade.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            shadowfade.maxUserHealthFraction = .85f;


            // Slicing Maelstrom (no charge)
            AISkillDriver maelstromCloser = masterObject.AddComponent<AISkillDriver>();
            maelstromCloser.skillSlot = SkillSlot.Secondary;
            maelstromCloser.requiredSkill = maelstromSkill;
            maelstromCloser.requireSkillReady = true;
            maelstromCloser.requireEquipmentReady = false;
            maelstromCloser.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            maelstromCloser.minDistance = 0f;
            maelstromCloser.maxDistance = 20f;
            maelstromCloser.selectionRequiresTargetLoS = true;
            maelstromCloser.activationRequiresTargetLoS = true;
            maelstromCloser.activationRequiresAimConfirmation = true;
            maelstromCloser.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            maelstromCloser.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            maelstromCloser.ignoreNodeGraph = false;
            maelstromCloser.noRepeat = false;
            maelstromCloser.shouldSprint = false;
            maelstromCloser.shouldFireEquipment = false;
            maelstromCloser.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Slicing Maelstrom (25% charge)
            AISkillDriver maelstromClose = masterObject.AddComponent<AISkillDriver>();
            maelstromClose.skillSlot = SkillSlot.Secondary;
            maelstromClose.requiredSkill = maelstromSkill;
            maelstromClose.requireSkillReady = true;
            maelstromClose.requireEquipmentReady = false;
            maelstromClose.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            maelstromClose.minDistance = 20f;
            maelstromClose.maxDistance = 35f;
            maelstromClose.selectionRequiresTargetLoS = true;
            maelstromClose.activationRequiresTargetLoS = true;
            maelstromClose.activationRequiresAimConfirmation = true;
            maelstromClose.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            maelstromClose.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            maelstromClose.ignoreNodeGraph = false;
            maelstromClose.noRepeat = false;
            maelstromClose.shouldSprint = false;
            maelstromClose.shouldFireEquipment = false;
            maelstromClose.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            maelstromClose.driverUpdateTimerOverride = .5f;


            // Slicing Maelstrom (50% charge)
            AISkillDriver maelstromMedium = masterObject.AddComponent<AISkillDriver>();
            maelstromMedium.skillSlot = SkillSlot.Secondary;
            maelstromMedium.requiredSkill = maelstromSkill;
            maelstromMedium.requireSkillReady = true;
            maelstromMedium.requireEquipmentReady = false;
            maelstromMedium.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            maelstromMedium.minDistance = 35f;
            maelstromMedium.maxDistance = 50f;
            maelstromMedium.selectionRequiresTargetLoS = true;
            maelstromMedium.activationRequiresTargetLoS = true;
            maelstromMedium.activationRequiresAimConfirmation = true;
            maelstromMedium.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            maelstromMedium.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            maelstromMedium.ignoreNodeGraph = false;
            maelstromMedium.noRepeat = false;
            maelstromMedium.shouldSprint = false;
            maelstromMedium.shouldFireEquipment = false;
            maelstromMedium.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            maelstromMedium.driverUpdateTimerOverride = 1f;


            // Slicing Maelstrom (75% charge)
            AISkillDriver maelstromFar = masterObject.AddComponent<AISkillDriver>();
            maelstromFar.skillSlot = SkillSlot.Secondary;
            maelstromFar.requiredSkill = maelstromSkill;
            maelstromFar.requireSkillReady = true;
            maelstromFar.requireEquipmentReady = false;
            maelstromFar.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            maelstromFar.minDistance = 50f;
            maelstromFar.maxDistance = 65f;
            maelstromFar.selectionRequiresTargetLoS = true;
            maelstromFar.activationRequiresTargetLoS = true;
            maelstromFar.activationRequiresAimConfirmation = true;
            maelstromFar.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            maelstromFar.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            maelstromFar.ignoreNodeGraph = false;
            maelstromFar.noRepeat = false;
            maelstromFar.shouldSprint = false;
            maelstromFar.shouldFireEquipment = false;
            maelstromFar.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            maelstromFar.driverUpdateTimerOverride = 1.5f;


            // Slicing Maelstrom (100% charge)
            AISkillDriver maelstromFarther = masterObject.AddComponent<AISkillDriver>();
            maelstromFarther.skillSlot = SkillSlot.Secondary;
            maelstromFarther.requiredSkill = maelstromSkill;
            maelstromFarther.requireSkillReady = true;
            maelstromFarther.requireEquipmentReady = false;
            maelstromFarther.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            maelstromFarther.minDistance = 65f;
            maelstromFarther.maxDistance = 85f;
            maelstromFarther.selectionRequiresTargetLoS = true;
            maelstromFarther.activationRequiresTargetLoS = true;
            maelstromFarther.activationRequiresAimConfirmation = true;
            maelstromFarther.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            maelstromFarther.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            maelstromFarther.ignoreNodeGraph = false;
            maelstromFarther.noRepeat = false;
            maelstromFarther.shouldSprint = false;
            maelstromFarther.shouldFireEquipment = false;
            maelstromFarther.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            maelstromFarther.driverUpdateTimerOverride = 2f;


            // Shoot primary while retreating (point-blank)
            AISkillDriver primaryRetreat = masterObject.AddComponent<AISkillDriver>();
            primaryRetreat.skillSlot = SkillSlot.Primary;
            primaryRetreat.requiredSkill = hungeringGazeSkill;
            primaryRetreat.requireSkillReady = true;
            primaryRetreat.requireEquipmentReady = false;
            primaryRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryRetreat.minDistance = 0f;
            primaryRetreat.maxDistance = 20f;
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


            // Shoot primary while strafing (close)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requiredSkill = hungeringGazeSkill;
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


            // Shoot primary while approaching (far)
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requiredSkill = hungeringGazeSkill;
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


            // Use Ruin against sufficiently injured targets (hopefully with enough stacks of Ruin)
            AISkillDriver ruin = masterObject.AddComponent<AISkillDriver>();
            ruin.skillSlot = SkillSlot.Special;
            ruin.requiredSkill = ruinSkill;
            ruin.requireSkillReady = true;
            ruin.requireEquipmentReady = false;
            ruin.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            ruin.minDistance = 0f;
            ruin.maxDistance = float.PositiveInfinity;
            ruin.selectionRequiresTargetLoS = false;
            ruin.activationRequiresTargetLoS = false;
            ruin.activationRequiresAimConfirmation = false;
            //ruin.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            ruin.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            ruin.ignoreNodeGraph = false;
            ruin.noRepeat = true;
            ruin.shouldSprint = false;
            ruin.shouldFireEquipment = false;
            ruin.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            ruin.maxTargetHealthFraction = .5f;
            ruin.minTargetHealthFraction = .01f;


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


            // Shadowfade off cooldown (owner version)
            AISkillDriver shadowfadeOwner = masterObject.AddComponent<AISkillDriver>();
            shadowfadeOwner.skillSlot = SkillSlot.Utility;
            shadowfadeOwner.requiredSkill = shadowfadeSkill;
            shadowfadeOwner.requireSkillReady = true;
            shadowfadeOwner.requireEquipmentReady = false;
            shadowfadeOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            shadowfadeOwner.minDistance = 0f;
            shadowfadeOwner.maxDistance = float.PositiveInfinity;
            shadowfadeOwner.selectionRequiresTargetLoS = false;
            shadowfadeOwner.activationRequiresTargetLoS = false;
            shadowfadeOwner.activationRequiresAimConfirmation = false;
            //shadowfadeOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            shadowfadeOwner.aimType = AISkillDriver.AimType.AtCurrentLeader;
            shadowfadeOwner.ignoreNodeGraph = false;
            shadowfadeOwner.noRepeat = true;
            shadowfadeOwner.shouldSprint = true;
            shadowfadeOwner.shouldFireEquipment = false;
            shadowfadeOwner.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            shadowfadeOwner.maxUserHealthFraction = .9f;


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


            // Release inputs
            AISkillDriver releaseInput = masterObject.AddComponent<AISkillDriver>();
            releaseInput.skillSlot = SkillSlot.None;
            releaseInput.requireSkillReady = false;
            releaseInput.requireEquipmentReady = false;
            releaseInput.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            releaseInput.minDistance = 0f;
            releaseInput.maxDistance = float.PositiveInfinity;
            releaseInput.selectionRequiresTargetLoS = false;
            releaseInput.activationRequiresTargetLoS = false;
            releaseInput.activationRequiresAimConfirmation = false;
            releaseInput.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            releaseInput.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            releaseInput.ignoreNodeGraph = false;
            releaseInput.noRepeat = false;
            releaseInput.shouldSprint = false;
            releaseInput.shouldFireEquipment = false;
            releaseInput.buttonPressType = AISkillDriver.ButtonPressType.Abstain;


            // Overrides
            maelstromCloser.nextHighPriorityOverride = releaseInput;
            maelstromClose.nextHighPriorityOverride = releaseInput;
            maelstromMedium.nextHighPriorityOverride = releaseInput;
            maelstromFar.nextHighPriorityOverride = releaseInput;
            maelstromFarther.nextHighPriorityOverride = releaseInput;
        }
    }
}
