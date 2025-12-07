using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace ImprovedSurvivorAI
{
    class MultAI
    {
        // Asset references
        public static ToolbotWeaponSkillDef nailgunSkill = Addressables.LoadAssetAsync<ToolbotWeaponSkillDef>("RoR2/Base/Toolbot/ToolbotBodyFireNailgun.asset").WaitForCompletion();
        public static ToolbotWeaponSkillDef rebarSkill = Addressables.LoadAssetAsync<ToolbotWeaponSkillDef>("RoR2/Base/Toolbot/ToolbotBodyFireSpear.asset").WaitForCompletion();
        public static ToolbotWeaponSkillDef launcherSkill = Addressables.LoadAssetAsync<ToolbotWeaponSkillDef>("RoR2/Base/Toolbot/ToolbotBodyFireGrenadeLauncher.asset").WaitForCompletion();
        public static ToolbotWeaponSkillDef sawSkill = Addressables.LoadAssetAsync<ToolbotWeaponSkillDef>("RoR2/Base/Toolbot/ToolbotBodyFireBuzzsaw.asset").WaitForCompletion();
        public static SkillDef retoolSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Toolbot/ToolbotBodySwap.asset").WaitForCompletion();
        public static SkillDef powerModeOnSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Toolbot/ToolbotDualWield.asset").WaitForCompletion();

        // Fields
        public static List<CharacterMaster> powerModeUsers = new();

        public MultAI(GameObject masterObject)
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


            // Misc
            On.EntityStates.Toolbot.ToolbotDualWield.OnEnter += ToolbotDualWield_OnEnter;
            On.EntityStates.Toolbot.ToolbotDualWield.OnExit += ToolbotDualWield_OnExit;
            On.RoR2.CharacterAI.BaseAI.UpdateBodyInputs += PowerModeInputs;
            CharacterBody.onBodyStartGlobal += SpecialLoadoutBehavior;


            // Use secondary off cooldown
            AISkillDriver secondary = masterObject.AddComponent<AISkillDriver>();
            secondary.skillSlot = SkillSlot.Secondary;
            secondary.requireSkillReady = true;
            secondary.requireEquipmentReady = false;
            secondary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            secondary.minDistance = 0f;
            secondary.maxDistance = 60f;
            secondary.selectionRequiresTargetLoS = true;
            secondary.activationRequiresTargetLoS = true;
            secondary.activationRequiresAimConfirmation = true;
            //secondary.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            secondary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            secondary.ignoreNodeGraph = false;
            secondary.noRepeat = false;
            secondary.shouldSprint = false;
            secondary.shouldFireEquipment = false;
            secondary.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Use utility off cooldown to ram the target
            AISkillDriver utilityClose = masterObject.AddComponent<AISkillDriver>();
            utilityClose.skillSlot = SkillSlot.Utility;
            utilityClose.requireSkillReady = true;
            utilityClose.requireEquipmentReady = false;
            utilityClose.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utilityClose.minDistance = 0f;
            utilityClose.maxDistance = 25f;
            utilityClose.selectionRequiresTargetLoS = true;
            utilityClose.activationRequiresTargetLoS = true;
            utilityClose.activationRequiresAimConfirmation = false;
            utilityClose.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityClose.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utilityClose.ignoreNodeGraph = true;
            utilityClose.noRepeat = true;
            utilityClose.shouldSprint = true;
            utilityClose.shouldFireEquipment = false;
            utilityClose.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            utilityClose.selectionRequiresTargetNonFlier = true;
            utilityClose.driverUpdateTimerOverride = .5f;


            AISkillDriver powerModeOn = masterObject.AddComponent<AISkillDriver>();
            powerModeOn.skillSlot = SkillSlot.Special;
            powerModeOn.requiredSkill = powerModeOnSkill;
            powerModeOn.requireSkillReady = true;
            powerModeOn.requireEquipmentReady = false;
            powerModeOn.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            powerModeOn.minDistance = 0f;
            powerModeOn.maxDistance = 60f;
            powerModeOn.selectionRequiresTargetLoS = true;
            powerModeOn.activationRequiresTargetLoS = true;
            powerModeOn.activationRequiresAimConfirmation = false;
            //powerModeOn.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            powerModeOn.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            powerModeOn.ignoreNodeGraph = false;
            powerModeOn.noRepeat = true;
            powerModeOn.shouldSprint = false;
            powerModeOn.shouldFireEquipment = false;
            powerModeOn.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use utility off cooldown to move around
            AISkillDriver utilityFar = masterObject.AddComponent<AISkillDriver>();
            utilityFar.skillSlot = SkillSlot.Utility;
            utilityFar.requireSkillReady = true;
            utilityFar.requireEquipmentReady = false;
            utilityFar.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utilityFar.minDistance = 60f;
            utilityFar.maxDistance = float.PositiveInfinity;
            utilityFar.selectionRequiresTargetLoS = false;
            utilityFar.activationRequiresTargetLoS = false;
            utilityFar.activationRequiresAimConfirmation = false;
            //utilityFar.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityFar.aimType = AISkillDriver.AimType.MoveDirection;
            utilityFar.ignoreNodeGraph = false;
            utilityFar.noRepeat = true;
            utilityFar.shouldSprint = true;
            utilityFar.shouldFireEquipment = false;
            utilityFar.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            utilityFar.selectionRequiresOnGround = true;
            utilityFar.driverUpdateTimerOverride = .5f;


            // Nailgun while retreating (point-blank)
            AISkillDriver nailgunRetreat = masterObject.AddComponent<AISkillDriver>();
            nailgunRetreat.skillSlot = SkillSlot.Primary;
            nailgunRetreat.requiredSkill = nailgunSkill;
            nailgunRetreat.requireSkillReady = true;
            nailgunRetreat.requireEquipmentReady = false;
            nailgunRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            nailgunRetreat.minDistance = 0f;
            nailgunRetreat.maxDistance = 10f;
            nailgunRetreat.selectionRequiresTargetLoS = true;
            nailgunRetreat.activationRequiresTargetLoS = true;
            nailgunRetreat.activationRequiresAimConfirmation = false;
            nailgunRetreat.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            nailgunRetreat.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            nailgunRetreat.ignoreNodeGraph = false;
            nailgunRetreat.noRepeat = false;
            nailgunRetreat.shouldSprint = false;
            nailgunRetreat.shouldFireEquipment = false;
            nailgunRetreat.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            nailgunRetreat.driverUpdateTimerOverride = .5f;


            // Nailgun while strafing (close)
            AISkillDriver nailgunStrafe = masterObject.AddComponent<AISkillDriver>();
            nailgunStrafe.skillSlot = SkillSlot.Primary;
            nailgunStrafe.requiredSkill = nailgunSkill;
            nailgunStrafe.requireSkillReady = true;
            nailgunStrafe.requireEquipmentReady = false;
            nailgunStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            nailgunStrafe.minDistance = 0f;
            nailgunStrafe.maxDistance = 30f;
            nailgunStrafe.selectionRequiresTargetLoS = true;
            nailgunStrafe.activationRequiresTargetLoS = true;
            nailgunStrafe.activationRequiresAimConfirmation = false;
            nailgunStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            nailgunStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            nailgunStrafe.ignoreNodeGraph = false;
            nailgunStrafe.noRepeat = false;
            nailgunStrafe.shouldSprint = false;
            nailgunStrafe.shouldFireEquipment = false;
            nailgunStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            nailgunStrafe.driverUpdateTimerOverride = .5f;


            // Nailgun while approaching (far)
            AISkillDriver nailgunChase = masterObject.AddComponent<AISkillDriver>();
            nailgunChase.skillSlot = SkillSlot.Primary;
            nailgunChase.requiredSkill = nailgunSkill;
            nailgunChase.requireSkillReady = true;
            nailgunChase.requireEquipmentReady = false;
            nailgunChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            nailgunChase.minDistance = 0f;
            nailgunChase.maxDistance = 60f;
            nailgunChase.selectionRequiresTargetLoS = true;
            nailgunChase.activationRequiresTargetLoS = true;
            nailgunChase.activationRequiresAimConfirmation = false;
            nailgunChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            nailgunChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            nailgunChase.ignoreNodeGraph = false;
            nailgunChase.noRepeat = false;
            nailgunChase.shouldSprint = false;
            nailgunChase.shouldFireEquipment = false;
            nailgunChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            nailgunChase.driverUpdateTimerOverride = .5f;


            // Rebar while retreating (close)
            AISkillDriver rebarRetreat = masterObject.AddComponent<AISkillDriver>();
            rebarRetreat.skillSlot = SkillSlot.Primary;
            rebarRetreat.requiredSkill = rebarSkill;
            rebarRetreat.requireSkillReady = true;
            rebarRetreat.requireEquipmentReady = false;
            rebarRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            rebarRetreat.minDistance = 0f;
            rebarRetreat.maxDistance = 20f;
            rebarRetreat.selectionRequiresTargetLoS = true;
            rebarRetreat.activationRequiresTargetLoS = true;
            rebarRetreat.activationRequiresAimConfirmation = true;
            rebarRetreat.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            rebarRetreat.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            rebarRetreat.ignoreNodeGraph = false;
            rebarRetreat.noRepeat = false;
            rebarRetreat.shouldSprint = false;
            rebarRetreat.shouldFireEquipment = false;
            rebarRetreat.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            rebarRetreat.driverUpdateTimerOverride = .5f;


            // Rebar while strafing (far)
            AISkillDriver rebarStrafe = masterObject.AddComponent<AISkillDriver>();
            rebarStrafe.skillSlot = SkillSlot.Primary;
            rebarStrafe.requiredSkill = rebarSkill;
            rebarStrafe.requireSkillReady = true;
            rebarStrafe.requireEquipmentReady = false;
            rebarStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            rebarStrafe.minDistance = 0f;
            rebarStrafe.maxDistance = 60f;
            rebarStrafe.selectionRequiresTargetLoS = true;
            rebarStrafe.activationRequiresTargetLoS = true;
            rebarStrafe.activationRequiresAimConfirmation = true;
            rebarStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            rebarStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            rebarStrafe.ignoreNodeGraph = false;
            rebarStrafe.noRepeat = false;
            rebarStrafe.shouldSprint = false;
            rebarStrafe.shouldFireEquipment = false;
            rebarStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            rebarStrafe.driverUpdateTimerOverride = .5f;


            // Rebar while approaching (very far)
            AISkillDriver rebarChase = masterObject.AddComponent<AISkillDriver>();
            rebarChase.skillSlot = SkillSlot.Primary;
            rebarChase.requiredSkill = rebarSkill;
            rebarChase.requireSkillReady = true;
            rebarChase.requireEquipmentReady = false;
            rebarChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            rebarChase.minDistance = 0f;
            rebarChase.maxDistance = 300f;
            rebarChase.selectionRequiresTargetLoS = true;
            rebarChase.activationRequiresTargetLoS = true;
            rebarChase.activationRequiresAimConfirmation = true;
            rebarChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            rebarChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            rebarChase.ignoreNodeGraph = false;
            rebarChase.noRepeat = false;
            rebarChase.shouldSprint = false;
            rebarChase.shouldFireEquipment = false;
            rebarChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            rebarChase.driverUpdateTimerOverride = .5f;


            // Scrap Launcher while retreating (point-blank)
            AISkillDriver launcherRetreat = masterObject.AddComponent<AISkillDriver>();
            launcherRetreat.skillSlot = SkillSlot.Primary;
            launcherRetreat.requiredSkill = launcherSkill;
            launcherRetreat.requireSkillReady = false;
            launcherRetreat.requireEquipmentReady = false;
            launcherRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            launcherRetreat.minDistance = 0f;
            launcherRetreat.maxDistance = 10f;
            launcherRetreat.selectionRequiresTargetLoS = true;
            launcherRetreat.activationRequiresTargetLoS = true;
            launcherRetreat.activationRequiresAimConfirmation = true;
            launcherRetreat.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            launcherRetreat.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            launcherRetreat.ignoreNodeGraph = false;
            launcherRetreat.noRepeat = false;
            launcherRetreat.shouldSprint = false;
            launcherRetreat.shouldFireEquipment = false;
            launcherRetreat.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            launcherRetreat.driverUpdateTimerOverride = .5f;


            // Scrap Launcher while strafing
            AISkillDriver launcherStrafe = masterObject.AddComponent<AISkillDriver>();
            launcherStrafe.skillSlot = SkillSlot.Primary;
            launcherStrafe.requiredSkill = launcherSkill;
            launcherStrafe.requireSkillReady = false;
            launcherStrafe.requireEquipmentReady = false;
            launcherStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            launcherStrafe.minDistance = 0f;
            launcherStrafe.maxDistance = 50f;
            launcherStrafe.selectionRequiresTargetLoS = true;
            launcherStrafe.activationRequiresTargetLoS = true;
            launcherStrafe.activationRequiresAimConfirmation = true;
            launcherStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            launcherStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            launcherStrafe.ignoreNodeGraph = false;
            launcherStrafe.noRepeat = false;
            launcherStrafe.shouldSprint = false;
            launcherStrafe.shouldFireEquipment = false;
            launcherStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            launcherStrafe.driverUpdateTimerOverride = .5f;


            // Scrap Launcher while approaching (far)
            AISkillDriver launcherChase = masterObject.AddComponent<AISkillDriver>();
            launcherChase.skillSlot = SkillSlot.Primary;
            launcherChase.requiredSkill = launcherSkill;
            launcherChase.requireSkillReady = false;
            launcherChase.requireEquipmentReady = false;
            launcherChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            launcherChase.minDistance = 0f;
            launcherChase.maxDistance = 150f;
            launcherChase.selectionRequiresTargetLoS = true;
            launcherChase.activationRequiresTargetLoS = true;
            launcherChase.activationRequiresAimConfirmation = true;
            launcherChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            launcherChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            launcherChase.ignoreNodeGraph = false;
            launcherChase.noRepeat = false;
            launcherChase.shouldSprint = false;
            launcherChase.shouldFireEquipment = false;
            launcherChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            launcherChase.driverUpdateTimerOverride = .5f;


            // Saw while approaching (point-blank)
            AISkillDriver sawMelee = masterObject.AddComponent<AISkillDriver>();
            sawMelee.skillSlot = SkillSlot.Primary;
            sawMelee.requiredSkill = sawSkill;
            sawMelee.requireSkillReady = true;
            sawMelee.requireEquipmentReady = false;
            sawMelee.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sawMelee.minDistance = 0f;
            sawMelee.maxDistance = 10f;
            sawMelee.selectionRequiresTargetLoS = false;
            sawMelee.activationRequiresTargetLoS = false;
            sawMelee.activationRequiresAimConfirmation = false;
            sawMelee.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            sawMelee.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sawMelee.ignoreNodeGraph = true;
            sawMelee.noRepeat = false;
            sawMelee.shouldSprint = true;
            sawMelee.shouldFireEquipment = false;
            sawMelee.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            sawMelee.driverUpdateTimerOverride = .5f;


            // Saw while approaching
            AISkillDriver sawChase = masterObject.AddComponent<AISkillDriver>();
            sawChase.skillSlot = SkillSlot.Primary;
            sawChase.requiredSkill = sawSkill;
            sawChase.requireSkillReady = true;
            sawChase.requireEquipmentReady = false;
            sawChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sawChase.minDistance = 0f;
            sawChase.maxDistance = 400f;
            sawChase.selectionRequiresTargetLoS = false;
            sawChase.activationRequiresTargetLoS = false;
            sawChase.activationRequiresAimConfirmation = false;
            sawChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            sawChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sawChase.ignoreNodeGraph = false;
            sawChase.noRepeat = false;
            sawChase.shouldSprint = true;
            sawChase.shouldFireEquipment = false;
            sawChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            sawChase.resetCurrentEnemyOnNextDriverSelection = true;
            sawChase.driverUpdateTimerOverride = .5f;


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
            utilityOwner.activationRequiresAimConfirmation = true;
            utilityOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityOwner.aimType = AISkillDriver.AimType.MoveDirection;
            utilityOwner.ignoreNodeGraph = false;
            utilityOwner.noRepeat = true;
            utilityOwner.shouldSprint = true;
            utilityOwner.shouldFireEquipment = false;
            utilityOwner.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            utilityOwner.resetCurrentEnemyOnNextDriverSelection = true;
            utilityOwner.selectionRequiresOnGround = true;
            utilityOwner.driverUpdateTimerOverride = .5f;


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


            // Use Retool after dashing
            AISkillDriver retool = masterObject.AddComponent<AISkillDriver>();
            retool.skillSlot = SkillSlot.Special;
            retool.requiredSkill = retoolSkill;
            retool.requireSkillReady = true;
            retool.requireEquipmentReady = false;
            retool.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            retool.minDistance = 0f;
            retool.maxDistance = float.PositiveInfinity;
            retool.selectionRequiresTargetLoS = false;
            retool.activationRequiresTargetLoS = false;
            retool.activationRequiresAimConfirmation = false;
            //retool.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            retool.aimType = AISkillDriver.AimType.AtMoveTarget;
            retool.ignoreNodeGraph = false;
            retool.noRepeat = true;
            retool.shouldSprint = true;
            retool.shouldFireEquipment = false;
            retool.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            retool.driverUpdateTimerOverride = .4f;


            // Overrides
            utilityFar.nextHighPriorityOverride = retool;
        }

        // Register any MUL-T that currently has Power Mode active
        private void ToolbotDualWield_OnEnter(On.EntityStates.Toolbot.ToolbotDualWield.orig_OnEnter orig, EntityStates.Toolbot.ToolbotDualWield self)
        {
            orig(self);

            if (self.characterBody)
            {
                CharacterMaster master = self.characterBody.master;
                if (master.masterIndex == MasterCatalog.FindMasterIndex("ToolbotMonsterMaster"))
                {
                    if (!powerModeUsers.Contains(master))
                    {
                        powerModeUsers.Add(master);

                        AISkillDriver[] skillDrivers = master.GetComponents<AISkillDriver>();
                        foreach (AISkillDriver skillDriver in skillDrivers)
                        {
                            if (skillDriver.skillSlot == SkillSlot.Utility && skillDriver.skillSlot == SkillSlot.Secondary)
                            {
                                skillDriver.enabled = false;  // Disable Utility & Secondary skill drivers from being selected while active
                            };
                        }
                    }
                }
            }
        }

        private void ToolbotDualWield_OnExit(On.EntityStates.Toolbot.ToolbotDualWield.orig_OnExit orig, EntityStates.Toolbot.ToolbotDualWield self)
        {
            orig(self);

            if (self.characterBody)
            {
                CharacterMaster master = self.characterBody.master;
                if (powerModeUsers.Contains(master))
                {
                    powerModeUsers.Remove(master);

                    AISkillDriver[] skillDrivers = master.GetComponents<AISkillDriver>();
                    foreach (AISkillDriver skillDriver in skillDrivers)
                    {
                        if (skillDriver.skillSlot == SkillSlot.Utility && skillDriver.skillSlot == SkillSlot.Secondary)
                        {
                            skillDriver.enabled = true;  // Re-enable the disabled skill drivers
                        };
                    }
                }
            }
        }

        // Hold down both Primary & Secondary while Power Mode is active
        private void PowerModeInputs(On.RoR2.CharacterAI.BaseAI.orig_UpdateBodyInputs orig, BaseAI self)
        {
            orig(self);

            foreach (CharacterMaster powerModeUser in powerModeUsers)
            {
                BaseAI ai = powerModeUser.GetComponent<BaseAI>();
                if (ai && ai.bodyInputBank)
                {
                    ai.bodyInputBank.skill1.PushState(true);
                    ai.bodyInputBank.skill2.PushState(true);
                }
            }
        }

        // Special cases for special loadouts
        private void SpecialLoadoutBehavior(CharacterBody body)
        {
            if (body.master && body.bodyIndex == BodyCatalog.FindBodyIndex("ToolbotBody") && !body.isPlayerControlled)
            {
                Loadout.BodyLoadoutManager skillLoadout = body.master.loadout.bodyLoadoutManager;
                AISkillDriver[] skillDrivers = body.master.GetComponents<AISkillDriver>();

                if (skillLoadout == null)
                {
                    return;
                }

                uint primary = skillLoadout.GetSkillVariant(body.bodyIndex, 0);
                uint misc = skillLoadout.GetSkillVariant(body.bodyIndex, 1);
                uint secondary = skillLoadout.GetSkillVariant(body.bodyIndex, 2);
                uint utility = skillLoadout.GetSkillVariant(body.bodyIndex, 3);
                uint special = skillLoadout.GetSkillVariant(body.bodyIndex, 4);

                // Adjust Power Mode distances
                if (special == 1)
                {
                    float activationDistance;

                    switch (misc)
                    {
                        case 1: // Rebar in the off-hand
                            activationDistance = 300f;
                            break;

                        case 2: // Scrap Launcher in the off-hand
                            activationDistance = 150f;
                            break;

                        case 0: // Nailgun in the off-hand
                            activationDistance = 60f;
                            break;

                        case 3: // Saw in the off-hand
                            activationDistance = 10f;
                            break;

                        default:
                            activationDistance = 60f;
                            break;
                    }

                    foreach (AISkillDriver skillDriver in skillDrivers)
                    {
                        if (skillDriver.requiredSkill == powerModeOnSkill)
                        {
                            skillDriver.maxDistance = activationDistance;
                        };
                    }
                }

                // Double Rebar + Retool loadout
                if (primary == 1 && misc == 1 && special == 0)
                {
                    AISkillDriver retoolBehavior = null;
                    foreach (AISkillDriver skillDriver in skillDrivers)
                    {
                        if (skillDriver.requiredSkill == retoolSkill)
                        {
                            retoolBehavior = skillDriver;
                        };
                    }

                    if (retoolBehavior != null)
                    {
                        retoolBehavior.movementType = AISkillDriver.MovementType.StrafeMovetarget;
                        retoolBehavior.shouldSprint = false;

                        foreach (AISkillDriver skillDriver in skillDrivers)
                        {
                            if (skillDriver.requiredSkill == rebarSkill)
                            {
                                skillDriver.nextHighPriorityOverride = retoolBehavior; // Retool after every Rebar
                                skillDriver.activationRequiresAimConfirmation = false;
                                skillDriver.driverUpdateTimerOverride = -1f;
                            };
                        }
                    } 
                }
            }
        }
    }
}
