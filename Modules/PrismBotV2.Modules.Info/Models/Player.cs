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


        public Player()
        {

        }

        public Player(string playerName)
        {
            Name = playerName;
        }

    }
}
