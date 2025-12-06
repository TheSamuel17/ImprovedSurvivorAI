using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class RailgunnerAI
    {
        // Asset references
        public static RailgunSkillDef smartRoundsSkill = Addressables.LoadAssetAsync<RailgunSkillDef>("RoR2/DLC1/Railgunner/RailgunnerBodyFirePistol.asset").WaitForCompletion();
        public static RailgunSkillDef snipeHeavySkill = Addressables.LoadAssetAsync<RailgunSkillDef>("RoR2/DLC1/Railgunner/RailgunnerBodyFireSnipeHeavy.asset").WaitForCompletion();
        public static RailgunSkillDef snipeLightSkill = Addressables.LoadAssetAsync<RailgunSkillDef>("RoR2/DLC1/Railgunner/RailgunnerBodyFireSnipeLight.asset").WaitForCompletion();
        public static SkillDef activeReloadSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC1/Railgunner/RailgunnerBodyActiveReload.asset").WaitForCompletion();
        public static RailgunSkillDef fireSuperchargeSkill = Addressables.LoadAssetAsync<RailgunSkillDef>("RoR2/DLC1/Railgunner/RailgunnerBodyFireSnipeSuper.asset").WaitForCompletion();
        public static RailgunSkillDef fireCryochargeSkill = Addressables.LoadAssetAsync<RailgunSkillDef>("RoR2/DLC1/Railgunner/RailgunnerBodyFireSnipeCryo.asset").WaitForCompletion();

        public RailgunnerAI(GameObject masterObject)
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


            // Fire Supercharge
            AISkillDriver fireSupercharge = masterObject.AddComponent<AISkillDriver>();
            fireSupercharge.skillSlot = SkillSlot.Primary;
            fireSupercharge.requiredSkill = fireSuperchargeSkill;
            fireSupercharge.requireSkillReady = true;
            fireSupercharge.requireEquipmentReady = false;
            fireSupercharge.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            fireSupercharge.minDistance = 0f;
            fireSupercharge.maxDistance = 300f;
            fireSupercharge.selectionRequiresTargetLoS = true;
            fireSupercharge.activationRequiresTargetLoS = true;
            fireSupercharge.activationRequiresAimConfirmation = true;
            fireSupercharge.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            fireSupercharge.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            fireSupercharge.ignoreNodeGraph = false;
            fireSupercharge.noRepeat = false;
            fireSupercharge.shouldSprint = false;
            fireSupercharge.shouldFireEquipment = false;
            fireSupercharge.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Fire Cryocharge
            AISkillDriver fireCryocharge = masterObject.AddComponent<AISkillDriver>();
            fireCryocharge.skillSlot = SkillSlot.Primary;
            fireCryocharge.requiredSkill = fireCryochargeSkill;
            fireCryocharge.requireSkillReady = true;
            fireCryocharge.requireEquipmentReady = false;
            fireCryocharge.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            fireCryocharge.minDistance = 0f;
            fireCryocharge.maxDistance = 300f;
            fireCryocharge.selectionRequiresTargetLoS = true;
            fireCryocharge.activationRequiresTargetLoS = true;
            fireCryocharge.activationRequiresAimConfirmation = true;
            fireCryocharge.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            fireCryocharge.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            fireCryocharge.ignoreNodeGraph = false;
            fireCryocharge.noRepeat = false;
            fireCryocharge.shouldSprint = false;
            fireCryocharge.shouldFireEquipment = false;
            fireCryocharge.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Charge up Special skill
            AISkillDriver chargeSpecial = masterObject.AddComponent<AISkillDriver>();
            chargeSpecial.skillSlot = SkillSlot.Special;
            chargeSpecial.requireSkillReady = true;
            chargeSpecial.requireEquipmentReady = false;
            chargeSpecial.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            chargeSpecial.minDistance = 15f;
            chargeSpecial.maxDistance = 300f;
            chargeSpecial.selectionRequiresTargetLoS = true;
            chargeSpecial.activationRequiresTargetLoS = true;
            chargeSpecial.activationRequiresAimConfirmation = true;
            chargeSpecial.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            chargeSpecial.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            chargeSpecial.ignoreNodeGraph = false;
            chargeSpecial.noRepeat = false;
            chargeSpecial.shouldSprint = false;
            chargeSpecial.shouldFireEquipment = false;
            chargeSpecial.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Fire heavy sniper
            AISkillDriver snipeHeavy = masterObject.AddComponent<AISkillDriver>();
            snipeHeavy.skillSlot = SkillSlot.Primary;
            snipeHeavy.requiredSkill = snipeHeavySkill;
            snipeHeavy.requireSkillReady = true;
            snipeHeavy.requireEquipmentReady = false;
            snipeHeavy.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            snipeHeavy.minDistance = 15f;
            snipeHeavy.maxDistance = 300f;
            snipeHeavy.selectionRequiresTargetLoS = true;
            snipeHeavy.activationRequiresTargetLoS = true;
            snipeHeavy.activationRequiresAimConfirmation = true;
            snipeHeavy.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            snipeHeavy.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            snipeHeavy.ignoreNodeGraph = false;
            snipeHeavy.noRepeat = false;
            snipeHeavy.shouldSprint = false;
            snipeHeavy.shouldFireEquipment = false;
            snipeHeavy.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Fire light sniper
            AISkillDriver snipeLight = masterObject.AddComponent<AISkillDriver>();
            snipeLight.skillSlot = SkillSlot.Primary;
            snipeLight.requiredSkill = snipeLightSkill;
            snipeLight.requireSkillReady = true;
            snipeLight.requireEquipmentReady = false;
            snipeLight.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            snipeLight.minDistance = 15f;
            snipeLight.maxDistance = 300f;
            snipeLight.selectionRequiresTargetLoS = true;
            snipeLight.activationRequiresTargetLoS = true;
            snipeLight.activationRequiresAimConfirmation = true;
            snipeLight.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            snipeLight.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            snipeLight.ignoreNodeGraph = false;
            snipeLight.noRepeat = false;
            snipeLight.shouldSprint = false;
            snipeLight.shouldFireEquipment = false;
            snipeLight.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Scope in
            AISkillDriver scope = masterObject.AddComponent<AISkillDriver>();
            scope.skillSlot = SkillSlot.Secondary;
            scope.requireSkillReady = true;
            scope.requireEquipmentReady = false;
            scope.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            scope.minDistance = 15f;
            scope.maxDistance = 300f;
            scope.selectionRequiresTargetLoS = true;
            scope.activationRequiresTargetLoS = true;
            scope.activationRequiresAimConfirmation = true;
            scope.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            scope.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            scope.ignoreNodeGraph = false;
            scope.noRepeat = false;
            scope.shouldSprint = false;
            scope.shouldFireEquipment = false;
            scope.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Throw a mine at close targets
            AISkillDriver utilityClose = masterObject.AddComponent<AISkillDriver>();
            utilityClose.skillSlot = SkillSlot.Utility;
            utilityClose.requireSkillReady = false;
            utilityClose.requireEquipmentReady = false;
            utilityClose.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utilityClose.minDistance = 0f;
            utilityClose.maxDistance = 15f;
            utilityClose.selectionRequiresTargetLoS = true;
            utilityClose.activationRequiresTargetLoS = true;
            utilityClose.activationRequiresAimConfirmation = true;
            utilityClose.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            utilityClose.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utilityClose.ignoreNodeGraph = false;
            utilityClose.noRepeat = true;
            utilityClose.shouldSprint = false;
            utilityClose.shouldFireEquipment = false;
            utilityClose.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Shoot primary while retreating
            AISkillDriver primary = masterObject.AddComponent<AISkillDriver>();
            primary.skillSlot = SkillSlot.Primary;
            primary.requiredSkill = smartRoundsSkill;
            primary.requireSkillReady = true;
            primary.requireEquipmentReady = false;
            primary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primary.minDistance = 0f;
            primary.maxDistance = 15f;
            primary.selectionRequiresTargetLoS = true;
            primary.activationRequiresTargetLoS = true;
            primary.activationRequiresAimConfirmation = false;
            primary.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            primary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primary.ignoreNodeGraph = false;
            primary.noRepeat = false;
            primary.shouldSprint = false;
            primary.shouldFireEquipment = false;
            primary.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primary.driverUpdateTimerOverride = .5f;


            // Sprint while strafing if caught in the open while skills are disabled
            AISkillDriver sprintStrafe = masterObject.AddComponent<AISkillDriver>();
            sprintStrafe.skillSlot = SkillSlot.None;
            sprintStrafe.requireSkillReady = false;
            sprintStrafe.requireEquipmentReady = false;
            sprintStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sprintStrafe.minDistance = 0f;
            sprintStrafe.maxDistance = 200f;
            sprintStrafe.selectionRequiresTargetLoS = true;
            sprintStrafe.activationRequiresTargetLoS = true;
            sprintStrafe.activationRequiresAimConfirmation = false;
            sprintStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            sprintStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sprintStrafe.ignoreNodeGraph = false;
            sprintStrafe.noRepeat = false;
            sprintStrafe.shouldSprint = true;
            sprintStrafe.shouldFireEquipment = false;
            sprintStrafe.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            sprintStrafe.resetCurrentEnemyOnNextDriverSelection = true;


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


            // Wait for active reload
            AISkillDriver waitForReload = masterObject.AddComponent<AISkillDriver>();
            waitForReload.skillSlot = SkillSlot.None;
            waitForReload.requireSkillReady = false;
            waitForReload.requireEquipmentReady = false;
            waitForReload.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            waitForReload.minDistance = 0f;
            waitForReload.maxDistance = float.PositiveInfinity;
            waitForReload.selectionRequiresTargetLoS = false;
            waitForReload.activationRequiresTargetLoS = false;
            waitForReload.activationRequiresAimConfirmation = false;
            waitForReload.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            waitForReload.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            waitForReload.ignoreNodeGraph = false;
            waitForReload.noRepeat = false;
            waitForReload.shouldSprint = false;
            waitForReload.shouldFireEquipment = false;
            waitForReload.buttonPressType = AISkillDriver.ButtonPressType.Abstain;
            waitForReload.driverUpdateTimerOverride = .6f;


            // Active reload
            AISkillDriver activeReload = masterObject.AddComponent<AISkillDriver>();
            activeReload.skillSlot = SkillSlot.Primary;
            activeReload.requiredSkill = activeReloadSkill;
            activeReload.requireSkillReady = false;
            activeReload.requireEquipmentReady = false;
            activeReload.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            activeReload.minDistance = 0f;
            activeReload.maxDistance = float.PositiveInfinity;
            activeReload.selectionRequiresTargetLoS = false;
            activeReload.activationRequiresTargetLoS = false;
            activeReload.activationRequiresAimConfirmation = false;
            activeReload.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            activeReload.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            activeReload.ignoreNodeGraph = false;
            activeReload.noRepeat = false;
            activeReload.shouldSprint = false;
            activeReload.shouldFireEquipment = false;
            activeReload.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Overrides
            snipeHeavy.nextHighPriorityOverride = waitForReload;
            waitForReload.nextHighPriorityOverride = activeReload;
        }
    }
}
