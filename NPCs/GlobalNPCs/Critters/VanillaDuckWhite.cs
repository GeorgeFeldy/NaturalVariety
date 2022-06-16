using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaDuckWhite : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.DuckWhite;
        }

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nThe White Pekin is a breed of the domestic duck (Anas platyrhynchos domesticus) that has been domesticated by humans, but feral populations can be found in the wild along mallards.")
           });

        }
    }
}
