using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testfan2.Models;

namespace testfan2.ViewModels
{
    public class ViewGks
    {
        public IEnumerable<FantasyTeam> fantasyTeams { get; set; }
        public IEnumerable<Player> players { get; set; }
        public IEnumerable<Customer> customers { get; set; }
    }
}