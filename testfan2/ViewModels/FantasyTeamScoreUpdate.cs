using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testfan2.Models;

namespace testfan2.ViewModels
{
    public class FantasyTeamScoreUpdate
    {
        
        public string PlayerName { get; set; }
        public Position Position { get; set; }
        public string Team { get; set; }
        public int Score { get; set; }

    }
}