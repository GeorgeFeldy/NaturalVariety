using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;


using NaturalVariety.Items.Critters;
// using NaturalVariety.Items.Placeable.Banners;

namespace NaturalVariety.NPCs.Critters
{
    public class Pochard : Waterfowl 
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/Pochard";

        public override void SetDefaults()
        {
            base.SetDefaults();
            // NPC.catchItem = (short)ModContent.ItemType<BlackbirdItem>(); // placeholder
            NPC.catchItem = ItemID.Star; // placeholder
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "Hee hee")
            });
        }
    }

    public class PochardFly : WaterfowlFly
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/Pochard";

        public override void SetDefaults()
        {
            base.SetDefaults();
            // NPC.catchItem = (short)ModContent.ItemType<BlackbirdItem>(); // placeholder
            NPC.catchItem = ItemID.Star; // placeholder
        }

        // public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        // {
        //     bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        //     {
        //         BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        //         BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
        // 
        //         new FlavorTextBestiaryInfoElement(
        //            "Hee hee")
        //     });
        // }
    }

}

