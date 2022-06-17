using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace NaturalVariety.NPCs.GlobalNPCs.Critters
{
    public class VanillaGrebe : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == NPCID.Grebe;


        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
           {
                new FlavorTextBestiaryInfoElement(
                   NaturalVariety.ModTag + ":\nThe black-necked grebe or eared grebe (Podiceps nigricollis) is a member of the grebe family of water birds. " +
                   "The species' breeding plumage has the head, neck, breast, and upper parts coloured black to blackish brown, with the exception of " +
                   "the ochre-coloured fan of feathers extending behind the eye over the eye-coverts and sides of the nape.This species breeds in vegetated areas of freshwater lakes across Europe, Asia, Africa, and America.")
           });

        }


    }
}
