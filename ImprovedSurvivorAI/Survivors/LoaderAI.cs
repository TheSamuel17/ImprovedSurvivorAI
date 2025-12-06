using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class LoaderAI
    {
        // Asset references
        public static SteppedSkillDef chargePunchSkill = Addressables.LoadAssetAsync<SteppedSkillDef>("RoR2/Base/Loader/ChargeFist.asset").WaitForCompletion();
        public static SteppedSkillDef thunderPunchSkill = Addressables.LoadAssetAsync<SteppedSkillDef>("RoR2/Base/Loader/ChargeZapFist.asset").WaitForCompletion();
        public static SkillDef pylonSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Loader/ThrowPylon.asset").WaitForCompletion();
        public static SteppedSkillDef slamSkill = Addressables.LoadAssetAsync<SteppedSkillDef>("RoR2/Base/Loader/GroundSlam.asset").WaitForCompletion();

        public LoaderAI(GameObject masterObject)
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


            // Use Thunderslam off cooldown
            AISkillDriver thunderslam = masterObject.AddComponent<AISkillDriver>();
            thunderslam.skillSlot = SkillSlot.Special;
            thunderslam.requiredSkill = slamSkill;
            thunderslam.requireSkillReady = true;
            thunderslam.requireEquipmentReady = false;
            thunderslam.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            thunderslam.minDistance = 0f;
            thunderslam.maxDistance = 10f;
            thunderslam.selectionRequiresTargetLoS = false;
            thunderslam.activationRequiresTargetLoS = false;
            thunderslam.activationRequiresAimConfirmation = false;
            thunderslam.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            thunderslam.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            thunderslam.ignoreNodeGraph = true;
            thunderslam.noRepeat = true;
            thunderslam.shouldSprint = false;
            thunderslam.shouldFireEquipment = false;
            thunderslam.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            thunderslam.selectionRequiresOnGround = true;


            // Use Pylon off cooldown
            AISkillDriver pylon = masterObject.AddComponent<AISkillDriver>();
            pylon.skillSlot = SkillSlot.Special;
            pylon.requiredSkill = pylonSkill;
            pylon.requireSkillReady = true;
            pylon.requireEquipmentReady = false;
            pylon.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            pylon.minDistance = 20f;
            pylon.maxDistance = 80f;
            pylon.selectionRequiresTargetLoS = true;
            pylon.activationRequiresTargetLoS = true;
            pylon.activationRequiresAimConfirmation = true;
            pylon.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            pylon.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            pylon.ignoreNodeGraph = false;
            pylon.noRepeat = true;
            pylon.shouldSprint = false;
            pylon.shouldFireEquipment = false;
            pylon.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use secondary off cooldown
            AISkillDriver grapple = masterObject.AddComponent<AISkillDriver>();
            grapple.skillSlot = SkillSlot.Secondary;
            grapple.requireSkillReady = true;
            grapple.requireEquipmentReady = false;
            grapple.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            grapple.minDistance = 20f;
            grapple.maxDistance = 80f;
            grapple.selectionRequiresTargetLoS = true;
            grapple.activationRequiresTargetLoS = true;
            grapple.activationRequiresAimConfirmation = true;
            grapple.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            grapple.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            grapple.ignoreNodeGraph = false;
            grapple.noRepeat = true;
            grapple.shouldSprint = true;
            grapple.shouldFireEquipment = false;
            grapple.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            grapple.driverUpdateTimerOverride = 1f;


            // Use Charged Gauntlet off cooldown
            AISkillDriver chargePunch = masterObject.AddComponent<AISkillDriver>();
            chargePunch.skillSlot = SkillSlot.Utility;
            chargePunch.requiredSkill = chargePunchSkill;
            chargePunch.requireSkillReady = true;
            chargePunch.requireEquipmentReady = false;
            chargePunch.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            chargePunch.minDistance = 5f;
            chargePunch.maxDistance = 50f;
            chargePunch.selectionRequiresTargetLoS = true;
            chargePunch.activationRequiresTargetLoS = true;
            chargePunch.activationRequiresAimConfirmation = false;
            chargePunch.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            chargePunch.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            chargePunch.ignoreNodeGraph = false;
            chargePunch.noRepeat = true;
            chargePunch.shouldSprint = true;
            chargePunch.shouldFireEquipment = false;
            chargePunch.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            chargePunch.driverUpdateTimerOverride = 2.5f;


            // Use Thunder Gauntlet off cooldown
            AISkillDriver thunderPunch = masterObject.AddComponent<AISkillDriver>();
            thunderPunch.skillSlot = SkillSlot.Utility;
            thunderPunch.requiredSkill = thunderPunchSkill;
            thunderPunch.requireSkillReady = true;
            thunderPunch.requireEquipmentReady = false;
            thunderPunch.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            thunderPunch.minDistance = 0f;
            thunderPunch.maxDistance = 35f;
            thunderPunch.selectionRequiresTargetLoS = true;
            thunderPunch.activationRequiresTargetLoS = true;
            thunderPunch.activationRequiresAimConfirmation = true;
            thunderPunch.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            thunderPunch.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            thunderPunch.ignoreNodeGraph = false;
            thunderPunch.noRepeat = true;
            thunderPunch.shouldSprint = true;
            thunderPunch.shouldFireEquipment = false;
            thunderPunch.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            thunderPunch.driverUpdateTimerOverride = .5f;


            // Use primary while strafing
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 5f;
            primaryStrafe.selectionRequiresTargetLoS = false;
            primaryStrafe.activationRequiresTargetLoS = false;
            primaryStrafe.activationRequiresAimConfirmation = false;
            primaryStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primaryStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryStrafe.ignoreNodeGraph = true;
            primaryStrafe.noRepeat = false;
            primaryStrafe.shouldSprint = false;
            primaryStrafe.shouldFireEquipment = false;
            primaryStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryStrafe.driverUpdateTimerOverride = .5f;


            // Use primary while approaching
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requireSkillReady = true;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 10f;
            primaryChase.selectionRequiresTargetLoS = false;
            primaryChase.activationRequiresTargetLoS = false;
            primaryChase.activationRequiresAimConfirmation = false;
            primaryChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            primaryChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryChase.ignoreNodeGraph = true;
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


            // Overrides
            pylon.nextHighPriorityOverride = grapple;
        }
    }
}
