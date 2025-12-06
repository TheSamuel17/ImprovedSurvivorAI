using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class VoidFiendAI
    {
        // Asset references
        public static SkillDef drownSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC1/VoidSurvivor/FireHandBeam.asset").WaitForCompletion();
        public static SkillDef corruptedDrownSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC1/VoidSurvivor/FireCorruptBeam.asset").WaitForCompletion();
        public static SkillDef floodSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC1/VoidSurvivor/ChargeMegaBlaster.asset").WaitForCompletion();
        public static SkillDef corruptedFloodSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC1/VoidSurvivor/FireCorruptDisk.asset").WaitForCompletion();
        public static SkillDef trespassSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC1/VoidSurvivor/VoidBlinkUp.asset").WaitForCompletion();
        public static SkillDef corruptedTrespassSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC1/VoidSurvivor/VoidBlinkDown.asset").WaitForCompletion();
        public static VoidSurvivorSkillDef suppressSkill = Addressables.LoadAssetAsync<VoidSurvivorSkillDef>("RoR2/DLC1/VoidSurvivor/CrushCorruption.asset").WaitForCompletion();

        public VoidFiendAI(GameObject masterObject)
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


            // Uncorrupted: attempt a Suppress if injured
            AISkillDriver suppress = masterObject.AddComponent<AISkillDriver>();
            suppress.skillSlot = SkillSlot.Special;
            suppress.requiredSkill = suppressSkill;
            suppress.requireSkillReady = true;
            suppress.requireEquipmentReady = false;
            suppress.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            suppress.minDistance = 0f;
            suppress.maxDistance = float.PositiveInfinity;
            suppress.selectionRequiresTargetLoS = false;
            suppress.activationRequiresTargetLoS = false;
            suppress.activationRequiresAimConfirmation = false;
            suppress.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            suppress.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            suppress.ignoreNodeGraph = false;
            suppress.noRepeat = true;
            suppress.shouldSprint = true;
            suppress.shouldFireEquipment = false;
            suppress.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            suppress.maxUserHealthFraction = .75f;
            suppress.driverUpdateTimerOverride = 1f;


            // Uncorrupted: use Trespass
            AISkillDriver trespass = masterObject.AddComponent<AISkillDriver>();
            trespass.skillSlot = SkillSlot.Utility;
            trespass.requiredSkill = trespassSkill;
            trespass.requireSkillReady = true;
            trespass.requireEquipmentReady = false;
            trespass.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            trespass.minDistance = 20f;
            trespass.maxDistance = 40f;
            trespass.selectionRequiresTargetLoS = true;
            trespass.activationRequiresTargetLoS = true;
            trespass.activationRequiresAimConfirmation = false;
            trespass.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            trespass.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            trespass.ignoreNodeGraph = false;
            trespass.noRepeat = true;
            trespass.shouldSprint = true;
            trespass.shouldFireEquipment = false;
            trespass.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            trespass.driverUpdateTimerOverride = 1.1f;


            // Corrupted: use Trespass
            AISkillDriver corruptedTrespass = masterObject.AddComponent<AISkillDriver>();
            corruptedTrespass.skillSlot = SkillSlot.Utility;
            corruptedTrespass.requiredSkill = corruptedTrespassSkill;
            corruptedTrespass.requireSkillReady = true;
            corruptedTrespass.requireEquipmentReady = false;
            corruptedTrespass.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            corruptedTrespass.minDistance = 40f;
            corruptedTrespass.maxDistance = 80f;
            corruptedTrespass.selectionRequiresTargetLoS = true;
            corruptedTrespass.activationRequiresTargetLoS = true;
            corruptedTrespass.activationRequiresAimConfirmation = true;
            corruptedTrespass.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            corruptedTrespass.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            corruptedTrespass.ignoreNodeGraph = false;
            corruptedTrespass.noRepeat = true;
            corruptedTrespass.shouldSprint = true;
            corruptedTrespass.shouldFireEquipment = false;
            corruptedTrespass.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            corruptedTrespass.driverUpdateTimerOverride = .5f;


            // Uncorrupted: charge up Flood off cooldown
            AISkillDriver flood = masterObject.AddComponent<AISkillDriver>();
            flood.skillSlot = SkillSlot.Secondary;
            flood.requiredSkill = floodSkill;
            flood.requireSkillReady = true;
            flood.requireEquipmentReady = false;
            flood.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            flood.minDistance = 0f;
            flood.maxDistance = 100f;
            flood.selectionRequiresTargetLoS = true;
            flood.activationRequiresTargetLoS = true;
            flood.activationRequiresAimConfirmation = true;
            flood.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            flood.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            flood.ignoreNodeGraph = false;
            flood.noRepeat = false;
            flood.shouldSprint = true;
            flood.shouldFireEquipment = false;
            flood.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            flood.driverUpdateTimerOverride = 1.6f;


            // Corrupted: shoot Flood off cooldown
            AISkillDriver corruptedFlood = masterObject.AddComponent<AISkillDriver>();
            corruptedFlood.skillSlot = SkillSlot.Secondary;
            corruptedFlood.requiredSkill = corruptedFloodSkill;
            corruptedFlood.requireSkillReady = true;
            corruptedFlood.requireEquipmentReady = false;
            corruptedFlood.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            corruptedFlood.minDistance = 0f;
            corruptedFlood.maxDistance = 40f;
            corruptedFlood.selectionRequiresTargetLoS = true;
            corruptedFlood.activationRequiresTargetLoS = true;
            corruptedFlood.activationRequiresAimConfirmation = true;
            corruptedFlood.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            corruptedFlood.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            corruptedFlood.ignoreNodeGraph = false;
            corruptedFlood.noRepeat = false;
            corruptedFlood.shouldSprint = false;
            corruptedFlood.shouldFireEquipment = false;
            corruptedFlood.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Corrupted: shoot primary while strafing
            AISkillDriver corruptedPrimary = masterObject.AddComponent<AISkillDriver>();
            corruptedPrimary.skillSlot = SkillSlot.Primary;
            corruptedPrimary.requiredSkill = corruptedDrownSkill;
            corruptedPrimary.requireSkillReady = true;
            corruptedPrimary.requireEquipmentReady = false;
            corruptedPrimary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            corruptedPrimary.minDistance = 0f;
            corruptedPrimary.maxDistance = 35f;
            corruptedPrimary.selectionRequiresTargetLoS = true;
            corruptedPrimary.activationRequiresTargetLoS = true;
            corruptedPrimary.activationRequiresAimConfirmation = false;
            corruptedPrimary.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            corruptedPrimary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            corruptedPrimary.ignoreNodeGraph = false;
            corruptedPrimary.noRepeat = false;
            corruptedPrimary.shouldSprint = false;
            corruptedPrimary.shouldFireEquipment = false;
            corruptedPrimary.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            corruptedPrimary.driverUpdateTimerOverride = .5f;


            // Uncorrupted: shoot primary while retreating (close)
            AISkillDriver primaryRetreat = masterObject.AddComponent<AISkillDriver>();
            primaryRetreat.skillSlot = SkillSlot.Primary;
            primaryRetreat.requiredSkill = drownSkill;
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
            primaryRetreat.driverUpdateTimerOverride = .5f;


            // Uncorrupted: shoot primary while strafing (medium)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requiredSkill = drownSkill;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 60f;
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


            // Uncorrupted: shoot primary while approaching (far)
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requiredSkill = drownSkill;
            primaryChase.requireSkillReady = true;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 300f;
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
            flood.nextHighPriorityOverride = releaseInput;
        }
    }
}
