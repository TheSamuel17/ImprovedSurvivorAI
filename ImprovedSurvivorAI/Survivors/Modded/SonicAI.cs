using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;

namespace ImprovedSurvivorAI
{
    class SonicAI
    {
        // SkillDef references
        public static SkillDef sonicBoomSkill;
        public static SkillDef parrySkill;
        public static SkillDef parryFollowUpSkill;
        public static SkillDef sonicBoomSkillSuper;
        public static SkillDef parrySkillSuper;
        public static SkillDef parryFollowUpSkillSuper;

        public SonicAI(GameObject masterObject)
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


            // SkillDef reference
            foreach (SkillDef skillDef in Main.listSkillDefs)
            {
                switch (skillDef.skillName)
                {
                    case "DS_GAMING_SONIC_THE_HEDGEHOG_BODY_SECONDARY_SONIC_BOOM_NAME":
                        sonicBoomSkill = skillDef;
                        break;

                    case "DS_GAMING_SONIC_THE_HEDGEHOG_BODY_SECONDARY_PARRY_NAME":
                        parrySkill = skillDef;
                        break;

                    case "DS_GAMING_SONIC_THE_HEDGEHOG_BODY_SECONDARY_PARRY_FOLLOW_UP_NAME":
                        parryFollowUpSkill = skillDef;
                        break;

                    case "DS_GAMING_SONIC_THE_HEDGEHOG_BODY_SUPER_SECONDARY_SONIC_BOOM_NAME":
                        sonicBoomSkillSuper = skillDef;
                        break;

                    case "DS_GAMING_SONIC_THE_HEDGEHOG_BODY_SUPER_SECONDARY_PARRY_NAME":
                        parrySkillSuper = skillDef;
                        break;

                    case "DS_GAMING_SONIC_THE_HEDGEHOG_BODY_SUPER_SECONDARY_PARRY_FOLLOW_UP_NAME":
                        parryFollowUpSkillSuper = skillDef;
                        break;
                }
            }


            // Use the parry follow-up whenever available
            AISkillDriver parryFollowUp = masterObject.AddComponent<AISkillDriver>();
            parryFollowUp.skillSlot = SkillSlot.Secondary;
            parryFollowUp.requiredSkill = parryFollowUpSkill;
            parryFollowUp.requireSkillReady = true;
            parryFollowUp.requireEquipmentReady = false;
            parryFollowUp.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            parryFollowUp.minDistance = 0f;
            parryFollowUp.maxDistance = 10f;
            parryFollowUp.selectionRequiresTargetLoS = false;
            parryFollowUp.activationRequiresTargetLoS = false;
            parryFollowUp.activationRequiresAimConfirmation = false;
            parryFollowUp.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            parryFollowUp.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            parryFollowUp.ignoreNodeGraph = false;
            parryFollowUp.noRepeat = false;
            parryFollowUp.shouldSprint = false;
            parryFollowUp.shouldFireEquipment = false;
            parryFollowUp.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use the parry follow-up whenever available (Super version)
            AISkillDriver parryFollowUpSuper = masterObject.AddComponent<AISkillDriver>();
            parryFollowUpSuper.skillSlot = SkillSlot.Secondary;
            parryFollowUpSuper.requiredSkill = parryFollowUpSkillSuper;
            parryFollowUpSuper.requireSkillReady = true;
            parryFollowUpSuper.requireEquipmentReady = false;
            parryFollowUpSuper.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            parryFollowUpSuper.minDistance = 0f;
            parryFollowUpSuper.maxDistance = 10f;
            parryFollowUpSuper.selectionRequiresTargetLoS = false;
            parryFollowUpSuper.activationRequiresTargetLoS = false;
            parryFollowUpSuper.activationRequiresAimConfirmation = false;
            parryFollowUpSuper.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            parryFollowUpSuper.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            parryFollowUpSuper.ignoreNodeGraph = false;
            parryFollowUpSuper.noRepeat = false;
            parryFollowUpSuper.shouldSprint = false;
            parryFollowUpSuper.shouldFireEquipment = false;
            parryFollowUpSuper.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Sonic Boom off cooldown
            AISkillDriver sonicBoom = masterObject.AddComponent<AISkillDriver>();
            sonicBoom.skillSlot = SkillSlot.Secondary;
            sonicBoom.requiredSkill = sonicBoomSkill;
            sonicBoom.requireSkillReady = true;
            sonicBoom.requireEquipmentReady = false;
            sonicBoom.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sonicBoom.minDistance = 0f;
            sonicBoom.maxDistance = 80f;
            sonicBoom.selectionRequiresTargetLoS = true;
            sonicBoom.activationRequiresTargetLoS = true;
            sonicBoom.activationRequiresAimConfirmation = true;
            sonicBoom.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            sonicBoom.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sonicBoom.ignoreNodeGraph = false;
            sonicBoom.noRepeat = false;
            sonicBoom.shouldSprint = false;
            sonicBoom.shouldFireEquipment = false;
            sonicBoom.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Sonic Boom off cooldown (Super version)
            AISkillDriver sonicBoomSuper = masterObject.AddComponent<AISkillDriver>();
            sonicBoomSuper.skillSlot = SkillSlot.Secondary;
            sonicBoomSuper.requiredSkill = sonicBoomSkillSuper;
            sonicBoomSuper.requireSkillReady = true;
            sonicBoomSuper.requireEquipmentReady = false;
            sonicBoomSuper.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sonicBoomSuper.minDistance = 0f;
            sonicBoomSuper.maxDistance = 80f;
            sonicBoomSuper.selectionRequiresTargetLoS = true;
            sonicBoomSuper.activationRequiresTargetLoS = true;
            sonicBoomSuper.activationRequiresAimConfirmation = true;
            sonicBoomSuper.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            sonicBoomSuper.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sonicBoomSuper.ignoreNodeGraph = false;
            sonicBoomSuper.noRepeat = false;
            sonicBoomSuper.shouldSprint = false;
            sonicBoomSuper.shouldFireEquipment = false;
            sonicBoomSuper.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use special off cooldown
            AISkillDriver special = masterObject.AddComponent<AISkillDriver>();
            special.skillSlot = SkillSlot.Special;
            special.requireSkillReady = true;
            special.requireEquipmentReady = false;
            special.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            special.minDistance = 0f;
            special.maxDistance = 60f;
            special.selectionRequiresTargetLoS = true;
            special.activationRequiresTargetLoS = true;
            special.activationRequiresAimConfirmation = true;
            special.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            special.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            special.ignoreNodeGraph = false;
            special.noRepeat = false;
            special.shouldSprint = false;
            special.shouldFireEquipment = false;
            special.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use primary (melee) while strafing
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 4f;
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


