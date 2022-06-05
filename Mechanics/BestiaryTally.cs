using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using NaturalVariety.NPCs.Critters;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;

namespace NaturalVariety.Mechanics
{
    public class BestiaryTally : GlobalNPC
    {

        private static IBestiaryInfoElement tallyInfo;

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return !(entity.ExcludedFromDeathTally());
        }

        public override bool SpecialOnKill(NPC npc)
        {
            BestiaryEntry entry = Main.BestiaryDB.FindEntryByNPCID(npc.netID);

            tallyInfo = new TallyBestiaryInfoElement(npc, npc.netID);

            int infoIdx = entry.Info.Count;
            entry.Info.Add(tallyInfo);

            if (npc.life <= 0)
            {
                entry.Info.RemoveAt(infoIdx);
                tallyInfo = new TallyBestiaryInfoElement(npc, npc.netID);
                entry.Info.Add(tallyInfo);

                Main.BestiaryDB.Register(entry);
            }

            return false;
        }


        // public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        // {
        //     
        //     bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        //     {
        //         new TallyBestiaryInfoElement(npc.type),
        //         new FlavorTextBestiaryInfoElement(NPC.killCount[npc.type].ToString())
        //     });
        // }
    }

    public class TallyBestiaryInfoElement : NPCNetIdBestiaryInfoElement, IBestiaryInfoElement
    {
        private readonly int tally;
        private readonly int bestiaryCount;
        private readonly string display = "";

        // public TallyBestiaryInfoElement(int npcNetId) : base(npcNetId)
        // {
        //     this.tally = 0;
        // }

        public TallyBestiaryInfoElement(NPC npc, int npcNetId) : base(npcNetId)
        {

            this.tally = NPC.killCount[Item.NPCtoBanner(npc.BannerID())];
            this.bestiaryCount = Main.BestiaryTracker.Kills.GetKillCount(npc.GetBestiaryCreditId());
            this.display = "Bestiary: " + this.bestiaryCount + ", Tally: " + this.tally;
            this.display = "B : " + this.bestiaryCount + ", T : " + this.tally;

        }
       
        public new UIElement ProvideUIElement(BestiaryUICollectionInfo info)
        {

            UIElement element = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
            {
                Width = new StyleDimension(-14f, 1f),
                Height = new StyleDimension(34f, 0f),
                BackgroundColor = new Color(43, 56, 101),
                BorderColor = Color.Transparent,
                Left = new StyleDimension(5f, 0f)
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



            UIElement fieldImageTally = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_HP"))
            {
                Top = new StyleDimension(0, 0f),
                Left = new StyleDimension(3, 0f)
            };

            fieldImageTally.HAlign = 0f;
            fieldImageTally.Left = new StyleDimension(5f, 0f);

            UIText uITextTally = new UIText(this.display)
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
                Top = new StyleDimension(0 + 35 * 2, 0f)
            };

            if (fieldImageTally != null)
                element.Append(fieldImageTally);

            element.Append(uITextTally);

            fieldImageTally.OnUpdate += ShowName_Tally;

            return element;

        }


        private void ShowName_Tally(UIElement element)
        {
            if (element != null)
            {
                Main.instance.MouseText("Kill count", 0, 0);
            }
        }





    }


}
