// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using System.Text;

namespace Kaitai
{
            public partial class ParentChunkData : KaitaiStruct
        {
            public static ParentChunkData FromFile(string fileName)
            {
                return new ParentChunkData(new KaitaiStream(fileName));
            }

            public ParentChunkData(KaitaiStream p__io, KaitaiStruct p__parent = null, Riff p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _formType = m_io.ReadU4le();
                __raw_subchunksSlot = m_io.ReadBytesFull();
                var io___raw_subchunksSlot = new KaitaiStream(__raw_subchunksSlot);
                _subchunksSlot = new Slot(io___raw_subchunksSlot, this, m_root);
            }
            public partial class Slot : KaitaiStruct
            {
                public static Slot FromFile(string fileName)
                {
                    return new Slot(new KaitaiStream(fileName));
                }

                public Slot(KaitaiStream p__io, ParentChunkData p__parent = null, Riff p__root = null) : base(p__io)
                {
                    m_parent = p__parent;
                    m_root = p__root;
                    _read();
                }
                private void _read()
                {
                }
                private Riff m_root;
                private ParentChunkData m_parent;
                public Riff M_Root { get { return m_root; } }
                public ParentChunkData M_Parent { get { return m_parent; } }
            }
            private uint _formType;
            private Slot _subchunksSlot;
            private Riff m_root;
            private KaitaiStruct m_parent;
            private byte[] __raw_subchunksSlot;
            public uint FormType { get { return _formType; } }
            public Slot SubchunksSlot { get { return _subchunksSlot; } }
            public Riff M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
            public byte[] M_RawSubchunksSlot { get { return __raw_subchunksSlot; } }
        }


            public partial class Chunk : KaitaiStruct
        {
            public static Chunk FromFile(string fileName)
            {
                return new Chunk(new KaitaiStream(fileName));
            }

            public Chunk(KaitaiStream p__io, KaitaiStruct p__parent = null, Riff p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _id = m_io.ReadU4le();
                _len = m_io.ReadU4le();
                __raw_dataSlot = m_io.ReadBytes(Len);
                var io___raw_dataSlot = new KaitaiStream(__raw_dataSlot);
                _dataSlot = new Slot(io___raw_dataSlot, this, m_root);
                _padByte = m_io.ReadBytes(KaitaiStream.Mod(Len, 2));
            }
            public partial class Slot : KaitaiStruct
            {
                public static Slot FromFile(string fileName)
                {
                    return new Slot(new KaitaiStream(fileName));
                }

                public Slot(KaitaiStream p__io, Chunk p__parent = null, Riff p__root = null) : base(p__io)
                {
                    m_parent = p__parent;
                    m_root = p__root;
                    _read();
                }
                private void _read()
                {
                }
                private Riff m_root;
                private Chunk m_parent;
                public Riff M_Root { get { return m_root; } }
                public Chunk M_Parent { get { return m_parent; } }
            }
            private uint _id;
            private uint _len;
            private Slot _dataSlot;
            private byte[] _padByte;
            private Riff m_root;
            private KaitaiStruct m_parent;
            private byte[] __raw_dataSlot;
            public uint Id { get { return _id; } }
            public uint Len { get { return _len; } }
            public Slot DataSlot { get { return _dataSlot; } }
            public byte[] PadByte { get { return _padByte; } }
            public Riff M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
            public byte[] M_RawDataSlot { get { return __raw_dataSlot; } }
        }



    /// <summary>
    /// The Resource Interchange File Format (RIFF) is a generic file container format
    /// for storing data in tagged chunks. It is primarily used to store multimedia
    /// such as sound and video, though it may also be used to store any arbitrary data.
    ///
    /// The Microsoft implementation is mostly known through container formats
    /// like AVI, ANI and WAV, which use RIFF as their basis.
    /// </summary>
    /// <remarks>
    /// Reference: <a href="https://www.johnloomis.org/cpe102/asgn/asgn1/riff.html">Source</a>
    /// </remarks>
    public partial class Riff : KaitaiStruct
    {
        public static Riff FromFile(string fileName)
        {
            return new Riff(new KaitaiStream(fileName));
        }


