using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testfan2.Models;
using System.ComponentModel.DataAnnotations;

namespace testfan2.ViewModels
{
    public class RoundTeamViewModel
    {
       
        public int PlayerRoundStatID { get; set; }
       public string PlayerName { get; set; }
        public int MinutesPlayed { get; set; }
        [UIHint("Cleansheet")]
        public bool CleanSheet { get; set; }
        public int GoalsConceded { get; set; }
        public int goalScored { get; set; }
        [UIHint("yellowcard")]
        public bool YellowCarded { get; set; }
        [UIHint("redcard")]
        public bool RedCarded { get; set; }
        [UIHint("iswin")]
        public bool IsWin { get; set; }
        [UIHint("motm")]
        public bool ManOfTheMatch { get; set; }
        public int playerTotal { get; set; }
    }
}