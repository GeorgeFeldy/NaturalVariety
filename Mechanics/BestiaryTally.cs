using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using NaturalVariety.UI;
using NaturalVariety.Utils;

namespace NaturalVariety.Mechanics
{
    public class BestiaryTallyWorldLoad : ModSystem
    { 
        public override void OnWorldLoad()
        {
            BestiaryTally.SetKillCountsInBestiary();
        }

    }

    public class BestiaryTallyGlobalNPC : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return true;
        }

        public override bool SpecialOnKill(NPC npc)
        {
            BestiaryTally.SetKillCountsInBestiary();
            return false;
        }
    }

    public static class BestiaryTally
    {

        /// <summary>
        /// Updates kill counts in bestiary for all loaded npcs 
        /// </summary>
        public static void SetKillCountsInBestiary() 
        {
            for (int npcId = -65; npcId < NPCLoader.NPCCount; npcId++)
            {
                NPC npc = ContentSamples.NpcsByNetId[npcId];
                BestiaryTally.SetKillCountsInBestiary(npc);
            }
        }

        /// <summary>
        /// Updates kill counts for a single NPC 
        /// </summary>
        /// <param name="npc"></param>
        public static void SetKillCountsInBestiary(NPC npc) // for 1 npc 
        {
            IBestiaryInfoElement tallyInfo;
            
            BestiaryEntry entry = Main.BestiaryDB.FindEntryByNPCID(npc.netID);
            tallyInfo = new BestiaryTallyInfoElement(npc, npc.netID);
            entry.Info.RemoveAll(IsTallyBestiaryInfoElement);
            entry.Info.Add(tallyInfo);

            Main.BestiaryDB.Register(entry);
        }

        /// <summary>
        /// Sets kill count in bestiary for all variants of a given npc 
        /// </summary>
        /// <param name="npc"></param>
        // public static void SetKillCountsInBestiaryForAllVariants(NPC npc) 
        // {
        //     List<NPC> listOfVariants = NetIdHelper.GetListOfSameBannerVariants(npc);
        // 
        //     foreach (NPC varNpc in listOfVariants)
        //     {
        //         SetKillCountsInBestiary(varNpc);
        //     }
        // }

        public static bool IsTallyBestiaryInfoElement(IBestiaryInfoElement element)
        {
            return element.GetType() == typeof(BestiaryTallyInfoElement);
        }
    }
}
