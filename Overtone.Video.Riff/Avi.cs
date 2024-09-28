// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

namespace Kaitai
{

    /// <remarks>
    /// Reference: <a href="https://learn.microsoft.com/en-us/previous-versions/ms779636(v=vs.85)">Source</a>
    /// </remarks>
    public partial class Avi : KaitaiStruct
    {
        public static Avi FromFile(string fileName)
        {
            return new Avi(new KaitaiStream(fileName));
        }


        public enum ChunkType
        {
            Idx1 = 829973609,
            Junk = 1263424842,
            Info = 1330007625,
            Isft = 1413894985,
            List = 1414744396,
            Strf = 1718776947,
            Avih = 1751742049,
            Strh = 1752331379,
            Movi = 1769369453,
            Hdrl = 1819436136,
            Strl = 1819440243,
        }

        public enum StreamType
        {
            Mids = 1935960429,
            Vids = 1935960438,
            Auds = 1935963489,
            Txts = 1937012852,
        }

        public enum HandlerType
        {
            Mp3 = 85,
            Ac3 = 8192,
            Dts = 8193,
            Cvid = 1684633187,
            Xvid = 1684633208,
        }
        public Avi(KaitaiStream p__io, KaitaiStruct p__parent = null, Avi p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _magic1 = m_io.ReadBytes(4);
            if (!((KaitaiStream.ByteArrayCompare(Magic1, new byte[] { 82, 73, 70, 70 }) == 0)))
            {
                throw new ValidationNotEqualError(new byte[] { 82, 73, 70, 70 }, Magic1, M_Io, "/seq/0");
            }
            _fileSize = m_io.ReadU4le();
            _magic2 = m_io.ReadBytes(4);
            if (!((KaitaiStream.ByteArrayCompare(Magic2, new byte[] { 65, 86, 73, 32 }) == 0)))
            {
                throw new ValidationNotEqualError(new byte[] { 65, 86, 73, 32 }, Magic2, M_Io, "/seq/2");
            }
            __raw_data = m_io.ReadBytes((FileSize - 4));
            var io___raw_data = new KaitaiStream(__raw_data);
            _data = new Blocks(io___raw_data, this, m_root);
        }
        public partial class ListBody : KaitaiStruct
        {
            public static ListBody FromFile(string fileName)
            {
                return new ListBody(new KaitaiStream(fileName));
            }

            public ListBody(KaitaiStream p__io, Block p__parent = null, Avi p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _listType = ((ChunkType) m_io.ReadU4le());
                _data = new Blocks(m_io, this, m_root);
            }
            private ChunkType _listType;
            private Blocks _data;
            private Avi m_root;
            private Block m_parent;
            public ChunkType ListType { get { return _listType; } }
            public Blocks Data { get { return _data; } }
            public Avi M_Root { get { return m_root; } }
            public Block M_Parent { get { return m_parent; } }
        }
        public partial class Rect : KaitaiStruct
        {
            public static Rect FromFile(string fileName)
            {
                return new Rect(new KaitaiStream(fileName));
            }

            public Rect(KaitaiStream p__io, StrhBody p__parent = null, Avi p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _left = m_io.ReadS2le();
                _top = m_io.ReadS2le();
                _right = m_io.ReadS2le();
                _bottom = m_io.ReadS2le();
            }
            private short _left;
            private short _top;
            private short _right;
            private short _bottom;
            private Avi m_root;
            private StrhBody m_parent;
            public short Left { get { return _left; } }
            public short Top { get { return _top; } }
            public short Right { get { return _right; } }
            public short Bottom { get { return _bottom; } }
            public Avi M_Root { get { return m_root; } }
            public StrhBody M_Parent { get { return m_parent; } }
        }
        public partial class Blocks : KaitaiStruct
        {
            public static Blocks FromFile(string fileName)
            {
                return new Blocks(new KaitaiStream(fileName));
            }

