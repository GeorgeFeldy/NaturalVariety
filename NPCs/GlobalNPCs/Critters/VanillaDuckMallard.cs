using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;
using Terraria.GameContent.Bestiary;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaDuckMallard : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Duck;// ||
                                               //(entity.type == NPCID.Duck2);
        }

        //public override bool PreAI(NPC npc)
        //{
        //    NPC.setNPCName("Mallard Duck", npc.type);
        //    return true;
        //}

        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                    NaturalVariety.ModTag + ":\nThe mallard or wild duck (Anas platyrhynchos) is a dabbling duck that breeds throughout " +
                   "the temperate and subtropical Americas, Eurasia, and North Africa. Males have purple patches on their wings, while the " +
                   "females (hens or ducks) have mainly brown-speckled plumage. Both sexes have an area of white-bordered black "+
                   "or iridescent blue feathers called a speculum on their wings; males especially tend to have blue speculum feathers.")
           });

        }


    }
}
