using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaFrog : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.Frog;

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nThe red-eyed tree frog (Agalychnis callidryas) is a hylid native to Neotropical rainforests where it ranges from Mexico, through Central America, to Colombia. " +
                    "This species has large, bright red eyes with vertically narrowed pupils. ")
           });

        }
    }
}
