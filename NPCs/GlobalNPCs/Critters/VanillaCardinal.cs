﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;
using Terraria.GameContent.Bestiary;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaBlueJay : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.BirdBlue;
        }

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nThe blue jay (Cyanocitta cristata) is a passerine bird in the family Corvidae, " +
                   "native to eastern North America. It lives in most of the eastern and central United States; some eastern populations may be migratory." +
                   "Its coloration is predominantly blue, with a white chest and underparts, and a blue crest; it has a black, U-shaped collar around its " +
                   "neck and a black border behind the crest.")
           });

        }


    }
}