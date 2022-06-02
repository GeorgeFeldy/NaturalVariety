using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;
using Terraria.GameContent.Bestiary;

namespace NaturalVariety.NPCs.GlobalNPCs
{
    public class VanillaGrebe : GlobalNPC
    {

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return (entity.type == NPCID.Grebe); // ||
                   //(entity.type == NPCID.Grebe2);
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
                   "[c/0EFF0E:Natural Variety]:\nThe black-necked grebe or eared grebe (Podiceps nigricollis) is a member of the grebe family of water birds. " +
                   "The species' breeding plumage has the head, neck, breast, and upper parts coloured black to blackish brown, with the exception of the ochre-coloured " + 
                   "fan of feathers extending behind the eye over the eye-coverts and sides of the nape.This species breeds in vegetated areas of freshwater lakes across Europe, Asia, Africa, and America.")
           });

        }


    }
}
