using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaMouse : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.Mouse;


        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nMice are small rodents that are known to have a pointed snout, small rounded ears, a body-length scaly tail, and a high breeding rate. " +
                    "The best known mouse species is the common house mouse (Mus musculus). Mice are also popular as pets. In some places, certain kinds of field mice are locally common. " +
                    "They are known to invade homes for food and shelter.")
           });
        }
    }
}
