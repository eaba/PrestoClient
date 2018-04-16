﻿using System;
using System.Runtime.InteropServices;

namespace BAMCIS.PrestoClient.Model
{
    /// <summary>
    /// From io.airlift.slice.Slice.java
    /// 
    /// TODO: Finish implementing
    /// </summary>
    public sealed class Slice : IComparable<Slice>
    {
        #region Private Fields

        private static readonly int INSTANCE_SIZE = 0;

        private static readonly byte[] COMPACT = new byte[0];

        private static readonly object NOT_COMPACT = null;

        // sun.misc.Unsafe.ARRAY_BYTE_BASE_OFFSET;
        private static readonly int ARRAY_BYTE_BASE_OFFSET = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// Base object for relative addresses.  If null, the address is an
        /// absolute location in memory.
        /// </summary>
        public object Base { get; }

        /// <summary>
        /// If base is null, address is the absolute memory location of data for
        /// this slice; otherwise, address is the offset from the base object.
        /// This base plus relative offset addressing is taken directly from
        /// the Unsafe interface.
        /// Note: if base object is a byte array, this address ARRAY_BYTE_BASE_OFFSET,
        /// since the byte array data starts AFTER the byte array object header.
        /// </summary>
        public long Address { get; }

        public int Size { get; }

        /// <summary>
        /// Bytes retained by the slice
        /// </summary>
        public long RetainedSize { get; }

        /// <summary>
        /// Reference has two use cases:
        /// 1. It can be an object this slice must hold onto to assure that the
        /// underlying memory is not freed by the garbage collector.
        /// It is typically a ByteBuffer object, but can be any object.
        /// This is not needed for arrays, since the array is referenced by {@code base}.
        /// 2. If reference is not used to prevent garbage collector from freeing the
        /// underlying memory, it will be used to indicate if the slice is compact.
        /// When
        /// { @code reference == COMPACT }, the slice is considered as compact.
        /// Otherwise, it will be null.
        /// A slice is considered compact if the base object is an heap array and
        /// it contains the whole array.
        /// Thus, for the first use case, the slice is always considered as not compact.
        /// </summary>
        public object Reference { get; }

        #endregion

        #region Constructors

        public Slice()
        {
            this.Base = null;
            this.Address = 0;
            this.Size = 0;
            this.RetainedSize = INSTANCE_SIZE;
            this.Reference = COMPACT;
        }

        /// <summary>
        /// Creates a slice over the specified array.
        /// </summary>
        /// <param name="base"></param>
        public Slice(byte[] @base)
        {
            this.Base = @base ?? throw new ArgumentNullException("base");
            this.Address = ARRAY_BYTE_BASE_OFFSET;  
            this.Size = @base.Length;
            this.RetainedSize = INSTANCE_SIZE + Marshal.SizeOf(@base);
            this.Reference = COMPACT;
        }

        /// <summary>
        /// Creates a slice over the specified array range.
        /// </summary>
        /// <param name="base"></param>
        /// <param name="offset">The array position at which the slice begins</param>
        /// <param name="length">The number of array positions to include in the slice</param>
        public Slice(byte[] @base, int offset, int length)
        {
            ParameterCheck.OutOfRange(offset < @base.Length && offset + length < @base.Length, "offset");

            this.Base = @base ?? throw new ArgumentNullException("base");
            this.Address = ARRAY_BYTE_BASE_OFFSET + offset;
            this.Size = length;
            this.RetainedSize = INSTANCE_SIZE + Marshal.SizeOf(@base);
            this.Reference = (offset == 0 && length == @base.Length) ? COMPACT : NOT_COMPACT;
        }

        public int CompareTo(Slice other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
