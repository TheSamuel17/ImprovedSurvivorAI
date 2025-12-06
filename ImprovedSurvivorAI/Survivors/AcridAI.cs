using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class AcridAI
    {
        // Asset references
        public static SkillDef spitSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Croco/CrocoSpit.asset").WaitForCompletion();
        public static SkillDef biteSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Croco/CrocoBite.asset").WaitForCompletion();

        public AcridAI(GameObject masterObject)
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
            

            // Use Epidemic off cooldown
            AISkillDriver epidemic = masterObject.AddComponent<AISkillDriver>();
            epidemic.skillSlot = SkillSlot.Special;
            epidemic.requireSkillReady = true;
            epidemic.requireEquipmentReady = false;
            epidemic.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            epidemic.minDistance = 0f;
            epidemic.maxDistance = 150f;
            epidemic.selectionRequiresTargetLoS = true;
            epidemic.activationRequiresTargetLoS = true;
            epidemic.activationRequiresAimConfirmation = true;
            epidemic.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            epidemic.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            epidemic.ignoreNodeGraph = false;
            epidemic.noRepeat = true;
            epidemic.shouldSprint = true;
            epidemic.shouldFireEquipment = false;
            epidemic.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use utility on target off cooldown
            AISkillDriver utility = masterObject.AddComponent<AISkillDriver>();
            utility.skillSlot = SkillSlot.Utility;
            utility.requireSkillReady = true;
            utility.requireEquipmentReady = false;
            utility.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utility.minDistance = 0f;
            utility.maxDistance = 30f;
            utility.selectionRequiresTargetLoS = true;
            utility.activationRequiresTargetLoS = true;
            utility.activationRequiresAimConfirmation = true;
            utility.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utility.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utility.ignoreNodeGraph = true;
            utility.noRepeat = true;
            utility.shouldSprint = true;
            utility.shouldFireEquipment = false;
            utility.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Neurotoxin off cooldown
            AISkillDriver spit = masterObject.AddComponent<AISkillDriver>();
            spit.skillSlot = SkillSlot.Secondary;
            spit.requiredSkill = spitSkill;
            spit.requireSkillReady = true;
            spit.requireEquipmentReady = false;
            spit.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            spit.minDistance = 0f;
            spit.maxDistance = 150f;
            spit.selectionRequiresTargetLoS = true;
            spit.activationRequiresTargetLoS = true;
            spit.activationRequiresAimConfirmation = true;
            spit.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            spit.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            spit.ignoreNodeGraph = false;
            spit.noRepeat = false;
            spit.shouldSprint = true;
            spit.shouldFireEquipment = false;
            spit.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Ravenous Bite off cooldown
            AISkillDriver bite = masterObject.AddComponent<AISkillDriver>();
            bite.skillSlot = SkillSlot.Secondary;
            bite.requiredSkill = biteSkill;
            bite.requireSkillReady = true;
            bite.requireEquipmentReady = false;
            bite.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            bite.minDistance = 0f;
            bite.maxDistance = 5f;
            bite.selectionRequiresTargetLoS = false;
            bite.activationRequiresTargetLoS = false;
            bite.activationRequiresAimConfirmation = true;
            bite.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            bite.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            bite.ignoreNodeGraph = true;
            bite.noRepeat = false;
            bite.shouldSprint = true;
            bite.shouldFireEquipment = false;
            bite.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


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


            // Kite around at mid-range
            AISkillDriver utilityFar = masterObject.AddComponent<AISkillDriver>();
            utilityFar.skillSlot = SkillSlot.Utility;
            utilityFar.requireSkillReady = false;
            utilityFar.requireEquipmentReady = false;
            utilityFar.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utilityFar.minDistance = 40f;
            utilityFar.maxDistance = 80f;
            utilityFar.selectionRequiresTargetLoS = true;
            utilityFar.activationRequiresTargetLoS = true;
            utilityFar.activationRequiresAimConfirmation = false;
            utilityFar.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            utilityFar.aimType = AISkillDriver.AimType.MoveDirection;
            utilityFar.ignoreNodeGraph = false;
            utilityFar.noRepeat = false;
            utilityFar.shouldSprint = true;
            utilityFar.shouldFireEquipment = false;
            utilityFar.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


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


            // Use utility to follow the owner
            AISkillDriver utilityOwner = masterObject.AddComponent<AISkillDriver>();
            utilityOwner.skillSlot = SkillSlot.Utility;
            utilityOwner.requireSkillReady = true;
            utilityOwner.requireEquipmentReady = false;
            utilityOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            utilityOwner.minDistance = 40f;
            utilityOwner.maxDistance = float.PositiveInfinity;
            utilityOwner.selectionRequiresTargetLoS = false;
            utilityOwner.activationRequiresTargetLoS = false;
            utilityOwner.activationRequiresAimConfirmation = true;
            utilityOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityOwner.aimType = AISkillDriver.AimType.MoveDirection;
            utilityOwner.ignoreNodeGraph = false;
            utilityOwner.noRepeat = true;
            utilityOwner.shouldSprint = true;
            utilityOwner.shouldFireEquipment = false;
            utilityOwner.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


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


            // Sprint towards the target (infinite range)
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
