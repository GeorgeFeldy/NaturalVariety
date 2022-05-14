using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// using NaturalVariety.Items.Critters;
// using NaturalVariety.Items.Placeable.Banners;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.DataStructures;

namespace NaturalVariety.NPCs.Critters
{
    public class EurasianJay : ModNPC
    {
        // public enum BirdType
        // {
        //     Blackbird,
        //     .....
        // }


        // public BirdType BirdType_local
        // {
        //     get => (BirdType_local)NPC.ai[3];
        //     set => NPC.ai[3] = (int)value;
        // }

        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("");
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Bird];
            NPCID.Sets.CountsAsCritter[Type] = true;
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 14;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0;
            NPC.knockBackResist = 0.8f;
            NPC.aiStyle = 24;
            NPC.alpha = 0;
            AnimationType = NPCID.Bird; // Use vanilla bird's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
            // placeholders:
            NPC.catchItem = ItemID.Bird;
            Banner = Item.NPCtoBanner(NPCID.Bird);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return SpawnCondition.OverworldDayBirdCritter.Chance; // Spawn with 1/5th the chance of a regular zombie.
		}


        // public override void ModifyNPCLoot(NPCLoot npcLoot)
        // {
        //  TODO: add loot here (dont kill pls)
        // }

        // public override void OnSpawn(IEntitySource source)
        // {
        //     if (?)
        //     {
        //             BirdType_c = (BirdType)Main.rand.Next(maxEnum);
        //     }
        // 
        //     TimerRand = Main.rand.Next(80, 180);
        //     NPC.alpha = 0;  ----- set to default NPC.alpha = 255 as Hallam did 
        // }
     

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement(
                    "Eurasian Jay (Garrulus glandarius)")
            });
        }

      
    }
}