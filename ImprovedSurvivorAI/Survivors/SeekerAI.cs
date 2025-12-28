using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class SeekerAI
    {
        // Asset references
        public static SkillDef unseenHandSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Seeker/SeekerBodyUnseenHand.asset").WaitForCompletion();
        public static SkillDef soulSpiralSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Seeker/SeekerBodySoulSpiral.asset").WaitForCompletion();
        public static SkillDef sojournSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Seeker/SeekerBodySojourn.asset").WaitForCompletion();
        public static SkillDef reprieveSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Seeker/SeekerBodyReprieve.asset").WaitForCompletion();
        public static SkillDef meditateSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Seeker/SeekerBodyMeditate.asset").WaitForCompletion();
        public static SkillDef palmBlastSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Seeker/SeekerPalmBlast.asset").WaitForCompletion();

        public SeekerAI(GameObject masterObject)
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
            CharacterMaster.onCharacterMasterDiscovered += CharacterMaster_onCharacterMasterDiscovered;
            On.EntityStates.Seeker.Meditate.OnEnter += HandleMeditateInputs;
            On.EntityStates.Seeker.Meditate.UpdateUIInputSequence += NoMeditateErrors;


            // Cancel Sojourn when sufficiently injured
            /*AISkillDriver cancelSojournLowHP = masterObject.AddComponent<AISkillDriver>();
            cancelSojournLowHP.skillSlot = SkillSlot.Utility;
            cancelSojournLowHP.requiredSkill = sojournSkill;
            cancelSojournLowHP.requireSkillReady = false;
            cancelSojournLowHP.requireEquipmentReady = false;
            cancelSojournLowHP.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            cancelSojournLowHP.minDistance = 0f;
            cancelSojournLowHP.maxDistance = float.PositiveInfinity;
            cancelSojournLowHP.selectionRequiresTargetLoS = false;
            cancelSojournLowHP.activationRequiresTargetLoS = false;
            cancelSojournLowHP.activationRequiresAimConfirmation = false;
            cancelSojournLowHP.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            cancelSojournLowHP.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            cancelSojournLowHP.ignoreNodeGraph = true;
            cancelSojournLowHP.noRepeat = false;
            cancelSojournLowHP.shouldSprint = true;
            cancelSojournLowHP.shouldFireEquipment = false;
            cancelSojournLowHP.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            cancelSojournLowHP.maxUserHealthFraction = .5f;*/


            // Cancel Sojourn when sufficiently close
            /*AISkillDriver cancelSojourn = masterObject.AddComponent<AISkillDriver>();
            cancelSojourn.skillSlot = SkillSlot.Utility;
            cancelSojourn.requiredSkill = sojournSkill;
            cancelSojourn.requireSkillReady = false;
            cancelSojourn.requireEquipmentReady = false;
            cancelSojourn.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            cancelSojourn.minDistance = 0f;
            cancelSojourn.maxDistance = 15f;
            cancelSojourn.selectionRequiresTargetLoS = false;
            cancelSojourn.activationRequiresTargetLoS = false;
            cancelSojourn.activationRequiresAimConfirmation = false;
            cancelSojourn.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            cancelSojourn.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            cancelSojourn.ignoreNodeGraph = true;
            cancelSojourn.noRepeat = false;
            cancelSojourn.shouldSprint = true;
            cancelSojourn.shouldFireEquipment = false;
            cancelSojourn.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;*/


            // Cancel Reprieve when sufficiently close
            /*AISkillDriver cancelReprieve = masterObject.AddComponent<AISkillDriver>();
            cancelReprieve.skillSlot = SkillSlot.Utility;
            cancelReprieve.requiredSkill = reprieveSkill;
            cancelReprieve.requireSkillReady = false;
            cancelReprieve.requireEquipmentReady = false;
            cancelReprieve.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            cancelReprieve.minDistance = 0f;
            cancelReprieve.maxDistance = 20f;
            cancelReprieve.selectionRequiresTargetLoS = false;
            cancelReprieve.activationRequiresTargetLoS = false;
            cancelReprieve.activationRequiresAimConfirmation = false;
            cancelReprieve.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            cancelReprieve.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            cancelReprieve.ignoreNodeGraph = true;
            cancelReprieve.noRepeat = false;
            cancelReprieve.shouldSprint = true;
            cancelReprieve.shouldFireEquipment = false;
            cancelReprieve.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;*/


            // Use Meditate off cooldown
            AISkillDriver meditate = masterObject.AddComponent<AISkillDriver>();
            meditate.skillSlot = SkillSlot.Special;
            meditate.requiredSkill = meditateSkill;
            meditate.requireSkillReady = true;
            meditate.requireEquipmentReady = false;
            meditate.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            meditate.minDistance = 0f;
            meditate.maxDistance = float.PositiveInfinity;
            meditate.selectionRequiresTargetLoS = false;
            meditate.activationRequiresTargetLoS = false;
            meditate.activationRequiresAimConfirmation = false;
            meditate.movementType = AISkillDriver.MovementType.Stop;
            meditate.aimType = AISkillDriver.AimType.None;
            meditate.ignoreNodeGraph = false;
            meditate.noRepeat = true;
            meditate.shouldSprint = false;
            meditate.shouldFireEquipment = false;
            meditate.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Use Meditate off cooldown (but owner)
            AISkillDriver meditateOwner = masterObject.AddComponent<AISkillDriver>();
            meditateOwner.skillSlot = SkillSlot.Special;
            meditateOwner.requiredSkill = meditateSkill;
            meditateOwner.requireSkillReady = true;
            meditateOwner.requireEquipmentReady = false;
            meditateOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            meditateOwner.minDistance = 0f;
            meditateOwner.maxDistance = float.PositiveInfinity;
            meditateOwner.selectionRequiresTargetLoS = false;
            meditateOwner.activationRequiresTargetLoS = false;
            meditateOwner.activationRequiresAimConfirmation = false;
            meditateOwner.movementType = AISkillDriver.MovementType.Stop;
            meditateOwner.aimType = AISkillDriver.AimType.None;
            meditateOwner.ignoreNodeGraph = false;
            meditateOwner.noRepeat = true;
            meditateOwner.shouldSprint = false;
            meditateOwner.shouldFireEquipment = false;
            meditateOwner.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Use Palm Blast off cooldown
            AISkillDriver palmBlast = masterObject.AddComponent<AISkillDriver>();
            palmBlast.skillSlot = SkillSlot.Special;
            palmBlast.requiredSkill = palmBlastSkill;
            palmBlast.requireSkillReady = true;
            palmBlast.requireEquipmentReady = false;
            palmBlast.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            palmBlast.minDistance = 0f;
            palmBlast.maxDistance = 100f;
            palmBlast.selectionRequiresTargetLoS = false;
            palmBlast.activationRequiresTargetLoS = false;
            palmBlast.activationRequiresAimConfirmation = true;
            palmBlast.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            palmBlast.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            palmBlast.ignoreNodeGraph = false;
            palmBlast.noRepeat = true;
            palmBlast.shouldSprint = false;
            palmBlast.shouldFireEquipment = false;
            palmBlast.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            palmBlast.driverUpdateTimerOverride = 1f;


            // Use Palm Blast off cooldown to heal the nearest ally
            AISkillDriver palmBlastFriend = masterObject.AddComponent<AISkillDriver>();
            palmBlastFriend.skillSlot = SkillSlot.Special;
            palmBlastFriend.requiredSkill = palmBlastSkill;
            palmBlastFriend.requireSkillReady = true;
            palmBlastFriend.requireEquipmentReady = false;
            palmBlastFriend.moveTargetType = AISkillDriver.TargetType.NearestFriendlyInSkillRange;
            palmBlastFriend.minDistance = 0f;
            palmBlastFriend.maxDistance = 100f;
            palmBlastFriend.selectionRequiresTargetLoS = false;
            palmBlastFriend.activationRequiresTargetLoS = false;
            palmBlastFriend.activationRequiresAimConfirmation = true;
            palmBlastFriend.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            palmBlastFriend.aimType = AISkillDriver.AimType.AtMoveTarget;
            palmBlastFriend.ignoreNodeGraph = false;
            palmBlastFriend.noRepeat = true;
            palmBlastFriend.shouldSprint = false;
            palmBlastFriend.shouldFireEquipment = false;
            palmBlastFriend.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            palmBlastFriend.driverUpdateTimerOverride = 1f;


            // Use Unseen Hand off cooldown
            AISkillDriver unseenHand = masterObject.AddComponent<AISkillDriver>();
            unseenHand.skillSlot = SkillSlot.Secondary;
            unseenHand.requiredSkill = unseenHandSkill;
            unseenHand.requireSkillReady = true;
            unseenHand.requireEquipmentReady = false;
            unseenHand.moveTargetType = AISkillDriver.TargetType.Custom;
            unseenHand.minDistance = 0f;
            unseenHand.maxDistance = 300f;
            unseenHand.selectionRequiresTargetLoS = false;
            unseenHand.activationRequiresTargetLoS = false;
            unseenHand.activationRequiresAimConfirmation = false;
            unseenHand.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            unseenHand.aimType = AISkillDriver.AimType.AtMoveTarget;
            unseenHand.ignoreNodeGraph = false;
            unseenHand.noRepeat = true;
            unseenHand.shouldSprint = false;
            unseenHand.shouldFireEquipment = false;
            unseenHand.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            unseenHand.driverUpdateTimerOverride = .5f;


            // Use Soul Spiral off cooldown
            AISkillDriver soulSpiral = masterObject.AddComponent<AISkillDriver>();
            soulSpiral.skillSlot = SkillSlot.Secondary;
            soulSpiral.requiredSkill = soulSpiralSkill;
            soulSpiral.requireSkillReady = true;
            soulSpiral.requireEquipmentReady = false;
            soulSpiral.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            soulSpiral.minDistance = 0f;
            soulSpiral.maxDistance = float.PositiveInfinity;
            soulSpiral.selectionRequiresTargetLoS = false;
            soulSpiral.activationRequiresTargetLoS = false;
            soulSpiral.activationRequiresAimConfirmation = false;
            soulSpiral.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            soulSpiral.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            soulSpiral.ignoreNodeGraph = false;
            soulSpiral.noRepeat = false;
            soulSpiral.shouldSprint = true;
            soulSpiral.shouldFireEquipment = false;
            soulSpiral.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Sojourn off cooldown
            /*AISkillDriver sojourn = masterObject.AddComponent<AISkillDriver>();
            sojourn.skillSlot = SkillSlot.Utility;
            sojourn.requiredSkill = sojournSkill;
            sojourn.requireSkillReady = true;
            sojourn.requireEquipmentReady = false;
            sojourn.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sojourn.minDistance = 0f;
            sojourn.maxDistance = 150f;
            sojourn.selectionRequiresTargetLoS = true;
            sojourn.activationRequiresTargetLoS = true;
            sojourn.activationRequiresAimConfirmation = true;
            sojourn.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            sojourn.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sojourn.ignoreNodeGraph = true;
            sojourn.noRepeat = true;
            sojourn.shouldSprint = true;
            sojourn.shouldFireEquipment = false;
            sojourn.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            sojourn.driverUpdateTimerOverride = 1.5f;*/


            // Use Reprieve off cooldown
            AISkillDriver reprieve = masterObject.AddComponent<AISkillDriver>();
            reprieve.skillSlot = SkillSlot.Utility;
            reprieve.requiredSkill = reprieveSkill;
            reprieve.requireSkillReady = true;
            reprieve.requireEquipmentReady = false;
            reprieve.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            reprieve.minDistance = 0f;
            reprieve.maxDistance = 300f;
            reprieve.selectionRequiresTargetLoS = true;
            reprieve.activationRequiresTargetLoS = true;
            reprieve.activationRequiresAimConfirmation = true;
            reprieve.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            reprieve.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            reprieve.ignoreNodeGraph = true;
            reprieve.noRepeat = true;
            reprieve.shouldSprint = true;
            reprieve.shouldFireEquipment = false;
            reprieve.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            reprieve.driverUpdateTimerOverride = .5f;


            // Use primary while retreating (point-blank)
            AISkillDriver primaryRetreat = masterObject.AddComponent<AISkillDriver>();
            primaryRetreat.skillSlot = SkillSlot.Primary;
            primaryRetreat.requireSkillReady = true;
            primaryRetreat.requireEquipmentReady = false;
            primaryRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryRetreat.minDistance = 0f;
            primaryRetreat.maxDistance = 5f;
            primaryRetreat.selectionRequiresTargetLoS = false;
            primaryRetreat.activationRequiresTargetLoS = false;
            primaryRetreat.activationRequiresAimConfirmation = true;
            primaryRetreat.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            primaryRetreat.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryRetreat.ignoreNodeGraph = false;
            primaryRetreat.noRepeat = false;
            primaryRetreat.shouldSprint = false;
            primaryRetreat.shouldFireEquipment = false;
            primaryRetreat.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryRetreat.driverUpdateTimerOverride = .5f;


            // Use primary while strafing (close)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 30f;
            primaryStrafe.selectionRequiresTargetLoS = false;
            primaryStrafe.activationRequiresTargetLoS = false;
            primaryStrafe.activationRequiresAimConfirmation = true;
            primaryStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primaryStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryStrafe.ignoreNodeGraph = false;
            primaryStrafe.noRepeat = false;
            primaryStrafe.shouldSprint = false;
            primaryStrafe.shouldFireEquipment = false;
            primaryStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryStrafe.driverUpdateTimerOverride = .5f;


            // Use primary while approaching (far)
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requireSkillReady = true;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 60f;
            primaryChase.selectionRequiresTargetLoS = false;
            primaryChase.activationRequiresTargetLoS = false;
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


            // Use Soul Spiral off cooldown (owner version)
            AISkillDriver soulSpiralOwner = masterObject.AddComponent<AISkillDriver>();
            soulSpiralOwner.skillSlot = SkillSlot.Secondary;
            soulSpiralOwner.requiredSkill = soulSpiralSkill;
            soulSpiralOwner.requireSkillReady = true;
            soulSpiralOwner.requireEquipmentReady = false;
            soulSpiralOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            soulSpiralOwner.minDistance = 0f;
            soulSpiralOwner.maxDistance = float.PositiveInfinity;
            soulSpiralOwner.selectionRequiresTargetLoS = false;
            soulSpiralOwner.activationRequiresTargetLoS = false;
            soulSpiralOwner.activationRequiresAimConfirmation = false;
            soulSpiralOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            soulSpiralOwner.aimType = AISkillDriver.AimType.AtCurrentLeader;
            soulSpiralOwner.ignoreNodeGraph = false;
            soulSpiralOwner.noRepeat = true;
            soulSpiralOwner.shouldSprint = true;
            soulSpiralOwner.shouldFireEquipment = false;
            soulSpiralOwner.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


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


            // Release Unseen Hand
            AISkillDriver releaseUnseenHand = masterObject.AddComponent<AISkillDriver>();
            releaseUnseenHand.skillSlot = SkillSlot.None;
            releaseUnseenHand.requireSkillReady = false;
            releaseUnseenHand.requireEquipmentReady = false;
            releaseUnseenHand.moveTargetType = AISkillDriver.TargetType.Custom;
            releaseUnseenHand.minDistance = 0f;
            releaseUnseenHand.maxDistance = float.PositiveInfinity;
            releaseUnseenHand.selectionRequiresTargetLoS = false;
            releaseUnseenHand.activationRequiresTargetLoS = false;
            releaseUnseenHand.activationRequiresAimConfirmation = false;
            releaseUnseenHand.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            releaseUnseenHand.aimType = AISkillDriver.AimType.AtMoveTarget;
            releaseUnseenHand.ignoreNodeGraph = false;
            releaseUnseenHand.noRepeat = false;
            releaseUnseenHand.shouldSprint = false;
            releaseUnseenHand.shouldFireEquipment = false;
            releaseUnseenHand.buttonPressType = AISkillDriver.ButtonPressType.Abstain;


            // Overrides
            unseenHand.nextHighPriorityOverride = releaseUnseenHand;
        }

        private void CharacterMaster_onCharacterMasterDiscovered(CharacterMaster master)
        {
            if (master.masterIndex == MasterCatalog.FindMasterIndex("SeekerMonsterMaster"))
            {
                var component = master.GetComponent<UnseenHandTargeting>();
                if (!component && master.gameObject)
                {
                    component = master.gameObject.AddComponent<UnseenHandTargeting>();
                }
            }
        }

        private void HandleMeditateInputs(On.EntityStates.Seeker.Meditate.orig_OnEnter orig, EntityStates.Seeker.Meditate self)
        {
            orig(self);

            CharacterBody seekerBody = self.characterBody;
            if (seekerBody && !seekerBody.isPlayerControlled)
            {
                SeekerController seekerController = self.seekerController;
                seekerController.StartCoroutine(RegisterInputs(self, seekerController));
            }
        }

        public IEnumerator RegisterInputs(EntityStates.Seeker.Meditate self, SeekerController seekerController)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(.15f);

                if (self.gameObject && self.inputCorrectSoundString != null && seekerController)
                {
                    // Fake successful inputs lol
                    Util.PlaySound(self.inputCorrectSoundString, self.gameObject);
                    seekerController.meditationInputStep++;
                }  
            }
        }

        // This is literally just anti-NRE code
        private void NoMeditateErrors(On.EntityStates.Seeker.Meditate.orig_UpdateUIInputSequence orig, EntityStates.Seeker.Meditate self)
        {
            CharacterBody seekerBody = self.characterBody;
            if (seekerBody && !seekerBody.isPlayerControlled)
            {
                return;
            }

            orig(self);
        }
    }

    public class UnseenHandTargeting : MonoBehaviour
    {
        public static float updateInterval = .05f; // Seconds
        public static float timer = 0f;

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer < updateInterval) return;
            timer -= updateInterval;

            BaseAI ai = gameObject.GetComponent<BaseAI>();
            if (ai && ai.currentEnemy.gameObject && ai.currentEnemy.characterBody)
            {
                Vector3 wallPosition = Vector3.zero;
                bool hasTarget = false;

                Ray aimRay = new Ray(ai.currentEnemy.gameObject.transform.position, Vector3.down);
                float rayLength = 80f + Mathf.Abs(ai.currentEnemy.characterBody.transform.position.y - ai.currentEnemy.characterBody.footPosition.y);
                if (Physics.Raycast(aimRay, out var hitInfo, rayLength, LayerIndex.world.mask))
                {
                    if (ai.HasLOS(hitInfo.point + Vector3.up * .02f))
                    {
                        hasTarget = true;
                        wallPosition = hitInfo.point + Vector3.up * .02f;
                    }
                }

                if (hasTarget)
                {
                    ai.customTarget._gameObject = ai.currentEnemy.gameObject;
                    ai.customTarget.lastKnownBullseyePosition = wallPosition;
                    ai.customTarget.lastKnownBullseyePositionTime = Run.FixedTimeStamp.now;
                    ai.customTarget.unset = false;
                }
                else
                {
                    ai.customTarget._gameObject = null;
                    ai.customTarget.lastKnownBullseyePosition = null;
                    ai.customTarget.lastKnownBullseyePositionTime = Run.FixedTimeStamp.negativeInfinity;
                    ai.customTarget.unset = true;
                }
            }

        }
    }
}
