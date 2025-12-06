using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class CaptainAI
    {
        // Asset references
        public static SkillDef orbitalProbesSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Captain/CallAirstrike.asset").WaitForCompletion();
        public static SkillDef diabloStrikeSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Captain/CallAirstrikeAlt.asset").WaitForCompletion();
        public static CaptainOrbitalSkillDef callProbesSkill = Addressables.LoadAssetAsync<CaptainOrbitalSkillDef>("RoR2/Base/Captain/PrepAirstrike.asset").WaitForCompletion();
        public static CaptainOrbitalSkillDef callDiabloSkill = Addressables.LoadAssetAsync<CaptainOrbitalSkillDef>("RoR2/Base/Captain/PrepAirstrikeAlt.asset").WaitForCompletion();
        public static SkillDef supplyDropHealing = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Captain/CallSupplyDropHealing.asset").WaitForCompletion();
        public static SkillDef supplyDropShocking = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Captain/CallSupplyDropShocking.asset").WaitForCompletion();
        public static SkillDef supplyDropEquipment = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Captain/CallSupplyDropEquipmentRestock.asset").WaitForCompletion();
        public static SkillDef supplyDropHacking = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Captain/CallSupplyDropHacking.asset").WaitForCompletion();

        // List the different beacon types
        public static SkillDef[] supplyDropTypes = {supplyDropHealing, supplyDropShocking, supplyDropEquipment, supplyDropHacking};

        public CaptainAI(GameObject masterObject, bool enableCaptainBeacons)
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
            if (enableCaptainBeacons) {
                On.EntityStates.Captain.Weapon.SetupSupplyDrop.OnEnter += SupplyBeaconTargeting;
            } 

            
            if (enableCaptainBeacons)
            {
                // Call Orbital Supply Drops - First
                for (int i = 0; i < supplyDropTypes.Length; i++)
                {
                    AISkillDriver callSupplyDropPrimary = masterObject.AddComponent<AISkillDriver>();
                    callSupplyDropPrimary.skillSlot = SkillSlot.Primary;
                    callSupplyDropPrimary.requiredSkill = supplyDropTypes[i];
                    callSupplyDropPrimary.requireSkillReady = true;
                    callSupplyDropPrimary.requireEquipmentReady = false;
                    callSupplyDropPrimary.moveTargetType = AISkillDriver.TargetType.Custom;
                    callSupplyDropPrimary.minDistance = 0f;
                    callSupplyDropPrimary.maxDistance = float.PositiveInfinity;
                    callSupplyDropPrimary.selectionRequiresTargetLoS = false;
                    callSupplyDropPrimary.activationRequiresTargetLoS = false;
                    callSupplyDropPrimary.activationRequiresAimConfirmation = true;
                    callSupplyDropPrimary.movementType = AISkillDriver.MovementType.Stop;
                    callSupplyDropPrimary.aimType = AISkillDriver.AimType.AtMoveTarget;
                    callSupplyDropPrimary.ignoreNodeGraph = false;
                    callSupplyDropPrimary.noRepeat = false;
                    callSupplyDropPrimary.shouldSprint = false;
                    callSupplyDropPrimary.shouldFireEquipment = false;
                    callSupplyDropPrimary.buttonPressType = AISkillDriver.ButtonPressType.Hold;
                }

                // Call Orbital Supply Drops - Second
                for (int i = 0; i < supplyDropTypes.Length; i++)
                {
                    AISkillDriver callSupplyDropSecondary = masterObject.AddComponent<AISkillDriver>();
                    callSupplyDropSecondary.skillSlot = SkillSlot.Secondary;
                    callSupplyDropSecondary.requiredSkill = supplyDropTypes[i];
                    callSupplyDropSecondary.requireSkillReady = true;
                    callSupplyDropSecondary.requireEquipmentReady = false;
                    callSupplyDropSecondary.moveTargetType = AISkillDriver.TargetType.Custom;
                    callSupplyDropSecondary.minDistance = 0f;
                    callSupplyDropSecondary.maxDistance = float.PositiveInfinity;
                    callSupplyDropSecondary.selectionRequiresTargetLoS = false;
                    callSupplyDropSecondary.activationRequiresTargetLoS = false;
                    callSupplyDropSecondary.activationRequiresAimConfirmation = true;
                    callSupplyDropSecondary.movementType = AISkillDriver.MovementType.Stop;
                    callSupplyDropSecondary.aimType = AISkillDriver.AimType.AtMoveTarget;
                    callSupplyDropSecondary.ignoreNodeGraph = false;
                    callSupplyDropSecondary.noRepeat = false;
                    callSupplyDropSecondary.shouldSprint = false;
                    callSupplyDropSecondary.shouldFireEquipment = false;
                    callSupplyDropSecondary.buttonPressType = AISkillDriver.ButtonPressType.Hold;
                }
            }

            // Call Orbital Probes
            AISkillDriver callProbes = masterObject.AddComponent<AISkillDriver>();
            callProbes.skillSlot = SkillSlot.Primary;
            callProbes.requiredSkill = callProbesSkill;
            callProbes.requireSkillReady = true;
            callProbes.requireEquipmentReady = false;
            callProbes.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            callProbes.minDistance = 0f;
            callProbes.maxDistance = 300f;
            callProbes.selectionRequiresTargetLoS = true;
            callProbes.activationRequiresTargetLoS = true;
            callProbes.activationRequiresAimConfirmation = true;
            callProbes.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            callProbes.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            callProbes.ignoreNodeGraph = false;
            callProbes.noRepeat = false;
            callProbes.shouldSprint = false;
            callProbes.shouldFireEquipment = false;
            callProbes.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Call Diablo Strike
            AISkillDriver callDiablo = masterObject.AddComponent<AISkillDriver>();
            callDiablo.skillSlot = SkillSlot.Primary;
            callDiablo.requiredSkill = callDiabloSkill;
            callDiablo.requireSkillReady = true;
            callDiablo.requireEquipmentReady = false;
            callDiablo.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            callDiablo.minDistance = 0f;
            callDiablo.maxDistance = 300f;
            callDiablo.selectionRequiresTargetLoS = true;
            callDiablo.activationRequiresTargetLoS = true;
            callDiablo.activationRequiresAimConfirmation = true;
            callDiablo.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            callDiablo.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            callDiablo.ignoreNodeGraph = false;
            callDiablo.noRepeat = false;
            callDiablo.shouldSprint = false;
            callDiablo.shouldFireEquipment = false;
            callDiablo.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            if (enableCaptainBeacons)
            {
                // Use special off cooldown
                AISkillDriver supplyDrop = masterObject.AddComponent<AISkillDriver>();
                supplyDrop.skillSlot = SkillSlot.Special;
                supplyDrop.requireSkillReady = true;
                supplyDrop.requireEquipmentReady = false;
                supplyDrop.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
                supplyDrop.minDistance = 0f;
                supplyDrop.maxDistance = float.PositiveInfinity;
                supplyDrop.selectionRequiresTargetLoS = true;
                supplyDrop.activationRequiresTargetLoS = true;
                supplyDrop.activationRequiresAimConfirmation = true;
                supplyDrop.movementType = AISkillDriver.MovementType.Stop;
                supplyDrop.aimType = AISkillDriver.AimType.AtCurrentLeader;
                supplyDrop.ignoreNodeGraph = false;
                supplyDrop.noRepeat = false;
                supplyDrop.shouldSprint = false;
                supplyDrop.shouldFireEquipment = false;
                supplyDrop.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            }


            // Use secondary off cooldown
            AISkillDriver secondary = masterObject.AddComponent<AISkillDriver>();
            secondary.skillSlot = SkillSlot.Secondary;
            secondary.requireSkillReady = true;
            secondary.requireEquipmentReady = false;
            secondary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            secondary.minDistance = 0f;
            secondary.maxDistance = 100f;
            secondary.selectionRequiresTargetLoS = true;
            secondary.activationRequiresTargetLoS = true;
            secondary.activationRequiresAimConfirmation = true;
            secondary.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            secondary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            secondary.ignoreNodeGraph = false;
            secondary.noRepeat = true;
            secondary.shouldSprint = false;
            secondary.shouldFireEquipment = false;
            secondary.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            secondary.driverUpdateTimerOverride = .35f;


            // Use utility off cooldown
            AISkillDriver utility = masterObject.AddComponent<AISkillDriver>();
            utility.skillSlot = SkillSlot.Utility;
            utility.requireSkillReady = true;
            utility.requireEquipmentReady = false;
            utility.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            utility.minDistance = 0f;
            utility.maxDistance = 300f;
            utility.selectionRequiresTargetLoS = true;
            utility.activationRequiresTargetLoS = true;
            utility.activationRequiresAimConfirmation = true;
            utility.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            utility.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            utility.ignoreNodeGraph = false;
            utility.noRepeat = false;
            utility.shouldSprint = false;
            utility.shouldFireEquipment = false;
            utility.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Shoot uncharged primary while retreating (point-blank)
            AISkillDriver primaryRetreat = masterObject.AddComponent<AISkillDriver>();
            primaryRetreat.skillSlot = SkillSlot.Primary;
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
            primaryRetreat.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Shoot uncharged primary while strafing (close)
            AISkillDriver primary = masterObject.AddComponent<AISkillDriver>();
            primary.skillSlot = SkillSlot.Primary;
            primary.requireSkillReady = true;
            primary.requireEquipmentReady = false;
            primary.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primary.minDistance = 0f;
            primary.maxDistance = 25f;
            primary.selectionRequiresTargetLoS = true;
            primary.activationRequiresTargetLoS = true;
            primary.activationRequiresAimConfirmation = true;
            primary.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primary.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primary.ignoreNodeGraph = false;
            primary.noRepeat = false;
            primary.shouldSprint = false;
            primary.shouldFireEquipment = false;
            primary.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Charge primary while approaching (far)
            AISkillDriver primaryFar = masterObject.AddComponent<AISkillDriver>();
            primaryFar.skillSlot = SkillSlot.Primary;
            primaryFar.requireSkillReady = true;
            primaryFar.requireEquipmentReady = false;
            primaryFar.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryFar.minDistance = 0f;
            primaryFar.maxDistance = 50f;
            primaryFar.selectionRequiresTargetLoS = true;
            primaryFar.activationRequiresTargetLoS = true;
            primaryFar.activationRequiresAimConfirmation = false;
            primaryFar.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            primaryFar.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryFar.ignoreNodeGraph = false;
            primaryFar.noRepeat = false;
            primaryFar.shouldSprint = false;
            primaryFar.shouldFireEquipment = false;
            primaryFar.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryFar.driverUpdateTimerOverride = .75f;


            // Charge primary while approaching (farther)
            AISkillDriver primaryFarther = masterObject.AddComponent<AISkillDriver>();
            primaryFarther.skillSlot = SkillSlot.Primary;
            primaryFarther.requireSkillReady = true;
            primaryFarther.requireEquipmentReady = false;
            primaryFarther.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryFarther.minDistance = 0f;
            primaryFarther.maxDistance = 100f;
            primaryFarther.selectionRequiresTargetLoS = true;
            primaryFarther.activationRequiresTargetLoS = true;
            primaryFarther.activationRequiresAimConfirmation = false;
            primaryFarther.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            primaryFarther.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            primaryFarther.ignoreNodeGraph = false;
            primaryFarther.noRepeat = false;
            primaryFarther.shouldSprint = false;
            primaryFarther.shouldFireEquipment = false;
            primaryFarther.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            primaryFarther.driverUpdateTimerOverride = 1.5f;


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
            primaryFar.nextHighPriorityOverride = releaseInput;
            primaryFarther.nextHighPriorityOverride = releaseInput;
        }

        private void SupplyBeaconTargeting(On.EntityStates.Captain.Weapon.SetupSupplyDrop.orig_OnEnter orig, EntityStates.Captain.Weapon.SetupSupplyDrop self)
        {
            orig(self);

            CharacterBody captainBody = self.characterBody;
            if (captainBody && captainBody.gameObject)
            {
                CharacterMaster captainMaster = captainBody.master;
                if (captainMaster)
                {
                    BaseAI ai = captainMaster.GetComponent<BaseAI>();
                    if (ai)
                    {
                        ai.customTarget._gameObject = captainBody.gameObject;
                        ai.customTarget.lastKnownBullseyePosition = captainBody.footPosition;
                        ai.customTarget.lastKnownBullseyePositionTime = Run.FixedTimeStamp.now;
                        ai.customTarget.unset = false;
                    }
                }
            }
        }
    }
}
