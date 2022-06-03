using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;
using Terraria.GameContent.Bestiary;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaCardinal: GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.BirdRed;
        }

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nThe northern cardinal (Cardinalis cardinalis) is a bird in the genus Cardinalis; it is also known colloquially as the redbird, common cardinal, red cardinal, or just cardinal. " +
                    "It can be found in southeastern Canada, through the eastern United States, southern Arizona, southern California, and south through Mexico, Belize, and Guatemala. It has a distinctive crest on the head and " +
                    "a mask on the face which is black in the male and gray in the female. The male is a vibrant red, while the female is a reddish olive color. ")
           });

        }


    }
}
