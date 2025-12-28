using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImprovedSurvivorAI
{
    public class InitSurvivors
    {
        public static void AdjustVanillaSurvivors()
        {
            //  Commando  //
            GameObject commandoMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(commandoMaster);
            new CommandoAI(commandoMaster);

            //  Huntress  //
            GameObject huntressMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Huntress/HuntressMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(huntressMaster);
            new HuntressAI(huntressMaster);

            //  Bandit  //
            GameObject banditMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Bandit2/Bandit2MonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(banditMaster);
            new BanditAI(banditMaster);

            //  MUL-T  //
            GameObject multMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Toolbot/ToolbotMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(multMaster);
            new MultAI(multMaster);

            //  Engineer  //
            GameObject engiMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Engi/EngiMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(engiMaster);
            new EngineerAI(engiMaster);

            //  Artificer  //
            GameObject artiMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MageMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(artiMaster);
            new ArtificerAI(artiMaster);

            //  Mercenary  //
            GameObject mercMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Merc/MercMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(mercMaster);
            new MercenaryAI(mercMaster);

            //  REX  //
            GameObject rexMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Treebot/TreebotMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(rexMaster);
            new RexAI(rexMaster);

            //  Loader  //
            GameObject loaderMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Loader/LoaderMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(loaderMaster);
            new LoaderAI(loaderMaster);

            //  Acrid  //
            GameObject acridMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Croco/CrocoMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(acridMaster);
            new AcridAI(acridMaster);

            //  Captain  //
            GameObject captainMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Captain/CaptainMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(captainMaster);
            new CaptainAI(captainMaster, Main.enableCaptainBeacons);

            //  Railgunner  //
            GameObject railgunnerMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/RailgunnerMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(railgunnerMaster);
            new RailgunnerAI(railgunnerMaster);

            //  Void Fiend  //
            GameObject voidFiendMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidSurvivorMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(voidFiendMaster);
            new VoidFiendAI(voidFiendMaster);

            //  Seeker  //
            GameObject seekerMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Seeker/SeekerMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(seekerMaster);
            new SeekerAI(seekerMaster);

            //  False Son  //
            GameObject falseSonMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/FalseSon/FalseSonMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(falseSonMaster);
            new FalseSonAI(falseSonMaster);

            //  CHEF  //
            GameObject chefMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Chef/ChefMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(chefMaster);
            new ChefAI(chefMaster);

            //  Operator  //
            GameObject operatorMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/Drone Tech/DroneTechMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(operatorMaster);
            new OperatorAI(operatorMaster);

            //  Drifter  //
            GameObject drifterMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC3/Drifter/DrifterMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(drifterMaster);
            new DrifterAI(drifterMaster);

            //  Heretic  //
            GameObject hereticMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Heretic/HereticMonsterMaster.prefab").WaitForCompletion();
            Main.ClearSkillDrivers(hereticMaster);
            new HereticAI(hereticMaster);
        }

        public static void AdjustModdedSurvivors()
        {
            //  Enforcer  //
            GameObject enforcerMaster = MasterCatalog.FindMasterPrefab("EnforcerMonsterMaster");
            if (enforcerMaster)
            {
                Main.ClearSkillDrivers(enforcerMaster);
                new EnforcerAI(enforcerMaster);
            }

            //  Enforcer  //
            GameObject nemesisEnforcerMaster = MasterCatalog.FindMasterPrefab("NemesisEnforcerMonsterMaster");
            if (nemesisEnforcerMaster)
            {
                Main.ClearSkillDrivers(nemesisEnforcerMaster);
                new NemesisEnforcerAI(nemesisEnforcerMaster);
            }

            //  Sonic  //
            GameObject sonicMaster = MasterCatalog.FindMasterPrefab("SonicTheHedgehogMonsterMaster");
            if (sonicMaster)
            {
                Main.ClearSkillDrivers(sonicMaster);
                new SonicAI(sonicMaster);
            }
        }
    }
}
