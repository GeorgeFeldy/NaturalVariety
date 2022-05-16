using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

using NaturalVariety.Items.Critters;

namespace NaturalVariety.NPCs.Critters
{
    public class Blackbird: Songbird 
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<BlackbirdItem>();
        }
     
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                    "Blackbird (Turdus merula) is an Old World thrush in the family Turdidae. The male is all black except for " +
                    "a yellow eye-ring and bill and has a rich, melodious song; the adult female and juvenile have mainly dark brown plumage. ")
            });
        }

      
    }
}