            public Blocks(KaitaiStream p__io, KaitaiStruct p__parent = null, Avi p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _entries = new List<Block>();
                {
                    var i = 0;
                    while (!m_io.IsEof) {
                        _entries.Add(new Block(m_io, this, m_root));
                        i++;
                    }
                }
            }
            private List<Block> _entries;
            private Avi m_root;
            private KaitaiStruct m_parent;
            public List<Block> Entries { get { return _entries; } }
            public Avi M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// Main header of an AVI file, defined as AVIMAINHEADER structure
        /// </summary>
        /// <remarks>
        /// Reference: <a href="https://learn.microsoft.com/en-us/previous-versions/ms779632(v=vs.85)">Source</a>
        /// </remarks>
        public partial class AvihBody : KaitaiStruct
        {
            public static AvihBody FromFile(string fileName)
            {
                return new AvihBody(new KaitaiStream(fileName));
            }

            public AvihBody(KaitaiStream p__io, Block p__parent = null, Avi p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _microSecPerFrame = m_io.ReadU4le();
                _maxBytesPerSec = m_io.ReadU4le();
                _paddingGranularity = m_io.ReadU4le();
                _flags = m_io.ReadU4le();
                _totalFrames = m_io.ReadU4le();
                _initialFrames = m_io.ReadU4le();
                _streams = m_io.ReadU4le();
                _suggestedBufferSize = m_io.ReadU4le();
                _width = m_io.ReadU4le();
                _height = m_io.ReadU4le();
                _reserved = m_io.ReadBytes(16);
            }
            private uint _microSecPerFrame;
            private uint _maxBytesPerSec;
            private uint _paddingGranularity;
            private uint _flags;
            private uint _totalFrames;
            private uint _initialFrames;
            private uint _streams;
            private uint _suggestedBufferSize;
            private uint _width;
            private uint _height;
            private byte[] _reserved;
            private Avi m_root;
            private Block m_parent;
            public uint MicroSecPerFrame { get { return _microSecPerFrame; } }
            public uint MaxBytesPerSec { get { return _maxBytesPerSec; } }
            public uint PaddingGranularity { get { return _paddingGranularity; } }
            public uint Flags { get { return _flags; } }
            public uint TotalFrames { get { return _totalFrames; } }
            public uint InitialFrames { get { return _initialFrames; } }
            public uint Streams { get { return _streams; } }
            public uint SuggestedBufferSize { get { return _suggestedBufferSize; } }
            public uint Width { get { return _width; } }
            public uint Height { get { return _height; } }
            public byte[] Reserved { get { return _reserved; } }
            public Avi M_Root { get { return m_root; } }
            public Block M_Parent { get { return m_parent; } }
        }
        public partial class Block : KaitaiStruct
        {
            public static Block FromFile(string fileName)
            {
                return new Block(new KaitaiStream(fileName));
            }

            public Block(KaitaiStream p__io, Blocks p__parent = null, Avi p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _fourCc = ((ChunkType) m_io.ReadU4le());
                _blockSize = m_io.ReadU4le();
                switch (FourCc) {
                case ChunkType.List: {
                    __raw_data = m_io.ReadBytes(BlockSize);
                    var io___raw_data = new KaitaiStream(__raw_data);
                    _data = new ListBody(io___raw_data, this, m_root);
                    break;
                }
                case ChunkType.Avih: {
                    __raw_data = m_io.ReadBytes(BlockSize);
                    var io___raw_data = new KaitaiStream(__raw_data);
                    _data = new AvihBody(io___raw_data, this, m_root);
                    break;
                }
                case ChunkType.Strh: {
                    __raw_data = m_io.ReadBytes(BlockSize);
                    var io___raw_data = new KaitaiStream(__raw_data);
                    _data = new StrhBody(io___raw_data, this, m_root);
                    break;
                }
                default: {
                    _data = m_io.ReadBytes(BlockSize);
                    break;
                }
                }
            }
            private ChunkType _fourCc;
            private uint _blockSize;
            private object _data;
            private Avi m_root;
            private Blocks m_parent;
            private byte[] __raw_data;
            public ChunkType FourCc { get { return _fourCc; } }
            public uint BlockSize { get { return _blockSize; } }
            public object Data { get { return _data; } }
            public Avi M_Root { get { return m_root; } }
            public Blocks M_Parent { get { return m_parent; } }
            public byte[] M_RawData { get { return __raw_data; } }
        }

