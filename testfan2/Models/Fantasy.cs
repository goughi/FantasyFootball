using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using testfan2.DAL;

namespace testfan2.Models
{
    //public enum NationCode
    //{
    //    IRE, ALB, ESP, ENG, GER, NIR, WAL, ROM, FRA, AUS, BEL, CZE, CRO, HUN, ISL, ITA, POL, POR, RUS, SLO, SWE, TUR, UKR, SUI
    //}
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
    public enum Venue
    {
        SaintDenis, Marseille, Lyon, Lille, Bordeaux, Paris, SaintEtienne, Nice, Lens, Toulouse
    }
    //each nation will be rated a-d for goal probability purposes
    public enum TeamGoalWeight { A, B, C, D }


    public enum RoundStage
    {
        FirstRound, SecondRound, ThirdRound, Last16, QuarterFinal, SemiFinal, Final
    }

    public class Customer
    {
        public int CustomerID { get; set; }
     
        [Display(Name = "Surname")]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        [Required(ErrorMessage ="You must enter a surname")]
        public String CustomerSurname { get; set; }
      
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "You must enter a firstname")]
        public String CustomerFirstName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public String CustomerEmail { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Display(Name = "Mobile number")]
        public String CustomerPhoneNumber { get; set; }
    }

    public class FantasyTeam
    {
        private const double MaxTeamValue = 100.00;
        private const int teamSize = 11;

        //Team Id
        [Key]
        public int TeamID { get; set; }

        //Team Name
        [Required(ErrorMessage = "You must enter a Team Name")]
        [StringLength(50, MinimumLength = 3)]
        public String TeamName { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }



        public int? FantasyLeagueID { get; set; }
        public virtual FantasyLeague FantasyLeague { get; set; }


        public virtual ICollection<Player> Players { get; set; }

        FantasyFootballContext db = new FantasyFootballContext();
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
    public class FantasyLeague
    {
        public int FantasyLeagueId { get; set; }
        public String FantasyLeagueName { get; set; }


        //public int CustomerId { get; set; }
        //public virtual Customer Customer { get; set; }

        public virtual ICollection<FantasyTeam> FantasyTeams { get; set; }

    }






    public class PlayerRoundStat
    {
        public int PlayerRoundStatID { get; set; }

        //foreign keys
        public int? FixtureId { get; set; }
        public virtual Fixture Fixture { get; set; }
        public int? PlayerID { get; set; }
        public virtual Player Player { get; set; }

        [Range(0, 90, ErrorMessage = "Player can only play between 0 and 90 minutes")]
        public int MinutesPlayed { get; set; }
        // public int GoalsScored { get; set; }
        //public int Assist { get; set; }
         public int CleanSheet { get; set; }
         public int GoalsConceded { get; set; }
        //public int OwnGoal { get; set; }
        public int goalScored { get; set; }
        // public int PenaltySaved { get; set; }
        //public int PenaltyMissed { get; set; }
        [UIHint("yellowcard")]
        public bool YellowCarded { get; set; }
        [UIHint("redcard")]
        public bool RedCarded { get; set; }
        [UIHint("iswin")]
        public bool IsWin { get; set; }
        // public int Saves { get; set; }
        [UIHint("motm")]
         public bool ManOfTheMatch { get; set; }



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
                    for (int i = 0; i < goalScored;i++) 
                    {
                        points += 5;
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

//classes - customer, fantasy team, fantasy league, credit card details, fantasy player, round, fixture, player, nation, player round stats, kp stats, fw s, def s, m s