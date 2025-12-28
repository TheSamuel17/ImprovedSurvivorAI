using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;

namespace ImprovedSurvivorAI
{
    class EnforcerAI
    {
        // SkillDef references
        public static SkillDef riotShotgunSkill;
        public static SkillDef superShotgunSkill;
        public static SkillDef machineGunSkill;
        public static SkillDef tearGasSkill;
        public static SkillDef shieldUpSkill;

        public EnforcerAI(GameObject masterObject)
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


            // SkillDef reference
            foreach (SkillDef skillDef in Main.listSkillDefs)
            {
                switch (skillDef.skillName)
                {
                    case "ENFORCER_PRIMARY_SHOTGUN_NAME":
                        riotShotgunSkill = skillDef;
                        break;

                    case "ENFORCER_PRIMARY_SUPERSHOTGUN_NAME":
                        superShotgunSkill = skillDef;
                        break;

                    case "ENFORCER_PRIMARY_RIFLE_NAME":
                        machineGunSkill = skillDef;
                        break;

                    case "ENFORCER_UTILITY_TEARGAS_NAME":
                        tearGasSkill = skillDef;
                        break;

                    case "ENFORCER_SPECIAL_SHIELDUP_NAME":
                        shieldUpSkill = skillDef;
                        break;
                }
            }


            // Misc
            CharacterBody.onBodyStartGlobal += SpecialLoadoutBehavior;


            // Put up shield
            AISkillDriver shieldUp = masterObject.AddComponent<AISkillDriver>();
            shieldUp.skillSlot = SkillSlot.Special;
            shieldUp.requiredSkill = shieldUpSkill;
            shieldUp.requireSkillReady = true;
            shieldUp.requireEquipmentReady = false;
            shieldUp.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            shieldUp.minDistance = 0f;
            shieldUp.maxDistance = 64f;
            shieldUp.selectionRequiresTargetLoS = true;
            shieldUp.activationRequiresTargetLoS = true;
            shieldUp.activationRequiresAimConfirmation = false;
            shieldUp.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            shieldUp.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            shieldUp.ignoreNodeGraph = false;
            shieldUp.noRepeat = true;
            shieldUp.shouldSprint = false;
            shieldUp.shouldFireEquipment = false;
            shieldUp.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            shieldUp.resetCurrentEnemyOnNextDriverSelection = true;


            // Use Secondary point-blank
            AISkillDriver secondary = masterObject.AddComponent<AISkillDriver>();
            secondary.skillSlot = SkillSlot.Secondary;
            secondary.requireSkillReady = true;
            secondary.requireEquipmentReady = false;
            secondary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            secondary.minDistance = 0f;
            secondary.maxDistance = 8f;
            secondary.selectionRequiresTargetLoS = false;
            secondary.activationRequiresTargetLoS = false;
            secondary.activationRequiresAimConfirmation = true;
            secondary.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            secondary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            secondary.ignoreNodeGraph = false;
            secondary.noRepeat = true;
            secondary.shouldSprint = false;
            secondary.shouldFireEquipment = false;
            secondary.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Tear Gas off cooldown
            AISkillDriver tearGas = masterObject.AddComponent<AISkillDriver>();
            tearGas.skillSlot = SkillSlot.Utility;
            tearGas.requiredSkill = tearGasSkill;
            tearGas.requireSkillReady = true;
            tearGas.requireEquipmentReady = false;
            tearGas.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            tearGas.minDistance = 0f;
            tearGas.maxDistance = 60f;
            tearGas.selectionRequiresTargetLoS = true;
            tearGas.activationRequiresTargetLoS = true;
            tearGas.activationRequiresAimConfirmation = true;
            tearGas.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            tearGas.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            tearGas.ignoreNodeGraph = false;
            tearGas.noRepeat = true;
            tearGas.shouldSprint = false;
            tearGas.shouldFireEquipment = false;
            tearGas.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use utility off cooldown - meant primarily for Stun Grenade
            AISkillDriver utility = masterObject.AddComponent<AISkillDriver>();
            utility.skillSlot = SkillSlot.Utility;
            utility.requireSkillReady = true;
            utility.requireEquipmentReady = false;
            utility.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utility.minDistance = 0f;
            utility.maxDistance = 50f;
            utility.selectionRequiresTargetLoS = true;
            utility.activationRequiresTargetLoS = true;
            utility.activationRequiresAimConfirmation = true;
            utility.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utility.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utility.ignoreNodeGraph = false;
            utility.noRepeat = false;
            utility.shouldSprint = false;
            utility.shouldFireEquipment = false;
            utility.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Shoot Heavy Machine Gun while retreating (point-blank)
            AISkillDriver machineGunRetreat = masterObject.AddComponent<AISkillDriver>();
            machineGunRetreat.skillSlot = SkillSlot.Primary;
            machineGunRetreat.requiredSkill = machineGunSkill;
            machineGunRetreat.requireSkillReady = true;
            machineGunRetreat.requireEquipmentReady = false;
            machineGunRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            machineGunRetreat.minDistance = 0f;
            machineGunRetreat.maxDistance = 10f;
            machineGunRetreat.selectionRequiresTargetLoS = true;
            machineGunRetreat.activationRequiresTargetLoS = true;
            machineGunRetreat.activationRequiresAimConfirmation = false;
            machineGunRetreat.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            machineGunRetreat.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            machineGunRetreat.ignoreNodeGraph = false;
            machineGunRetreat.noRepeat = false;
            machineGunRetreat.shouldSprint = false;
            machineGunRetreat.shouldFireEquipment = false;
            machineGunRetreat.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            machineGunRetreat.driverUpdateTimerOverride = .5f;