        /// <summary>
        /// Stream header (one header per stream), defined as AVISTREAMHEADER structure
        /// </summary>
        /// <remarks>
        /// Reference: <a href="https://learn.microsoft.com/en-us/previous-versions/ms779638(v=vs.85)">Source</a>
        /// </remarks>
        public partial class StrhBody : KaitaiStruct
        {
            public static StrhBody FromFile(string fileName)
            {
                return new StrhBody(new KaitaiStream(fileName));
            }

            public StrhBody(KaitaiStream p__io, Block p__parent = null, Avi p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _fccType = ((StreamType) m_io.ReadU4le());
                _fccHandler = ((HandlerType) m_io.ReadU4le());
                _flags = m_io.ReadU4le();
                _priority = m_io.ReadU2le();
                _language = m_io.ReadU2le();
                _initialFrames = m_io.ReadU4le();
                _scale = m_io.ReadU4le();
                _rate = m_io.ReadU4le();
                _start = m_io.ReadU4le();
                _length = m_io.ReadU4le();
                _suggestedBufferSize = m_io.ReadU4le();
                _quality = m_io.ReadU4le();
                _sampleSize = m_io.ReadU4le();
                _frame = new Rect(m_io, this, m_root);
            }
            private StreamType _fccType;
            private HandlerType _fccHandler;
            private uint _flags;
            private ushort _priority;
            private ushort _language;
            private uint _initialFrames;
            private uint _scale;
            private uint _rate;
            private uint _start;
            private uint _length;
            private uint _suggestedBufferSize;
            private uint _quality;
            private uint _sampleSize;
            private Rect _frame;
            private Avi m_root;
            private Block m_parent;

            /// <summary>
            /// Type of the data contained in the stream
            /// </summary>
            public StreamType FccType { get { return _fccType; } }

            /// <summary>
            /// Type of preferred data handler for the stream (specifies codec for audio / video streams)
            /// </summary>
            public HandlerType FccHandler { get { return _fccHandler; } }
            public uint Flags { get { return _flags; } }
            public ushort Priority { get { return _priority; } }
            public ushort Language { get { return _language; } }
            public uint InitialFrames { get { return _initialFrames; } }
            public uint Scale { get { return _scale; } }
            public uint Rate { get { return _rate; } }
            public uint Start { get { return _start; } }
            public uint Length { get { return _length; } }
            public uint SuggestedBufferSize { get { return _suggestedBufferSize; } }
            public uint Quality { get { return _quality; } }
            public uint SampleSize { get { return _sampleSize; } }
            public Rect Frame { get { return _frame; } }
            public Avi M_Root { get { return m_root; } }
            public Block M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// Stream format description
        /// </summary>
        public partial class StrfBody : KaitaiStruct
        {
            public static StrfBody FromFile(string fileName)
            {
                return new StrfBody(new KaitaiStream(fileName));
            }

            public StrfBody(KaitaiStream p__io, KaitaiStruct p__parent = null, Avi p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private Avi m_root;
            private KaitaiStruct m_parent;
            public Avi M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        private byte[] _magic1;
        private uint _fileSize;
        private byte[] _magic2;
        private Blocks _data;
        private Avi m_root;
        private KaitaiStruct m_parent;
        private byte[] __raw_data;
        public byte[] Magic1 { get { return _magic1; } }
        public uint FileSize { get { return _fileSize; } }
        public byte[] Magic2 { get { return _magic2; } }
        public Blocks Data { get { return _data; } }
        public Avi M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
        public byte[] M_RawData { get { return __raw_data; } }
    }
}
