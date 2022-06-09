using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.Utils
{
    /// <summary>
    /// Helper for vanilla NPC variant conversion 
    /// </summary>
    public static class NetIdHelper
    {


        public static List<NPC> GetListOfSameBannerVariants(NPC npc)
        {
            List <NPC> listOfSameBannerVariants = new();

            for(int npcId = 0; npcId < NPCLoader.NPCCount; npcId++)
            {
                NPC newNPC = new() { type = npcId };
                if(Item.NPCtoBanner(npcId) == Item.NPCtoBanner(Item.BannerToNPC(npc.type)))
                {
                    listOfSameBannerVariants.Add(newNPC);
                }
            }
            return listOfSameBannerVariants;
        }

        /// <summary>
        /// Maps the NetID of some variants to their base class for bestiary update purposes
        /// Similar to Vanilla ContentSamples 
        /// (yes i checked those manually for the sake of quality)
        /// </summary>
        /// <param name="netId"></param>
        /// <returns></returns>
        public static int MapBaseIdForVariants(int netId)
        {
            switch (netId)
            {
                // hornets
                case (NPCID.BigHornetStingy): return NPCID.Hornet;
                case (NPCID.LittleHornetStingy): return NPCID.Hornet;
                case (NPCID.BigHornetSpikey): return NPCID.Hornet;
                case (NPCID.LittleHornetSpikey): return NPCID.Hornet;
                case (NPCID.BigHornetLeafy): return NPCID.Hornet;
                case (NPCID.LittleHornetLeafy): return NPCID.Hornet;
                case (NPCID.BigHornetHoney): return NPCID.Hornet;
                case (NPCID.LittleHornetHoney): return NPCID.Hornet;
                case (NPCID.BigHornetFatty): return NPCID.Hornet;
                case (NPCID.LittleHornetFatty): return NPCID.Hornet;

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

        // negatives mapping 
        // NPCID.FromNetId(netID);

        /// <summary>
        /// a messier version of the above function
        /// </summary>
        /// <param name="netID"></param>
        /// <returns></returns>
        public static int GetBaseIdFromVariantWithEntry(int netID)
        {
            switch (netID)
            {
            case(-65) : return 42   ;                    
            case(-64) : return 42   ;                  
            case(-63) : return 42   ;                  
            case(-62) : return 42   ;                  
            case(-61) : return 42   ;                  
            case(-60) : return 42   ;                  
            case(-59) : return 42   ;                  
            case(-58) : return 42   ;                  
            case(-57) : return 42   ;                  
            case(-56) : return 42   ;                  
            case(-55) : return 223  ;                  
            case(-54) : return 223  ;                  
            case(-53) : return 21   ;                  
            case(-52) : return 21   ;                  
            case(-51) : return 21   ;                  
            case(-50) : return 21   ;                  
            case(-49) : return 21   ;                  
            case(-48) : return 21   ;                  
            case(-47) : return 21   ;                  
            case(-46) : return 21   ;                  
            case(-45) : return 3    ;                  
            case(-44) : return 3    ;                  
            case(-43) : return 2    ;                  
            case(-42) : return 2    ;                  
            case(-41) : return 2    ;                  
            case(-40) : return 2    ;                  
            case(-39) : return 2    ;                  
            case(-38) : return 2    ;                  
            case(-37) : return 3    ;                  
            case(-36) : return 3    ;                  
            case(-35) : return 3    ;                  
            case(-34) : return 3    ;                  
            case(-33) : return 3    ;                  
            case(-32) : return 3    ;                  
            case(-31) : return 186  ;                  
            case(-30) : return 186  ;                  
            case(-27) : return 3    ;                  
            case(-26) : return 3    ;                  
            case(-23) : return 173  ;                  
            case(-22) : return 173  ;                  
            case(-25) : return 183  ;                  
            case(-24) : return 183  ;                  
            case(-21) : return 176  ;                  
            case(-20) : return 176  ;                  
            case(-19) : return 176  ;                  
            case(-18) : return 176  ;                  
            case(-17) : return 42   ;                  
            case(-16) : return 42   ;                  
            case(-15) : return 77   ;                  
            case(-14) : return 31   ;                  
            case(-13) : return 31   ;                  
            case(-12) : return 6    ;                  
            case(-11) : return 6    ;                  
            case(497) : return 496  ;                  
            case(495) : return 494  ;                  
            case(498) : return 499  ;                  
            case(499) : return 499  ;                  
            case(500) : return 499  ;                  
            case(501) : return 499  ;                  
            case(502) : return 499  ;                  
            case(503) : return 499  ;                  
            case(504) : return 499  ;                  
            case(505) : return 499  ;                  
            case(506) : return 499  ;                  
            case(591) : return 590  ;                  
            case(430) : return 3    ;                  
            case(436) : return 200  ;                  
            case(431) : return 161  ;                  
            case(432) : return 186  ;                  
            case(433) : return 187  ;                  
            case(434) : return 188  ;                  
            case(435) : return 189  ;                  
            case(164) : return 165  ;                  
            case(236) : return 237  ;                  
            case(163) : return 238  ;                  
            case(239) : return 240  ;                  
            case(530) : return 531  ;                  
            case(449) : return  21  ;                  
            case(450) : return 201  ;                  
            case(451) : return 202  ;                  
            case(452) : return 203  ;                  
            case(595) : return 599  ;                  
            case(596) : return 599  ;                  
            case(597) : return 599  ;                  
            case(598) : return 599  ;                  
            case(600) : return 599  ;                  
            case(230) : return 55   ;                  
            case(593) : return 592  ;                  
            case(-2 ) : return 121  ;                   
            case(195) : return 196  ;                   
            case(198) : return 199  ;                   
            case(158) : return 159  ;                   
            case(568) : return 569  ;                   
            case(566) : return 567  ;                   
            case(576) : return 577  ;                   
            case(558) : return 560  ;                   
            case(559) : return 560  ;                   
            case(552) : return 554  ;                   
            case(553) : return 554  ;                   
            case(564) : return 565  ;                   
            case(570) : return 571  ;                   
            case(555) : return 557  ;                   
            case(556) : return 557  ;                   
            case(574) : return 575  ;                   
            case(561) : return 563  ;                   
            case(562) : return 563  ;                   
            case(572) : return 573  ;                   
            case(14 ) : return 13   ;
            case(15)  : return 13   ;
            default   : return netID;
		    }
        }

    }
}