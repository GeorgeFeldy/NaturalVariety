using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaRat : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Rat;
        }

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nRats are a family of rodents, best known being the members of the genus Rattus. The best-known Rattus species are the black rat (R. rattus) and the brown rat (R. norvegicus)." +
                    " They are omnivores and are considered serious pests by farmers. They are sometimes kept as pets!")
           });

        }


    }
}
