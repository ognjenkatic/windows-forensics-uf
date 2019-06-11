using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility
{
    public class ExpressHuffmanDecompression
    {
        //based on https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/nf-ntifs-rtldecompressbufferex
        private static ushort _EXPRESS_HUFFMAN_FORMAT = 4;
        
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern uint RtlGetCompressionWorkSpaceSize(ushort CompressionFormatAndEngine,
            ref ulong compressBufferWorkSpaceSize,
            ref ulong compressFragmentWorkSpaceSize);

        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern uint RtlDecompressBufferEx(ushort compressionFormat,
           byte[] uncompressedBuffer, int uncompressedBufferSize,
           byte[] compressedBuffer, int compresedBufferSize,
           ref int finalUncompressedSize,
           byte[] workSpace
           );

        public static byte[] Decompress(byte[] compressedData, int decompressedDataLength)
        {
            byte[] uncompressedBuffer = new byte[decompressedDataLength];

            ulong compressBufferWorkSpaceSize = 0;
            ulong compressFragmentWorkSpaceSize = 0;
            int finalUncompressedSize = 0;

            var retval = RtlGetCompressionWorkSpaceSize(_EXPRESS_HUFFMAN_FORMAT,
                ref compressBufferWorkSpaceSize,
                ref compressFragmentWorkSpaceSize);

            byte[] workSpace = new byte[compressFragmentWorkSpaceSize];

            RtlDecompressBufferEx(_EXPRESS_HUFFMAN_FORMAT,
                uncompressedBuffer,
                decompressedDataLength,
                compressedData,
                compressedData.Length,
                ref finalUncompressedSize,
                workSpace
                );

            return uncompressedBuffer;

        }
    }
}
