using Alkahest.Core.Game;
using Alkahest.Core.Net.Game.Serialization;

namespace Alkahest.Core.Net.Game.Packets
{
    public sealed class SUserStatusPacket : Packet
    {
        const string Name = "S_USER_STATUS";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new SUserStatusPacket();
        }

        [PacketField]
        public GameId Target { get; set; }

        [PacketField]
        public UserStatus Status { get; set; }

        [PacketField]
        public short Unknown1 { get; set; }

        [PacketField]
        public byte Unknown2 { get; set; }
    }
}
