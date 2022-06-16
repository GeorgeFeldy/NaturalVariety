using Terraria;
using Terraria.ID;

namespace NaturalVariety.Utils
{
    /// <summary>
    /// Helper for vanilla NPC variant conversion 
    /// </summary>
    public static class NetIdHelper
    {

        /// <summary>
        /// NPCs specifically excluded from banner display in bestiary 
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static bool IsExcludedFromBannerTally(NPC npc)
        {
            return npc.netID == NPCID.StardustCellSmall;
        }

        /// <summary>
        /// NPCs specifically excluded from bestiary display due to inconsistencies 
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static bool IsExcludedFromVariantsDisplay(NPC npc) // TODO: replace with BoolSet  
        {                                                         // Commented ones actually work 

            return (npc.netID == NPCID.BigHornetStingy ||
                    npc.netID == NPCID.LittleHornetStingy ||
                    npc.netID == NPCID.HornetStingy ||
                    npc.netID == NPCID.BigHornetSpikey ||
                    npc.netID == NPCID.LittleHornetSpikey ||
                    npc.netID == NPCID.HornetSpikey ||
                    npc.netID == NPCID.BigHornetLeafy ||
                    npc.netID == NPCID.LittleHornetLeafy ||
                    npc.netID == NPCID.HornetLeafy ||
                    npc.netID == NPCID.BigHornetHoney ||
                    npc.netID == NPCID.LittleHornetHoney ||
                    npc.netID == NPCID.HornetHoney ||
                    npc.netID == NPCID.BigHornetFatty ||
                    npc.netID == NPCID.LittleHornetFatty ||
                    npc.netID == NPCID.HornetFatty ||
                    npc.netID == NPCID.BigPantlessSkeleton ||
                    npc.netID == NPCID.SmallPantlessSkeleton ||
                    npc.netID == NPCID.PantlessSkeleton ||
                    npc.netID == NPCID.BigMisassembledSkeleton ||
                    npc.netID == NPCID.SmallMisassembledSkeleton ||
                    npc.netID == NPCID.MisassembledSkeleton ||
                    npc.netID == NPCID.BigHeadacheSkeleton ||
                    npc.netID == NPCID.SmallHeadacheSkeleton ||
                    npc.netID == NPCID.HeadacheSkeleton ||
                    npc.netID == NPCID.BigSkeleton ||
                    npc.netID == NPCID.SmallSkeleton ||
                    npc.netID == NPCID.Skeleton ||
                    npc.netID == NPCID.BigFemaleZombie ||
                    npc.netID == NPCID.SmallFemaleZombie ||
                    npc.netID == NPCID.FemaleZombie ||
                    npc.netID == NPCID.DemonEye ||
                    npc.netID == NPCID.PurpleEye ||
                    npc.netID == NPCID.GreenEye ||
                    npc.netID == NPCID.DialatedEye ||
                    npc.netID == NPCID.SleepyEye ||
                    npc.netID == NPCID.CataractEye ||
                    npc.netID == NPCID.DemonEye2 ||
                    npc.netID == NPCID.PurpleEye2 ||
                    npc.netID == NPCID.GreenEye2 ||
                    npc.netID == NPCID.DialatedEye2 ||
                    npc.netID == NPCID.SleepyEye2 ||
                    npc.netID == NPCID.CataractEye2 ||
                    //npc.netID == NPCID.TorchZombie ||
                    npc.netID == NPCID.BigTwiggyZombie ||
                    npc.netID == NPCID.SmallTwiggyZombie ||
                    npc.netID == NPCID.TwiggyZombie ||
                    npc.netID == NPCID.BigSwampZombie ||
                    npc.netID == NPCID.SmallSwampZombie ||
                    npc.netID == NPCID.SwampZombie ||
                    npc.netID == NPCID.BigSlimedZombie ||
                    npc.netID == NPCID.SmallSlimedZombie ||
                    npc.netID == NPCID.SlimedZombie ||
                    //npc.netID == NPCID.BigPincushionZombie ||
                    //npc.netID == NPCID.SmallPincushionZombie ||
                    //npc.netID == NPCID.PincushionZombie ||
                    npc.netID == NPCID.BigBaldZombie ||
                    npc.netID == NPCID.SmallBaldZombie ||
                    npc.netID == NPCID.BaldZombie ||
                    npc.netID == NPCID.BigZombie ||
                    npc.netID == NPCID.SmallZombie ||
                    npc.netID == NPCID.Zombie ||
                    //npc.netID == NPCID.ArmedTorchZombie ||
                    npc.netID == NPCID.ArmedZombie ||
                    npc.netID == NPCID.ArmedZombieCenx ||
                    npc.netID == NPCID.ArmedZombieEskimo ||
                    npc.netID == NPCID.ArmedZombiePincussion ||
                    npc.netID == NPCID.ArmedZombieSlimed ||
                    npc.netID == NPCID.ArmedZombieSwamp ||
                    npc.netID == NPCID.ArmedZombieTwiggy ||
                    npc.netID == NPCID.BoneThrowingSkeleton ||
                    npc.netID == NPCID.BoneThrowingSkeleton2 ||
                    npc.netID == NPCID.BoneThrowingSkeleton3 ||
                    npc.netID == NPCID.BoneThrowingSkeleton4);
        }


