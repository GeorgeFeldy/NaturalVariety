using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

using NaturalVariety.Items.Critters;

using Terraria.Audio;

namespace NaturalVariety.NPCs.Critters
{
    public class GoldSeagull : Waterfowl 
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/GoldSeagull";

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<GoldSeagullItem>();
            NPC.rarity = 3;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                // BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                   "The golden seagull (Larus aururs) is a gull species endemic to Terraria's beaches. It is classified as endangered (EN) in the IUCN red list, due to illegal captures for their precious plumage.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float baseWaterChance = SpawnCondition.OverworldWaterSurfaceCritter.Chance * 0.00125f; // 1/800 water surface 
            float baseLandChance = SpawnCondition.Overworld.Chance * 0.00125f; // 1/800 any surface enemy 

            bool beachCondition = spawnInfo.Player.ZoneBeach; 

            ushort tileType = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType;
            bool sandCondition = (tileType == TileID.Sand) || (tileType == TileID.ShellPile); 
            
            return (beachCondition && sandCondition) ? baseWaterChance + baseLandChance : 0f;
        }

        public override void AI()
        {
            base.AI();
            if(Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Seagull);    
            }
        }

    }

    public class GoldSeagullFly : WaterfowlFly
    {

        public override string Texture => "NaturalVariety/NPCs/Critters/GoldSeagull";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Gold Seagull");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.catchItem = (short)ModContent.ItemType<GoldSeagullItem>();
            NPC.rarity = 3;
        }

        public override void AI()
        {
            base.AI();
            if (Main.rand.NextBool(400) && !Main.dedServ)
            {
                SoundEngine.PlaySound(SoundID.Seagull);
            }
        }

    }

}

