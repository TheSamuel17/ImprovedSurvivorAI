using RoR2;
using RoR2.CharacterAI;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    class HuntressAI
    {
        // Asset references
        public static SkillDef blinkSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Huntress/HuntressBodyBlink.asset").WaitForCompletion();
        public static SkillDef phaseBlinkSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Huntress/HuntressBodyMiniBlink.asset").WaitForCompletion();
        public static SkillDef arrowRainSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Huntress/HuntressBodyArrowRain.asset").WaitForCompletion();
        public static SkillDef castBallistaSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Huntress/AimArrowSnipe.asset").WaitForCompletion();
        public static SkillDef fireBallistaSkill = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Huntress/FireArrowSnipe.asset").WaitForCompletion();

        public HuntressAI(GameObject masterObject)
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


            // Fire Ballista shots
            AISkillDriver fireBallista = masterObject.AddComponent<AISkillDriver>();
            fireBallista.skillSlot = SkillSlot.Primary;
            fireBallista.requiredSkill = fireBallistaSkill;
            fireBallista.requireSkillReady = false;
            fireBallista.requireEquipmentReady = false;
            fireBallista.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            fireBallista.minDistance = 0f;
            fireBallista.maxDistance = 300f;
            fireBallista.selectionRequiresTargetLoS = true;
            fireBallista.activationRequiresTargetLoS = true;
            fireBallista.activationRequiresAimConfirmation = true;
            fireBallista.movementType = AISkillDriver.MovementType.Stop;
            fireBallista.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            fireBallista.ignoreNodeGraph = false;
            fireBallista.noRepeat = false;
            fireBallista.shouldSprint = false;
            fireBallista.shouldFireEquipment = false;
            fireBallista.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            fireBallista.driverUpdateTimerOverride = .4f;


            // Use Arrow Rain off cooldown
            AISkillDriver castArrowRain = masterObject.AddComponent<AISkillDriver>();
            castArrowRain.skillSlot = SkillSlot.Special;
            castArrowRain.requiredSkill = arrowRainSkill;
            castArrowRain.requireSkillReady = true;
            castArrowRain.requireEquipmentReady = false;
            castArrowRain.moveTargetType = AISkillDriver.TargetType.Custom;
            castArrowRain.minDistance = 0f;
            castArrowRain.maxDistance = 300f;
            castArrowRain.selectionRequiresTargetLoS = false;
            castArrowRain.activationRequiresTargetLoS = false;
            castArrowRain.activationRequiresAimConfirmation = false;
            castArrowRain.movementType = AISkillDriver.MovementType.Stop;
            castArrowRain.aimType = AISkillDriver.AimType.AtMoveTarget;
            castArrowRain.ignoreNodeGraph = false;
            castArrowRain.noRepeat = false;
            castArrowRain.shouldSprint = false;
            castArrowRain.shouldFireEquipment = false;
            castArrowRain.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            castArrowRain.driverUpdateTimerOverride = .5f;


            // Use Ballista off cooldown
            AISkillDriver castBallista = masterObject.AddComponent<AISkillDriver>();
            castBallista.skillSlot = SkillSlot.Special;
            castBallista.requiredSkill = castBallistaSkill;
            castBallista.requireSkillReady = true;
            castBallista.requireEquipmentReady = false;
            castBallista.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            castBallista.minDistance = 0f;
            castBallista.maxDistance = 300f;
            castBallista.selectionRequiresTargetLoS = true;
            castBallista.activationRequiresTargetLoS = true;
            castBallista.activationRequiresAimConfirmation = false;
            castBallista.movementType = AISkillDriver.MovementType.Stop;
            castBallista.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            castBallista.ignoreNodeGraph = false;
            castBallista.noRepeat = false;
            castBallista.shouldSprint = false;
            castBallista.shouldFireEquipment = false;
            castBallista.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            castBallista.driverUpdateTimerOverride = .5f;


            // Phase Blink off cooldown towards move direction
            AISkillDriver phaseBlink = masterObject.AddComponent<AISkillDriver>();
            phaseBlink.skillSlot = SkillSlot.Utility;
            phaseBlink.requiredSkill = phaseBlinkSkill;
            phaseBlink.requireSkillReady = true;
            phaseBlink.requireEquipmentReady = false;
            phaseBlink.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            phaseBlink.minDistance = 0f;
            phaseBlink.maxDistance = float.PositiveInfinity;
            phaseBlink.selectionRequiresTargetLoS = false;
            phaseBlink.activationRequiresTargetLoS = false;
            phaseBlink.activationRequiresAimConfirmation = false;
            //phaseBlink.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            phaseBlink.aimType = AISkillDriver.AimType.MoveDirection;
            phaseBlink.ignoreNodeGraph = false;
            phaseBlink.noRepeat = true;
            phaseBlink.shouldSprint = true;
            phaseBlink.shouldFireEquipment = false;
            phaseBlink.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            phaseBlink.driverUpdateTimerOverride = .2f;
            phaseBlink.selectionRequiresOnGround = true;


            // Blink off cooldown while in combat
            AISkillDriver blink = masterObject.AddComponent<AISkillDriver>();
            blink.skillSlot = SkillSlot.Utility;
            blink.requiredSkill = blinkSkill;
            blink.requireSkillReady = true;
            blink.requireEquipmentReady = false;
            blink.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            blink.minDistance = 0f;
            blink.maxDistance = 60f;
            blink.selectionRequiresTargetLoS = true;
            blink.activationRequiresTargetLoS = true;
            blink.activationRequiresAimConfirmation = false;
            blink.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            blink.aimType = AISkillDriver.AimType.MoveDirection;
            blink.ignoreNodeGraph = false;
            blink.noRepeat = true;
            blink.shouldSprint = true;
            blink.shouldFireEquipment = false;
            blink.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            blink.driverUpdateTimerOverride = .2f;


            // Use Laser Glaive off cooldown
            AISkillDriver laserGlaive = masterObject.AddComponent<AISkillDriver>();
            laserGlaive.skillSlot = SkillSlot.Secondary;
            laserGlaive.requireSkillReady = true;
            laserGlaive.requireEquipmentReady = false;
            laserGlaive.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            laserGlaive.minDistance = 0f;
            laserGlaive.maxDistance = 60f;
            laserGlaive.selectionRequiresTargetLoS = true;
            laserGlaive.activationRequiresTargetLoS = true;
            laserGlaive.activationRequiresAimConfirmation = false;
            laserGlaive.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            laserGlaive.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            laserGlaive.ignoreNodeGraph = false;
            laserGlaive.noRepeat = false;
            laserGlaive.shouldSprint = true;
            laserGlaive.shouldFireEquipment = false;
            laserGlaive.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;


            // Retreat & shoot
            AISkillDriver retreat = masterObject.AddComponent<AISkillDriver>();
            retreat.skillSlot = SkillSlot.Primary;
            retreat.requireSkillReady = false;
            retreat.requireEquipmentReady = false;
            retreat.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            retreat.minDistance = 0f;
            retreat.maxDistance = 20f;
            retreat.selectionRequiresTargetLoS = true;
            retreat.activationRequiresTargetLoS = true;
            retreat.activationRequiresAimConfirmation = false;
            retreat.movementType = AISkillDriver.MovementType.FleeMoveTarget;
            retreat.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            retreat.ignoreNodeGraph = false;
            retreat.noRepeat = false;
            retreat.shouldSprint = true;
            retreat.shouldFireEquipment = false;
            retreat.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            retreat.driverUpdateTimerOverride = .5f;


            // Strafe & shoot
            AISkillDriver strafe = masterObject.AddComponent<AISkillDriver>();
            strafe.skillSlot = SkillSlot.Primary;
            strafe.requireSkillReady = false;
            strafe.requireEquipmentReady = false;
            strafe.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            strafe.minDistance = 0f;
            strafe.maxDistance = 60f;
            strafe.selectionRequiresTargetLoS = true;
            strafe.activationRequiresTargetLoS = true;
            strafe.activationRequiresAimConfirmation = false;
            strafe.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            strafe.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            strafe.ignoreNodeGraph = false;
            strafe.noRepeat = false;
            strafe.shouldSprint = true;
            strafe.shouldFireEquipment = false;
            strafe.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            strafe.driverUpdateTimerOverride = .5f;


            // Sprint towards the target & shoot
            AISkillDriver chase = masterObject.AddComponent<AISkillDriver>();
            chase.skillSlot = SkillSlot.Primary;
            chase.requireSkillReady = false;
            chase.requireEquipmentReady = false;
            chase.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            chase.minDistance = 0f;
            chase.maxDistance = 400f;
            chase.selectionRequiresTargetLoS = false;
            chase.activationRequiresTargetLoS = false;
            chase.activationRequiresAimConfirmation = false;
            chase.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            chase.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            chase.ignoreNodeGraph = false;
            chase.noRepeat = false;
            chase.shouldSprint = true;
            chase.shouldFireEquipment = false;
            chase.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            chase.resetCurrentEnemyOnNextDriverSelection = true;


            // Phase Blink off cooldown to follow the owner
            AISkillDriver phaseBlinkOwner = masterObject.AddComponent<AISkillDriver>();
            phaseBlinkOwner.skillSlot = SkillSlot.Utility;
            phaseBlinkOwner.requireSkillReady = true;
            phaseBlinkOwner.requireEquipmentReady = false;
            phaseBlinkOwner.moveTargetType = AISkillDriver.TargetType.CurrentLeader;
            phaseBlinkOwner.minDistance = 40f;
            phaseBlinkOwner.maxDistance = float.PositiveInfinity;
            phaseBlinkOwner.selectionRequiresTargetLoS = false;
            phaseBlinkOwner.activationRequiresTargetLoS = false;
            phaseBlinkOwner.activationRequiresAimConfirmation = false;
            phaseBlinkOwner.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            phaseBlinkOwner.aimType = AISkillDriver.AimType.MoveDirection;
            phaseBlinkOwner.ignoreNodeGraph = false;
            phaseBlinkOwner.noRepeat = false;
            phaseBlinkOwner.shouldSprint = true;
            phaseBlinkOwner.shouldFireEquipment = false;
            phaseBlinkOwner.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            phaseBlinkOwner.driverUpdateTimerOverride = .2f;
            phaseBlinkOwner.resetCurrentEnemyOnNextDriverSelection = true;
            phaseBlinkOwner.selectionRequiresOnGround = true;


            // Sprint towards the owner
            AISkillDriver followOwner = masterObject.AddComponent<AISkillDriver>();
            followOwner.skillSlot = SkillSlot.Primary;
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
            followOwner.buttonPressType = AISkillDriver.ButtonPressType.Hold;
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


            // Sprint towards the target & shoot
            AISkillDriver chaseLowPriority = masterObject.AddComponent<AISkillDriver>();
            chaseLowPriority.skillSlot = SkillSlot.Primary;
            chaseLowPriority.requireSkillReady = false;
            chaseLowPriority.requireEquipmentReady = false;
            chaseLowPriority.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            chaseLowPriority.minDistance = 0f;
            chaseLowPriority.maxDistance = float.PositiveInfinity;
            chaseLowPriority.selectionRequiresTargetLoS = false;
            chaseLowPriority.activationRequiresTargetLoS = false;
            chaseLowPriority.activationRequiresAimConfirmation = false;
            chaseLowPriority.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            chaseLowPriority.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            chaseLowPriority.ignoreNodeGraph = false;
            chaseLowPriority.noRepeat = false;
            chaseLowPriority.shouldSprint = true;
            chaseLowPriority.shouldFireEquipment = false;
            chaseLowPriority.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            chaseLowPriority.resetCurrentEnemyOnNextDriverSelection = true;


            // Fire Arrow Rain
            AISkillDriver fireArrowRain = masterObject.AddComponent<AISkillDriver>();
            fireArrowRain.skillSlot = SkillSlot.Primary;
            fireArrowRain.requireSkillReady = false;
            fireArrowRain.requireEquipmentReady = false;
            fireArrowRain.moveTargetType = AISkillDriver.TargetType.Custom;
            fireArrowRain.minDistance = 0f;
            fireArrowRain.maxDistance = 300f;
            fireArrowRain.selectionRequiresTargetLoS = false;
            fireArrowRain.activationRequiresTargetLoS = false;
            fireArrowRain.activationRequiresAimConfirmation = false;
            fireArrowRain.movementType = AISkillDriver.MovementType.Stop;
            fireArrowRain.aimType = AISkillDriver.AimType.AtMoveTarget;
            fireArrowRain.ignoreNodeGraph = false;
            fireArrowRain.noRepeat = false;
            fireArrowRain.shouldSprint = false;
            fireArrowRain.shouldFireEquipment = false;
            fireArrowRain.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            fireArrowRain.driverUpdateTimerOverride = .4f;


            // Airborne blink after Special skill
            AISkillDriver airBlink = masterObject.AddComponent<AISkillDriver>();
            airBlink.skillSlot = SkillSlot.Utility;
            airBlink.requireSkillReady = true;
            airBlink.requireEquipmentReady = false;
            airBlink.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            airBlink.minDistance = 0f;
            airBlink.maxDistance = float.PositiveInfinity;
            airBlink.selectionRequiresTargetLoS = false;
            airBlink.activationRequiresTargetLoS = false;
            airBlink.activationRequiresAimConfirmation = false;
            //airBlink.movementType = AISkillDriver.MovementType.StrafeMovetarget;
            airBlink.aimType = AISkillDriver.AimType.MoveDirection;
            airBlink.ignoreNodeGraph = false;
            airBlink.noRepeat = false;
            airBlink.shouldSprint = true;
            airBlink.shouldFireEquipment = false;
            airBlink.buttonPressType = AISkillDriver.ButtonPressType.Hold;
            airBlink.driverUpdateTimerOverride = .2f;


            // Overrides
            castArrowRain.nextHighPriorityOverride = fireArrowRain;
            fireArrowRain.nextHighPriorityOverride = airBlink;
            fireBallista.nextHighPriorityOverride = airBlink;
        }

        private void CharacterMaster_onCharacterMasterDiscovered(CharacterMaster master)
        {
            if (master.masterIndex == MasterCatalog.FindMasterIndex("HuntressMonsterMaster"))
            {
                var component = master.GetComponent<ArrowRainTargeting>();
                if (!component && master.gameObject)
                {
                    component = master.gameObject.AddComponent<ArrowRainTargeting>();
                }
            }
        }
    }

    public class ArrowRainTargeting : MonoBehaviour
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

                Vector3 originPos = ai.gameObject.transform.position;
                Vector3 targetPos = ai.currentEnemy.gameObject.transform.position;

                Ray aimRay = new Ray(targetPos, Vector3.down);
                float rayLength = 20f + Mathf.Abs(ai.currentEnemy.characterBody.transform.position.y - ai.currentEnemy.characterBody.footPosition.y);
                if (Physics.Raycast(aimRay, out var hitInfo, rayLength, LayerIndex.world.mask))
                {
                    if (ai.HasLOS(hitInfo.point + Vector3.up * .02f))
                    {
                        hasTarget = true;
                        wallPosition = hitInfo.point + Vector3.up * .02f;
                    }
                }

                // If the "place under target" thing doesn't work out, just check if it's against a sufficiently close surface
                if (!hasTarget)
                {
                    Vector3 direction = (targetPos - originPos).normalized;
                    aimRay = new Ray(originPos, direction);
                    rayLength = 20f + (originPos - targetPos).magnitude;
                    if (Physics.Raycast(aimRay, out var hitInfo2, rayLength, LayerIndex.world.mask))
                    {
                        if (ai.HasLOS(hitInfo2.point + direction * -.02f))
                        {
                            hasTarget = true;
                            wallPosition = hitInfo2.point + direction * -.02f;
                        }
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
