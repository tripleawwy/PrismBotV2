using System;
using System.Text;
using static PrismBotV2.Core.DLLImports.Kernel32DLL;

namespace PrismBotV2.Core.MemoryHelper
{
    /// <summary>
    /// A helperclass that allows the user to modify external program's memory space
    /// </summary>

    public static class MemoryWriter
    {
        /// <summary>
        /// Writes a float value to an external program at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <param name="value">No further explanation needed</param>
        public static void WriteValue(IntPtr processHandle, IntPtr targetAddress, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, targetAddress, buffer, buffer.Length, out _);
        }

        /// <summary>
        /// Writes an int value to an external program at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <param name="value">No further explanation needed</param>
        public static void WriteValue(IntPtr processHandle, IntPtr targetAddress, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, targetAddress, buffer, sizeof(int), out _);
        }

        /// <summary>
        /// Writes a bool value to an external program at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <param name="value">No further explanation needed</param>
        public static void WriteValue(IntPtr processHandle, IntPtr targetAddress, bool value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, targetAddress, buffer, sizeof(bool), out _);
        }

        /// <summary>
        /// Writes a char value to an external program at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <param name="value">No further explanation needed</param>
        public static void WriteValue(IntPtr processHandle, IntPtr targetAddress, char value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            WriteProcessMemory(processHandle, targetAddress, buffer, sizeof(char), out _);
        }

        /// <summary>
        /// Writes a string value to an external program at a certain address in memory
        /// </summary>
        /// <param name="processHandle">Handle to an external programm</param>
        /// <param name="targetAddress">Expects an accessible memory address</param>
        /// <param name="bunchOfChars">No further explanation needed</param>
        public static void WriteString(IntPtr processHandle, IntPtr targetAddress, string bunchOfChars)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(bunchOfChars);
            WriteProcessMemory(processHandle, targetAddress, buffer, bunchOfChars.Length, out _);
        }
    }
}