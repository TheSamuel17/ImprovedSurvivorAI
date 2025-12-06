using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class ChefAI
    {
        // Asset references
        public static SkillDef diceSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Chef/ChefDice.asset").WaitForCompletion();
        public static SkillDef boostedDiceSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Chef/ChefDiceBoosted.asset").WaitForCompletion();
        public static SkillDef searSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Chef/ChefSear.asset").WaitForCompletion();
        public static SkillDef boostedSearSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Chef/ChefSearBoosted.asset").WaitForCompletion();
        public static SkillDef iceBoxSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Chef/ChefIceBox.asset").WaitForCompletion();
        public static SkillDef boostedIceBoxSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Chef/ChefIceBoxBoosted.asset").WaitForCompletion();
        public static ChefOilSpillSkillDef oilSpillSkill = Addressables.LoadAssetAsync<ChefOilSpillSkillDef>("RoR2/DLC2/Chef/ChefOilSpill.asset").WaitForCompletion();
        public static SkillDef glazeSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Chef/ChefGlaze.asset").WaitForCompletion();
        public static SkillDef yesChefSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/DLC2/Chef/YesChef.asset").WaitForCompletion();

        public ChefAI(GameObject masterObject)
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


            // Boosted Dice if point blank
            /*AISkillDriver boostedDice = masterObject.AddComponent<AISkillDriver>();
            boostedDice.skillSlot = SkillSlot.Primary;
            boostedDice.requiredSkill = boostedDiceSkill;
            boostedDice.requireSkillReady = true;
            boostedDice.requireEquipmentReady = false;
            boostedDice.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            boostedDice.minDistance = 0f;
            boostedDice.maxDistance = 10f;
            boostedDice.selectionRequiresTargetLoS = true;
            boostedDice.activationRequiresTargetLoS = true;
            boostedDice.activationRequiresAimConfirmation = false;
            boostedDice.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            boostedDice.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            boostedDice.ignoreNodeGraph = true;
            boostedDice.noRepeat = false;
            boostedDice.shouldSprint = false;
            boostedDice.shouldFireEquipment = false;
            boostedDice.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;*/


            // Boosted Sear if farther
            /*AISkillDriver boostedSear = masterObject.AddComponent<AISkillDriver>();
            boostedSear.skillSlot = SkillSlot.Secondary;
            boostedSear.requiredSkill = boostedSearSkill;
            boostedSear.requireSkillReady = true;
            boostedSear.requireEquipmentReady = false;
            boostedSear.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            boostedSear.minDistance = 0f;
            boostedSear.maxDistance = 150f;
            boostedSear.selectionRequiresTargetLoS = true;
            boostedSear.activationRequiresTargetLoS = true;
            boostedSear.activationRequiresAimConfirmation = true;
            boostedSear.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            boostedSear.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            boostedSear.ignoreNodeGraph = false;
            boostedSear.noRepeat = false;
            boostedSear.shouldSprint = true;
            boostedSear.shouldFireEquipment = false;
            boostedSear.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;*/


            // Boosted Ice Box if farther
            /*AISkillDriver boostedIceBox = masterObject.AddComponent<AISkillDriver>();
            boostedIceBox.skillSlot = SkillSlot.Secondary;
            boostedIceBox.requiredSkill = boostedIceBoxSkill;
            boostedIceBox.requireSkillReady = true;
            boostedIceBox.requireEquipmentReady = false;
            boostedIceBox.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            boostedIceBox.minDistance = 0f;
            boostedIceBox.maxDistance = 150f;
            boostedIceBox.selectionRequiresTargetLoS = true;
            boostedIceBox.activationRequiresTargetLoS = true;
            boostedIceBox.activationRequiresAimConfirmation = true;
            boostedIceBox.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            boostedIceBox.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            boostedIceBox.ignoreNodeGraph = false;
            boostedIceBox.noRepeat = false;
            boostedIceBox.shouldSprint = false;
            boostedIceBox.shouldFireEquipment = false;
            boostedIceBox.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;*/


            // Glaze off cooldown
            AISkillDriver glaze = masterObject.AddComponent<AISkillDriver>();
            glaze.skillSlot = SkillSlot.Special;
            glaze.requiredSkill = glazeSkill;
            glaze.requireSkillReady = true;
            glaze.requireEquipmentReady = false;
            glaze.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            glaze.minDistance = 0f;
            glaze.maxDistance = 80f;
            glaze.selectionRequiresTargetLoS = true;
            glaze.activationRequiresTargetLoS = true;
            glaze.activationRequiresAimConfirmation = true;
            glaze.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            glaze.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            glaze.ignoreNodeGraph = false;
            glaze.noRepeat = true;
            glaze.shouldSprint = true;
            glaze.shouldFireEquipment = false;
            glaze.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Yes CHEF off cooldown
            /*AISkillDriver yesChef = masterObject.AddComponent<AISkillDriver>();
            yesChef.skillSlot = SkillSlot.Special;
            yesChef.requiredSkill = yesChefSkill;
            yesChef.requireSkillReady = true;
            yesChef.requireEquipmentReady = false;
            yesChef.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            yesChef.minDistance = 0f;
            yesChef.maxDistance = 400f;
            yesChef.selectionRequiresTargetLoS = false;
            yesChef.activationRequiresTargetLoS = false;
            yesChef.activationRequiresAimConfirmation = false;
            yesChef.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            yesChef.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            yesChef.ignoreNodeGraph = false;
            yesChef.noRepeat = false;
            yesChef.shouldSprint = true;
            yesChef.shouldFireEquipment = false;
            yesChef.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            yesChef.driverUpdateTimerOverride = 1f;*/


            // Mash Oil Spill to keep it going
            AISkillDriver oilSpill = masterObject.AddComponent<AISkillDriver>();
            oilSpill.skillSlot = SkillSlot.Utility;
            oilSpill.requiredSkill = oilSpillSkill;
            oilSpill.requireSkillReady = true;
            oilSpill.requireEquipmentReady = false;
            oilSpill.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            oilSpill.minDistance = 0f;
            oilSpill.maxDistance = 40f;
            oilSpill.selectionRequiresTargetLoS = false;
            oilSpill.activationRequiresTargetLoS = false;
            oilSpill.activationRequiresAimConfirmation = false;
            oilSpill.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            oilSpill.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            oilSpill.ignoreNodeGraph = true;
            oilSpill.noRepeat = false;
            oilSpill.shouldSprint = true;
            oilSpill.shouldFireEquipment = false;
            oilSpill.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            oilSpill.selectionRequiresOnGround = true;


            // Sear if close
            AISkillDriver sear = masterObject.AddComponent<AISkillDriver>();
            sear.skillSlot = SkillSlot.Secondary;
            sear.requiredSkill = searSkill;
            sear.requireSkillReady = true;
            sear.requireEquipmentReady = false;
            sear.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            sear.minDistance = 0f;
            sear.maxDistance = 30f;
            sear.selectionRequiresTargetLoS = true;
            sear.activationRequiresTargetLoS = true;
            sear.activationRequiresAimConfirmation = false;
            sear.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            sear.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            sear.ignoreNodeGraph = true;
            sear.noRepeat = true;
            sear.shouldSprint = true;
            sear.shouldFireEquipment = false;
            sear.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Ice Box off cooldown
            AISkillDriver iceBox = masterObject.AddComponent<AISkillDriver>();
            iceBox.skillSlot = SkillSlot.Secondary;
            iceBox.requiredSkill = iceBoxSkill;
            iceBox.requireSkillReady = true;
            iceBox.requireEquipmentReady = false;
            iceBox.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            iceBox.minDistance = 0f;
            iceBox.maxDistance = 150f;
            iceBox.selectionRequiresTargetLoS = true;
            iceBox.activationRequiresTargetLoS = true;
            iceBox.activationRequiresAimConfirmation = true;
            iceBox.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            iceBox.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            iceBox.ignoreNodeGraph = true;
            iceBox.noRepeat = false;
            iceBox.shouldSprint = false;
            iceBox.shouldFireEquipment = false;
            iceBox.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Shoot primary while retreating (point-blank)
            AISkillDriver primaryRetreat = masterObject.AddComponent<AISkillDriver>();
            primaryRetreat.skillSlot = SkillSlot.Primary;
            primaryRetreat.requiredSkill = diceSkill;
            primaryRetreat.requireSkillReady = true;
            primaryRetreat.requireEquipmentReady = false;
            primaryRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryRetreat.minDistance = 0f;
            primaryRetreat.maxDistance = 10f;
            primaryRetreat.selectionRequiresTargetLoS = true;
            primaryRetreat.activationRequiresTargetLoS = true;
            primaryRetreat.activationRequiresAimConfirmation = true;
            primaryRetreat.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            primaryRetreat.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryRetreat.ignoreNodeGraph = false;
            primaryRetreat.noRepeat = false;
            primaryRetreat.shouldSprint = false;
            primaryRetreat.shouldFireEquipment = false;
            primaryRetreat.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Shoot primary while strafing (close)
            AISkillDriver primaryStrafe = masterObject.AddComponent<AISkillDriver>();
            primaryStrafe.skillSlot = SkillSlot.Primary;
            primaryStrafe.requiredSkill = diceSkill;
            primaryStrafe.requireSkillReady = true;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 30f;
            primaryStrafe.selectionRequiresTargetLoS = true;
            primaryStrafe.activationRequiresTargetLoS = true;
            primaryStrafe.activationRequiresAimConfirmation = true;
            primaryStrafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primaryStrafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryStrafe.ignoreNodeGraph = false;
            primaryStrafe.noRepeat = false;
            primaryStrafe.shouldSprint = false;
            primaryStrafe.shouldFireEquipment = false;
            primaryStrafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Shoot primary while approaching (far)
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requiredSkill = diceSkill;
            primaryChase.requireSkillReady = true;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 55f;
            primaryChase.selectionRequiresTargetLoS = true;
            primaryChase.activationRequiresTargetLoS = true;
            primaryChase.activationRequiresAimConfirmation = true;
            primaryChase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            primaryChase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryChase.ignoreNodeGraph = false;
            primaryChase.noRepeat = false;
            primaryChase.shouldSprint = false;
            primaryChase.shouldFireEquipment = false;
            primaryChase.buttonPressType = AISkillDriver.ButtonPressType.Hold;


            // Hold primary to charge the cleavers
            AISkillDriver holdPrimary = masterObject.AddComponent<AISkillDriver>();
            holdPrimary.skillSlot = SkillSlot.Primary;
            holdPrimary.requiredSkill = diceSkill;
            holdPrimary.requireSkillReady = false;
            holdPrimary.requireEquipmentReady = false;
            holdPrimary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            holdPrimary.minDistance = 0f;
            holdPrimary.maxDistance = 55f;
            holdPrimary.selectionRequiresTargetLoS = true;
            holdPrimary.activationRequiresTargetLoS = true;
            holdPrimary.activationRequiresAimConfirmation = true;
            holdPrimary.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            holdPrimary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            holdPrimary.ignoreNodeGraph = false;
            holdPrimary.noRepeat = true;
            holdPrimary.shouldSprint = false;
            holdPrimary.shouldFireEquipment = false;
            holdPrimary.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            holdPrimary.driverUpdateTimerOverride = .8f;
            holdPrimary.selectionRequiresOnGround = true;


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
    }
}
