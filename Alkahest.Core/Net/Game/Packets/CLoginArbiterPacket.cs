using Alkahest.Core.Game;
using Alkahest.Core.Net.Game.Serialization;
using System.Collections.Generic;

namespace Alkahest.Core.Net.Game.Packets
{
    public sealed class CLoginArbiterPacket : Packet
    {
        const string Name = "C_LOGIN_ARBITER";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new CLoginArbiterPacket();
        }

        [PacketField]
        public string AccountName { get; set; }

        [PacketField]
        public List<byte> Ticket { get; } = new List<byte>();

        [PacketField]
        public int Unknown1 { get; set; }

        [PacketField]
        public byte Unknown2 { get; set; }

        [PacketField]
        public ClientRegion Region { get; set; }

        [PacketField]
        public uint PatchVersion { get; set; }
    }
}
