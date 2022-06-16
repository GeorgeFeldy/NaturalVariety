using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaRaven : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Raven;
        }

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nRavens are large passerine birds in the genus Corvus, which includes crows and ravens. " +
                    "It is best represented by species such as the all-black Common Raven (C. Corax), found all across the " +
                    "Northern Hemisphere, and is the most widely distributed of all corvids.")
           });
        }
    }
}
