using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;

namespace ImprovedSurvivorAI
{
    class NemesisEnforcerAI
    {
        // SkillDef references
        public static SkillDef hammerSkill;
        public static SkillDef minigunSkill;
        public static SkillDef dominanceHammerSkill;
        public static SkillDef dominanceMinigunSkill;
        public static SkillDef heatCrashSkill;
        public static SkillDef gasGrenadeSkill;
        public static SkillDef swapToMinigunSkill;
        public static SkillDef swapToHammerSkill;

        public NemesisEnforcerAI(GameObject masterObject)
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
                    case "NEMFORCER_PRIMARY_HAMMER_NAME":
                        hammerSkill = skillDef;
                        break;

                    case "NEMFORCER_PRIMARY_MINIGUN_NAME":
                        minigunSkill = skillDef;
                        break;

                    case "NEMFORCER_SECONDARY_BASH_NAME":
                        dominanceHammerSkill = skillDef;
                        break;

                    case "NEMFORCER_SECONDARY_SLAM_NAME":
                        dominanceMinigunSkill = skillDef;
                        break;

                    case "NEMFORCER_UTILITY_CRASH_NAME":
                        heatCrashSkill = skillDef;
                        break;

                    case "NEMFORCER_UTILITY_GAS_NAME":
                        gasGrenadeSkill = skillDef;
                        break;

                    case "NEMFORCER_SPECIAL_MINIGUNUP_NAME":
                        swapToMinigunSkill = skillDef;
                        break;

