using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaBlueJay : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.BirdBlue;

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nThe blue jay (Cyanocitta cristata) is a passerine bird in the family Corvidae. " +
                   "Its coloration is predominantly blue, with a white chest and underparts, and a blue crest; it has a black, U-shaped collar around its " +
                   "neck and a black border behind the crest.")
           });

        }


    }
}
