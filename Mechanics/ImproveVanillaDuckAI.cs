using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;

namespace NaturalVariety.Mechanics
{
    public class VanillaDuck : Waterfowl{ public override string Texture => "Terraria/Images/NPC_" + NPCID.Duck; }

   // public class VanillaDuckFly : WaterfowlFly{}

    public class ImproveVanillaDuckAI : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return (entity.type == NPCID.Duck || entity.type == NPCID.DuckWhite);
        }

        public override void AI(NPC npc)
        {
            VanillaDuck vanillaDuck = new();
            vanillaDuck.AI();
        }

    }
}
