﻿using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using NaturalVariety;
using NaturalVariety.UI;
using Microsoft.Xna.Framework;


namespace NaturalVariety
{
    public partial class NaturalVariety
    {
        public enum BestiaryMessageType : byte
        {
            SyncBestiaryKillCount
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            BestiaryMessageType messageType = (BestiaryMessageType)reader.ReadByte();
            if(messageType == BestiaryMessageType.SyncBestiaryKillCount)
            {
                if(Main.netMode == NetmodeID.MultiplayerClient)
                {
                    Mechanics.BestiaryTally.SetKillCountsInBestiary();
                }
            }
        }
    }
}

namespace NaturalVariety.Mechanics
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
        bool lastJarOfSoulsState;
        bool currentJarOfSoulsState;

        public override void PreUpdate()
        {
            this.lastJarOfSoulsState = Player.accJarOfSouls;
        }

        public override void PostUpdate()
        {
            this.currentJarOfSoulsState = Player.accJarOfSouls;
            if(lastJarOfSoulsState != currentJarOfSoulsState)
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
            if(Main.netMode == NetmodeID.SinglePlayer)
            {
                BestiaryTally.SetKillCountsInBestiary();
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ModPacket updateBestiaryPacket = Mod.GetPacket();
                updateBestiaryPacket.Write((byte)NaturalVariety.BestiaryMessageType.SyncBestiaryKillCount);
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