        public enum Fourcc
        {
            Riff = 1179011410,
            Info = 1330007625,
            List = 1414744396,
        }
        public Riff(KaitaiStream p__io, KaitaiStruct p__parent = null, Riff p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            f_chunkId = false;
            f_isRiffChunk = false;
            f_parentChunkData = false;
            f_subchunks = false;
            _read();
        }
        private void _read()
        {
            _chunk = new Chunk(m_io, this, m_root);
        }
        public partial class ListChunkData : KaitaiStruct
        {
            public static ListChunkData FromFile(string fileName)
            {
                return new ListChunkData(new KaitaiStream(fileName));
            }

            public ListChunkData(KaitaiStream p__io, ChunkType p__parent = null, Riff p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_parentChunkDataOfs = false;
                f_formType = false;
                f_formTypeReadable = false;
                f_subchunks = false;
                _read();
            }
            private void _read()
            {
                if (ParentChunkDataOfs < 0) {
                    _saveParentChunkDataOfs = m_io.ReadBytes(0);
                }
                _parentChunkData = new ParentChunkData(m_io, this, m_root);
            }
            private bool f_parentChunkDataOfs;
            private int _parentChunkDataOfs;
            public int ParentChunkDataOfs
            {
                get
                {
                    if (f_parentChunkDataOfs)
                        return _parentChunkDataOfs;
                    _parentChunkDataOfs = (int) (M_Io.Pos);
                    f_parentChunkDataOfs = true;
                    return _parentChunkDataOfs;
                }
            }
            private bool f_formType;
            private Fourcc _formType;
            public Fourcc FormType
            {
                get
                {
                    if (f_formType)
                        return _formType;
                    _formType = (Fourcc) (((Fourcc) ParentChunkData.FormType));
                    f_formType = true;
                    return _formType;
                }
            }
            private bool f_formTypeReadable;
            private string _formTypeReadable;
            public string FormTypeReadable
            {
                get
                {
                    if (f_formTypeReadable)
                        return _formTypeReadable;
                    long _pos = m_io.Pos;
                    m_io.Seek(ParentChunkDataOfs);
                    _formTypeReadable = Encoding.GetEncoding("ASCII").GetString(m_io.ReadBytes(4));
                    m_io.Seek(_pos);
                    f_formTypeReadable = true;
                    return _formTypeReadable;
                }
            }
            private bool f_subchunks;
            private List<KaitaiStruct> _subchunks;
            public List<KaitaiStruct> Subchunks
            {
                get
                {
                    if (f_subchunks)
                        return _subchunks;
                    KaitaiStream io = ParentChunkData.SubchunksSlot.M_Io;
                    long _pos = io.Pos;
                    io.Seek(0);
                    _subchunks = new List<KaitaiStruct>();
                    {
                        var i = 0;
                        while (!io.IsEof) {
                            switch (FormType) {
                            case Fourcc.Info: {
                                _subchunks.Add(new InfoSubchunk(io, this, m_root));
                                break;
                            }
                            default: {
                                _subchunks.Add(new ChunkType(io, this, m_root));
                                break;
                            }
                            }
                            i++;
                        }
                    }
                    io.Seek(_pos);
                    f_subchunks = true;
                    return _subchunks;
                }
            }
            private byte[] _saveParentChunkDataOfs;
            private ParentChunkData _parentChunkData;
            private Riff m_root;
            private ChunkType m_parent;
            public byte[] SaveParentChunkDataOfs { get { return _saveParentChunkDataOfs; } }
            public ParentChunkData ParentChunkData { get { return _parentChunkData; } }
            public Riff M_Root { get { return m_root; } }
            public ChunkType M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// All registered subchunks in the INFO chunk are NULL-terminated strings,
        /// but the unregistered might not be. By convention, the registered
        /// chunk IDs are in uppercase and the unregistered IDs are in lowercase.
        ///
        /// If the chunk ID of an INFO subchunk contains a lowercase
        /// letter, this chunk is considered as unregistered and thus we can make
        /// no assumptions about the type of data.
        /// </summary>
        public partial class InfoSubchunk : KaitaiStruct
        {
            public static InfoSubchunk FromFile(string fileName)
            {
                return new InfoSubchunk(new KaitaiStream(fileName));
            }

