using System;
using System.Text;
using static PrismBotV2.Core.DLLImports.Kernel32DLL;

namespace PrismBotV2.Core.MemoryHelper
{
    /// <summary>
    /// A helperclass that allows the user to read chunks of memory
    /// </summary>
    public static class MemoryReader
    {
        /// <summary>
        /// Returns an int value from an external program given at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <returns></returns>
        public static int ReadInt32(IntPtr processHandle, IntPtr targetAddress)
        {
            byte[] buffer = new byte[sizeof(int)];
            if (ReadProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out _))
            {
                return BitConverter.ToInt32(buffer, 0);
            }
            return 0;
        }

        internal static bool ReadBoolean(IntPtr processHandle, IntPtr targetAddress)
        {
            byte[] buffer = new byte[sizeof(bool)];
            if (ReadProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out _))
            {
                return BitConverter.ToBoolean(buffer, 0);
            }
            return false;
        }

        /// <summary>
        /// Returns an unsigned int value from an external program given at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <returns></returns>
        public static uint ReadUInt32(IntPtr processHandle, IntPtr targetAddress)
        {
            byte[] buffer = new byte[sizeof(int)];
            ReadProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out _);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Returns a float value from an external program given at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <returns></returns>
        public static float ReadFloat(IntPtr processHandle, IntPtr targetAddress)
        {
            byte[] buffer = new byte[sizeof(float)];
            ReadProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out _);
            return BitConverter.ToSingle(buffer, 0);
        }

        public static IntPtr ReadIntPtr(IntPtr processHandle, IntPtr targetAddress)
        {
            byte[] buffer = new byte[sizeof(float)];
            ReadProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out _);
            return new IntPtr(BitConverter.ToInt32(buffer, 0));
        }

        /// <summary>
        /// Returns a string from an external program given at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <param name="stringLength">Specifies the length of the string to be returned</param>
        /// <returns></returns>
        public static string ReadString(IntPtr processHandle, IntPtr targetAddress, int stringLength)
        {
            byte[] buffer = new byte[stringLength];
            ReadProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out _);
            return Encoding.ASCII.GetString(buffer);
        }

        /// <summary>
        /// Returns a byte array from an external program given at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <param name="byteArrayLength">Specifies the length of the string to be returned</param>
        /// <returns></returns>
        public static byte[] ReadByteArray(IntPtr processHandle, IntPtr targetAddress, int byteArrayLength)
        {
            byte[] buffer = new byte[byteArrayLength];
            ReadProcessMemory(processHandle, targetAddress, buffer, (uint)buffer.Length, out _);
            return buffer;
        }
    }
}