using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class BanditAI
    {
        // Asset references
        public static SkillDef serratedDaggerSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Bandit2/SlashBlade.asset").WaitForCompletion();
        public static SkillDef serratedShivSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Bandit2/Bandit2SerratedShivs.asset").WaitForCompletion();

        public BanditAI(GameObject masterObject)
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


            // Use Serrated Dagger off cooldown when close enough
            AISkillDriver serratedDagger = masterObject.AddComponent<AISkillDriver>();
            serratedDagger.skillSlot = SkillSlot.Secondary;
            serratedDagger.requiredSkill = serratedDaggerSkill;
            serratedDagger.requireSkillReady = true;
            serratedDagger.requireEquipmentReady = false;
            serratedDagger.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            serratedDagger.minDistance = 0f;
            serratedDagger.maxDistance = 5f;
            serratedDagger.selectionRequiresTargetLoS = false;
            serratedDagger.activationRequiresTargetLoS = false;
            serratedDagger.activationRequiresAimConfirmation = true;
            serratedDagger.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            serratedDagger.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            serratedDagger.ignoreNodeGraph = false;
            serratedDagger.noRepeat = false;
            serratedDagger.shouldSprint = true;
            serratedDagger.shouldFireEquipment = false;
            serratedDagger.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Serrated Shiv off cooldown
            AISkillDriver serratedShiv = masterObject.AddComponent<AISkillDriver>();
            serratedShiv.skillSlot = SkillSlot.Secondary;
            serratedShiv.requiredSkill = serratedShivSkill;
            serratedShiv.requireSkillReady = true;
            serratedShiv.requireEquipmentReady = false;
            serratedShiv.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            serratedShiv.minDistance = 0f;
            serratedShiv.maxDistance = 60f;
            serratedShiv.selectionRequiresTargetLoS = true;
            serratedShiv.activationRequiresTargetLoS = true;
            serratedShiv.activationRequiresAimConfirmation = true;
            serratedShiv.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            serratedShiv.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            serratedShiv.ignoreNodeGraph = false;
            serratedShiv.noRepeat = false;
            serratedShiv.shouldSprint = true;
            serratedShiv.shouldFireEquipment = false;
            serratedShiv.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use revolver off cooldown
            AISkillDriver revolver = masterObject.AddComponent<AISkillDriver>();
            revolver.skillSlot = SkillSlot.Special;
            revolver.requireSkillReady = true;
            revolver.requireEquipmentReady = false;
            revolver.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            revolver.minDistance = 0f;
            revolver.maxDistance = 150f;
            revolver.selectionRequiresTargetLoS = true;
            revolver.activationRequiresTargetLoS = true;
            revolver.activationRequiresAimConfirmation = true;
            //revolver.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            revolver.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            revolver.ignoreNodeGraph = false;
            revolver.noRepeat = false;
            revolver.shouldSprint = false;
            revolver.shouldFireEquipment = false;
            revolver.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            revolver.driverUpdateTimerOverride = 0.7f;


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
            primaryRetreat.activationRequiresAimConfirmation = true;
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
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 30f;
            primaryStrafe.selectionRequiresTargetLoS = true;
            primaryStrafe.activationRequiresTargetLoS = true;
            primaryStrafe.activationRequiresAimConfirmation = true;
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
            primaryChase.requireSkillReady = true;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 120f;
            primaryChase.selectionRequiresTargetLoS = true;
            primaryChase.activationRequiresTargetLoS = true;
            primaryChase.activationRequiresAimConfirmation = true;
            primaryChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            primaryChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryChase.ignoreNodeGraph = false;
            primaryChase.noRepeat = false;
            primaryChase.shouldSprint = false;
            primaryChase.shouldFireEquipment = false;
            primaryChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Use utility off cooldown (closer)
            AISkillDriver utilityCloser = masterObject.AddComponent<AISkillDriver>();
            utilityCloser.skillSlot = SkillSlot.Utility;
            utilityCloser.requireSkillReady = true;
            utilityCloser.requireEquipmentReady = false;
            utilityCloser.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utilityCloser.minDistance = 0f;
            utilityCloser.maxDistance = 15f;
            utilityCloser.selectionRequiresTargetLoS = false;
            utilityCloser.activationRequiresTargetLoS = false;
            utilityCloser.activationRequiresAimConfirmation = false;
            utilityCloser.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityCloser.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utilityCloser.ignoreNodeGraph = false;
            utilityCloser.noRepeat = false;
            utilityCloser.shouldSprint = true;
            utilityCloser.shouldFireEquipment = false;
            utilityCloser.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            utilityCloser.selectionRequiresOnGround = true;
            utilityCloser.driverUpdateTimerOverride = 1f;
            utilityCloser.resetCurrentEnemyOnNextDriverSelection = true;


            // Use utility off cooldown (close)
            AISkillDriver utilityClose = masterObject.AddComponent<AISkillDriver>();
            utilityClose.skillSlot = SkillSlot.Utility;
            utilityClose.requireSkillReady = true;
            utilityClose.requireEquipmentReady = false;
            utilityClose.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utilityClose.minDistance = 0f;
            utilityClose.maxDistance = 30f;
            utilityClose.selectionRequiresTargetLoS = false;
            utilityClose.activationRequiresTargetLoS = false;
            utilityClose.activationRequiresAimConfirmation = false;
            utilityClose.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityClose.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utilityClose.ignoreNodeGraph = false;
            utilityClose.noRepeat = false;
            utilityClose.shouldSprint = true;
            utilityClose.shouldFireEquipment = false;
            utilityClose.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            utilityClose.selectionRequiresOnGround = true;
            utilityClose.driverUpdateTimerOverride = 2f;
            utilityClose.resetCurrentEnemyOnNextDriverSelection = true;


            // Use utility off cooldown
            AISkillDriver utility = masterObject.AddComponent<AISkillDriver>();
            utility.skillSlot = SkillSlot.Utility;
            utility.requireSkillReady = true;
            utility.requireEquipmentReady = false;
            utility.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utility.minDistance = 0f;
            utility.maxDistance = 400f;
            utility.selectionRequiresTargetLoS = false;
            utility.activationRequiresTargetLoS = false;
            utility.activationRequiresAimConfirmation = false;
            utility.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utility.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utility.ignoreNodeGraph = false;
            utility.noRepeat = false;
            utility.shouldSprint = true;
            utility.shouldFireEquipment = false;
            utility.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            utility.selectionRequiresOnGround = true;
            utility.driverUpdateTimerOverride = 3f;
            utility.resetCurrentEnemyOnNextDriverSelection = true;


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


            // Use utility off cooldown to follow the owner
            AISkillDriver utilityOwner = masterObject.AddComponent<AISkillDriver>();
            utilityOwner.skillSlot = SkillSlot.Utility;
            utilityOwner.requireSkillReady = true;
            utilityOwner.requireEquipmentReady = false;
            utilityOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            utilityOwner.minDistance = 40f;
            utilityOwner.maxDistance = float.PositiveInfinity;
            utilityOwner.selectionRequiresTargetLoS = false;
            utilityOwner.activationRequiresTargetLoS = false;
            utilityOwner.activationRequiresAimConfirmation = false;
            utilityOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityOwner.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utilityOwner.ignoreNodeGraph = false;
            utilityOwner.noRepeat = true;
            utilityOwner.shouldSprint = true;
            utilityOwner.shouldFireEquipment = false;
            utilityOwner.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            utilityOwner.selectionRequiresOnGround = true;
            utilityOwner.resetCurrentEnemyOnNextDriverSelection = true;


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


            // Use utility off cooldown
            AISkillDriver utilityLowPriority = masterObject.AddComponent<AISkillDriver>();
            utilityLowPriority.skillSlot = SkillSlot.Utility;
            utilityLowPriority.requireSkillReady = true;
            utilityLowPriority.requireEquipmentReady = false;
            utilityLowPriority.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utilityLowPriority.minDistance = 0f;
            utilityLowPriority.maxDistance = float.PositiveInfinity;
            utilityLowPriority.selectionRequiresTargetLoS = false;
            utilityLowPriority.activationRequiresTargetLoS = false;
            utilityLowPriority.activationRequiresAimConfirmation = false;
            utilityLowPriority.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityLowPriority.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utilityLowPriority.ignoreNodeGraph = false;
            utilityLowPriority.noRepeat = false;
            utilityLowPriority.shouldSprint = true;
            utilityLowPriority.shouldFireEquipment = false;
            utilityLowPriority.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            utilityLowPriority.selectionRequiresOnGround = true;
            utilityLowPriority.driverUpdateTimerOverride = 3f;
            utilityLowPriority.resetCurrentEnemyOnNextDriverSelection = true;


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


            // Force Bandit to delay his shots a little bit
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
            forceDelay.shouldSprint = false;
            forceDelay.shouldFireEquipment = false;
            forceDelay.buttonPressType = AISkillDriver.ButtonPressType.Abstain;


            // Overrides
            primaryRetreat.nextHighPriorityOverride = forceDelay;
            primaryStrafe.nextHighPriorityOverride = forceDelay;
            primaryChase.nextHighPriorityOverride = forceDelay;
        }
    }
}
