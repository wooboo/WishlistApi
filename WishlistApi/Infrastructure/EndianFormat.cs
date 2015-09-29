using System;

namespace WishlistApi.Infrastructure
{
    public enum EndianFormat
    {
        /// <summary>Least Significant Bit order (lsb)</summary>
        /// <remarks>Right-to-Left</remarks>
        /// <see cref="BitConverter.IsLittleEndian" />
        Little,

        /// <summary>Most Significant Bit order (msb)</summary>
        /// <remarks>Left-to-Right</remarks>
        Big
    };
}