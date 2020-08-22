using System;
using System.Diagnostics;
using static PrismBotV2.Core.DLLImports.Kernel32DLL;

namespace PrismBotV2.Core.MemoryHelper
{
    /// <summary>
    /// Allows to get access to addresses and offsets within a KalOnline process in order to manipulate its memory space
    /// </summary>
    public class AddressesAndOffsets
    {

        //calculation helpers
        public Process Instance { get; private set; }
        public byte[] ModuleBuffer { get; private set; }
        public IntPtr ProcessHandle { get; private set; }

        //player related stuff
        public int PlayerSpeed_Offset { get; private set; }

        public IntPtr Player_Address { get; private set; }
        public IntPtr PlayerCurrentHP_Address { get; private set; }
        public IntPtr PlayerMaxHP_Address { get; private set; }
        public IntPtr PlayerCurrentMP_Address { get; private set; }
        public IntPtr PlayerMaxMP_Address { get; private set; }
        public int PlayerSpeedDoubled_Offset { get; private set; }

        //target related stuff
        public IntPtr CurrentTarget_Address { get; private set; }

        public int TargetServerX_Offset { get; private set; }
        public int TargetServerY_Offset { get; private set; }
        public int TargetServerZ_Offset { get; private set; }
        public int TargetX_Offset { get; private set; }
        public int TargetY_Offset { get; private set; }
        public int TargetZ_Offset { get; private set; }
        public int TargetID_Offset { get; private set; }
        public int TargetCurrentHP_Offset { get; private set; }
        public int TargetMaxHP_Offset { get; private set; }
        public int TargetName_Offset { get; private set; }

        //vftables
        public IntPtr GamePlayervftable { get; private set; }

        public IntPtr GameMonstervftable { get; private set; }
        public IntPtr GamePetvftable { get; private set; }
        public IntPtr GameHorsevftable { get; private set; }
        public IntPtr GameNPCvftable { get; private set; }
        public IntPtr Skillvftable { get; private set; }
        public IntPtr Itemvftable { get; private set; }
        public IntPtr Actionvftable { get; private set; }

        //target array stuff
        public IntPtr MonsterArrays_Address { get; private set; }

        public int MonsterObject_Offset { get; private set; }

        //address for the count of all target objects in above's array
        public IntPtr CharCount_Address { get; private set; }

        //game function addresses
        public IntPtr SendSkill_Address { get; private set; }

        public IntPtr SetTargetByID_Address { get; private set; }

        //skill related stuff
        public IntPtr QuickSlotBar_Address { get; private set; }

        public int QuickSlot_Offset { get; private set; }
        public int PreQuickSlot_Offset { get; private set; }
        public int CoolDown_Offset { get; private set; }
        public int CastTime_Offset { get; private set; }
        public int Range_Offset { get; private set; }
        public int Name_Offset { get; private set; }
        public int SkillID_Offset { get; private set; }

        private static AddressesAndOffsets AnOList;

