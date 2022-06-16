using NaturalVariety.Mechanics;
using System.IO;
using Terraria;
using Terraria.ID;

namespace NaturalVariety
{

    public partial class NaturalVariety
    {
        public enum MessageType : byte
        {
            SyncBestiaryKillCount
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType messageType = (MessageType)reader.ReadByte();
            if (messageType == MessageType.SyncBestiaryKillCount)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    BestiaryTally.SetKillCountsInBestiary();
                }
            }
        }
    }
}