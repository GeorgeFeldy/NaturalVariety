using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace NaturalVariety.Utils
{

    public static class SpawnHelper
    {

        public static float GoldCritterModifier(NPCSpawnInfo spawnInfo, float chance) => (spawnInfo.Player.RollLuck(NPC.goldCritterChance) == 0) ? chance : 0f;


        public static float ForestBirdChance(NPCSpawnInfo spawnInfo)
        {
            float chance = SpawnCondition.OverworldDayBirdCritter.Chance +
                           SpawnCondition.TownGeneralCritter.Chance;

            bool modifier = IsOnGrass(spawnInfo) && DryTile(spawnInfo);

            float newChance = modifier ? chance : 0f;

            return newChance;
        }


        public static float JungleCritterChance(NPCSpawnInfo spawnInfo)
        {
            float chance = SpawnCondition.SurfaceJungle.Chance +
                           SpawnCondition.TownJungleCritter.Chance;

            return chance;
        }


        public static float JungleBirdChance(NPCSpawnInfo spawnInfo)
        {

            bool modifier = DryTile(spawnInfo);

            float chance = JungleCritterChance(spawnInfo);

            float newChance = modifier ? chance : 0f;

            return newChance;
        }


        public static float DuckChance(NPCSpawnInfo spawnInfo)
        {
            float chance = SpawnCondition.OverworldWaterSurfaceCritter.Chance +    
                           SpawnCondition.TownOverworldWaterSurfaceCritter.Chance; 

            bool modifier = (InnerThird(spawnInfo) || spawnInfo.PlayerInTown);// && IsOnGrass(spawnInfo);

            chance = modifier ? chance : 0f;

            return chance;
        }

        public static float WadingBirdChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile;
            tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];

            float landChance = (SpawnCondition.OverworldDay.Chance + SpawnCondition.TownCritter.Chance) / 2;
            float waterChance = (SpawnCondition.OverworldWaterSurfaceCritter.Chance + SpawnCondition.TownOverworldWaterSurfaceCritter.Chance) / 2;
            float finalChance;

            bool landModifier;
            bool waterModifier;
            bool grassModifier = IsOnGrass(spawnInfo);

            landModifier = IsWaterNearby(spawnInfo);

            landChance = landModifier ? landChance : 0f;

            tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY - 1];
            waterModifier = tile.LiquidAmount < 0;

            waterChance = waterModifier ? waterChance : 0f;

            finalChance = grassModifier ? (landChance + waterChance) : 0f;

            return finalChance;
        }

        public static float FlamingoChance(NPCSpawnInfo spawnInfo)
        {
            float chance = WadingBirdChance(spawnInfo) / 2;

            bool modifier = OuterThird(spawnInfo);

            float newChance = modifier ? chance : 0f;

            return newChance;
        }


        public static float SeagullChance(NPCSpawnInfo spawnInfo)
        {
            float baseWaterChance = SpawnCondition.OverworldWaterSurfaceCritter.Chance / 2;
            float baseLandChance = SpawnCondition.Overworld.Chance / 2;

            //bool beachCondition = (spawnInfo.SpawnTileX < 250 || spawnInfo.SpawnTileX > Main.maxTilesX - 250); // only at the Ocean 
            bool beachCondition = spawnInfo.Player.ZoneBeach;

            bool sandCondition = IsOnBeachSand(spawnInfo);

            float newChance = (beachCondition && sandCondition) ? (baseWaterChance + baseLandChance) : 0f;

            return newChance;
        }

        

        public static bool IsOnGrass(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];

            return (tile.TileType == TileID.Grass) || (tile.TileType == TileID.HallowedGrass); // only on grass
        }

        public static bool IsOnBeachSand(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];

            return (tile.TileType == TileID.Sand) || (tile.TileType == TileID.ShellPile); // only on sand or shell piles 
        }

        public static bool IsWaterNearby(NPCSpawnInfo spawnInfo)
        {

            bool waterFlag = false;

            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];

            // set spawn on solid ground modifier to true only if there is water in close vicinity
            for (int i = -15; i <= 15; i++)
            {
                for (int j = -3; j <= 3; j++)
                {
                    tile = Main.tile[spawnInfo.SpawnTileX + i, spawnInfo.SpawnTileY + j];
                    if (tile.LiquidAmount > 0 && tile.LiquidType == LiquidID.Water)
                    {
                        waterFlag = true;
                    }
                }
            }

            return waterFlag;
        }

        public static bool DryTile(NPCSpawnInfo spawnInfo) => Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].LiquidAmount == 0;

        public static bool InnerThird(NPCSpawnInfo info) => Math.Abs(info.SpawnTileX - Main.spawnTileX) < Main.maxTilesX / 3;

        public static bool OuterThird(NPCSpawnInfo info) => Math.Abs(info.SpawnTileX - Main.spawnTileX) >= Main.maxTilesX / 3;
        
    }
}
