using Terraria;
using Terraria.ID;

namespace NaturalVariety.Mechanics.BestiaryTally
{
    /// <summary>
    /// Helper for bestiary display formatting and corner cases 
    /// </summary>
    public static class BestiaryTallyUtils
    {
        /// <summary>
        /// Get banner display format based on local player having Tally Counter  
        /// </summary>
        /// <param name="bannerTally"></param>
        /// <param name="playerHasTallyCounter"></param>
        /// <returns></returns>
        public static string FormatBannerDisplay(int bannerTally, bool playerHasTallyCounter) => playerHasTallyCounter ? bannerTally.ToString() : "< " + (bannerTally / 50 * 50 + 50).ToString();


        /// <summary>
        /// Add an extra hover text if bestiary entry has different name than the base banner type
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static string GetBannerTypeName(NPC npc)
        {
            string bannerTypeName;

            string currentNpcTypeName = Lang.GetNPCNameValue(npc.netID);

            if (npc.ModNPC == null)
            {
                bannerTypeName = Lang.GetNPCNameValue(Item.BannerToNPC(Item.NPCtoBanner(npc.BannerID())));
            }
            else
            {
                bannerTypeName = Lang.GetNPCNameValue(Item.BannerToNPC(npc.ModNPC.Banner));
            }

            return bannerTypeName != currentNpcTypeName ? "(" + bannerTypeName + ")" : "";
        }

        /// <summary>
        /// NPCs specifically excluded from banner display in bestiary 
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static bool IsExcludedFromBannerTally(NPC npc)
        {
            return npc.netID == NPCID.StardustCellSmall; // has same name but doesn't count for banner 
        }


        /// <summary>
        /// NPCs specifically excluded from bestiary display due to inconsistencies 
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static bool IsExcludedFromVariantsDisplay(NPC npc)
        {
            return npc.netID == NPCID.Slimer || // enough to display banner counter to get correct count
                   npc.netID == NPCID.Slimer2;
        }
    }
}