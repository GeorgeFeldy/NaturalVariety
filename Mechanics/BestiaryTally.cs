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

        public override bool InstancePerEntity => false;

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
        /// Sets kill counts in bestiary for all loaded npcs 
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
        public static void SetKillCountsInBestiary(NPC npc) 
        {
            
            BestiaryEntry entry = Main.BestiaryDB.FindEntryByNPCID(npc.netID);

            if (BestiaryEntryIsTrash(entry))
            {
                entry.Info.Clear();
                return;
            }

            IBestiaryInfoElement tallyInfo;

            tallyInfo = new BestiaryTallyInfoElement(npc, npc.netID);
            entry.Info.RemoveAll(IsTallyBestiaryInfoElement);
            entry.AddTags(tallyInfo);
        }

        public static bool IsTallyBestiaryInfoElement(IBestiaryInfoElement element)
        {
            return element.GetType() == typeof(BestiaryTallyInfoElement);
        }

        public static bool BestiaryEntryIsTrash(BestiaryEntry entry)
        {
            return entry.Info.Count == 0;
        }

    }
}
