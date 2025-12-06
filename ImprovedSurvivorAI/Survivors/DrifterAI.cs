using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class DrifterAI
    {
        // Asset references
        public static DrifterSkillDef cleanupSkill = Addressables.LoadAssetAsync<DrifterSkillDef>("RoR2/DLC3/Drifter/Cleanup.asset").WaitForCompletion();
        public static DrifterSkillDef cubeSkill = Addressables.LoadAssetAsync<DrifterSkillDef>("RoR2/DLC3/Drifter/JunkCube.asset").WaitForCompletion();
        public static SkillDef repossessSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drifter/Repossess.asset").WaitForCompletion();
        public static SkillDef repossessSlamSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drifter/SuffocateSlam.asset").WaitForCompletion();
        public static SkillDef repossessThrowSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drifter/EmptyBag.asset").WaitForCompletion();
        public static SkillDef tornadoSlamSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drifter/TornadoSlam.asset").WaitForCompletion();
        public static SkillDef tornadoSlamEndSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drifter/BluntForceTornado.asset").WaitForCompletion();
        public static DrifterSkillDef salvageSkill = Addressables.LoadAssetAsync<DrifterSkillDef>("RoR2/DLC3/Drifter/Salvage.asset").WaitForCompletion();

        public DrifterAI(GameObject masterObject)
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


            // Use Blunt Force on a captured target
            AISkillDriver repossessSlam = masterObject.AddComponent<AISkillDriver>();
            repossessSlam.skillSlot = SkillSlot.Primary;
            repossessSlam.requiredSkill = repossessSlamSkill;
            repossessSlam.requireSkillReady = true;
            repossessSlam.requireEquipmentReady = false;
            repossessSlam.moveTargetType = AISkillDriver.TargetType.Custom;
            repossessSlam.minDistance = 0f;
            repossessSlam.maxDistance = float.PositiveInfinity;
            repossessSlam.selectionRequiresTargetLoS = false;
            repossessSlam.activationRequiresTargetLoS = false;
            repossessSlam.activationRequiresAimConfirmation = false;
            repossessSlam.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            repossessSlam.aimType = AISkillDriver.AimType.AtMoveTarget;
            repossessSlam.ignoreNodeGraph = false;
            repossessSlam.noRepeat = false;
            repossessSlam.shouldSprint = false;
            repossessSlam.shouldFireEquipment = false;
            repossessSlam.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            repossessSlam.driverUpdateTimerOverride = 1f;


            // Throw out a captured target
            AISkillDriver repossessThrow = masterObject.AddComponent<AISkillDriver>();
            repossessThrow.skillSlot = SkillSlot.Utility;
            repossessThrow.requiredSkill = repossessThrowSkill;
            repossessThrow.requireSkillReady = true;
            repossessThrow.requireEquipmentReady = false;
            repossessThrow.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            repossessThrow.minDistance = 10f;
            repossessThrow.maxDistance = 40f;
            repossessThrow.selectionRequiresTargetLoS = true;
            repossessThrow.activationRequiresTargetLoS = true;
            repossessThrow.activationRequiresAimConfirmation = true;
            repossessThrow.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            repossessThrow.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            repossessThrow.ignoreNodeGraph = false;
            repossessThrow.noRepeat = true;
            repossessThrow.shouldSprint = false;
            repossessThrow.shouldFireEquipment = false;
            repossessThrow.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            repossessThrow.driverUpdateTimerOverride = .1f;


            // Use primary to end Tornado Slam
            AISkillDriver tornadoSlamEnd = masterObject.AddComponent<AISkillDriver>();
            tornadoSlamEnd.skillSlot = SkillSlot.Primary;
            tornadoSlamEnd.requiredSkill = tornadoSlamEndSkill;
            tornadoSlamEnd.requireSkillReady = true;
            tornadoSlamEnd.requireEquipmentReady = false;
            tornadoSlamEnd.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            tornadoSlamEnd.minDistance = 0f;
            tornadoSlamEnd.maxDistance = 5f;
            tornadoSlamEnd.selectionRequiresTargetLoS = false;
            tornadoSlamEnd.activationRequiresTargetLoS = false;
            tornadoSlamEnd.activationRequiresAimConfirmation = false;
            tornadoSlamEnd.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            tornadoSlamEnd.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            tornadoSlamEnd.ignoreNodeGraph = true;
            tornadoSlamEnd.noRepeat = false;
            tornadoSlamEnd.shouldSprint = false;
            tornadoSlamEnd.shouldFireEquipment = false;
            tornadoSlamEnd.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Salvage off cooldown (higher priority)
            AISkillDriver salvage = masterObject.AddComponent<AISkillDriver>();
            salvage.skillSlot = SkillSlot.Special;
            salvage.requiredSkill = salvageSkill;
            salvage.requireSkillReady = true;
            salvage.requireEquipmentReady = false;
            salvage.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            salvage.minDistance = 0f;
            salvage.maxDistance = float.PositiveInfinity;
            salvage.selectionRequiresTargetLoS = false;
            salvage.activationRequiresTargetLoS = false;
            salvage.activationRequiresAimConfirmation = false;
            salvage.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            salvage.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            salvage.ignoreNodeGraph = false;
            salvage.noRepeat = false;
            salvage.shouldSprint = false;
            salvage.shouldFireEquipment = false;
            salvage.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            salvage.driverUpdateTimerOverride = 1.5f;


            // Use Repossess to capture enemies
            AISkillDriver repossess = masterObject.AddComponent<AISkillDriver>();
            repossess.skillSlot = SkillSlot.Utility;
            repossess.requiredSkill = repossessSkill;
            repossess.requireSkillReady = true;
            repossess.requireEquipmentReady = false;
            repossess.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            repossess.minDistance = 0f;
            repossess.maxDistance = 20f;
            repossess.selectionRequiresTargetLoS = true;
            repossess.activationRequiresTargetLoS = true;
            repossess.activationRequiresAimConfirmation = true;
            repossess.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            repossess.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            repossess.ignoreNodeGraph = false;
            repossess.noRepeat = false;
            repossess.shouldSprint = false;
            repossess.shouldFireEquipment = false;
            repossess.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            repossess.driverUpdateTimerOverride = .5f;


            // Use Tornado Slam to close in
            AISkillDriver tornadoSlam = masterObject.AddComponent<AISkillDriver>();
            tornadoSlam.skillSlot = SkillSlot.Utility;
            tornadoSlam.requiredSkill = tornadoSlamSkill;
            tornadoSlam.requireSkillReady = true;
            tornadoSlam.requireEquipmentReady = false;
            tornadoSlam.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            tornadoSlam.minDistance = 20f;
            tornadoSlam.maxDistance = 100f;
            tornadoSlam.selectionRequiresTargetLoS = true;
            tornadoSlam.activationRequiresTargetLoS = true;
            tornadoSlam.activationRequiresAimConfirmation = true;
            tornadoSlam.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            tornadoSlam.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            tornadoSlam.ignoreNodeGraph = true;
            tornadoSlam.noRepeat = true;
            tornadoSlam.shouldSprint = false;
            tornadoSlam.shouldFireEquipment = false;
            tornadoSlam.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            tornadoSlam.driverUpdateTimerOverride = 1f;


            // Use Cleanup off cooldown
            AISkillDriver cleanup = masterObject.AddComponent<AISkillDriver>();
            cleanup.skillSlot = SkillSlot.Secondary;
            cleanup.requiredSkill = cleanupSkill;
            cleanup.requireSkillReady = true;
            cleanup.requireEquipmentReady = false;
            cleanup.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            cleanup.minDistance = 5f;
            cleanup.maxDistance = 50f;
            cleanup.selectionRequiresTargetLoS = true;
            cleanup.activationRequiresTargetLoS = true;
            cleanup.activationRequiresAimConfirmation = true;
            cleanup.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            cleanup.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            cleanup.ignoreNodeGraph = true;
            cleanup.noRepeat = true;
            cleanup.shouldSprint = false;
            cleanup.shouldFireEquipment = false;
            cleanup.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            cleanup.driverUpdateTimerOverride = .1f;


            // Use Junk Cube off cooldown
            AISkillDriver spawnCube = masterObject.AddComponent<AISkillDriver>();
            spawnCube.skillSlot = SkillSlot.Secondary;
            spawnCube.requiredSkill = cubeSkill;
            spawnCube.requireSkillReady = true;
            spawnCube.requireEquipmentReady = false;
            spawnCube.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            spawnCube.minDistance = 5f;
            spawnCube.maxDistance = 60f;
            spawnCube.selectionRequiresTargetLoS = true;
            spawnCube.activationRequiresTargetLoS = true;
            spawnCube.activationRequiresAimConfirmation = false;
            spawnCube.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            spawnCube.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            spawnCube.ignoreNodeGraph = true;
            spawnCube.noRepeat = true;
            spawnCube.shouldSprint = false;
            spawnCube.shouldFireEquipment = false;
            spawnCube.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            spawnCube.driverUpdateTimerOverride = .5f;


            // Use primary and strafe
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


            // Use primary and chase
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


            // Use Salvage off cooldown (lower priority, if there is a leader to find)
            AISkillDriver salvageOwner = masterObject.AddComponent<AISkillDriver>();
            salvageOwner.skillSlot = SkillSlot.Special;
            salvageOwner.requiredSkill = salvageSkill;
            salvageOwner.requireSkillReady = true;
            salvageOwner.requireEquipmentReady = false;
            salvageOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            salvageOwner.minDistance = 0f;
            salvageOwner.maxDistance = float.PositiveInfinity;
            salvageOwner.selectionRequiresTargetLoS = false;
            salvageOwner.activationRequiresTargetLoS = false;
            salvageOwner.activationRequiresAimConfirmation = false;
            //salvageOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            salvageOwner.aimType = AISkillDriver.AimType.AtCurrentLeader;
            salvageOwner.ignoreNodeGraph = false;
            salvageOwner.noRepeat = false;
            salvageOwner.shouldSprint = false;
            salvageOwner.shouldFireEquipment = false;
            salvageOwner.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Occasional strafing
            AISkillDriver strafing = masterObject.AddComponent<AISkillDriver>();
            strafing.skillSlot = SkillSlot.None;
            strafing.requireSkillReady = false;
            strafing.requireEquipmentReady = false;
            strafing.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            strafing.minDistance = 20f;
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
            strafing.driverUpdateTimerOverride = .5f;


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


            // Sprint towards the target - unlimited range
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


            // Use primary to hit Junk Cube
            AISkillDriver hitCube = masterObject.AddComponent<AISkillDriver>();
            hitCube.skillSlot = SkillSlot.Primary;
            hitCube.requireSkillReady = true;
            hitCube.requireEquipmentReady = false;
            hitCube.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            hitCube.minDistance = 0f;
            hitCube.maxDistance = 60f;
            hitCube.selectionRequiresTargetLoS = true;
            hitCube.activationRequiresTargetLoS = true;
            hitCube.activationRequiresAimConfirmation = false;
            hitCube.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            hitCube.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            hitCube.ignoreNodeGraph = false;
            hitCube.noRepeat = false;
            hitCube.shouldSprint = false;
            hitCube.shouldFireEquipment = false;
            hitCube.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            hitCube.driverUpdateTimerOverride = 1f;


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
            //releaseInput.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            releaseInput.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            releaseInput.ignoreNodeGraph = false;
            releaseInput.noRepeat = false;
            releaseInput.shouldSprint = false;
            releaseInput.shouldFireEquipment = false;
            releaseInput.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            releaseInput.driverUpdateTimerOverride = .4f;


            // Overrides
            cleanup.nextHighPriorityOverride = releaseInput;
            repossessThrow.nextHighPriorityOverride = releaseInput;
            repossess.nextHighPriorityOverride = repossessSlam;
            spawnCube.nextHighPriorityOverride = hitCube;
        }
    }
}
