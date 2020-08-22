using PrismBotV2.Core.MemoryHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismBotV2.Modules.Info.Models
{
    public static class PlayerFinder
    {

        public static List<Player> FindPlayers()
        {
            List<Player> playerList = new List<Player>();
            foreach (Process client in Process.GetProcessesByName("Engine"))
            {
                AddressesAndOffsets addressesAndOffsets = AddressesAndOffsets.GetEverything(client);
                IntPtr playerLocation = addressesAndOffsets.Player_Address;
                if (playerLocation != IntPtr.Zero)
                {
                    IntPtr playerNameLocation = MemoryReader.ReadIntPtr(client.Handle, playerLocation + addressesAndOffsets.TargetName_Offset);
                    playerList.Add( new Player() { Name = MemoryReader.ReadString(client.Handle, playerNameLocation, 16) });
                }
            }

            foreach (Process client in Process.GetProcessesByName("engine"))
            {
                AddressesAndOffsets addressesAndOffsets = AddressesAndOffsets.GetEverything(client);
                IntPtr playerLocation = addressesAndOffsets.Player_Address;
                if (playerLocation != IntPtr.Zero)
                {
                    IntPtr playerNameLocation = MemoryReader.ReadIntPtr(client.Handle, playerLocation + addressesAndOffsets.TargetName_Offset);
                    playerList.Add(new Player() { Name = MemoryReader.ReadString(client.Handle, playerNameLocation, 16) });
                }
            }
            return playerList;
        }


        public static Player FindPlayerByName(string playerName)
        {
            foreach (Process client in Process.GetProcessesByName("Engine"))
            {
                AddressesAndOffsets addressesAndOffsets = AddressesAndOffsets.GetEverything(client);
                IntPtr playerLocation = addressesAndOffsets.Player_Address;
                IntPtr playerNameLocation = MemoryReader.ReadIntPtr(client.Handle, playerLocation + addressesAndOffsets.TargetName_Offset);
                return new Player() { Name = MemoryReader.ReadString(client.Handle, playerNameLocation, 16) };
            }
            return null;
        }
    }

}
