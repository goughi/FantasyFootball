using System;
using System.Collections.Generic;
using testfan2.Models;

namespace testStatApp
{
    class TestScoreProbability
    {
        //goal probability 
        public static readonly int[] Goal = { 0, 1, 2, 3, 4, 5 };
        public static readonly double[] SameGoalWeight = { .2, .3, .25, .15, .07, .03 };
        public static readonly double[] BetterByOneGoalWeight = { .1, .29, .3, .18, .09, .04 };
        public static readonly double[] BetterByTwoGoalWeight = { .08, .25, .3, .2, .11, .06 };
        public static readonly double[] BetterByThreeGoalWeight = { .06, .2, .29, .23, .13, .08 };
        public static readonly double[] WorseByOneGoalWeight = { .4, .34, .2, .04, .02 };
        public static readonly double[] WorseByTwoGoalWeight = { .43, .36, .18, .021, .009 };
        public static readonly double[] WorseByThreeGoalWeight = { .5, .36, .13, .006, .004 };

        //teams
        public NationTeam a;
    public NationTeam b; 
        
        //fixture
        public Fixture f1 = new Fixture { FixtureId = 003, Venue = Venue.SaintDenis, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "IRE", AwayTeamNationCode = "SWE", HomeTeamScore = 0, AwayTeamScore = 0, AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false };

        //method to get hometeam score
        public int GetHomeTeamScore(NationTeam homeTeam, NationTeam awayTeam)
        {

            double[] weight = new double[] { };

            int[] list = Goal;
            if (homeTeam.TeamGoalWeight == awayTeam.TeamGoalWeight)
            {
                weight = SameGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight + 1) == awayTeam.TeamGoalWeight)
            {
                weight = BetterByOneGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight + 2) == awayTeam.TeamGoalWeight)
            {
                weight = BetterByTwoGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight + 3) == awayTeam.TeamGoalWeight)
            {
                weight = BetterByThreeGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight - 1) == awayTeam.TeamGoalWeight)
            {
                weight = WorseByOneGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight - 2) == awayTeam.TeamGoalWeight)
            {
                weight = WorseByTwoGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight - 3) == awayTeam.TeamGoalWeight)
            {
                weight = WorseByThreeGoalWeight;
            }
            var weighed_list = generateWeighedList(list, weight);
            Random random = new Random();
            int Myrandom = RandomNumber(0, weighed_list.Count - 1);

            int homeTeamGoal = (weighed_list[Myrandom]);
            return homeTeamGoal;
        }
        //method to get away team score
        public int GetAwayTeamScore(NationTeam homeTeam, NationTeam awayTeam)
        {

            double[] weight = new double[] { };

            int[] list = Goal;
            if (awayTeam.TeamGoalWeight == homeTeam.TeamGoalWeight)
            {
                weight = SameGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight + 1) == homeTeam.TeamGoalWeight)
            {
                weight = BetterByOneGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight + 2) == homeTeam.TeamGoalWeight)
            {
                weight = BetterByTwoGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight + 3) == homeTeam.TeamGoalWeight)
            {
                weight = BetterByThreeGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight - 1) == homeTeam.TeamGoalWeight)
            {
                weight = WorseByOneGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight - 2) == homeTeam.TeamGoalWeight)
            {
                weight = WorseByTwoGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight - 3) == homeTeam.TeamGoalWeight)
            {
                weight = WorseByThreeGoalWeight;
            }
            var weighed_list = generateWeighedList(list, weight);
           
            int Myrandom = RandomNumber(0, weighed_list.Count - 1);
            
        int awayTeamGoal = (weighed_list[Myrandom]);
            return awayTeamGoal;

        }
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
       
        //method to get a random number
        public static int RandomNumber(int min, int max)
    {
        lock (syncLock)
        { // synchronize
            return random.Next(min, max);
        }
    }
   

        public static List<int> generateWeighedList(int[] list, double[] weight)
        {
            List<int> weighed_list = new List<int>();
            // Loop over weights
            for (var i = 0; i < weight.Length; i++)
            {
                var multiples = weight[i] * 100;

                // Loop over the list of items
                for (var j = 0; j < multiples; j++)
                {
                    weighed_list.Add(list[i]);
                }
            }

            return weighed_list;

        }
        public int homescore;
        public int awayscore;
        //test constructor - pass in two teams
        public TestScoreProbability(NationTeam a, NationTeam b)
        {
            this.a = a;
            this.b = b;
           homescore = GetHomeTeamScore(a, b);
           awayscore = GetAwayTeamScore(a, b);
        }

        //print out result from two teams
        public override string ToString()
        {
            string result = "result:  "+ a.Nation + " "  + homescore + "-" + awayscore + " " + b.Nation;
            return result;
        }
        }
   
}
