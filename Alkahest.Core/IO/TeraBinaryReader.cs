using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using Alkahest.Core.Data;

namespace Alkahest.Core.IO
{
    public sealed class TeraBinaryReader : IDisposable
    {
        public static Encoding Encoding { get; } = Encoding.Unicode;

        public MemoryStream Stream => (MemoryStream)_reader.BaseStream;

        public int Position
        {
            get => (int)Stream.Position;
            set => Stream.Position = value;
        }

        public int Length => (int)Stream.Length;

        public bool EndOfStream => Position == Length;

        readonly BinaryReader _reader;

        bool _disposed;

        public TeraBinaryReader(byte[] data)
        {
            _reader = new BinaryReader(new MemoryStream(data, false), Encoding);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            _reader.Dispose();
        }

        public byte ReadByte()
        {
            return _reader.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return _reader.ReadSByte();
        }

        public ushort ReadUInt16()
        {
            return _reader.ReadUInt16();
        }

        public short ReadInt16()
        {
            return _reader.ReadInt16();
        }

        public uint ReadUInt32()
        {
            return _reader.ReadUInt32();
        }

        public int ReadInt32()
        {
            return _reader.ReadInt32();
        }

        public ulong ReadUInt64()
        {
            return _reader.ReadUInt64();
        }

        public long ReadInt64()
        {
            return _reader.ReadInt64();
        }

        public float ReadSingle()
        {
            return _reader.ReadSingle();
        }

        public bool ReadBoolean()
        {
            return _reader.ReadBoolean();
        }

        public string ReadString()
        {
            var list = new List<char>();

            while (true)
            {
                var ch = (char)_reader.ReadUInt16();

                if (ch == char.MinValue)
                    return new string(list.ToArray());

                list.Add(ch);
            }
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(_reader.ReadSingle(), _reader.ReadSingle(),
                _reader.ReadSingle());
        }

        public EntityId ReadEntityId()
        {
            return new EntityId(_reader.ReadUInt64());
        }

        public SkillId ReadSkillId()
        {
            return new SkillId(_reader.ReadUInt32());
        }

        public Angle ReadAngle()
        {
            return new Angle(_reader.ReadInt16());
        }

        public T Seek<T>(int position, Func<TeraBinaryReader, int, T> func)
        {
            var pos = Position;

            Position = position;

            var ret = func(this, pos);

            Position = pos;

            return ret;
        }

        public void Seek(int position, Action<TeraBinaryReader, int> action)
        {
            Seek(position, (r, op) =>
            {
                action(r, op);

                return (object)null;
            });
        }

        public bool CanRead(int size)
        {
            return Length - Position >= size;
        }
    }
}