                    case "NEMFORCER_SPECIAL_MINIGUNDOWN_NAME":
                        swapToHammerSkill = skillDef;
                        break;
                }
            }


            // Misc
            CharacterBody.onBodyStartGlobal += SpecialLoadoutBehavior;


            // Use Heat Crash off cooldown
            AISkillDriver heatCrash = masterObject.AddComponent<AISkillDriver>();
            heatCrash.skillSlot = SkillSlot.Utility;
            heatCrash.requiredSkill = heatCrashSkill;
            heatCrash.requireSkillReady = true;
            heatCrash.requireEquipmentReady = false;
            heatCrash.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            heatCrash.minDistance = 0f;
            heatCrash.maxDistance = 20f;
            heatCrash.selectionRequiresTargetLoS = false;
            heatCrash.activationRequiresTargetLoS = false;
            heatCrash.activationRequiresAimConfirmation = false;
            heatCrash.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            heatCrash.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            heatCrash.ignoreNodeGraph = true;
            heatCrash.noRepeat = false;
            heatCrash.shouldSprint = false;
            heatCrash.shouldFireEquipment = false;
            heatCrash.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            heatCrash.driverUpdateTimerOverride = 2f;


            // Use XM47 Grenade off cooldown
            AISkillDriver tearGas = masterObject.AddComponent<AISkillDriver>();
            tearGas.skillSlot = SkillSlot.Utility;
            tearGas.requiredSkill = gasGrenadeSkill;
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


            // Charge up secondary (hammer) off cooldown
            AISkillDriver secondaryHammer = masterObject.AddComponent<AISkillDriver>();
            secondaryHammer.skillSlot = SkillSlot.Secondary;
            secondaryHammer.requiredSkill = dominanceHammerSkill;
            secondaryHammer.requireSkillReady = true;
            secondaryHammer.requireEquipmentReady = false;
            secondaryHammer.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            secondaryHammer.minDistance = 0f;
            secondaryHammer.maxDistance = 25f;
            secondaryHammer.selectionRequiresTargetLoS = false;
            secondaryHammer.activationRequiresTargetLoS = false;
            secondaryHammer.activationRequiresAimConfirmation = false;
            secondaryHammer.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            secondaryHammer.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            secondaryHammer.ignoreNodeGraph = false;
            secondaryHammer.noRepeat = true;
            secondaryHammer.shouldSprint = true;
            secondaryHammer.shouldFireEquipment = false;
            secondaryHammer.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            secondaryHammer.driverUpdateTimerOverride = 2.75f;


            // Use secondary (minigun) off cooldown
            AISkillDriver secondaryMinigun = masterObject.AddComponent<AISkillDriver>();
            secondaryMinigun.skillSlot = SkillSlot.Secondary;
            secondaryMinigun.requiredSkill = dominanceMinigunSkill;
            secondaryMinigun.requireSkillReady = true;
            secondaryMinigun.requireEquipmentReady = false;
            secondaryMinigun.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            secondaryMinigun.minDistance = 0f;
            secondaryMinigun.maxDistance = 15f;
            secondaryMinigun.selectionRequiresTargetLoS = false;
            secondaryMinigun.activationRequiresTargetLoS = false;
            secondaryMinigun.activationRequiresAimConfirmation = false;
            secondaryMinigun.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            secondaryMinigun.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            secondaryMinigun.ignoreNodeGraph = false;
            secondaryMinigun.noRepeat = false;
            secondaryMinigun.shouldSprint = false;
            secondaryMinigun.shouldFireEquipment = false;
            secondaryMinigun.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Swap to hammer at close range
            AISkillDriver swapToHammerClose = masterObject.AddComponent<AISkillDriver>();
            swapToHammerClose.skillSlot = SkillSlot.Special;
            swapToHammerClose.requiredSkill = swapToHammerSkill;
            swapToHammerClose.requireSkillReady = true;
            swapToHammerClose.requireEquipmentReady = false;
            swapToHammerClose.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            swapToHammerClose.minDistance = 0f;
            swapToHammerClose.maxDistance = 25f;
            swapToHammerClose.selectionRequiresTargetLoS = false;
            swapToHammerClose.activationRequiresTargetLoS = false;
            swapToHammerClose.activationRequiresAimConfirmation = false;
            swapToHammerClose.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            swapToHammerClose.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            swapToHammerClose.ignoreNodeGraph = false;
            swapToHammerClose.noRepeat = true;
            swapToHammerClose.shouldSprint = false;
            swapToHammerClose.shouldFireEquipment = false;
            swapToHammerClose.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            swapToHammerClose.driverUpdateTimerOverride = .5f;


            // Swap to hammer when target is far
            AISkillDriver swapToHammerFar = masterObject.AddComponent<AISkillDriver>();
            swapToHammerFar.skillSlot = SkillSlot.Special;
            swapToHammerFar.requiredSkill = swapToHammerSkill;
            swapToHammerFar.requireSkillReady = true;
            swapToHammerFar.requireEquipmentReady = false;
            swapToHammerFar.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            swapToHammerFar.minDistance = 80f;
            swapToHammerFar.maxDistance = float.PositiveInfinity;
            swapToHammerFar.selectionRequiresTargetLoS = false;
            swapToHammerFar.activationRequiresTargetLoS = false;
            swapToHammerFar.activationRequiresAimConfirmation = false;
            swapToHammerFar.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            swapToHammerFar.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            swapToHammerFar.ignoreNodeGraph = false;
            swapToHammerFar.noRepeat = true;
            swapToHammerFar.shouldSprint = false;
            swapToHammerFar.shouldFireEquipment = false;
            swapToHammerFar.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            swapToHammerFar.driverUpdateTimerOverride = .5f;


            // Use primary (hammer) while strafing
            AISkillDriver hammerStrafe = masterObject.AddComponent<AISkillDriver>();
            hammerStrafe.skillSlot = SkillSlot.Primary;
            hammerStrafe.requiredSkill = hammerSkill;
            hammerStrafe.requireSkillReady = true;
            hammerStrafe.requireEquipmentReady = false;
            hammerStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            hammerStrafe.minDistance = 0f;
            hammerStrafe.maxDistance = 5f;
            hammerStrafe.selectionRequiresTargetLoS = false;
            hammerStrafe.activationRequiresTargetLoS = false;
            hammerStrafe.activationRequiresAimConfirmation = false;
            hammerStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            hammerStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            hammerStrafe.ignoreNodeGraph = true;
            hammerStrafe.noRepeat = false;
            hammerStrafe.shouldSprint = false;
            hammerStrafe.shouldFireEquipment = false;
            hammerStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            hammerStrafe.driverUpdateTimerOverride = .5f;


            // Use primary (hammer) while approaching
            AISkillDriver hammerChase = masterObject.AddComponent<AISkillDriver>();
            hammerChase.skillSlot = SkillSlot.Primary;
            hammerChase.requiredSkill = hammerSkill;
            hammerChase.requireSkillReady = true;
            hammerChase.requireEquipmentReady = false;
            hammerChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            hammerChase.minDistance = 0f;
            hammerChase.maxDistance = 10f;
            hammerChase.selectionRequiresTargetLoS = false;
            hammerChase.activationRequiresTargetLoS = false;
            hammerChase.activationRequiresAimConfirmation = false;
            hammerChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            hammerChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            hammerChase.ignoreNodeGraph = true;
            hammerChase.noRepeat = false;
            hammerChase.shouldSprint = false;
            hammerChase.shouldFireEquipment = false;
            hammerChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            hammerChase.driverUpdateTimerOverride = .5f;


            // Use primary (minigun) while strafing
            AISkillDriver minigunStrafe = masterObject.AddComponent<AISkillDriver>();
            minigunStrafe.skillSlot = SkillSlot.Primary;
            minigunStrafe.requiredSkill = minigunSkill;
            minigunStrafe.requireSkillReady = true;
            minigunStrafe.requireEquipmentReady = false;
            minigunStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            minigunStrafe.minDistance = 25f;
            minigunStrafe.maxDistance = 40f;
            minigunStrafe.selectionRequiresTargetLoS = true;
            minigunStrafe.activationRequiresTargetLoS = true;
            minigunStrafe.activationRequiresAimConfirmation = false;
            minigunStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            minigunStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            minigunStrafe.ignoreNodeGraph = false;
            minigunStrafe.noRepeat = false;
            minigunStrafe.shouldSprint = false;
            minigunStrafe.shouldFireEquipment = false;
            minigunStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            minigunStrafe.driverUpdateTimerOverride = 1f;


            // Use primary (minigun) while chasing
            AISkillDriver minigunChase = masterObject.AddComponent<AISkillDriver>();
            minigunChase.skillSlot = SkillSlot.Primary;
            minigunChase.requiredSkill = minigunSkill;
            minigunChase.requireSkillReady = true;
            minigunChase.requireEquipmentReady = false;
            minigunChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            minigunChase.minDistance = 25f;
            minigunChase.maxDistance = 80f;
            minigunChase.selectionRequiresTargetLoS = true;
            minigunChase.activationRequiresTargetLoS = true;
            minigunChase.activationRequiresAimConfirmation = false;
            minigunChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            minigunChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            minigunChase.ignoreNodeGraph = false;
            minigunChase.noRepeat = false;
            minigunChase.shouldSprint = false;
            minigunChase.shouldFireEquipment = false;
            minigunChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            minigunChase.driverUpdateTimerOverride = 1f;


            // Swap to minigun at a distance
            AISkillDriver swapToMinigun = masterObject.AddComponent<AISkillDriver>();
            swapToMinigun.skillSlot = SkillSlot.Special;
            swapToMinigun.requiredSkill = swapToMinigunSkill;
            swapToMinigun.requireSkillReady = true;
            swapToMinigun.requireEquipmentReady = false;
            swapToMinigun.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            swapToMinigun.minDistance = 25f;
            swapToMinigun.maxDistance = 80f;
            swapToMinigun.selectionRequiresTargetLoS = true;
            swapToMinigun.activationRequiresTargetLoS = true;
            swapToMinigun.activationRequiresAimConfirmation = false;
            swapToMinigun.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            swapToMinigun.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            swapToMinigun.ignoreNodeGraph = false;
            swapToMinigun.noRepeat = true;
            swapToMinigun.shouldSprint = false;
            swapToMinigun.shouldFireEquipment = false;
            swapToMinigun.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            swapToMinigun.driverUpdateTimerOverride = .5f;


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


            // Swap to hammer if there are no more enemies (& has a leader)
            AISkillDriver swapToHammerOwner = masterObject.AddComponent<AISkillDriver>();
            swapToHammerOwner.skillSlot = SkillSlot.Special;
            swapToHammerOwner.requiredSkill = swapToHammerSkill;
            swapToHammerOwner.requireSkillReady = true;
            swapToHammerOwner.requireEquipmentReady = false;
            swapToHammerOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            swapToHammerOwner.minDistance = 0f;
            swapToHammerOwner.maxDistance = float.PositiveInfinity;
            swapToHammerOwner.selectionRequiresTargetLoS = false;
            swapToHammerOwner.activationRequiresTargetLoS = false;
            swapToHammerOwner.activationRequiresAimConfirmation = false;
            swapToHammerOwner.movementType = AISkillDriver.MovementType.Stop;
            swapToHammerOwner.aimType = AISkillDriver.AimType.AtCurrentLeader;
            swapToHammerOwner.ignoreNodeGraph = false;
            swapToHammerOwner.noRepeat = true;
            swapToHammerOwner.shouldSprint = false;
            swapToHammerOwner.shouldFireEquipment = false;
            swapToHammerOwner.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            swapToHammerOwner.driverUpdateTimerOverride = .5f;
            swapToHammerOwner.resetCurrentEnemyOnNextDriverSelection = true;


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
            if (body.master && body.bodyIndex == BodyCatalog.FindBodyIndex("NemesisEnforcerBody") && !body.isPlayerControlled)
            {
                if (body.skillLocator && body.skillLocator.utility)
                {
                    // They're forcing my hand. Why are both Enforcer and Nemesis Enforcer's Stun Grenade skills named the exact same?
                    if (body.skillLocator.utility.skillDef != heatCrashSkill && body.skillLocator.utility.skillDef != gasGrenadeSkill)
                    {
                        AISkillDriver[] skillDrivers = body.master.GetComponents<AISkillDriver>();
                        foreach (AISkillDriver skillDriver in skillDrivers)
                        {
                            if (skillDriver.skillSlot == SkillSlot.Utility && skillDriver.requiredSkill == heatCrashSkill)
                            {
                                skillDriver.enabled = false; 
                            }
                        }
                    }
                    else if (body.skillLocator.utility.skillDef == heatCrashSkill) // Utility skill is Heat Crash
                    {
                        AISkillDriver[] skillDrivers = body.master.GetComponents<AISkillDriver>();
                        foreach (AISkillDriver skillDriver in skillDrivers)
                        {
                            if (skillDriver.skillSlot == SkillSlot.Utility && skillDriver.requiredSkill != heatCrashSkill)
                            {
                                skillDriver.enabled = false;
                            }
                        }
                    }
                    
                }
            }
        }
    }
}
