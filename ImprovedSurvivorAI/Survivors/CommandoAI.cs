using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class CommandoAI
    {
        // Asset references
        public static SkillDef phaseRoundSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Commando/CommandoBodyFireFMJ.asset").WaitForCompletion();
        public static SkillDef phaseBlastSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Commando/CommandoBodyFireShotgunBlast.asset").WaitForCompletion();
        public static SkillDef suppressiveFireSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Commando/CommandoBodyBarrage.asset").WaitForCompletion();
        public static SkillDef fragGrenadeSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Commando/ThrowGrenade.asset").WaitForCompletion();

        public CommandoAI(GameObject masterObject)
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


            // Use utility off cooldown towards move direction
            AISkillDriver utility = masterObject.AddComponent<AISkillDriver>();
            utility.skillSlot = SkillSlot.Utility;
            utility.requireSkillReady = true;
            utility.requireEquipmentReady = false;
            utility.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utility.minDistance = 0f;
            utility.maxDistance = float.PositiveInfinity;
            utility.selectionRequiresTargetLoS = false;
            utility.activationRequiresTargetLoS = false;
            utility.activationRequiresAimConfirmation = false;
            //utility.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            utility.aimType = AISkillDriver.AimType.MoveDirection;
            utility.ignoreNodeGraph = false;
            utility.noRepeat = true;
            utility.shouldSprint = true;
            utility.shouldFireEquipment = false;
            utility.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            utility.selectionRequiresOnGround = true;


            // Use Phase Blast off cooldown when close enough
            AISkillDriver phaseBlast = masterObject.AddComponent<AISkillDriver>();
            phaseBlast.skillSlot = SkillSlot.Secondary;
            phaseBlast.requiredSkill = phaseBlastSkill;
            phaseBlast.requireSkillReady = true;
            phaseBlast.requireEquipmentReady = false;
            phaseBlast.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            phaseBlast.minDistance = 0f;
            phaseBlast.maxDistance = 25f;
            phaseBlast.selectionRequiresTargetLoS = true;
            phaseBlast.activationRequiresTargetLoS = true;
            phaseBlast.activationRequiresAimConfirmation = true;
            phaseBlast.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            phaseBlast.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            phaseBlast.ignoreNodeGraph = false;
            phaseBlast.noRepeat = false;
            phaseBlast.shouldSprint = false;
            phaseBlast.shouldFireEquipment = false;
            phaseBlast.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Suppressive Fire off cooldown (point-blank - retreat)
            AISkillDriver barrageVeryClose = masterObject.AddComponent<AISkillDriver>();
            barrageVeryClose.skillSlot = SkillSlot.Special;
            barrageVeryClose.requiredSkill = suppressiveFireSkill;
            barrageVeryClose.requireSkillReady = true;
            barrageVeryClose.requireEquipmentReady = false;
            barrageVeryClose.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            barrageVeryClose.minDistance = 0f;
            barrageVeryClose.maxDistance = 10f;
            barrageVeryClose.selectionRequiresTargetLoS = true;
            barrageVeryClose.activationRequiresTargetLoS = true;
            barrageVeryClose.activationRequiresAimConfirmation = true;
            barrageVeryClose.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            barrageVeryClose.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            barrageVeryClose.ignoreNodeGraph = false;
            barrageVeryClose.noRepeat = false;
            barrageVeryClose.shouldSprint = false;
            barrageVeryClose.shouldFireEquipment = false;
            barrageVeryClose.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            barrageVeryClose.driverUpdateTimerOverride = .7f;


            // Use Suppressive Fire off cooldown (close - strafe)
            AISkillDriver barrageClose = masterObject.AddComponent<AISkillDriver>();
            barrageClose.skillSlot = SkillSlot.Special;
            barrageClose.requiredSkill = suppressiveFireSkill;
            barrageClose.requireSkillReady = true;
            barrageClose.requireEquipmentReady = false;
            barrageClose.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            barrageClose.minDistance = 0f;
            barrageClose.maxDistance = 30f;
            barrageClose.selectionRequiresTargetLoS = true;
            barrageClose.activationRequiresTargetLoS = true;
            barrageClose.activationRequiresAimConfirmation = true;
            barrageClose.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            barrageClose.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            barrageClose.ignoreNodeGraph = false;
            barrageClose.noRepeat = false;
            barrageClose.shouldSprint = false;
            barrageClose.shouldFireEquipment = false;
            barrageClose.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            barrageClose.driverUpdateTimerOverride = .7f;


            // Use Suppressive Fire off cooldown (far - chase)
            AISkillDriver barrageFar = masterObject.AddComponent<AISkillDriver>();
            barrageFar.skillSlot = SkillSlot.Special;
            barrageFar.requiredSkill = suppressiveFireSkill;
            barrageFar.requireSkillReady = true;
            barrageFar.requireEquipmentReady = false;
            barrageFar.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            barrageFar.minDistance = 0f;
            barrageFar.maxDistance = 150f;
            barrageFar.selectionRequiresTargetLoS = true;
            barrageFar.activationRequiresTargetLoS = true;
            barrageFar.activationRequiresAimConfirmation = true;
            barrageFar.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            barrageFar.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            barrageFar.ignoreNodeGraph = false;
            barrageFar.noRepeat = false;
            barrageFar.shouldSprint = false;
            barrageFar.shouldFireEquipment = false;
            barrageFar.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            barrageFar.driverUpdateTimerOverride = .7f;


            // Use Frag Grenade off cooldown
            AISkillDriver grenade = masterObject.AddComponent<AISkillDriver>();
            grenade.skillSlot = SkillSlot.Special;
            grenade.requiredSkill = fragGrenadeSkill;
            grenade.requireSkillReady = true;
            grenade.requireEquipmentReady = false;
            grenade.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            grenade.minDistance = 10f;
            grenade.maxDistance = 60f;
            grenade.selectionRequiresTargetLoS = true;
            grenade.activationRequiresTargetLoS = true;
            grenade.activationRequiresAimConfirmation = true;
            grenade.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            grenade.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            grenade.ignoreNodeGraph = false;
            grenade.noRepeat = true;
            grenade.shouldSprint = false;
            grenade.shouldFireEquipment = false;
            grenade.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Phase Round off cooldown; ignores LoS
            AISkillDriver phaseRound = masterObject.AddComponent<AISkillDriver>();
            phaseRound.skillSlot = SkillSlot.Secondary;
            phaseRound.requiredSkill = phaseRoundSkill;
            phaseRound.requireSkillReady = true;
            phaseRound.requireEquipmentReady = false;
            phaseRound.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            phaseRound.minDistance = 0f;
            phaseRound.maxDistance = 150f;
            phaseRound.selectionRequiresTargetLoS = false;
            phaseRound.activationRequiresTargetLoS = false;
            phaseRound.activationRequiresAimConfirmation = true;
            phaseRound.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            phaseRound.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            phaseRound.ignoreNodeGraph = false;
            phaseRound.noRepeat = false;
            phaseRound.shouldSprint = false;
            phaseRound.shouldFireEquipment = false;
            phaseRound.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


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


            // Shoot primary while strafing (close)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 40f;
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
            primaryChase.maxDistance = 120f;
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
            utilityOwner.activationRequiresAimConfirmation = false;
            utilityOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            utilityOwner.aimType = AISkillDriver.AimType.MoveDirection;
            utilityOwner.ignoreNodeGraph = false;
            utilityOwner.noRepeat = false;
            utilityOwner.shouldSprint = true;
            utilityOwner.shouldFireEquipment = false;
            utilityOwner.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            utilityOwner.resetCurrentEnemyOnNextDriverSelection = true;
            utilityOwner.selectionRequiresOnGround = true;


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
    }
}
