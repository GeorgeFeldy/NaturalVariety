using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using NaturalVariety.Utils;

namespace NaturalVariety.UI
{
    public class BestiaryTallyInfoElement : NPCNetIdBestiaryInfoElement, IBestiaryInfoElement
    {


        private readonly bool displayBannerTally = false;
        private readonly bool displayBestiaryTally = false;
        private readonly bool displayBossTally = false;

        private readonly int bannerTally;
        private readonly int bestiaryTally;

        private readonly string npcTypeName = "";
        private readonly string npcBestiaryAdditionalText = "";

        private readonly int noOfElements = 0;


        public BestiaryTallyInfoElement(NPC npc, int npcNetId) : base(npcNetId)
        {

            // NPC netNpc = ContentSamples.NpcsByNetId[npcNetId];
            bannerTally = NPC.killCount[Item.NPCtoBanner(npc.BannerID())];

            bestiaryTally = Main.BestiaryTracker.Kills.GetKillCount(npc.GetBestiaryCreditId()); 

            if (!NetIdHelper.IsExcludedFromVariantsDisplay(npc) && bestiaryTally > 0)
            {
                noOfElements = 1;

                if (npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                {
                    displayBossTally = true;
                }
                else
                {
                    displayBestiaryTally = true;
                }
            }

            if (bannerTally > 0)
            {
                noOfElements = 1;

                displayBannerTally = true;


                npcTypeName = GetBannerTypeName(npc);

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

            if(npc.netID == 54)
            {
                 ;
            }

        }

        public new UIElement ProvideUIElement(BestiaryUICollectionInfo info)
        {

            if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
                return null;


            // TODO: adjust based on amount displayed 
            UIElement element = new UIElement //= new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
            {
                Width = new StyleDimension(0f, 1f),
                Height = new StyleDimension(35f * noOfElements, 0f),
                // BackgroundColor = new Color(43, 56, 101),
                // BorderColor = Color.Transparent,
                //Left = new StyleDimension(5f, 0f)
            };

            element.SetPadding(0f);
            element.PaddingRight = 5f;

            // UIElement elementBestiaryCnt = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
            // {
            //     Width = new StyleDimension(-14f, 1f),
            //     Height = new StyleDimension(34f, 0f),
            //     BackgroundColor = new Color(43, 56, 101),
            //     BorderColor = Color.Transparent,
            //     Left = new StyleDimension(5f, 0f)
            // };
            // 
            // elementBestiaryCnt.SetPadding(0f);
            // elementBestiaryCnt.PaddingRight = 5f;


            UIElement fieldImageBanner = new UIImage(ModContent.Request<Texture2D>("NaturalVariety/Assets/UI/TallyBanner"))
            {
                Top = new StyleDimension(0, 0f),
                Left = new StyleDimension(3, 0f)
            };

            fieldImageBanner.HAlign = 0f;
            fieldImageBanner.Left = new StyleDimension(5f, 0f);

            UIText uITextBanner = new UIText((bannerTally).ToString())
            {
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

            UIText uITextBestiary = new UIText((bestiaryTally).ToString())
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

            if(noOfElements > 0)
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

        private string GetBannerTypeName(NPC npc)
        {
            string bannerTypeName = "";
            string currentNpcTypeName = "";


            currentNpcTypeName = Lang.GetNPCNameValue(npc.netID);

            if (npc.ModNPC == null)
            {
                bannerTypeName = Lang.GetNPCNameValue(Item.BannerToNPC(Item.NPCtoBanner(npc.BannerID())));
            }
            else
            {
                bannerTypeName = Lang.GetNPCNameValue(Item.BannerToNPC(npc.ModNPC.Banner));
            }    

                
            if (bannerTypeName != currentNpcTypeName)
            {
                return "(" + bannerTypeName + ")";
            }
            else
            {
                return "";
            }

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

            // string creditId = NetIdHelper.MapNetIdToBaseCreditId(netNpc);

            //bestiaryTally = Main.BestiaryTracker.Kills.GetKillCount(creditId); // npcBestiaryCreditIdsByNpcNetIds