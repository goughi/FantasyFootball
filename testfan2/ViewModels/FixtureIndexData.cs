using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testfan2.Models;

namespace testfan2.ViewModels
{
    public class FixtureIndexData
    {
        public int FixtureID{ get; set; }
        public RoundStage RoundStage { get; set; }
        public Venue Venue { get; set; }

        public TeamGoalWeight HomeTeamGoalWeight{ get; set; }
        public TeamGoalWeight AwayTeamGoalWeight { get; set; }

        public string HomeTeamNationCode { get; set; }
        public string AwayTeamNationCode { get; set; }

        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }

    }
}