            public InfoSubchunk(KaitaiStream p__io, ListChunkData p__parent = null, Riff p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_chunkData = false;
                f_isUnregisteredTag = false;
                f_idChars = false;
                f_chunkIdReadable = false;
                f_chunkOfs = false;
                _read();
            }
            private void _read()
            {
                if (ChunkOfs < 0) {
                    _saveChunkOfs = m_io.ReadBytes(0);
                }
                _chunk = new Chunk(m_io, this, m_root);
            }
            private bool f_chunkData;
            private string _chunkData;
            public string ChunkData
            {
                get
                {
                    if (f_chunkData)
                        return _chunkData;
                    KaitaiStream io = Chunk.DataSlot.M_Io;
                    long _pos = io.Pos;
                    io.Seek(0);
                    {
                        bool on = IsUnregisteredTag;
                        if (on == false)
                        {
                            _chunkData = Encoding.GetEncoding("UTF-8").GetString(io.ReadBytesTerm(0, false, true, true));
                        }
                    }
                    io.Seek(_pos);
                    f_chunkData = true;
                    return _chunkData;
                }
            }
            private bool f_isUnregisteredTag;
            private bool _isUnregisteredTag;

            /// <summary>
            /// Check if chunk_id contains lowercase characters ([a-z], ASCII 97 = a, ASCII 122 = z).
            /// </summary>
            public bool IsUnregisteredTag
            {
                get
                {
                    if (f_isUnregisteredTag)
                        return _isUnregisteredTag;
                    _isUnregisteredTag = (bool) ( (( ((IdChars[0] >= 97) && (IdChars[0] <= 122)) ) || ( ((IdChars[1] >= 97) && (IdChars[1] <= 122)) ) || ( ((IdChars[2] >= 97) && (IdChars[2] <= 122)) ) || ( ((IdChars[3] >= 97) && (IdChars[3] <= 122)) )) );
                    f_isUnregisteredTag = true;
                    return _isUnregisteredTag;
                }
            }
            private bool f_idChars;
            private byte[] _idChars;
            public byte[] IdChars
            {
                get
                {
                    if (f_idChars)
                        return _idChars;
                    long _pos = m_io.Pos;
                    m_io.Seek(ChunkOfs);
                    _idChars = m_io.ReadBytes(4);
                    m_io.Seek(_pos);
                    f_idChars = true;
                    return _idChars;
                }
            }
            private bool f_chunkIdReadable;
            private string _chunkIdReadable;
            public string ChunkIdReadable
            {
                get
                {
                    if (f_chunkIdReadable)
                        return _chunkIdReadable;
                    _chunkIdReadable = (string) (Encoding.GetEncoding("ASCII").GetString(IdChars));
                    f_chunkIdReadable = true;
                    return _chunkIdReadable;
                }
            }
            private bool f_chunkOfs;
            private int _chunkOfs;
            public int ChunkOfs
            {
                get
                {
                    if (f_chunkOfs)
                        return _chunkOfs;
                    _chunkOfs = (int) (M_Io.Pos);
                    f_chunkOfs = true;
                    return _chunkOfs;
                }
            }
            private byte[] _saveChunkOfs;
            private Chunk _chunk;
            private Riff m_root;
            private ListChunkData m_parent;
            public byte[] SaveChunkOfs { get { return _saveChunkOfs; } }
            public Chunk Chunk { get { return _chunk; } }
            public Riff M_Root { get { return m_root; } }
            public ListChunkData M_Parent { get { return m_parent; } }
        }
        public partial class ChunkType : KaitaiStruct
        {
            public static ChunkType FromFile(string fileName)
            {
                return new ChunkType(new KaitaiStream(fileName));
            }

