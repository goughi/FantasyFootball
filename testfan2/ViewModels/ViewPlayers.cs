using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using testfan2.Models;

namespace testfan2.ViewModels
{
    public class ViewPlayers
    {
        public int playerId { get; set;}
        public string playerName { get; set; }
       public Position Position { get; set; }
        [Display(Name = "Value (million)")]
        public double PlayerValue { get; set; }
        public bool added { get; set; }
    }
}