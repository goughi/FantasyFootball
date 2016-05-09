using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace testfan2.Models
{
   //Player Position
    public enum Position
    {
        [Display(Name = "Defender")]
        Defender,
        [Display(Name = "Goalkeeper")]
        GoalKeeper,
        [Display(Name = "Midfielder")]
        Midfielder,
        [Display(Name = "Forward")]
        Forward
    }

    //Teams playing in Euros
    public enum Nation
    {
        [Display(Name = "Albania")]
        Albania,
        [Display(Name = "Austria")]
        Austria,
        [Display(Name = "Belgium")]
        Belgium,
        [Display(Name = "Croatia")]
        Croatia,
        [Display(Name = "Czech Republic")]
        Czech,
        [Display(Name = "England")]
        England,
        [Display(Name = "France")]
        France,
        [Display(Name = "Germany")]
        Germany,
        [Display(Name = "Hungary")]
        Hungary,
        [Display(Name = "Iceland")]
        Iceland,
        [Display(Name = "Italy")]
        Italy,
        [Display(Name = "Northern Ireland")]
        NorthernIre,
        [Display(Name = "Poland")]
        Poland,
        [Display(Name = "Portugal")]
        Portugal,
        [Display(Name = "Republic of Ireland")]
        Ireland,
        [Display(Name = "Romania")]
        Romania,
        [Display(Name = "Russia")]
        Russia,
        [Display(Name = "Slovakia")]
        Slovakia,
        [Display(Name = "Spain")]
        Spain,
        [Display(Name = "Sweden")]
        Sweden,
        [Display(Name = "Switzerland")]
        Switzerland,
        [Display(Name = "Turkey")]
        Turkey,
        [Display(Name = "Ukraine")]
        Ukraine,
        [Display(Name = "Wales")]
        Wales
    }
    //Venues where games are being  held
    public enum Venue
    {
        SaintDenis, Marseille, Lyon, Lille, Bordeaux, Paris, SaintEtienne, Nice, Lens, Toulouse
    }
    //each nation will be rated a-d for goal probability purposes
    public enum TeamGoalWeight { A, B, C, D }

    //Each round will host a number of fixtures
    public enum RoundStage
    {
        FirstRound, SecondRound, ThirdRound, Last16, QuarterFinal, SemiFinal, Final
    }

    //Each user will create one fantasy team of 11 players
    public class FantasyTeam
    {
       private const double MaxTeamValue = 100.00;
        private const int teamSize = 11;

        //Team Id
        [Key]
        public int TeamID { get; set; }

        [ForeignKey("User")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        
        public virtual ApplicationUser User { get; set; }

        //Team Name
        [Required(ErrorMessage = "You must enter a Team Name")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Team Name")]
        public String TeamName { get; set; }

        
     
        public bool IsConfirmed { get; set; }
        //Teams will have scores for each round and an overall score for league standings
        public int FirstRoundScore { get; set; }
        public int SecondRoundScore { get; set; }
        public int OverallScore
        {
            get
            {
                return FirstRoundScore + SecondRoundScore;
            }
        }

                       
        [ForeignKey("FantasyLeague")]
        [Column(Order = 2)]
        public int FantasyLeagueID { get; set; }
        public virtual FantasyLeague FantasyLeague { get; set; }


        public virtual ICollection<Player> Players { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();
        //teams must consist of 1 goalkeeper, 4 defenders, 4 midfielders and 2 forwards.. max value will be used at a future date
        public const int maxGk = 1;
        public const int maxDf = 4;
        public const int maxMf = 4;
        public const int maxFd = 2;
        public int gkCount;
        public int dfCount;
        public int mfCount;
        public int fdCount;
        private const double maxValue = 100.00;
        public double totalValue { get; set; }
        public void AddPlayer(Player player)
        {
            if (player.Position == Position.GoalKeeper)
            {
                if (gkCount < maxGk)
                {
                    Players.Add(player);
                    totalValue += player.PlayerValue;
                    gkCount++;
                }
                else { throw new ArgumentException("You already have one goalkeeper"); }
            }
            else if (player.Position == Position.Defender)
            {
                if (dfCount < maxDf)
                {
                    Players.Add(player);
                    totalValue += player.PlayerValue;
                    dfCount++;
                    
                }
                else { throw new ArgumentException("You already have four defenders"); }
            }
            else if (player.Position == Position.Midfielder)
            {
                if (mfCount < maxMf)
                {
                    Players.Add(player);
                    totalValue += player.PlayerValue;
                    mfCount++;
                }
                else { throw new ArgumentException("You already have four midfielders"); }
            }
            else
            {
                if (fdCount < maxFd)
                {
                    Players.Add(player);
                    totalValue += player.PlayerValue;
                    fdCount++;
                }
                else { throw new ArgumentException("You already have two forwards"); }
            }
        }

    }
    //Fantasy League
    public class FantasyLeague
    {
        public int FantasyLeagueId { get; set; }
        public String FantasyLeagueName { get; set; }


        public virtual ICollection<FantasyTeam> FantasyTeams { get; set; }

    }

    //PlayerRoundStat calculates the score and stores the details
    public class PlayerRoundStat
    {
        public int PlayerRoundStatID { get; set; }

        //foreign keys
        public int? FixtureId { get; set; }
        public virtual Fixture Fixture { get; set; }
        public int PlayerID { get; set; }
        public virtual Player Player { get; set; }

        [Range(0, 90, ErrorMessage = "Player can only play between 0 and 90 minutes")]
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

        //Points for future use
        //public int OwnGoal { get; set; }
        // public int PenaltySaved { get; set; }
        //public int PenaltyMissed { get; set; }
        //public int Assist { get; set; }
        // public int Saves { get; set; }


        public int TotalPoints
        {
            get
            {
                int points;
                if (MinutesPlayed == 0)
                {
                    points = 0;
                }
                else
                {
                    if (MinutesPlayed < 60)
                    {
                        points = 2;
                    }
                    else
                    {
                        points = 4;
                    }
                    if (YellowCarded == true)
                    { points -= 2; }
                    if (RedCarded == true)
                    { points -= 6; }
                    if (CleanSheet == true)
                    { points += 5; }
                    if (ManOfTheMatch == true)
                    { points += 4; }
                    for (int i = 0; i < goalScored;i++) 
                    {
                        points += 5;
                    }
                    for (int i = 0; i < GoalsConceded; i++)
                    {
                        points -= 1;
                    }
                }
                return points;
            }
        }



    }

}

// public class CreditCardDetails
//{
//    public CardAddressCountry
//            CardAddressLine1 = model.CardAddressLine1,
//            CardAddressLine2 = model.CardAddressLine2,
//            CardAddressCity = model.CardAddressCity,
//            CardAddressZip = model.CardAddressZip,
//            CardCvc = model.CardCvc.ToString(CultureInfo.CurrentCulture),
//            CardExpirationMonth = model.CardExpirationMonth,
//            CardExpirationYear = model.CardExpirationYear,
//            CardName = model.CardName,
//            CardNumber = model.CardNumber
//public class Round
//{

//    public int RoundID { get; set; }
//    public RoundStage RoundStage { get; set; }
//    public DateTime startDate { get; set; }
//    public DateTime endDate { get; set; }
//    //public RoundStage getRoundStage
//    //{
//    //    get
//    //    {
//    //        if ((DateTime.Now <= this.endDate) || (DateTime.Now >= this.startDate))
//    //        {
//    //            return this.RoundStage;
//    //        }
//    //    }
//    //}
//}
//} 


//facebook like button  - anywhere in <body>
//    <div
//  class="fb-like"
//  data-share="true"
//  data-width="450"
//  data-show-faces="true">
//</div>

