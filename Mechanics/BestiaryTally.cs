using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using NaturalVariety.UI;

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

        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);
            BestiaryTally.SetKillCountsInBestiary();
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
            
            BestiaryEntry entry = Main.BestiaryDB.FindEntryByNPCID(npc.netID);

            if (BestiaryEntryIsHidden(entry) && npc.ModNPC != null)
            {
                entry.Info.Clear();
                Main.BestiaryDB.Register(entry);
                return;
            }

            IBestiaryInfoElement tallyInfo;

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

        public static bool BestiaryEntryIsHidden(BestiaryEntry entry)
        {
            if(entry.Icon == null)
            {
                return true;
            }

            return (entry.Info.Count <= 0) || (entry.Icon.GetType() != typeof(UnlockableNPCEntryIcon));
        }

    }
}