        /// <summary>
        /// Returns an object which contains specific addresses and offsets needed for memory modifications in KalOnline
        /// </summary>
        /// <param name="process">Expects a valid running process of KalOnline</param>
        /// <returns></returns>
        public static AddressesAndOffsets GetEverything(Process process)
        {
            {
                if (AnOList == null)
                {
                    AnOList = new AddressesAndOffsets(process);
                }
                return AnOList;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="process">Expects a valid running process of KalOnline</param>
        private AddressesAndOffsets(Process process)
        {
            Instance = process;
            ModuleBuffer = new byte[Instance.MainModule.ModuleMemorySize];
            ReadProcessMemory(Instance.Handle, Instance.MainModule.BaseAddress, ModuleBuffer, (uint)Instance.MainModule.ModuleMemorySize, out _);
            SetAddressesAndOffsets();
        }

        //class methods
        /// <summary>
        /// determines addresses and offsets with patternscans
        /// </summary>
        private void SetAddressesAndOffsets()
        {
            Player_Address = new IntPtr(FindPointers(new byte[] { 0x68, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x91, 0x00, 0x00, 0x00, 0x00, 0x52, 0x68, 0x00, 0x00, 0x00, 0x00, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x88, 0x00, 0x00, 0x00, 0x00, 0x51, 0x68, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x15, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x82, 0x00, 0x00, 0x00, 0x00, 0x50, 0x68, 0x00, 0x00, 0x00, 0x00 }, "x????xx????xx????xx????x????xx????xx????xx????xx????xx????", 7, true));
            PlayerCurrentHP_Address = new IntPtr(FindPointers(new byte[] { 0xd9, 0, 0, 0xd9, 0, 0, 0, 0, 0, 0xda, 0, 0xdf, 0, 0xf6, 0, 0, 0x7a, 0, 0xd9, 0, 0, 0xd9, 0, 0, 0, 0, 0, 0xda, 0, 0xdf, 0, 0xf6, 0, 0, 0x7b, 0 }, "x??x?????x?x?x??x?x??x?????x?x?x??x?", 5, true));
            PlayerMaxHP_Address = new IntPtr(FindPointers(new byte[] { 0xa1, 0, 0, 0, 0, 0x33, 0, 0, 0, 0, 0, 0x89, 0, 0, 0xdb, 0, 0, 0xda, 0, 0, 0, 0, 0, 0xd9, 0, 0, 0xdb, 0, 0, 0, 0, 0, 0xda, 0, 0, 0, 0, 0, 0xd9, 0, 0, 0xdf, 0, 0, 0, 0, 0, 0xdf, 0, 0, 0, 0, 0, 0xde, 0, 0xd9, 0, 0, 0x83, 0, 0, 0, 0, 0, 0 }, "x????x?????x??x??x?????x??x?????x?????x??x?????x?????x?x??x??????", 19, true));
            PlayerCurrentMP_Address = new IntPtr(FindPointers(new byte[] { 0xa1, 0, 0, 0, 0, 0x33, 0, 0, 0, 0, 0, 0x89, 0, 0, 0xdb, 0, 0, 0xda, 0, 0, 0, 0, 0, 0xd9, 0, 0, 0xdb, 0, 0, 0, 0, 0, 0xda, 0, 0, 0, 0, 0, 0xd9, 0, 0, 0xdf, 0, 0, 0, 0, 0, 0xdf, 0, 0, 0, 0, 0, 0xde, 0, 0xd9, 0, 0, 0x83, 0, 0, 0, 0, 0, 0 }, "x????x?????x??x??x?????x??x?????x?????x??x?????x?????x?x??x??????", 28, true));
            PlayerMaxMP_Address = new IntPtr(FindPointers(new byte[] { 0xa1, 0, 0, 0, 0, 0x33, 0, 0, 0, 0, 0, 0x89, 0, 0, 0xdb, 0, 0, 0xda, 0, 0, 0, 0, 0, 0xd9, 0, 0, 0xdb, 0, 0, 0, 0, 0, 0xda, 0, 0, 0, 0, 0, 0xd9, 0, 0, 0xdf, 0, 0, 0, 0, 0, 0xdf, 0, 0, 0, 0, 0, 0xde, 0, 0xd9, 0, 0, 0x83, 0, 0, 0, 0, 0, 0 }, "x????x?????x??x??x?????x??x?????x?????x??x?????x?????x?x??x??????", 34, true));
            PlayerSpeed_Offset = FindPointers(new byte[] { 0x83, 0, 0, 0, 0, 0, 0, 0x74, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x89, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x89, 0, 0, 0, 0, 0 }, "x??????x?x?????x?????x?????x?????x?????x?????x?????x?????x?????x?????", 65, true);
            //TODO find pattern for speed doubled
            PlayerSpeedDoubled_Offset = 0x47DA;

            CurrentTarget_Address = new IntPtr(FindPointers(new byte[] { 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8B, 0, 0, 0, 0, 0, 0x89, 0, 0, 0x8b, 0, 0, 0x8b, 0, 0x89, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x89, 0, 0, 0xb8, 0, 0, 0, 0 }, "x?????????x?????x??x??x?x??x??????x??x??x????", 2, true));
            TargetServerX_Offset = FindPointers(new byte[] { 0x68, 0, 0, 0, 0, 0x8B, 0x0D, 0, 0, 0, 0, 0x8B, 0x91, 0, 0, 0, 0, 0x52, 0x68, 0, 0, 0, 0, 0xA1, 0, 0, 0, 0, 0x8B, 0x88, 0, 0, 0, 0, 0x51, 0x68, 0, 0, 0, 0, 0x8B, 0x15, 0, 0, 0, 0, 0x8B, 0x82, 0, 0, 0, 0, 0x50, 0x68, 0, 0, 0, 0 }, "x????xx????xx????xx????x????xx????xx????xx????xx????xx????", 48, true);
            TargetServerY_Offset = FindPointers(new byte[] { 0x68, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x91, 0x00, 0x00, 0x00, 0x00, 0x52, 0x68, 0x00, 0x00, 0x00, 0x00, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x88, 0x00, 0x00, 0x00, 0x00, 0x51, 0x68, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x15, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x82, 0x00, 0x00, 0x00, 0x00, 0x50, 0x68, 0x00, 0x00, 0x00, 0x00 }, "x????xx????xx????xx????x????xx????xx????xx????xx????xx????", 30, true);
            TargetServerZ_Offset = FindPointers(new byte[] { 0x68, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x91, 0x00, 0x00, 0x00, 0x00, 0x52, 0x68, 0x00, 0x00, 0x00, 0x00, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x88, 0x00, 0x00, 0x00, 0x00, 0x51, 0x68, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x15, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x82, 0x00, 0x00, 0x00, 0x00, 0x50, 0x68, 0x00, 0x00, 0x00, 0x00 }, "x????xx????xx????xx????x????xx????xx????xx????xx????xx????", 13, true);
            TargetX_Offset = TargetServerX_Offset + 0xC;
            TargetY_Offset = TargetServerY_Offset + 0xC;
            TargetZ_Offset = TargetServerZ_Offset + 0xC;
            TargetID_Offset = FindPointers(new byte[] { 0x8b, 0, 0, 0, 0, 0, 0x3b, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x52, 0x68, 0, 0, 0, 0, 0x68, 0, 0, 0, 0 }, "x?????x????x???????x?????x?????xx????x????", 27, true);
            TargetCurrentHP_Offset = FindPointers(new byte[] { 0x83, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x50, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x52, 0x68, 0, 0, 0, 0, 0x8D, 0, 0, 0, 0, 0, 0x50 }, "x??x?????x?????xx?????x?????xx????x?????x", 24, true);
            TargetMaxHP_Offset = FindPointers(new byte[] { 0x83, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x50, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x52, 0x68, 0, 0, 0, 0, 0x8D, 0, 0, 0, 0, 0, 0x50 }, "x??x?????x?????xx?????x?????xx????x?????x", 11, true);
            TargetName_Offset = FindPointers(new byte[] { 0x8b, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x52, 0x68, 0, 0, 0, 0, 0x68, 0, 0, 0, 0, 0xe8, 0, 0, 0, 0, 0x83, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0xa1, 0, 0, 0, 0, 0x83, 0, 0, 0, 0, 0, 0, 0x74, 0, 0x8b }, "x??x?????xx????x????x????x??x??????x????x??????x?x", 5, true);

            MonsterArrays_Address = new IntPtr(FindPointers(new byte[] { 0x55, 0x8b, 0, 0x83, 0, 0, 0xa1, 0, 0, 0, 0, 0x89, 0, 0, 0x8b, 0, 0, 0x8b, 0, 0x89, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x89, 0, 0, 0xb9, 0, 0, 0, 0 }, "xx?x??x????x??x??x?x??x??????x??x??x????", 7, true));
            MonsterObject_Offset = 0x10;

            CharCount_Address = MonsterArrays_Address + 0x4;

            GamePlayervftable = new IntPtr(FindPointers(new byte[] { 0x55, 0x8b, 0, 0x83, 0, 0, 0x89, 0, 0, 0x8b, 0, 0, 0xe8, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x81, 0, 0, 0, 0, 0, 0x89, 0, 0, 0x8d, 0, 0, 0x52, 0x8d, 0, 0, 0x50, 0x8b, 0, 0, 0xe8, 0, 0, 0, 0, 0x8b }, "xx?x??x??x??x????x??x?????x??x?????x??x??xx??xx??x????x", 22, true));
            GameMonstervftable = new IntPtr(FindPointers(new byte[] { 0x55, 0x8b, 0, 0x51, 0x89, 0, 0, 0x8b, 0, 0, 0xe8, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc6, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8b }, "xx?xx??x??x????x??x?????x??x?????????x??x?????????x??x??????x??x?????????x", 20, true));
            //GamePetvftable = FindPointers(); //TODO
            //GameHorsevftable = FindPointers(); //TODO
            GameNPCvftable = new IntPtr(FindPointers(new byte[] { 0xc7, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x81, 0, 0, 0, 0, 0, 0x89, 0, 0, 0x8d, 0, 0, 0x89, 0, 0, 0x6a, 0, 0x6a, 0, 0x8b, 0, 0, 0xe8, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc6, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8b }, "x?????x??x?????x??x??x??x?x?x??x????x??x?????????x??x?????????x??x?????????x??x??????x??x?????????x", 2, true));

            SendSkill_Address = new IntPtr(FindPointers(new byte[] { 0x55, 0x8B, 0xEC, 0x81, 0xEC, 0, 0, 0, 0, 0x56, 0xE8, 0, 0, 0, 0, 0x85, 0xC0, 0x74, 0x17, 0x83, 0x3D, 0, 0, 0, 0, 0, 0x74, 0x0E }, "xxxxx??xxxx????xxxxxx???xxxx", 0, false));
            SetTargetByID_Address = new IntPtr(FindPointers(new byte[] { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x10, 0x83, 0x3D, 0, 0, 0, 0, 0, 0x75, 0x0C, 0x0F, 0xB6, 0x05, 0, 0, 0, 0, 0x83, 0xF8, 0x01, 0x74, 0x05 }, "xxxxxxxx????xxxxxx????xxxxx", 0, false));

            QuickSlotBar_Address = new IntPtr(FindPointers(new byte[] { 0x55, 0x8b, 0, 0x83, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x81, 0, 0, 0, 0, 0, 0x89, 0, 0 }, "xx?x??x??????x??x?????x?????x??", 40, true));
            PreQuickSlot_Offset = FindPointers(new byte[] { 0x55, 0x8b, 0, 0x83, 0, 0, 0x89, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x83, 0, 0, 0, 0, 0, 0, 0x74, 0, 0x8b, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x8b, 0, 0, 0, 0, 0 }, "xx?x??x??x??x?????x??x?????????x??x??????x?x??x?????x??x?????", 48, true);
            Skillvftable = new IntPtr(FindPointers(new byte[] { 0x55, 0x8b, 0, 0x81, 0, 0, 0, 0, 0, 0x89, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0xc7, 0, 0, 0, 0, 0, 0, 0xeb, 0 }, "xx?x?????x?????x?????x?????x??????x??????x?", 23, true));

            Range_Offset = FindPointers(new byte[] { 0x83, 0, 0, 0, 0x7d, 0, 0x8b, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x8d, 0, 0, 0, 0, 0, 0, 0x89, 0, 0, 0x8b, 0, 0, 0x8b, 0, 0x8b, 0, 0, 0x8b, 0, 0x8b, 0, 0x8b, 0, 0, 0xff, 0, 0x8b, 0, 0 }, "x???x?x??x?????x??????x??x??x?x??x?x?x??x?x??", 18, true) + 0x14;
            CastTime_Offset = Range_Offset + 0x4;
            CoolDown_Offset = Range_Offset + 0x8;

            SkillID_Offset = FindPointers(new byte[] { 0x55, 0x8b, 0, 0x83, 0, 0, 0x89, 0, 0, 0x8b, 0, 0, 0x8b, 0, 0, 0, 0, 0, 0x51, 0xe8, 0, 0, 0, 0, 0x83, 0, 0, 0x89, 0, 0, 0x83, 0, 0, 0, 0, 0, 0, 0x0f, 0, 0, 0, 0, 0, 0x8b }, "xx?x??x??x??x?????xx????x??x??x??????x?????x", 14, true);
            QuickSlot_Offset = FindPointers(new byte[] { 0x83, 0, 0, 0, 0x0f, 0, 0, 0, 0, 0, 0x83, 0, 0, 0, 0x0f, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x69, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x8d, 0, 0, 0, 0, 0, 0, 0x89, 0, 0 }, "x???x?????x???x?????x??x?????x??x??????x??", 25, true);
            Name_Offset = FindPointers(new byte[] { 0x83, 0, 0, 0, 0x0f, 0, 0, 0, 0, 0, 0x83, 0, 0, 0, 0x0f, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x69, 0, 0, 0, 0, 0, 0x8b, 0, 0, 0x8d, 0, 0, 0, 0, 0, 0, 0x89, 0, 0 }, "x???x?????x???x?????x??x?????x??x??????x??", 35, true);
            DebugPrintOffsets();
        }

        private void DebugPrintOffsets()
        {
            Console.WriteLine(nameof(Player_Address) + Player_Address.ToString("X8"));
            Console.WriteLine(nameof(PlayerSpeed_Offset) + PlayerSpeed_Offset.ToString("X8"));
            Console.WriteLine(nameof(CurrentTarget_Address) + CurrentTarget_Address.ToString("X8"));
            Console.WriteLine(nameof(QuickSlotBar_Address) + QuickSlotBar_Address.ToString("X8"));
            Console.WriteLine(nameof(QuickSlot_Offset) + QuickSlot_Offset.ToString("X8"));
            Console.WriteLine(nameof(PreQuickSlot_Offset) + PreQuickSlot_Offset.ToString("X8"));
            Console.WriteLine(nameof(Skillvftable) + Skillvftable.ToString("X8"));
            Console.WriteLine(nameof(Range_Offset) + Range_Offset.ToString("X8"));
            Console.WriteLine(nameof(CastTime_Offset) + CastTime_Offset.ToString("X8"));
            Console.WriteLine(nameof(CoolDown_Offset) + CoolDown_Offset.ToString("X8"));
            Console.WriteLine(nameof(Name_Offset) + Name_Offset.ToString("X8"));
            Console.WriteLine(nameof(SkillID_Offset) + SkillID_Offset.ToString("X8"));
        }

        /// <summary>
        /// Pointerscan for a pattern of bytes
        /// </summary>
        /// <param name="patternToBeFound">Pattern of Bytes to be found in Memory</param>
        /// <param name="mask">Mask to identify known and unknown parameters of the byte pattern</param>
        /// <param name="codeOffset">Location of the pointer that shall be found (starting with 0 at the first byte in the pattern)</param>
        /// <param name="extract">Determines whether the actual address is returned or the address which is written at this specific address is returned</param>
        /// <returns></returns>
        unsafe private int FindPointers(byte[] patternToBeFound, string mask, int codeOffset, bool extract)
        {
            int startingAddress = (int)Instance.MainModule.BaseAddress;
            int moduleSize = Instance.MainModule.ModuleMemorySize - mask.Length;

            fixed (byte* _buffer = ModuleBuffer)
            {
                for (uint i = 0; i < moduleSize; i++)
                {
                    if (Compare(_buffer + i, patternToBeFound, mask))
                    {
                        if (extract)
                        {
                            return *(int*)(_buffer + i + codeOffset);
                        }
                        else
                        {
                            return (int)(startingAddress + i + codeOffset);
                        }
                    }
                }
                return 0x7FFFFFFF;
            }
        }

        /// <summary>
        /// Compares a byte from an array with a byte at a certain place in memory and returns the result
        /// </summary>
        /// <param name="buffer">A byte array (given by an external program)</param>
        /// <param name="patternToBeFound">Pattern of bytes to be found in memory</param>
        /// <param name="mask">Mask to identify known and unknown parameters of the byte pattern</param>
        /// <returns></returns>
        unsafe private bool Compare(byte* buffer, byte[] patternToBeFound, string mask)
        {
            for (int i = 0; i < mask.Length; i++, buffer++)
            {
                if (mask[i] == 'x' && *buffer != patternToBeFound[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}