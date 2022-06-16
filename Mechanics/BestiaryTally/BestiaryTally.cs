using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;


namespace NaturalVariety.Mechanics.BestiaryTally
{

    /// <summary>
    /// Do a bestiary kill count update on world load
    /// </summary>
    public class BestiaryTallyWorldLoad : ModSystem
    {
        public override void OnWorldLoad()
        {
            BestiaryTally.SetKillCountsInBestiary();
        }
    }

    /// <summary>
    /// Do bestiary kill count update when a player equips/unequips Tally Counter items 
    /// (Inventory included)
    /// </summary>
    public class BestiaryTallyPlayer : ModPlayer
    {
        bool lastPlayerTallyCounterState;
        bool currentPlayerTallyCounterState;

        public override void PreUpdate()
        {
            lastPlayerTallyCounterState = Player.accJarOfSouls;
        }

        public override void PostUpdate()
        {
            currentPlayerTallyCounterState = Player.accJarOfSouls;
            if (lastPlayerTallyCounterState != currentPlayerTallyCounterState)
            {
                BestiaryTally.SetKillCountsInBestiary();
            }
        }

    }

    /// <summary>
    /// Update bestiary kill count on an NPC kill 
    /// </summary>
    public class BestiaryTallyGlobalNPC : GlobalNPC
    {

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return true;
        }

        public override void OnKill(NPC npc)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                BestiaryTally.SetKillCountsInBestiary();
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ModPacket updateBestiaryPacket = Mod.GetPacket();
                updateBestiaryPacket.Write((byte)NaturalVariety.MessageType.SyncBestiaryKillCount);
                updateBestiaryPacket.Send();
            }
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
                SetKillCountsInBestiary(npc);
            }
        }

        /// <summary>
        /// Updates kill counts for a single NPC 
        /// </summary>
        /// <param name="npc"></param>
        public static void SetKillCountsInBestiary(NPC npc)
        {

            BestiaryEntry entry = Main.BestiaryDB.FindEntryByNPCID(npc.netID);

            if (BestiaryEntryIsHidden(entry))
            {
                entry.Info.Clear();
                return;
            }

            IBestiaryInfoElement tallyInfo = new BestiaryTallyInfoElement(npc, npc.netID);
            entry.Info.RemoveAll(IsTallyBestiaryInfoElement);
            entry.AddTags(tallyInfo);
        }

        public static bool IsTallyBestiaryInfoElement(IBestiaryInfoElement element)
        {
            return element.GetType() == typeof(BestiaryTallyInfoElement);
        }

        public static bool BestiaryEntryIsHidden(BestiaryEntry entry)
        {
            return entry.Info.Count == 0;
        }

    }
}
