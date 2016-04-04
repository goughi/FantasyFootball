using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testfan2.Models;

namespace testfan2.ViewModels
{
    public class RoundTeamViewModel
    {
        public int RoundTeamId { get; set; }
        public List<Player> RoundTeamPlayers { get; set; }
        public int teamTotal { get; set; }
    }
}