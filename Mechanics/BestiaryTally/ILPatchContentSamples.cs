using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using System.Reflection;

namespace NaturalVariety.Mechanics.BestiaryTally
{
    /// <summary>
    /// Patches the Terraria.ID.ContentSamples.ModifyNPCIds() method, 
    /// which has inconsistencies in mapping variants to correct bestiary entry 
    /// (see IdMap dictionary)
    /// </summary>
    public class ILPatchContentSamples : ILoadable
    {
        public void Load(Mod mod)
        {
            IL.Terraria.ID.ContentSamples.ModifyNPCIds += HookPatchIds;
        }

        public void Unload() { }

        private static void HookPatchIds(ILContext il)
        {
            var c = new ILCursor(il);

            foreach (int id in toReplace)                          // each existing id that will be replaced 
            {
                if (!c.TryGotoNext(i => i.MatchLdcI4(id))) return; // find instruction that maps said id 

                c.Index += 2;                                      // skip 2 instructions
                c.Remove();                                        // remove the instr that maps to vanilla id 
                c.Emit(OpCodes.Ldc_I4, IdMap[id]);			       // replace with desired id 
            }

            c.Index++;

            c.EmitDelegate(InsertMapping);

        }

        private static void InsertMapping()
        {
            foreach (int id in toInsert)
            {
                ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[id] = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[IdMap[id]];
            }
        }

        /// <summary>
        /// List of all NPC ids in ModifyNPCIds() that are modified to the wrong base id 
        /// has to be ordered!!!
        /// </summary>
        private static readonly List<int> toReplace = new()
        {
            NPCID.BigHornetStingy           ,
            NPCID.LittleHornetStingy        ,
            NPCID.BigHornetSpikey           ,
            NPCID.LittleHornetSpikey        ,
            NPCID.BigHornetLeafy            ,
            NPCID.LittleHornetLeafy         ,
            NPCID.BigHornetHoney            ,
            NPCID.LittleHornetHoney         ,
            NPCID.BigHornetFatty            ,
            NPCID.LittleHornetFatty         ,
            NPCID.BigPantlessSkeleton       ,
            NPCID.SmallPantlessSkeleton     ,
            NPCID.BigMisassembledSkeleton   ,
            NPCID.SmallMisassembledSkeleton ,
            NPCID.BigHeadacheSkeleton       ,
            NPCID.SmallHeadacheSkeleton     ,
            NPCID.BigFemaleZombie           ,
            NPCID.SmallFemaleZombie         ,
            NPCID.DemonEye2                 ,
            NPCID.PurpleEye2                ,
            NPCID.GreenEye2                 ,
            NPCID.DialatedEye2              ,
            NPCID.SleepyEye2                ,
            NPCID.CataractEye2              ,
            NPCID.BigTwiggyZombie           ,
            NPCID.SmallTwiggyZombie         ,
            NPCID.BigSwampZombie            ,
            NPCID.SmallSwampZombie          ,
            NPCID.BigSlimedZombie           ,
            NPCID.SmallSlimedZombie

        };

        private static readonly List<int> toInsert = new()
        {
            NPCID.BigBaldZombie             ,
            NPCID.SmallBaldZombie           ,
            NPCID.Duck2                     ,
            NPCID.DuckWhite2                ,
            NPCID.Grebe2                    ,
            NPCID.Seagull2
        };

        private static readonly Dictionary<int, int> IdMap = new()
        {
            { NPCID.BigHornetStingy           , NPCID.HornetStingy},
            { NPCID.LittleHornetStingy        , NPCID.HornetStingy},
            { NPCID.BigHornetSpikey           , NPCID.HornetSpikey},
            { NPCID.LittleHornetSpikey        , NPCID.HornetSpikey},
            { NPCID.BigHornetLeafy            , NPCID.HornetLeafy},
            { NPCID.LittleHornetLeafy         , NPCID.HornetLeafy},
            { NPCID.BigHornetHoney            , NPCID.HornetHoney},
            { NPCID.LittleHornetHoney         , NPCID.HornetHoney},
            { NPCID.BigHornetFatty            , NPCID.HornetFatty},
            { NPCID.LittleHornetFatty         , NPCID.HornetFatty},
            { NPCID.BigPantlessSkeleton       , NPCID.PantlessSkeleton},
            { NPCID.SmallPantlessSkeleton     , NPCID.PantlessSkeleton},
            { NPCID.BigMisassembledSkeleton   , NPCID.MisassembledSkeleton},
            { NPCID.SmallMisassembledSkeleton , NPCID.MisassembledSkeleton},
            { NPCID.BigHeadacheSkeleton       , NPCID.HeadacheSkeleton},
            { NPCID.SmallHeadacheSkeleton     , NPCID.HeadacheSkeleton},
            { NPCID.BigFemaleZombie           , NPCID.FemaleZombie},
            { NPCID.SmallFemaleZombie         , NPCID.FemaleZombie},
            { NPCID.DemonEye2                 , NPCID.DemonEye},
            { NPCID.PurpleEye2                , NPCID.PurpleEye},
            { NPCID.GreenEye2                 , NPCID.GreenEye},
            { NPCID.DialatedEye2              , NPCID.DialatedEye},
            { NPCID.SleepyEye2                , NPCID.SleepyEye},
            { NPCID.CataractEye2              , NPCID.CataractEye},
            { NPCID.BigTwiggyZombie           , NPCID.TwiggyZombie},
            { NPCID.SmallTwiggyZombie         , NPCID.TwiggyZombie},
            { NPCID.BigSwampZombie            , NPCID.SwampZombie},
            { NPCID.SmallSwampZombie          , NPCID.SwampZombie},
            { NPCID.BigSlimedZombie           , NPCID.SlimedZombie},
            { NPCID.SmallSlimedZombie         , NPCID.SlimedZombie},
            { NPCID.BigBaldZombie             , NPCID.BaldZombie},
            { NPCID.SmallBaldZombie           , NPCID.BaldZombie},
            { NPCID.Duck2                     , NPCID.Duck},
            { NPCID.DuckWhite2                , NPCID.DuckWhite},
            { NPCID.Grebe2                    , NPCID.Grebe},
            { NPCID.Seagull2                  , NPCID.Seagull}
        };


    }


}
