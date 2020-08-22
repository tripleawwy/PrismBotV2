using PrismBotV2.Core.MemoryHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PrismBotV2.Modules.Info.Models
{
    public class Player
    {

        public int CurrentHP { get; set; } = 0;
        public int MaxHP { get; set; } = 0;
        public int CurrentMP { get; set; } = 0;
        public int MaxMP { get; set; } = 0;
        public string Name { get; set; } = "TemplateName";
        public Process KalClient { get; set; }

        private static Dictionary<Player, int> playerProcessList = new Dictionary<Player, int>();

        public Player()
        {

        }

        private Player(Process process)
        {
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(OnExit);
            KalClient = process;
        }

        public static Dictionary<Player, int> GetAllPlayers()
        {
            foreach (Process kalClient in Process.GetProcessesByName("Engine"))
            {
                if (!playerProcessList.ContainsValue(kalClient.Id))
                {
                    playerProcessList.Add(new Player(kalClient), kalClient.Id);
                }
            }
            return playerProcessList;
        }

        private void OnExit(object sender, EventArgs e)
        {
            if (sender is Process)
            {
                playerProcessList[this] = 0;
            }
        }

        public static Player GetPlayerByName()
        {

            return null;
        }

        //private int GetCurrentHP()
        //{
        //    return (int)(MemoryReader.ReadFloat(InheritingClient.Handle, AnOList.PlayerCurrentHP_Address) * MaxHP);
        //}

        //private int GetMaxHP()
        //{
        //    return MemoryReader.ReadInt32(InheritingClient.Handle, AnOList.PlayerMaxHP_Address);
        //}

        //private int GetCurrentMP()
        //{
        //    return MemoryReader.ReadInt32(InheritingClient.Handle, AnOList.PlayerCurrentMP_Address);
        //}

        //private int GetMaxMP()
        //{
        //    return MemoryReader.ReadInt32(InheritingClient.Handle, AnOList.PlayerMaxMP_Address);
        //}

        //private string GetPlayerName()
        //{
        //    return MemoryReader.ReadString(InheritingClient.Handle, new IntPtr(BitConverter.ToInt32(TargetBuffer, AnOList.TargetName_Offset)), 16);
        //}

    }
}
