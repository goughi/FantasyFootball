using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testfan2.Models;

namespace testfan2.ViewModels
{
    public class ScorerIndexData
    {
        public int playerId { get; set; }
        public string playerName { get; set; }
        
        public double GoalWeight { get; set; }
        public bool scored { get; set; }
    }
}