            // Shoot Heavy Machine Gun while strafing (close)
            AISkillDriver machineGunStrafe = masterObject.AddComponent<AISkillDriver>();
            machineGunStrafe.skillSlot = SkillSlot.Primary;
            machineGunStrafe.requiredSkill = machineGunSkill;
            machineGunStrafe.requireSkillReady = true;
            machineGunStrafe.requireEquipmentReady = false;
            machineGunStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            machineGunStrafe.minDistance = 0f;
            machineGunStrafe.maxDistance = 40f;
            machineGunStrafe.selectionRequiresTargetLoS = true;
            machineGunStrafe.activationRequiresTargetLoS = true;
            machineGunStrafe.activationRequiresAimConfirmation = false;
            machineGunStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            machineGunStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            machineGunStrafe.ignoreNodeGraph = false;
            machineGunStrafe.noRepeat = false;
            machineGunStrafe.shouldSprint = false;
            machineGunStrafe.shouldFireEquipment = false;
            machineGunStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            machineGunStrafe.driverUpdateTimerOverride = .5f;


            // Shoot Heavy Machine Gun while approaching (far)
            AISkillDriver machineGunChase = masterObject.AddComponent<AISkillDriver>();
            machineGunChase.skillSlot = SkillSlot.Primary;
            machineGunChase.requiredSkill = machineGunSkill;
            machineGunChase.requireSkillReady = true;
            machineGunChase.requireEquipmentReady = false;
            machineGunChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            machineGunChase.minDistance = 0f;
            machineGunChase.maxDistance = 120f;
            machineGunChase.selectionRequiresTargetLoS = true;
            machineGunChase.activationRequiresTargetLoS = true;
            machineGunChase.activationRequiresAimConfirmation = false;
            machineGunChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            machineGunChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            machineGunChase.ignoreNodeGraph = false;
            machineGunChase.noRepeat = false;
            machineGunChase.shouldSprint = false;
            machineGunChase.shouldFireEquipment = false;
            machineGunChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            machineGunChase.driverUpdateTimerOverride = .5f;


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


            // Shoot primary Gun while strafing (close)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 30f;
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
            primaryChase.maxDistance = 64f;
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


            // Use secondary off cooldown towards move direction
            AISkillDriver secondaryWhilePathing = masterObject.AddComponent<AISkillDriver>();
            secondaryWhilePathing.skillSlot = SkillSlot.Secondary;
            secondaryWhilePathing.requireSkillReady = true;
            secondaryWhilePathing.requireEquipmentReady = false;
            secondaryWhilePathing.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            secondaryWhilePathing.minDistance = 150f;
            secondaryWhilePathing.maxDistance = 400f;
            secondaryWhilePathing.selectionRequiresTargetLoS = false;
            secondaryWhilePathing.activationRequiresTargetLoS = false;
            secondaryWhilePathing.activationRequiresAimConfirmation = false;
            //secondaryWhilePathing.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            secondaryWhilePathing.aimType = AISkillDriver.AimType.MoveDirection;
            secondaryWhilePathing.ignoreNodeGraph = false;
            secondaryWhilePathing.noRepeat = true;
            secondaryWhilePathing.shouldSprint = true;
            secondaryWhilePathing.shouldFireEquipment = false;
            secondaryWhilePathing.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            secondaryWhilePathing.selectionRequiresOnGround = true;


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
        }

        private void SpecialLoadoutBehavior(CharacterBody body)
        {
            if (body.master && body.bodyIndex == BodyCatalog.FindBodyIndex("EnforcerBody") && !body.isPlayerControlled)
            {
                if (body.skillLocator && body.skillLocator.primary && body.skillLocator.primary.skillDef == machineGunSkill)
                {
                    AISkillDriver[] skillDrivers = body.master.GetComponents<AISkillDriver>();
                    foreach (AISkillDriver skillDriver in skillDrivers)
                    {
                        if (skillDriver.requiredSkill == shieldUpSkill)
                        {
                            skillDriver.maxDistance = 120f; // Extend distance of shield behavior if running Heavy Machine Gun
                        };
                    }
                }
            }
        }
    }
}