        /// <summary>
        /// Maps variant IDs to their base counterpart. 
        /// ContentSamples does this but is inconsistent for Zombies, Skeletons, Hornets and Demon Eyes.. 
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static string MapNetIdToBaseCreditId(NPC npc)
        {

            int netId = npc.netID;

            string defaultCreditId = npc.GetBestiaryCreditId();

            switch (netId)
            {
                case (NPCID.BigHornetStingy): return "HornetStingy";
                case (NPCID.LittleHornetStingy): return "HornetStingy";
                case (NPCID.BigHornetSpikey): return "HornetSpikey";
                case (NPCID.LittleHornetSpikey): return "HornetSpikey";
                case (NPCID.BigHornetLeafy): return "HornetLeafy";
                case (NPCID.LittleHornetLeafy): return "HornetLeafy";
                case (NPCID.BigHornetHoney): return "HornetHoney";
                case (NPCID.LittleHornetHoney): return "HornetHoney";
                case (NPCID.BigHornetFatty): return "HornetFatty";
                case (NPCID.LittleHornetFatty): return "HornetFatty";

                // raincoat zombies 
                case (NPCID.BigRainZombie): return "ZombieRaincoat";
                case (NPCID.SmallRainZombie): return "ZombieRaincoat";

                // skeletons                          
                case (NPCID.BigPantlessSkeleton): return "PantlessSkeleton";
                case (NPCID.SmallPantlessSkeleton): return "PantlessSkeleton";
                case (NPCID.BigMisassembledSkeleton): return "MisassembledSkeleton";
                case (NPCID.SmallMisassembledSkeleton): return "MisassembledSkeleton";
                case (NPCID.BigHeadacheSkeleton): return "HeadacheSkeleton";
                case (NPCID.SmallHeadacheSkeleton): return "HeadacheSkeleton";
                case (NPCID.BigSkeleton): return "Skeleton";
                case (NPCID.SmallSkeleton): return "Skeleton";
                case (NPCID.BigFemaleZombie): return "FemaleZombie";
                case (NPCID.SmallFemaleZombie): return "FemaleZombie";

                // map negative id eye variants to their counterparts
                case (NPCID.DemonEye2): return "DemonEye";
                case (NPCID.PurpleEye2): return "PurpleEye";
                case (NPCID.GreenEye2): return "GreenEye";
                case (NPCID.DialatedEye2): return "DialatedEye";
                case (NPCID.SleepyEye2): return "SleepyEye";
                case (NPCID.CataractEye2): return "CataractEye";

                // some more zombies
                case (NPCID.BigTwiggyZombie): return "TwiggyZombie";
                case (NPCID.SmallTwiggyZombie): return "TwiggyZombie";
                case (NPCID.BigSwampZombie): return "SwampZombie";
                case (NPCID.SmallSwampZombie): return "SwampZombie";
                case (NPCID.BigSlimedZombie): return "SlimedZombie";
                case (NPCID.SmallSlimedZombie): return "SlimedZombie";
                case (NPCID.BigPincushionZombie): return "PincushionZombie";
                case (NPCID.SmallPincushionZombie): return "PincushionZombie";
                case (NPCID.BigBaldZombie): return "BaldZombie";
                case (NPCID.SmallBaldZombie): return "BaldZombie";
                case (NPCID.BigZombie): return "Zombie";
                case (NPCID.SmallZombie): return "Zombie";

                // crimson 
                case (NPCID.BigCrimslime): return "Crimslime";
                case (NPCID.LittleCrimslime): return "Crimslime";
                case (NPCID.BigCrimera): return "Crimera";
                case (NPCID.LittleCrimera): return "Crimera";

                // moss hornets 
                case (NPCID.GiantMossHornet): return "MossHornet";
                case (NPCID.BigMossHornet): return "MossHornet";
                case (NPCID.LittleMossHornet): return "MossHornet";
                case (NPCID.TinyMossHornet): return "MossHornet";

                // base hornet variants 
                case (NPCID.BigStinger): return "Hornet";
                case (NPCID.LittleStinger): return "Hornet";

                // special skeles
                case (NPCID.HeavySkeleton): return "ArmoredSkeleton";
                case (NPCID.BigBoned): return "AngryBones";
                case (NPCID.ShortBones): return "AngryBones";

                case (NPCID.BigEater): return "EaterofSouls";
                case (NPCID.LittleEater): return "EaterofSouls";

                // keep those 
                // case (NPCID.JungleSlime): 
                // case (NPCID.YellowSlime): 
                // case (NPCID.RedSlime): 
                // case (NPCID.PurpleSlime): 
                // case (NPCID.BlackSlime): 
                // case (NPCID.BabySlime): 
                // case (NPCID.Pinky): 
                // case (NPCID.GreenSlime): 
                // case (NPCID.Slimer2): 
                // case (NPCID.Slimeling) :     

                case (NPCID.GiantShelly2): return "GiantShelly2";
                case (NPCID.Crawdad2): return "Crawdad"; // not even used 
                case (NPCID.Salamander): return "Salamander2";
                case (NPCID.Salamander3): return "Salamander2";
                case (NPCID.Salamander4): return "Salamander2";
                case (NPCID.Salamander5): return "Salamander2";
                case (NPCID.Salamander6): return "Salamander2";
                case (NPCID.Salamander7): return "Salamander2";
                case (NPCID.Salamander8): return "Salamander2";
                case (NPCID.Salamander9): return "Salamander2";

                // armed zombies to normal 
                case (NPCID.ArmedTorchZombie): return "TorchZombie";
                case (NPCID.ArmedZombie): return "Zombie";
                case (NPCID.ArmedZombieCenx): return "FemaleZombie"; // ??
                case (NPCID.ArmedZombieEskimo): return "ZombieEskimo";
                case (NPCID.ArmedZombiePincussion): return "PincushionZombie";
                case (NPCID.ArmedZombieSlimed): return "SlimedZombie";
                case (NPCID.ArmedZombieSwamp): return "SwampZombie";
                case (NPCID.ArmedZombieTwiggy): return "TwiggyZombie";

                // spiders (fighter) to wall variant 
                case (NPCID.WallCreeper): return "WallCreeperWall";
                case (NPCID.JungleCreeper): return "JungleCreeperWall";
                case (NPCID.BlackRecluse): return "BlackRecluseWall";
                case (NPCID.BloodCrawler): return "BloodCrawlerWall";
                case (NPCID.DesertScorpionWalk): return "DesertScorpionWall";

                case (NPCID.BoneThrowingSkeleton): return "Skeleton";
                case (NPCID.BoneThrowingSkeleton2): return "HeadacheSkeleton";
                case (NPCID.BoneThrowingSkeleton3): return "MisassembledSkeleton";
                case (NPCID.BoneThrowingSkeleton4): return "PantlessSkeleton";

                case (NPCID.BlackDragonfly): return "RedDragonfly";
                case (NPCID.BlueDragonfly): return "RedDragonfly";
                case (NPCID.GreenDragonfly): return "RedDragonfly";
                case (NPCID.OrangeDragonfly): return "RedDragonfly";
                case (NPCID.YellowDragonfly): return "RedDragonfly";

                case (NPCID.GoldfishWalker): return "Goldfish";
                case (NPCID.GoldGoldfishWalker): return "GoldGoldfish";

                case (NPCID.Slimer2): return "Slimer";

                case (NPCID.LostGirl): return "Nymph";
                case (NPCID.Lihzahrd): return "LihzahrdCrawler";

                case (NPCID.VampireBat): return "Vampire";

                case (NPCID.DD2WitherBeastT2): return "DD2WitherBeastT3";
                case (NPCID.DD2SkeletonT1): return "DD2SkeletonT3";
                case (NPCID.DD2OgreT2): return "DD2OgreT3";
                case (NPCID.DD2WyvernT1): return "DD2WyvernT3";
                case (NPCID.DD2WyvernT2): return "DD2WyvernT3";
                case (NPCID.DD2GoblinT1): return "DD2GoblinT3";
                case (NPCID.DD2GoblinT2): return "DD2GoblinT3";
                case (NPCID.DD2DarkMageT1): return "DD2DarkMageT3";
                case (NPCID.DD2DrakinT2): return "DD2DrakinT3";
                case (NPCID.DD2GoblinBomberT1): return "DD2GoblinBomberT3";
                case (NPCID.DD2GoblinBomberT2): return "DD2GoblinBomberT3";
                case (NPCID.DD2KoboldFlyerT2): return "DD2KoboldFlyerT3";
                case (NPCID.DD2KoboldWalkerT2): return "DD2KoboldWalkerT3";
                case (NPCID.DD2JavelinstT1): return "DD2JavelinstT3";
                case (NPCID.DD2JavelinstT2): return "DD2JavelinstT3";

                case (NPCID.EaterofWorldsBody): return "EaterofWorldsHead";
                case (NPCID.EaterofWorldsTail): return "EaterofWorldsHead";

                default: return defaultCreditId;
            }
        }