            public ChunkType(KaitaiStream p__io, KaitaiStruct p__parent = null, Riff p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_chunkOfs = false;
                f_chunkId = false;
                f_chunkIdReadable = false;
                f_chunkData = false;
                _read();
            }
            private void _read()
            {
                if (ChunkOfs < 0) {
                    _saveChunkOfs = m_io.ReadBytes(0);
                }
                _chunk = new Chunk(m_io, this, m_root);
            }
            private bool f_chunkOfs;
            private int _chunkOfs;
            public int ChunkOfs
            {
                get
                {
                    if (f_chunkOfs)
                        return _chunkOfs;
                    _chunkOfs = (int) (M_Io.Pos);
                    f_chunkOfs = true;
                    return _chunkOfs;
                }
            }
            private bool f_chunkId;
            private Fourcc _chunkId;
            public Fourcc ChunkId
            {
                get
                {
                    if (f_chunkId)
                        return _chunkId;
                    _chunkId = (Fourcc) (((Fourcc) Chunk.Id));
                    f_chunkId = true;
                    return _chunkId;
                }
            }
            private bool f_chunkIdReadable;
            private string _chunkIdReadable;
            public string ChunkIdReadable
            {
                get
                {
                    if (f_chunkIdReadable)
                        return _chunkIdReadable;
                    long _pos = m_io.Pos;
                    m_io.Seek(ChunkOfs);
                    _chunkIdReadable = Encoding.GetEncoding("ASCII").GetString(m_io.ReadBytes(4));
                    m_io.Seek(_pos);
                    f_chunkIdReadable = true;
                    return _chunkIdReadable;
                }
            }
            private bool f_chunkData;
            private ListChunkData _chunkData;
            public ListChunkData ChunkData
            {
                get
                {
                    if (f_chunkData)
                        return _chunkData;
                    KaitaiStream io = Chunk.DataSlot.M_Io;
                    long _pos = io.Pos;
                    io.Seek(0);
                    switch (ChunkId) {
                    case Fourcc.List: {
                        _chunkData = new ListChunkData(io, this, m_root);
                        break;
                    }
                    }
                    io.Seek(_pos);
                    f_chunkData = true;
                    return _chunkData;
                }
            }
            private byte[] _saveChunkOfs;
            private Chunk _chunk;
            private Riff m_root;
            private KaitaiStruct m_parent;
            public byte[] SaveChunkOfs { get { return _saveChunkOfs; } }
            public Chunk Chunk { get { return _chunk; } }
            public Riff M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        private bool f_chunkId;
        private Fourcc _chunkId;
        public Fourcc ChunkId
        {
            get
            {
                if (f_chunkId)
                    return _chunkId;
                _chunkId = (Fourcc) (((Fourcc) Chunk.Id));
                f_chunkId = true;
                return _chunkId;
            }
        }
        private bool f_isRiffChunk;
        private bool _isRiffChunk;
        public bool IsRiffChunk
        {
            get
            {
                if (f_isRiffChunk)
                    return _isRiffChunk;
                _isRiffChunk = (bool) (ChunkId == Fourcc.Riff);
                f_isRiffChunk = true;
                return _isRiffChunk;
            }
        }
        private bool f_parentChunkData;
        private ParentChunkData _parentChunkData;
        public ParentChunkData ParentChunkData
        {
            get
            {
                if (f_parentChunkData)
                    return _parentChunkData;
                if (IsRiffChunk) {
                    KaitaiStream io = Chunk.DataSlot.M_Io;
                    long _pos = io.Pos;
                    io.Seek(0);
                    _parentChunkData = new ParentChunkData(io, this, m_root);
                    io.Seek(_pos);
                    f_parentChunkData = true;
                }
                return _parentChunkData;
            }
        }
        private bool f_subchunks;
        private List<ChunkType> _subchunks;
        public List<ChunkType> Subchunks
        {
            get
            {
                if (f_subchunks)
                    return _subchunks;
                if (IsRiffChunk) {
                    KaitaiStream io = ParentChunkData.SubchunksSlot.M_Io;
                    long _pos = io.Pos;
                    io.Seek(0);
                    _subchunks = new List<ChunkType>();
                    {
                        var i = 0;
                        while (!io.IsEof) {
                            _subchunks.Add(new ChunkType(io, this, m_root));
                            i++;
                        }
                    }
                    io.Seek(_pos);
                    f_subchunks = true;
                }
                return _subchunks;
            }
        }
        private Chunk _chunk;
        private Riff m_root;
        private KaitaiStruct m_parent;
        public Chunk Chunk { get { return _chunk; } }
        public Riff M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
