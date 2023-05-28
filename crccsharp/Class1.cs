using System;
using System.Runtime.InteropServices;

namespace CRC
{
    public class Crc
    {
        private const ushort MaskPoly16 = 0x8408;

        [DllImport("crc.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int crc_test(int A);

        [DllImport("crc.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort wCalcCRC16(byte[] a_ubMsg, int a_iMsgLen);

        public static int Test(int A)
        {
            return (A == 2) ? 1 : 0;
        }

        private static ushort Crc16(byte a_ubDataIn, ushort a_wCrc)
        {
            byte result = 0;
            byte i = 0;

            for (i = 8; i != 0; i--)
            {
                result = (byte)(a_ubDataIn ^ a_wCrc);
                a_ubDataIn = (byte)(a_ubDataIn >> 1);
                a_wCrc = (ushort)(a_wCrc >> 1);
                if ((result & 0x01) != 0)
                {
                    a_wCrc ^= MaskPoly16;
                }
            }
            return a_wCrc;
        }

        private static ushort Crcppp(byte[] buf, ushort length)
        {
            ushort Crc = unchecked((ushort)~0x00);

            for (int i = 0; i < length; i++)
            {
                Crc = Crc16(buf[i], Crc);
            }

            return unchecked((ushort)~Crc);
        }

        public static ushort CalculateCRC16(byte[] a_ubMsg, int a_iMsgLen)
        {
            return Crcppp(a_ubMsg, (ushort)a_iMsgLen);
        }
    }
}
