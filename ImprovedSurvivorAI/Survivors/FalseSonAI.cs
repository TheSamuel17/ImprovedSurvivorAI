using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class FalseSonAI
    {
        // Asset references
        public static SkillDef lunarSpikesSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/FalseSon/FalseSonBodyLunarSpikes.asset").WaitForCompletion();
        public static SkillDef lunarStakesSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/FalseSon/FalseSonLunarStake.asset").WaitForCompletion();
        public static SkillDef stepSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/FalseSon/FalseSonBodyStep.asset").WaitForCompletion();
        public static SkillDef meridiansWillSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/FalseSon/FalseSonMeridiansWill.asset").WaitForCompletion();
        public static SteppedSkillDef laserSkill = Addressables.LoadAssetAsync<SteppedSkillDef>("RoR2/DLC2/FalseSon/FalseSonBodyLaser.asset").WaitForCompletion();
        public static SteppedSkillDef laserBurstSkill = Addressables.LoadAssetAsync<SteppedSkillDef>("RoR2/DLC2/FalseSon/FalseSonBodyLaserBurst.asset").WaitForCompletion();

        public FalseSonAI(GameObject masterObject)
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


            // Use Lunar Spikes off cooldown
            AISkillDriver lunarSpikes = masterObject.AddComponent<AISkillDriver>();
            lunarSpikes.skillSlot = SkillSlot.Secondary;
            lunarSpikes.requiredSkill = lunarSpikesSkill;
            lunarSpikes.requireSkillReady = true;
            lunarSpikes.requireEquipmentReady = false;
            lunarSpikes.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            lunarSpikes.minDistance = 0f;
            lunarSpikes.maxDistance = 150f;
            lunarSpikes.selectionRequiresTargetLoS = true;
            lunarSpikes.activationRequiresTargetLoS = true;
            lunarSpikes.activationRequiresAimConfirmation = true;
            lunarSpikes.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            lunarSpikes.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            lunarSpikes.ignoreNodeGraph = false;
            lunarSpikes.noRepeat = false;
            lunarSpikes.shouldSprint = true;
            lunarSpikes.shouldFireEquipment = false;
            lunarSpikes.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Lunar Stakes off cooldown
            AISkillDriver lunarStakes = masterObject.AddComponent<AISkillDriver>();
            lunarStakes.skillSlot = SkillSlot.Secondary;
            lunarStakes.requiredSkill = lunarStakesSkill;
            lunarStakes.requireSkillReady = true;
            lunarStakes.requireEquipmentReady = false;
            lunarStakes.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            lunarStakes.minDistance = 0f;
            lunarStakes.maxDistance = 150f;
            lunarStakes.selectionRequiresTargetLoS = true;
            lunarStakes.activationRequiresTargetLoS = true;
            lunarStakes.activationRequiresAimConfirmation = true;
            lunarStakes.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            lunarStakes.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            lunarStakes.ignoreNodeGraph = false;
            lunarStakes.noRepeat = false;
            lunarStakes.shouldSprint = false;
            lunarStakes.shouldFireEquipment = false;
            lunarStakes.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Laser Burst off cooldown
            AISkillDriver laserBurst = masterObject.AddComponent<AISkillDriver>();
            laserBurst.skillSlot = SkillSlot.Special;
            laserBurst.requiredSkill = laserBurstSkill;
            laserBurst.requireSkillReady = true;
            laserBurst.requireEquipmentReady = false;
            laserBurst.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            laserBurst.minDistance = 0f;
            laserBurst.maxDistance = 300f;
            laserBurst.selectionRequiresTargetLoS = true;
            laserBurst.activationRequiresTargetLoS = true;
            laserBurst.activationRequiresAimConfirmation = true;
            laserBurst.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            laserBurst.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            laserBurst.ignoreNodeGraph = false;
            laserBurst.noRepeat = false;
            laserBurst.shouldSprint = false;
            laserBurst.shouldFireEquipment = false;
            laserBurst.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Step of the Brothers off cooldown
            AISkillDriver dash = masterObject.AddComponent<AISkillDriver>();
            dash.skillSlot = SkillSlot.Utility;
            dash.requiredSkill = stepSkill;
            dash.requireSkillReady = true;
            dash.requireEquipmentReady = false;
            dash.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            dash.minDistance = 20f;
            dash.maxDistance = 35f;
            dash.selectionRequiresTargetLoS = true;
            dash.activationRequiresTargetLoS = true;
            dash.activationRequiresAimConfirmation = true;
            dash.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            dash.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            dash.ignoreNodeGraph = true;
            dash.noRepeat = false;
            dash.shouldSprint = true;
            dash.shouldFireEquipment = false;
            dash.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use Meridian's Will off cooldown
            AISkillDriver meridiansWill = masterObject.AddComponent<AISkillDriver>();
            meridiansWill.skillSlot = SkillSlot.Utility;
            meridiansWill.requiredSkill = meridiansWillSkill;
            meridiansWill.requireSkillReady = true;
            meridiansWill.requireEquipmentReady = false;
            meridiansWill.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            meridiansWill.minDistance = 0f;
            meridiansWill.maxDistance = 400f;
            meridiansWill.selectionRequiresTargetLoS = true;
            meridiansWill.activationRequiresTargetLoS = true;
            meridiansWill.activationRequiresAimConfirmation = true;
            meridiansWill.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            meridiansWill.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            meridiansWill.ignoreNodeGraph = true;
            meridiansWill.noRepeat = true;
            meridiansWill.shouldSprint = true;
            meridiansWill.shouldFireEquipment = false;
            meridiansWill.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Initiate primary, leading to a slam
            AISkillDriver primary = masterObject.AddComponent<AISkillDriver>();
            primary.skillSlot = SkillSlot.Primary;
            primary.requireSkillReady = true;
            primary.requireEquipmentReady = false;
            primary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primary.minDistance = 0f;
            primary.maxDistance = 15f;
            primary.selectionRequiresTargetLoS = true;
            primary.activationRequiresTargetLoS = true;
            primary.activationRequiresAimConfirmation = false;
            primary.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primary.ignoreNodeGraph = false;
            primary.noRepeat = false;
            primary.shouldSprint = false;
            primary.shouldFireEquipment = false;
            primary.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primary.driverUpdateTimerOverride = .2f;


            // Charge up and use Laser of the Father off cooldown
            AISkillDriver laser = masterObject.AddComponent<AISkillDriver>();
            laser.skillSlot = SkillSlot.Special;
            laser.requiredSkill = laserSkill;
            laser.requireSkillReady = true;
            laser.requireEquipmentReady = false;
            laser.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            laser.minDistance = 0f;
            laser.maxDistance = 300f;
            laser.selectionRequiresTargetLoS = true;
            laser.activationRequiresTargetLoS = true;
            laser.activationRequiresAimConfirmation = true;
            laser.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            laser.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            laser.ignoreNodeGraph = false;
            laser.noRepeat = true;
            laser.shouldSprint = false;
            laser.shouldFireEquipment = false;
            laser.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            laser.driverUpdateTimerOverride = 2f;


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


            // Use Meridian's Will towards the owner off cooldown
            AISkillDriver meridiansWillOwner = masterObject.AddComponent<AISkillDriver>();
            meridiansWillOwner.skillSlot = SkillSlot.Utility;
            meridiansWillOwner.requiredSkill = meridiansWillSkill;
            meridiansWillOwner.requireSkillReady = true;
            meridiansWillOwner.requireEquipmentReady = false;
            meridiansWillOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            meridiansWillOwner.minDistance = 20f;
            meridiansWillOwner.maxDistance = float.PositiveInfinity;
            meridiansWillOwner.selectionRequiresTargetLoS = true;
            meridiansWillOwner.activationRequiresTargetLoS = true;
            meridiansWillOwner.activationRequiresAimConfirmation = true;
            meridiansWillOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            meridiansWillOwner.aimType = AISkillDriver.AimType.AtCurrentLeader;
            meridiansWillOwner.ignoreNodeGraph = true;
            meridiansWillOwner.noRepeat = true;
            meridiansWillOwner.shouldSprint = true;
            meridiansWillOwner.shouldFireEquipment = false;
            meridiansWillOwner.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


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


            // Use Meridian's Will off cooldown
            AISkillDriver meridiansWillLowPriority = masterObject.AddComponent<AISkillDriver>();
            meridiansWillLowPriority.skillSlot = SkillSlot.Utility;
            meridiansWillLowPriority.requiredSkill = meridiansWillSkill;
            meridiansWillLowPriority.requireSkillReady = true;
            meridiansWillLowPriority.requireEquipmentReady = false;
            meridiansWillLowPriority.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            meridiansWillLowPriority.minDistance = 0f;
            meridiansWillLowPriority.maxDistance = float.PositiveInfinity;
            meridiansWillLowPriority.selectionRequiresTargetLoS = true;
            meridiansWillLowPriority.activationRequiresTargetLoS = true;
            meridiansWillLowPriority.activationRequiresAimConfirmation = true;
            meridiansWillLowPriority.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            meridiansWillLowPriority.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            meridiansWillLowPriority.ignoreNodeGraph = true;
            meridiansWillLowPriority.noRepeat = true;
            meridiansWillLowPriority.shouldSprint = true;
            meridiansWillLowPriority.shouldFireEquipment = false;
            meridiansWillLowPriority.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


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


            // Charge up slam
            AISkillDriver slam = masterObject.AddComponent<AISkillDriver>();
            slam.skillSlot = SkillSlot.Secondary;
            slam.requireSkillReady = true;
            slam.requireEquipmentReady = false;
            slam.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            slam.minDistance = 0f;
            slam.maxDistance = 15f;
            slam.selectionRequiresTargetLoS = false;
            slam.activationRequiresTargetLoS = false;
            slam.activationRequiresAimConfirmation = false;
            slam.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            slam.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            slam.ignoreNodeGraph = true;
            slam.noRepeat = false;
            slam.shouldSprint = true;
            slam.shouldFireEquipment = false;
            slam.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            slam.driverUpdateTimerOverride = 1.6f;


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
            primary.nextHighPriorityOverride = slam;
            slam.nextHighPriorityOverride = releaseInput;
        }
    }
}