        /// <summary>
        /// Maps the NetID of some variants to their base type with entry in bestiary for bestiary update purposes
        /// Similar to Vanilla ContentSamples 
        /// (yes i checked those manually)
        /// </summary>
        /// <param name="netId"></param>
        /// <returns></returns>
        public static int MapBaseIdForVariantsBestiary(int netId)
        {
            switch (netId)
            {
                // hornets
                case (NPCID.BigHornetStingy): return NPCID.HornetStingy;
                case (NPCID.LittleHornetStingy): return NPCID.HornetStingy;
                case (NPCID.BigHornetSpikey): return NPCID.HornetSpikey;
                case (NPCID.LittleHornetSpikey): return NPCID.HornetSpikey;
                case (NPCID.BigHornetLeafy): return NPCID.HornetLeafy;
                case (NPCID.LittleHornetLeafy): return NPCID.HornetLeafy;
                case (NPCID.BigHornetHoney): return NPCID.HornetHoney;
                case (NPCID.LittleHornetHoney): return NPCID.HornetHoney;
                case (NPCID.BigHornetFatty): return NPCID.HornetFatty;
                case (NPCID.LittleHornetFatty): return NPCID.HornetFatty;

                // raincoat zombies 
                case (NPCID.BigRainZombie): return NPCID.ZombieRaincoat;
                case (NPCID.SmallRainZombie): return NPCID.ZombieRaincoat;

                // skeletons                          
                case (NPCID.BigPantlessSkeleton): return NPCID.PantlessSkeleton;
                case (NPCID.SmallPantlessSkeleton): return NPCID.PantlessSkeleton;
                case (NPCID.BigMisassembledSkeleton): return NPCID.MisassembledSkeleton;
                case (NPCID.SmallMisassembledSkeleton): return NPCID.MisassembledSkeleton;
                case (NPCID.BigHeadacheSkeleton): return NPCID.HeadacheSkeleton;
                case (NPCID.SmallHeadacheSkeleton): return NPCID.HeadacheSkeleton;
                case (NPCID.BigSkeleton): return NPCID.Skeleton;
                case (NPCID.SmallSkeleton): return NPCID.Skeleton;
                case (NPCID.BigFemaleZombie): return NPCID.FemaleZombie;
                case (NPCID.SmallFemaleZombie): return NPCID.FemaleZombie;

                // map negative id eye variants to their counterparts
                case (NPCID.DemonEye2): return NPCID.DemonEye;
                case (NPCID.PurpleEye2): return NPCID.PurpleEye;
                case (NPCID.GreenEye2): return NPCID.GreenEye;
                case (NPCID.DialatedEye2): return NPCID.DialatedEye;
                case (NPCID.SleepyEye2): return NPCID.SleepyEye;
                case (NPCID.CataractEye2): return NPCID.CataractEye;

                // some more zombies
                case (NPCID.BigTwiggyZombie): return NPCID.TwiggyZombie;
                case (NPCID.SmallTwiggyZombie): return NPCID.TwiggyZombie;
                case (NPCID.BigSwampZombie): return NPCID.SwampZombie;
                case (NPCID.SmallSwampZombie): return NPCID.SwampZombie;
                case (NPCID.BigSlimedZombie): return NPCID.SlimedZombie;
                case (NPCID.SmallSlimedZombie): return NPCID.SlimedZombie;
                case (NPCID.BigPincushionZombie): return NPCID.PincushionZombie;
                case (NPCID.SmallPincushionZombie): return NPCID.PincushionZombie;
                case (NPCID.BigBaldZombie): return NPCID.BaldZombie;
                case (NPCID.SmallBaldZombie): return NPCID.BaldZombie;
                case (NPCID.BigZombie): return NPCID.Zombie;
                case (NPCID.SmallZombie): return NPCID.Zombie;

                // crimson 
                case (NPCID.BigCrimslime): return NPCID.Crimslime;
                case (NPCID.LittleCrimslime): return NPCID.Crimslime;
                case (NPCID.BigCrimera): return NPCID.Crimera;
                case (NPCID.LittleCrimera): return NPCID.Crimera;

                // moss hornets 
                case (NPCID.GiantMossHornet): return NPCID.MossHornet;
                case (NPCID.BigMossHornet): return NPCID.MossHornet;
                case (NPCID.LittleMossHornet): return NPCID.MossHornet;
                case (NPCID.TinyMossHornet): return NPCID.MossHornet;

                // base hornet variants 
                case (NPCID.BigStinger): return NPCID.Hornet;
                case (NPCID.LittleStinger): return NPCID.Hornet;

                // special skeles
                case (NPCID.HeavySkeleton): return NPCID.ArmoredSkeleton;
                case (NPCID.BigBoned): return NPCID.AngryBones;
                case (NPCID.ShortBones): return NPCID.AngryBones;

                case (NPCID.BigEater): return NPCID.EaterofSouls;
                case (NPCID.LittleEater): return NPCID.EaterofSouls;

                // keep those 
                // case (NPCID.JungleSlime):
                // case (NPCID.YellowSlime):
                // case (NPCID.RedSlime):
                // case (NPCID.PurpleSlime):
                // case (NPCID.BlackSlime):
                // case (NPCID.BabySlime):
                // case (NPCID.Pinky):
                // case (NPCID.GreenSlime):
                // case (NPCID.Slimer2):
                // case (NPCID.Slimeling):     

                case (NPCID.GiantShelly2): return NPCID.GiantShelly2;
                case (NPCID.Crawdad2): return NPCID.Crawdad; // not even used 
                case (NPCID.Salamander): return NPCID.Salamander2;
                case (NPCID.Salamander3): return NPCID.Salamander2;
                case (NPCID.Salamander4): return NPCID.Salamander2;
                case (NPCID.Salamander5): return NPCID.Salamander2;
                case (NPCID.Salamander6): return NPCID.Salamander2;
                case (NPCID.Salamander7): return NPCID.Salamander2;
                case (NPCID.Salamander8): return NPCID.Salamander2;
                case (NPCID.Salamander9): return NPCID.Salamander2;

                // armed zombies to normal 
                case (NPCID.ArmedTorchZombie): return NPCID.TorchZombie;
                case (NPCID.ArmedZombie): return NPCID.Zombie;
                case (NPCID.ArmedZombieCenx): return NPCID.FemaleZombie; // ??
                case (NPCID.ArmedZombieEskimo): return NPCID.ZombieEskimo;
                case (NPCID.ArmedZombiePincussion): return NPCID.PincushionZombie;
                case (NPCID.ArmedZombieSlimed): return NPCID.SlimedZombie;
                case (NPCID.ArmedZombieSwamp): return NPCID.SwampZombie;
                case (NPCID.ArmedZombieTwiggy): return NPCID.TwiggyZombie;

                // spiders (fighter) to wall variant 
                case (NPCID.WallCreeper): return NPCID.WallCreeperWall;
                case (NPCID.JungleCreeper): return NPCID.JungleCreeperWall;
                case (NPCID.BlackRecluse): return NPCID.BlackRecluseWall;
                case (NPCID.BloodCrawler): return NPCID.BloodCrawlerWall;
                case (NPCID.DesertScorpionWalk): return NPCID.DesertScorpionWall;

                case (NPCID.BoneThrowingSkeleton): return NPCID.Skeleton;
                case (NPCID.BoneThrowingSkeleton2): return NPCID.HeadacheSkeleton;
                case (NPCID.BoneThrowingSkeleton3): return NPCID.MisassembledSkeleton;
                case (NPCID.BoneThrowingSkeleton4): return NPCID.PantlessSkeleton;

                case (NPCID.BlackDragonfly): return NPCID.RedDragonfly;
                case (NPCID.BlueDragonfly): return NPCID.RedDragonfly;
                case (NPCID.GreenDragonfly): return NPCID.RedDragonfly;
                case (NPCID.OrangeDragonfly): return NPCID.RedDragonfly;
                case (NPCID.YellowDragonfly): return NPCID.RedDragonfly;

                case (NPCID.GoldfishWalker): return NPCID.Goldfish;
                case (NPCID.GoldGoldfishWalker): return NPCID.GoldGoldfish;

                case (NPCID.Slimer2): return NPCID.Slimer;

                case (NPCID.LostGirl): return NPCID.Nymph;
                case (NPCID.Lihzahrd): return NPCID.LihzahrdCrawler;

                case (NPCID.VampireBat): return NPCID.Vampire;

                case (NPCID.DD2WitherBeastT2): return NPCID.DD2WitherBeastT3;
                case (NPCID.DD2SkeletonT1): return NPCID.DD2SkeletonT3;
                case (NPCID.DD2OgreT2): return NPCID.DD2OgreT3;
                case (NPCID.DD2WyvernT1): return NPCID.DD2WyvernT3;
                case (NPCID.DD2WyvernT2): return NPCID.DD2WyvernT3;
                case (NPCID.DD2GoblinT1): return NPCID.DD2GoblinT3;
                case (NPCID.DD2GoblinT2): return NPCID.DD2GoblinT3;
                case (NPCID.DD2DarkMageT1): return NPCID.DD2DarkMageT3;
                case (NPCID.DD2DrakinT2): return NPCID.DD2DrakinT3;
                case (NPCID.DD2GoblinBomberT1): return NPCID.DD2GoblinBomberT3;
                case (NPCID.DD2GoblinBomberT2): return NPCID.DD2GoblinBomberT3;
                case (NPCID.DD2KoboldFlyerT2): return NPCID.DD2KoboldFlyerT3;
                case (NPCID.DD2KoboldWalkerT2): return NPCID.DD2KoboldWalkerT3;
                case (NPCID.DD2JavelinstT1): return NPCID.DD2JavelinstT3;
                case (NPCID.DD2JavelinstT2): return NPCID.DD2JavelinstT3;

                case (NPCID.EaterofWorldsBody): return NPCID.EaterofWorldsHead;
                case (NPCID.EaterofWorldsTail): return NPCID.EaterofWorldsHead;

                default: return netId;
            }
        }
    }
}