            // Use primary (melee) while approaching
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requireSkillReady = true;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 8f;
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


            // Use Parry off cooldown
            AISkillDriver parry = masterObject.AddComponent<AISkillDriver>();
            parry.skillSlot = SkillSlot.Secondary;
            parry.requiredSkill = parrySkill;
            parry.requireSkillReady = true;
            parry.requireEquipmentReady = false;
            parry.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            parry.minDistance = 10f;
            parry.maxDistance = 45f;
            parry.selectionRequiresTargetLoS = true;
            parry.activationRequiresTargetLoS = true;
            parry.activationRequiresAimConfirmation = false;
            parry.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            parry.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            parry.ignoreNodeGraph = false;
            parry.noRepeat = true;
            parry.shouldSprint = false;
            parry.shouldFireEquipment = false;
            parry.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Parry off cooldown (Super version)
            AISkillDriver parrySuper = masterObject.AddComponent<AISkillDriver>();
            parrySuper.skillSlot = SkillSlot.Secondary;
            parrySuper.requiredSkill = parrySkillSuper;
            parrySuper.requireSkillReady = true;
            parrySuper.requireEquipmentReady = false;
            parrySuper.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            parrySuper.minDistance = 10f;
            parrySuper.maxDistance = 45f;
            parrySuper.selectionRequiresTargetLoS = true;
            parrySuper.activationRequiresTargetLoS = true;
            parrySuper.activationRequiresAimConfirmation = false;
            parrySuper.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            parrySuper.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            parrySuper.ignoreNodeGraph = false;
            parrySuper.noRepeat = true;
            parrySuper.shouldSprint = false;
            parrySuper.shouldFireEquipment = false;
            parrySuper.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use primary (Homing Attack) while approaching
            AISkillDriver homingAttack = masterObject.AddComponent<AISkillDriver>();
            homingAttack.skillSlot = SkillSlot.Primary;
            homingAttack.requireSkillReady = true;
            homingAttack.requireEquipmentReady = false;
            homingAttack.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            homingAttack.minDistance = 8f;
            homingAttack.maxDistance = 45f;
            homingAttack.selectionRequiresTargetLoS = true;
            homingAttack.activationRequiresTargetLoS = true;
            homingAttack.activationRequiresAimConfirmation = false;
            homingAttack.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            homingAttack.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            homingAttack.ignoreNodeGraph = false;
            homingAttack.noRepeat = false;
            homingAttack.shouldSprint = true;
            homingAttack.shouldFireEquipment = false;
            homingAttack.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Sprint towards the target, Boost when available
            AISkillDriver sprintChase = masterObject.AddComponent<AISkillDriver>();
            sprintChase.skillSlot = SkillSlot.Utility;
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
            sprintChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            sprintChase.resetCurrentEnemyOnNextDriverSelection = true;


            // Sprint towards the owner (boost)
            AISkillDriver followOwnerBoost = masterObject.AddComponent<AISkillDriver>();
            followOwnerBoost.skillSlot = SkillSlot.Utility;
            followOwnerBoost.requireSkillReady = false;
            followOwnerBoost.requireEquipmentReady = false;
            followOwnerBoost.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            followOwnerBoost.minDistance = 40f;
            followOwnerBoost.maxDistance = float.PositiveInfinity;
            followOwnerBoost.selectionRequiresTargetLoS = false;
            followOwnerBoost.activationRequiresTargetLoS = false;
            followOwnerBoost.activationRequiresAimConfirmation = false;
            followOwnerBoost.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            followOwnerBoost.aimType = AISkillDriver.AimType.AtCurrentLeader;
            followOwnerBoost.ignoreNodeGraph = false;
            followOwnerBoost.noRepeat = false;
            followOwnerBoost.shouldSprint = true;
            followOwnerBoost.shouldFireEquipment = false;
            followOwnerBoost.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            followOwnerBoost.resetCurrentEnemyOnNextDriverSelection = true;


            // Sprint towards the owner (no boost)
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


            // Sprint towards the target, Boost while available - unlimited range
            AISkillDriver sprintChaseLowPriority = masterObject.AddComponent<AISkillDriver>();
            sprintChaseLowPriority.skillSlot = SkillSlot.Utility;
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
            sprintChaseLowPriority.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            sprintChaseLowPriority.resetCurrentEnemyOnNextDriverSelection = true;
        }
    }
}
