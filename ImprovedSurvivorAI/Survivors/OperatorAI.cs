using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class OperatorAI
    {
        // Asset references
        public static DroneTechDroneSkillDef commandDroneFallbackSkill = Addressables.LoadAssetAsync<DroneTechDroneSkillDef>("RoR2/DLC3/Drone Tech/Command.asset").WaitForCompletion();
        public static SkillDef commandDocSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drone Tech/CommandStimSkillDef.asset").WaitForCompletion();
        public static SkillDef commandHealingDroneSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drone Tech/CommandHealSkillDef.asset").WaitForCompletion();
        public static SkillDef commandBarrierDroneSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drone Tech/CommandRecharge.asset").WaitForCompletion();
        public static SkillDef commandEmergencyDroneSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drone Tech/CommandHealNova.asset").WaitForCompletion();
        public static DroneTechDroneSkillDef ramDroneSkill = Addressables.LoadAssetAsync<DroneTechDroneSkillDef>("RoR2/DLC3/Drone Tech/CommandHeadbutt.asset").WaitForCompletion();
        public static SkillDef ascentProtocolSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drone Tech/DroneLeap.asset").WaitForCompletion();
        public static SkillDef firewallShield = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC3/Drone Tech/CommandShieldFormation.asset").WaitForCompletion();

        public OperatorAI(GameObject masterObject)
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


            // Use special off cooldown
            AISkillDriver special = masterObject.AddComponent<AISkillDriver>();
            special.skillSlot = SkillSlot.Special;
            special.requireSkillReady = true;
            special.requireEquipmentReady = false;
            special.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            special.minDistance = 0f;
            special.maxDistance = 100f;
            special.selectionRequiresTargetLoS = true;
            special.activationRequiresTargetLoS = true;
            special.activationRequiresAimConfirmation = true;
            special.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            special.aimType = AISkillDriver.AimType.MoveDirection;
            special.ignoreNodeGraph = false;
            special.noRepeat = true;
            special.shouldSprint = true;
            special.shouldFireEquipment = false;
            special.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Ascent Protocol off cooldown
            AISkillDriver ascentProtocol = masterObject.AddComponent<AISkillDriver>();
            ascentProtocol.skillSlot = SkillSlot.Utility;
            ascentProtocol.requiredSkill = ascentProtocolSkill;
            ascentProtocol.requireSkillReady = true;
            ascentProtocol.requireEquipmentReady = false;
            ascentProtocol.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            ascentProtocol.minDistance = 0f;
            ascentProtocol.maxDistance = 100f;
            ascentProtocol.selectionRequiresTargetLoS = true;
            ascentProtocol.activationRequiresTargetLoS = true;
            ascentProtocol.activationRequiresAimConfirmation = false;
            ascentProtocol.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            ascentProtocol.aimType = AISkillDriver.AimType.MoveDirection;
            ascentProtocol.ignoreNodeGraph = false;
            ascentProtocol.noRepeat = true;
            ascentProtocol.shouldSprint = true;
            ascentProtocol.shouldFireEquipment = false;
            ascentProtocol.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use ADMIN-OVERRIDE to command DOC
            AISkillDriver commandDoc = masterObject.AddComponent<AISkillDriver>();
            commandDoc.skillSlot = SkillSlot.Secondary;
            commandDoc.requiredSkill = commandDocSkill;
            commandDoc.requireSkillReady = true;
            commandDoc.requireEquipmentReady = false;
            commandDoc.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            commandDoc.minDistance = 0f;
            commandDoc.maxDistance = 250f;
            commandDoc.selectionRequiresTargetLoS = true;
            commandDoc.activationRequiresTargetLoS = true;
            commandDoc.activationRequiresAimConfirmation = false;
            commandDoc.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            commandDoc.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            commandDoc.ignoreNodeGraph = false;
            commandDoc.noRepeat = false;
            commandDoc.shouldSprint = false;
            commandDoc.shouldFireEquipment = false;
            commandDoc.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use ADMIN-OVERRIDE to command Healing Drone
            AISkillDriver commandHealingDrone = masterObject.AddComponent<AISkillDriver>();
            commandHealingDrone.skillSlot = SkillSlot.Secondary;
            commandHealingDrone.requiredSkill = commandHealingDroneSkill;
            commandHealingDrone.requireSkillReady = true;
            commandHealingDrone.requireEquipmentReady = false;
            commandHealingDrone.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            commandHealingDrone.minDistance = 0f;
            commandHealingDrone.maxDistance = 250f;
            commandHealingDrone.selectionRequiresTargetLoS = true;
            commandHealingDrone.activationRequiresTargetLoS = true;
            commandHealingDrone.activationRequiresAimConfirmation = false;
            commandHealingDrone.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            commandHealingDrone.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            commandHealingDrone.ignoreNodeGraph = false;
            commandHealingDrone.noRepeat = false;
            commandHealingDrone.shouldSprint = false;
            commandHealingDrone.shouldFireEquipment = false;
            commandHealingDrone.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use ADMIN-OVERRIDE to command Barrier Drone
            AISkillDriver commandBarrierDrone = masterObject.AddComponent<AISkillDriver>();
            commandBarrierDrone.skillSlot = SkillSlot.Secondary;
            commandBarrierDrone.requiredSkill = commandBarrierDroneSkill;
            commandBarrierDrone.requireSkillReady = true;
            commandBarrierDrone.requireEquipmentReady = false;
            commandBarrierDrone.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            commandBarrierDrone.minDistance = 0f;
            commandBarrierDrone.maxDistance = 100f;
            commandBarrierDrone.selectionRequiresTargetLoS = true;
            commandBarrierDrone.activationRequiresTargetLoS = true;
            commandBarrierDrone.activationRequiresAimConfirmation = false;
            commandBarrierDrone.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            commandBarrierDrone.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            commandBarrierDrone.ignoreNodeGraph = false;
            commandBarrierDrone.noRepeat = false;
            commandBarrierDrone.shouldSprint = false;
            commandBarrierDrone.shouldFireEquipment = false;
            commandBarrierDrone.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use ADMIN-OVERRIDE to command Emergency Drone when injured
            AISkillDriver commandEmergencyDrone = masterObject.AddComponent<AISkillDriver>();
            commandEmergencyDrone.skillSlot = SkillSlot.Secondary;
            commandEmergencyDrone.requiredSkill = commandEmergencyDroneSkill;
            commandEmergencyDrone.requireSkillReady = true;
            commandEmergencyDrone.requireEquipmentReady = false;
            commandEmergencyDrone.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            commandEmergencyDrone.minDistance = 0f;
            commandEmergencyDrone.maxDistance = float.PositiveInfinity;
            commandEmergencyDrone.selectionRequiresTargetLoS = false;
            commandEmergencyDrone.activationRequiresTargetLoS = false;
            commandEmergencyDrone.activationRequiresAimConfirmation = false;
            commandEmergencyDrone.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            commandEmergencyDrone.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            commandEmergencyDrone.ignoreNodeGraph = false;
            commandEmergencyDrone.noRepeat = false;
            commandEmergencyDrone.shouldSprint = false;
            commandEmergencyDrone.shouldFireEquipment = false;
            commandEmergencyDrone.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            commandEmergencyDrone.maxUserHealthFraction = .6f;


            // Use ADMIN-OVERRIDE to command Emergency Drone if a nearby target is injured
            AISkillDriver commandEmergencyDroneFriendly = masterObject.AddComponent<AISkillDriver>();
            commandEmergencyDroneFriendly.skillSlot = SkillSlot.Secondary;
            commandEmergencyDroneFriendly.requiredSkill = commandEmergencyDroneSkill;
            commandEmergencyDroneFriendly.requireSkillReady = true;
            commandEmergencyDroneFriendly.requireEquipmentReady = false;
            commandEmergencyDroneFriendly.moveTargetType = AISkillDriver.TargetType.NearestFriendlyInSkillRange;
            commandEmergencyDroneFriendly.minDistance = 0f;
            commandEmergencyDroneFriendly.maxDistance = 50f;
            commandEmergencyDroneFriendly.selectionRequiresTargetLoS = false;
            commandEmergencyDroneFriendly.activationRequiresTargetLoS = false;
            commandEmergencyDroneFriendly.activationRequiresAimConfirmation = false;
            commandEmergencyDroneFriendly.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            commandEmergencyDroneFriendly.aimType = AISkillDriver.AimType.AtMoveTarget;
            commandEmergencyDroneFriendly.ignoreNodeGraph = false;
            commandEmergencyDroneFriendly.noRepeat = false;
            commandEmergencyDroneFriendly.shouldSprint = false;
            commandEmergencyDroneFriendly.shouldFireEquipment = false;
            commandEmergencyDroneFriendly.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            commandEmergencyDroneFriendly.maxTargetHealthFraction = .6f;


            // Use ADMIN-OVERRIDE to command everything else
            AISkillDriver commandDrone = masterObject.AddComponent<AISkillDriver>();
            commandDrone.skillSlot = SkillSlot.Secondary;
            commandDrone.requiredSkill = commandDroneFallbackSkill;
            commandDrone.requireSkillReady = true;
            commandDrone.requireEquipmentReady = false;
            commandDrone.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            commandDrone.minDistance = 0f;
            commandDrone.maxDistance = 100f;
            commandDrone.selectionRequiresTargetLoS = true;
            commandDrone.activationRequiresTargetLoS = true;
            commandDrone.activationRequiresAimConfirmation = true;
            commandDrone.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            commandDrone.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            commandDrone.ignoreNodeGraph = false;
            commandDrone.noRepeat = false;
            commandDrone.shouldSprint = false;
            commandDrone.shouldFireEquipment = false;
            commandDrone.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use CMD-SWARM off cooldown
            AISkillDriver ramDrone = masterObject.AddComponent<AISkillDriver>();
            ramDrone.skillSlot = SkillSlot.Secondary;
            ramDrone.requiredSkill = ramDroneSkill;
            ramDrone.requireSkillReady = true;
            ramDrone.requireEquipmentReady = false;
            ramDrone.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            ramDrone.minDistance = 0f;
            ramDrone.maxDistance = 150f;
            ramDrone.selectionRequiresTargetLoS = true;
            ramDrone.activationRequiresTargetLoS = true;
            ramDrone.activationRequiresAimConfirmation = true;
            ramDrone.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            ramDrone.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            ramDrone.ignoreNodeGraph = false;
            ramDrone.noRepeat = true;
            ramDrone.shouldSprint = false;
            ramDrone.shouldFireEquipment = false;
            ramDrone.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            ramDrone.driverUpdateTimerOverride = 1f;


            // Use FIREWALL off cooldown
            AISkillDriver firewall = masterObject.AddComponent<AISkillDriver>();
            firewall.skillSlot = SkillSlot.Utility;
            firewall.requiredSkill = firewallShield;
            firewall.requireSkillReady = true;
            firewall.requireEquipmentReady = false;
            firewall.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            firewall.minDistance = 0f;
            firewall.maxDistance = 100f;
            firewall.selectionRequiresTargetLoS = true;
            firewall.activationRequiresTargetLoS = true;
            firewall.activationRequiresAimConfirmation = false;
            firewall.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            firewall.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            firewall.ignoreNodeGraph = false;
            firewall.noRepeat = true;
            firewall.shouldSprint = false;
            firewall.shouldFireEquipment = false;
            firewall.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            firewall.driverUpdateTimerOverride = 1.5f;


            // Shoot primary while retreating (point-blank)
            AISkillDriver primaryRetreat = masterObject.AddComponent<AISkillDriver>();
            primaryRetreat.skillSlot = SkillSlot.Primary;
            primaryRetreat.requireSkillReady = true;
            primaryRetreat.requireEquipmentReady = false;
            primaryRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryRetreat.minDistance = 0f;
            primaryRetreat.maxDistance = 20f;
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
            primaryRetreat.driverUpdateTimerOverride = .5f;


            // Shoot primary while strafing (close)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 60f;
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
            primaryStrafe.driverUpdateTimerOverride = .5f;


            // Shoot primary while approaching (far)
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requireSkillReady = true;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 250f;
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
            ramDrone.nextHighPriorityOverride = releaseInput;
        }
    }
}
