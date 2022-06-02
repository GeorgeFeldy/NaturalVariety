// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using ReLogic.Content;
// using Terraria;
// using Terraria.ID;
// using Terraria.UI;
// using Terraria.ModLoader;
// using NaturalVariety.NPCs.Critters;
// using Terraria.GameContent.Bestiary;
// using Terraria.GameContent.UI.Elements;
// 
// namespace NaturalVariety.Mechanics
// {
//     public class BestiaryTally : GlobalNPC
//     {
// 
//         public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
//         {
//             return !(entity.ExcludedFromDeathTally());
//         }
// 
// 
//         public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
//         {
//             base.SetBestiary(npc, database, bestiaryEntry);
// 
//             bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
//             {
//                 //new TallyBestiaryInfoElement(npc.type),
//                 new FlavorTextBestiaryInfoElement(NPC.killCount[npc.type].ToString())
//             });
// 
// 
//         }
//     }
// 
//     public class TallyBestiaryInfoElement : ModBestiaryInfoElement
//     {
//         int npcId;
//         int tally;
// 
//         public TallyBestiaryInfoElement(int npcId)
//         {
//             this.npcId = npcId;
//             this.tally = NPC.killCount[npcId];
//         }
//        
//         public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
//         {
// 
//             UIElement uIElement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
//             {
//                 Width = new StyleDimension(-14f, 1f),
//                 Height = new StyleDimension(34f, 0f),
//                 BackgroundColor = new Color(43, 56, 101),
//                 BorderColor = Color.Transparent,
//                 Left = new StyleDimension(5f, 0f)
//             };
// 
//             uIElement.SetPadding(0f);
//             uIElement.PaddingRight = 5f;
// 
//             UIElement filterImage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_HP"))
//             {
//                 Top = new StyleDimension(0, 0f),
//                 Left = new StyleDimension(3, 0f)
//             };
// 
//             filterImage.HAlign = 0f;
//             filterImage.Left = new StyleDimension(5f, 0f);
// 
//             UIText uIText = new UIText(tally.ToString())
//             {
//                 HAlign = 0f,
//                 Left = new StyleDimension(38f, 0f),
//                 TextOriginX = 0f,
//                 VAlign = 0.5f,
//                 DynamicallyScaleDownToWidth = true
//             };
// 
//             UIHorizontalSeparator separator = new()
//             {
//                 Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
//                 Color = new Color(89, 116, 213, 255) * 0.9f,
//                 Left = new StyleDimension(0f, 0f),
//                 Top = new StyleDimension(0 + 35 * 2, 0f)
//             };
// 
//             if (filterImage != null)
//                 uIElement.Append(filterImage);
// 
//             uIElement.Append(uIText);
//             return uIElement;
// 
//         }
// 
//         private void AddOnHover(UIElement button)
//         {
//             button.OnUpdate += delegate (UIElement e) {
//                 ShowButtonName(e);
//             };
//         }
// 
//         private void ShowButtonName(UIElement element)
//         {
//             if (element.IsMouseHovering)
//             {
//                 string textValue = GetDisplayNameKey();
//                 Main.instance.MouseText(textValue, 0, 0);
//             }
//         }
//         public string GetDisplayNameKey() => tally.ToString();
// 
//     }
// 
// 
// }
