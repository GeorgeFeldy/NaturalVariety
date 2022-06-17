using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaDuckMallard : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Duck;
        }

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nThe mallard or wild duck (Anas platyrhynchos) is a dabbling duck that breeds in " +
                   "the temperate and subtropical areas. Males have purple patches on their wings, while the " +
                   "females (hens or ducks) have mainly brown-speckled plumage. Both sexes have an area of white-bordered black "+
                   "or iridescent blue feathers called a speculum on their wings; males especially tend to have blue speculum feathers.")
           });

        }
    }
}
