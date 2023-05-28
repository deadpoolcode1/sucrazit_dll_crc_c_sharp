using System;
using System.Runtime.InteropServices;

namespace crccsharp
{
    public class CRC
    {
        private const ushort MaskPoly16 = 0x8408;

        public static int Test(int A)
        {
            return (A == 2) ? 1 : 0;
        }

        private static ushort Crc16(byte a_ubDataIn, ushort a_wCrc)
        {
            byte Result = 0;
            byte i = 0;

            for (i = 8; i != 0; i--)
            {
                Result = (byte)(a_ubDataIn ^ a_wCrc);
                a_ubDataIn = (byte)(a_ubDataIn >> 1);
                a_wCrc = (ushort)(a_wCrc >> 1);
                if ((Result & 0x01) != 0)
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