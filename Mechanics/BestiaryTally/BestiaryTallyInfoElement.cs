using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NaturalVariety.Utils;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace NaturalVariety.Mechanics.BestiaryTally
{
    public class BestiaryTallyInfoElement : NPCNetIdBestiaryInfoElement, IBestiaryInfoElement, IBestiaryPrioritizedElement
    {
        private readonly bool playerHasTallyCounter;

        private readonly bool displayBannerTally = false;
        private readonly bool displayBestiaryTally = false;
        private readonly bool displayBossTally = false;

        private readonly int bannerTally;
        private readonly int bestiaryTally;

        private readonly string bannerTallyText = "";
        private readonly string bestiaryTallyText = "";

        private readonly string npcTypeName = "";
        private readonly string npcBestiaryAdditionalText = "";

        private readonly int noOfElements = 0;

        public float OrderPriority => 1f;

        public BestiaryTallyInfoElement(NPC npc, int npcNetId) : base(npcNetId)
        {

            playerHasTallyCounter = Main.LocalPlayer.accJarOfSouls;

            bannerTally = NPC.killCount[Item.NPCtoBanner(npc.BannerID())];

            bestiaryTally = Main.BestiaryTracker.Kills.GetKillCount(npc.GetBestiaryCreditId());

            if (!BestiaryTallyUtils.IsExcludedFromVariantsDisplay(npc) && bestiaryTally > 0 && playerHasTallyCounter)
            {
                noOfElements = 1;
                bestiaryTallyText = bestiaryTally.ToString();

                if (npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                {
                    displayBossTally = true;
                }
                else
                {
                    displayBestiaryTally = true;
                }
            }

            if (!BestiaryTallyUtils.IsExcludedFromBannerTally(npc) && bannerTally > 0)
            {
                noOfElements = 1;
                displayBannerTally = true;
                npcTypeName = BestiaryTallyUtils.GetBannerTypeName(npc);
                bannerTallyText = BestiaryTallyUtils.FormatBannerDisplay(bannerTally, playerHasTallyCounter);

                if (bestiaryTally == bannerTally)
                {
                    displayBestiaryTally = false;
                }
            }

            if (displayBannerTally && displayBestiaryTally)
            {
                npcBestiaryAdditionalText = "for this variant";
                noOfElements = 2;
            }
        }

        public new UIElement ProvideUIElement(BestiaryUICollectionInfo info)
        {

            if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
                return null;

            UIElement element = new UIElement
            {
                Width = new StyleDimension(0f, 1f),
                Height = new StyleDimension(35f * noOfElements + (noOfElements > 0 ? 5f : 0), 0f),
            };

            element.SetPadding(0f);
            element.PaddingRight = 5f;

            UIElement fieldImageBanner = new UIImage(ModContent.Request<Texture2D>("NaturalVariety/Assets/UI/TallyBanner"))
            {
                Top = new StyleDimension(0, 0f),
                Left = new StyleDimension(3, 0f)
            };

            fieldImageBanner.HAlign = 0f;
            fieldImageBanner.Left = new StyleDimension(5f, 0f);

            UIText uITextBanner = new UIText(bannerTallyText)
            {
                TextColor = playerHasTallyCounter ? Color.White : Color.Gold,
                HAlign = 0f,
                Left = new StyleDimension(38f, 0f),
                TextOriginX = 0f,
                VAlign = 0.5f,
                DynamicallyScaleDownToWidth = true
            };

            UIElement fieldImageBestiary = new UIImage(ModContent.Request<Texture2D>("NaturalVariety/Assets/UI/TallyBestiary"))
            {
                Top = new StyleDimension(35f * (noOfElements - 1), 0f),
                Left = new StyleDimension(3, 0f)
            };

            fieldImageBestiary.HAlign = 0f;
            fieldImageBestiary.Left = new StyleDimension(5f, 0f);

            UIElement fieldImageBoss = new UIImage(ModContent.Request<Texture2D>("NaturalVariety/Assets/UI/TallyBoss"))
            {
                Top = new StyleDimension(35f * (noOfElements - 1), 0f),
                Left = new StyleDimension(3, 0f)
            };

            fieldImageBoss.HAlign = 0f;
            fieldImageBoss.Left = new StyleDimension(5f, 0f);

            UIText uITextBestiary = new UIText(bestiaryTallyText)
            {
                HAlign = 0f,
                Left = new StyleDimension(38f, 0f),
                TextOriginX = 0f,
                VAlign = 0.5f,
                DynamicallyScaleDownToWidth = true
            };


            UIHorizontalSeparator separator = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
                Color = new Color(89, 116, 213, 255) * 0.9f,
                Left = new StyleDimension(0f, 0f),
                Top = new StyleDimension(0 + 35 * noOfElements, 0f)
            };

            if (noOfElements > 0)
            {
                element.Append(separator);
            }

            if (fieldImageBestiary != null && displayBestiaryTally)
            {
                fieldImageBestiary.Append(uITextBestiary);
                element.Append(fieldImageBestiary);
                fieldImageBestiary.OnUpdate += ShowNameBestiary;
            }

            if (fieldImageBoss != null && displayBossTally)
            {
                fieldImageBoss.Append(uITextBestiary);
                element.Append(fieldImageBoss);
                fieldImageBoss.OnUpdate += ShowNameBoss;
            }


            if (fieldImageBanner != null && displayBannerTally)
            {
                fieldImageBanner.Append(uITextBanner);
                element.Append(fieldImageBanner);
                fieldImageBanner.OnUpdate += ShowNameBanner;
            }

            return element;

        }

        private void ShowNameBanner(UIElement element)
        {
            if (element.IsMouseHovering && element != null)
            {
                Main.instance.MouseText("Kill count " + npcTypeName, 0, 0);
            }
        }

        private void ShowNameBestiary(UIElement element)
        {
            if (element.IsMouseHovering && element != null)
            {
                Main.instance.MouseText("Kill count " + npcBestiaryAdditionalText, 0, 0);
            }
        }

        private void ShowNameBoss(UIElement element)
        {
            if (element.IsMouseHovering && element != null)
            {
                Main.instance.MouseText("Boss kill count", 0, 0);
            }
        }
    }
}
