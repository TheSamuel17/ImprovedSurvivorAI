using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    public class ArtificerAI
    {
        // Asset references
        public static SkillDef nanoBombSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Mage/MageBodyNovaBomb.asset").WaitForCompletion();
        public static SkillDef nanoSpearSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Mage/MageBodyIceBomb.asset").WaitForCompletion();
        public static SkillDef flamethrowerSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Mage/MageBodyFlamethrower.asset").WaitForCompletion();
        public static SkillDef ionSurgeSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Mage/MageBodyFlyUp.asset").WaitForCompletion();

        public ArtificerAI(GameObject masterObject)
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
            On.RoR2.CharacterAI.BaseAI.FixedUpdate += SnapfreezeTargeting;


            // Use Ion Surge off cooldown (point-blank)
            AISkillDriver ionSurge = masterObject.AddComponent<AISkillDriver>();
            ionSurge.skillSlot = SkillSlot.Special;
            ionSurge.requiredSkill = ionSurgeSkill;
            ionSurge.requireSkillReady = true;
            ionSurge.requireEquipmentReady = false;
            ionSurge.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            ionSurge.minDistance = 0f;
            ionSurge.maxDistance = 14f;
            ionSurge.selectionRequiresTargetLoS = true;
            ionSurge.activationRequiresTargetLoS = true;
            ionSurge.activationRequiresAimConfirmation = false;
            ionSurge.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            ionSurge.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            ionSurge.ignoreNodeGraph = false;
            ionSurge.noRepeat = true;
            ionSurge.shouldSprint = true;
            ionSurge.shouldFireEquipment = false;
            ionSurge.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Use utility off cooldown
            AISkillDriver utility = masterObject.AddComponent<AISkillDriver>();
            utility.skillSlot = SkillSlot.Utility;
            utility.requireSkillReady = true;
            utility.requireEquipmentReady = false;
            utility.moveTargetType = AISkillDriver.TargetType.Custom;
            utility.minDistance = 0f;
            utility.maxDistance = 100f;
            utility.selectionRequiresTargetLoS = false;
            utility.activationRequiresTargetLoS = false;
            utility.activationRequiresAimConfirmation = false;
            utility.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            utility.aimType = AISkillDriver.AimType.AtMoveTarget;
            utility.ignoreNodeGraph = false;
            utility.noRepeat = true;
            utility.shouldSprint = false;
            utility.shouldFireEquipment = false;
            utility.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            utility.driverUpdateTimerOverride = .5f;


            // Use Flamethrower off cooldown
            AISkillDriver flamethrower = masterObject.AddComponent<AISkillDriver>();
            flamethrower.skillSlot = SkillSlot.Special;
            flamethrower.requiredSkill = flamethrowerSkill;
            flamethrower.requireSkillReady = true;
            flamethrower.requireEquipmentReady = false;
            flamethrower.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            flamethrower.minDistance = 0f;
            flamethrower.maxDistance = 20f;
            flamethrower.selectionRequiresTargetLoS = true;
            flamethrower.activationRequiresTargetLoS = true;
            flamethrower.activationRequiresAimConfirmation = false;
            flamethrower.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            flamethrower.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            flamethrower.ignoreNodeGraph = false;
            flamethrower.noRepeat = true;
            flamethrower.shouldSprint = false;
            flamethrower.shouldFireEquipment = false;
            flamethrower.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            flamethrower.driverUpdateTimerOverride = 2f;


            // Use Nano-Bomb off cooldown
            AISkillDriver nanoBomb = masterObject.AddComponent<AISkillDriver>();
            nanoBomb.skillSlot = SkillSlot.Secondary;
            nanoBomb.requiredSkill = nanoBombSkill;
            nanoBomb.requireSkillReady = true;
            nanoBomb.requireEquipmentReady = false;
            nanoBomb.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            nanoBomb.minDistance = 0f;
            nanoBomb.maxDistance = 50f;
            nanoBomb.selectionRequiresTargetLoS = true;
            nanoBomb.activationRequiresTargetLoS = true;
            nanoBomb.activationRequiresAimConfirmation = false;
            nanoBomb.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            nanoBomb.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            nanoBomb.ignoreNodeGraph = false;
            nanoBomb.noRepeat = false;
            nanoBomb.shouldSprint = false;
            nanoBomb.shouldFireEquipment = false;
            nanoBomb.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            nanoBomb.driverUpdateTimerOverride = 2.2f;


            // Use Nano-Spear off cooldown
            AISkillDriver nanoSpear = masterObject.AddComponent<AISkillDriver>();
            nanoSpear.skillSlot = SkillSlot.Secondary;
            nanoSpear.requiredSkill = nanoSpearSkill;
            nanoSpear.requireSkillReady = true;
            nanoSpear.requireEquipmentReady = false;
            nanoSpear.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            nanoSpear.minDistance = 0f;
            nanoSpear.maxDistance = 150f;
            nanoSpear.selectionRequiresTargetLoS = false;
            nanoSpear.activationRequiresTargetLoS = false;
            nanoSpear.activationRequiresAimConfirmation = false;
            nanoSpear.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            nanoSpear.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            nanoSpear.ignoreNodeGraph = false;
            nanoSpear.noRepeat = false;
            nanoSpear.shouldSprint = false;
            nanoSpear.shouldFireEquipment = false;
            nanoSpear.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            nanoSpear.driverUpdateTimerOverride = 2.2f;


            // Shoot primary while retreating (point-blank)
            AISkillDriver primaryRetreat = masterObject.AddComponent<AISkillDriver>();
            primaryRetreat.skillSlot = SkillSlot.Primary;
            primaryRetreat.requireSkillReady = false;
            primaryRetreat.requireEquipmentReady = false;
            primaryRetreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryRetreat.minDistance = 0f;
            primaryRetreat.maxDistance = 20f;
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
            primaryStrafe.requireSkillReady = false;
            primaryStrafe.requireEquipmentReady = false;
            primaryStrafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryStrafe.minDistance = 0f;
            primaryStrafe.maxDistance = 60f;
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
            primaryStrafe.driverUpdateTimerOverride = .5f;


            // Shoot primary while approaching (far)
            AISkillDriver primaryChase = masterObject.AddComponent<AISkillDriver>();
            primaryChase.skillSlot = SkillSlot.Primary;
            primaryChase.requireSkillReady = false;
            primaryChase.requireEquipmentReady = false;
            primaryChase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            primaryChase.minDistance = 0f;
            primaryChase.maxDistance = 150f;
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
            AISkillDriver releaseUtility = masterObject.AddComponent<AISkillDriver>();
            releaseUtility.skillSlot = SkillSlot.None;
            releaseUtility.requireSkillReady = false;
            releaseUtility.requireEquipmentReady = false;
            releaseUtility.moveTargetType = AISkillDriver.TargetType.Custom;
            releaseUtility.minDistance = 0f;
            releaseUtility.maxDistance = float.PositiveInfinity;
            releaseUtility.selectionRequiresTargetLoS = false;
            releaseUtility.activationRequiresTargetLoS = false;
            releaseUtility.activationRequiresAimConfirmation = false;
            releaseUtility.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            releaseUtility.aimType = AISkillDriver.AimType.AtMoveTarget;
            releaseUtility.ignoreNodeGraph = false;
            releaseUtility.noRepeat = false;
            releaseUtility.shouldSprint = false;
            releaseUtility.shouldFireEquipment = false;
            releaseUtility.buttonPressType = AISkillDriver.ButtonPressType.Abstain;


            // Overrides
            utility.nextHighPriorityOverride = releaseUtility;
        }

        private void SnapfreezeTargeting(On.RoR2.CharacterAI.BaseAI.orig_FixedUpdate orig, BaseAI self)
        {
            orig(self);

            if (self.master.masterIndex == MasterCatalog.FindMasterIndex("MageMonsterMaster"))
            {
                if (self.currentEnemy.gameObject && self.currentEnemy.characterBody)
                {
                    Vector3 wallPosition = Vector3.zero;
                    bool hasTarget = false;

                    if (self.HasLOS(self.currentEnemy.gameObject.transform.position))
                    {
                        Ray aimRay = new Ray(self.currentEnemy.gameObject.transform.position, Vector3.down);
                        float rayLength = 8f + Mathf.Abs(self.currentEnemy.characterBody.transform.position.y - self.currentEnemy.characterBody.footPosition.y);
                        if (Physics.Raycast(aimRay, out var hitInfo, rayLength, LayerIndex.world.mask))
                        {
                            if (self.HasLOS(hitInfo.point + Vector3.up * .05f))
                            {
                                hasTarget = true;
                                wallPosition = hitInfo.point + Vector3.up * .05f;
                            }
                        }
                    }     

                    if (hasTarget)
                    {
                        self.customTarget._gameObject = self.currentEnemy.gameObject;
                        self.customTarget.lastKnownBullseyePosition = wallPosition;
                        self.customTarget.lastKnownBullseyePositionTime = Run.FixedTimeStamp.now;
                        self.customTarget.unset = false;
                    }
                    else
                    {
                        self.customTarget._gameObject = null;
                        self.customTarget.lastKnownBullseyePosition = null;
                        self.customTarget.lastKnownBullseyePositionTime = Run.FixedTimeStamp.negativeInfinity;
                        self.customTarget.unset = true;
                    }
                }
            }
        }
    }
}
