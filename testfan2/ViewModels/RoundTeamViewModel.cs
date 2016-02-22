using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testfan2.Models;

namespace testfan2.ViewModels
{
    public class RoundTeamViewModel
    {
        public List<Player> RoundTeamPlayers { get; set; }
        public decimal teamTotal { get; set; }
    }
}