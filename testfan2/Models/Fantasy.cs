using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

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
    public enum RoundStage
    {
        FirstRound, SecondRound, ThirdRound, Last16, QuarterFinal, SemiFinal, Final
    }

    public class Customer
    {
        public int CustomerID { get; set; }
        public String CustomerSurname { get; set; }
        public String CustomerFirstName { get; set; }
        public String CustomerEmail { get; set; }
        public String CustomerPhoneNumber { get; set; }
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

    //}
    public class FantasyTeam
    {

        private const double MaxTeamValue = 100.00;
        private const int teamSize = 11;

        //Team Id
        [Key]
        public int TeamID { get; set; }

        //Team Name
        [Required(ErrorMessage = "You must enter a Team Name")]
        [StringLength(52)]
        public String TeamName { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

     

        public int? FantasyLeagueID { get; set; }
        public virtual FantasyLeague FantasyLeague { get; set; }


        public virtual ICollection<Player> Players { get; set; }

        //public FantasyPlayer MakeTransfer(FantasyPlayer playerX)
        //{

        //}
        //public int CalculateTeamRoundScore()
        //{
        //    int total = 0;

        //    foreach(Player p in FantasyPlayers)
        //    {
                
        //    }
            
        //}
        //public int CalculateTeamTotalScore()
        //{

        //}
    }
    public class FantasyLeague
    {
        public int FantasyLeagueId { get; set; }
        public String FantasyLeagueName { get; set; }


        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<FantasyTeam> FantasyTeams { get; set; }

    }

    public class Player
    {
        //Player ID
        public int PlayerID { get; set; }

        //Player's name
        [Display(Name = "Player's Name")]
        [Required(ErrorMessage = "You must enter a PLayer's name")]
        public String PlayerName { get; set; }

        //Player's position
        public Position Position { get; set; }

        //Date of birth
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }

        //player's value
        [Display(Name = "Value")]
        [Required(ErrorMessage = "You must enter a value for player's value")]
        [Range(3, 15, ErrorMessage = "Player's value ranges from 3 to 15 million Euros")]
        public double PlayerValue { get; set; }

        //NationCode is the foreign key
        [Required]
        public String NationCode { get; set; }
        public virtual NationTeam NationTeam { get; set; }

    }
    public class RoundTeam
    {
        //Fantasy player id
        public int RoundTeamID { get; set; }

        //foreign keys
        public int TeamID { get; set; }
        public int PlayerID { get; set; }
        public virtual Player Player { get; set; }
        public virtual FantasyTeam FantasyTeam { get; set; }

        public int? RoundId { get; set; }
        public virtual Round Round { get; set; }
        
            FantasyFootballContext db = new FantasyFootballContext();
        
            private const double maxValue = 100.00;
            public double totalValue { get; set; }
            //public const string RosterKey = "RostId";
            
          
            public void AddPlayer(Player player)
            {
            
                totalValue += player.PlayerValue;
                var p = db.RoundTeams.SingleOrDefault(
                    t => t.PlayerID == player.PlayerID&& t.TeamID == FantasyTeam.TeamID);

                if ((p == null) && (totalValue <= maxValue))
                {
                // add player if not already on team
                p = new RoundTeam;

                    db.RoundTeams.Add(p);
                    db.SaveChanges();
                }
                else
                {
                   
                }

            }
            public void RemovePlayer(int id)
            {
                // Get the cart
                var player = db.RoundTeams.Single(
                    p => p.PlayerID == id);

              

                if (player != null)
                {

                        db.RoundTeams.Remove(player);
                    
                    // Save changes
                    db.SaveChanges();
                }
                
            }

            public List<Player> GetRoundTeam()
            {
                return db.Players.Where(
                    t => t.PlayerID == Player.PlayerID).ToList();
            }

            public decimal GetTotal()
            {
                // sum all players in roundteam
                decimal? total = (from players in db.RoundTeams
                                  where players.PlayerID == PlayerID
                                  select (int?)
                                  players.Player.PlayerValue).Sum();

                return total ?? decimal.Zero;
            }
    
        }
    public class NationTeam
    {
        [Key]
        [RegularExpression("([A-Z]{3})", ErrorMessage = "Invalid Nation Code")]
        public String NationCode { get; set; }

        //Nation Name
        [Display(Name = "Team")]
        public Nation Nation { get; set; }

        public virtual ICollection<Fixture> Fixtures { get; set; }
        public virtual ICollection<Player> Players { get; set; }

    }
    public class Fixture
    {
        public int FixtureId { get; set; }
        public Venue Venue { get; set; }
        public DateTime FixtureDate { get; set; }
        public Nation? HomeTeam { get; set; }
        public Nation? AwayTeam { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; } 
        public int? RoundID { get; set; }
        public virtual Round Round { get; set; }

    }
    public class Round
    {
       
        public int RoundID { get; set; }
        public RoundStage RoundStage { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        //public RoundStage getRoundStage
        //{
        //    get
        //    {
        //        if ((DateTime.Now <= this.endDate) || (DateTime.Now >= this.startDate))
        //        {
        //            return this.RoundStage;
        //        }
        //    }
        //}
    }

    public class PlayerRoundStat
    {
        public int PlayerRoundStatID { get; set; }

        //foreign keys
        public int? RoundID { get; set; }
        public virtual Round Round { get; set; }
        public int? PlayerID { get; set; }
        public virtual Player Player { get; set; }

        [Range(0, 90, ErrorMessage = "Player can only play between 0 and 90 minutes")]
        public int MinutesPlayed { get; set; }
        // public int GoalsScored { get; set; }
        public int Assist { get; set; }
        // public int CleanSheet { get; set; }
        // public int GoalsConceded { get; set; }
        public int OwnGoal { get; set; }
        // public int PenaltySaved { get; set; }
        //public int PenaltyMissed { get; set; }
        public bool YellowCarded { get; set; }
        public bool RedCarded { get; set; }
        // public int Saves { get; set; }
        // public bool ManOfTheMatch { get; set; }

        

        public int GetTotalPoints()
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
                if (OwnGoal > 0)
                { points -= (OwnGoal * 4); }
            }
            return points;
        }



    }


    public class FantasyFootballContext : DbContext
    {
        public FantasyFootballContext() : base("DefaultConnection")
        {
            Database.SetInitializer<FantasyFootballContext>(new DropCreateDatabaseIfModelChanges<FantasyFootballContext>());
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<FantasyTeam> FantasyTeams { get; set; }
        public DbSet<RoundTeam> RoundTeams { get; set; }
        public DbSet<PlayerRoundStat> PlayerRoundStats { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FantasyLeague> FantasyLeagues { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<NationTeam> NationTeams { get; set; }
    }
}

//public class DrinkRepository
//{
//    public void AddStore(CoffeeStore cs)
//    {
//        using (DrinkContext db = new DrinkContext())
//        {
//            try
//            {
//                db.CoffeeStores.Add(cs);
//                db.SaveChanges();
//            }
//            catch (Exception e)
//            { Console.WriteLine(e.ToString()); }
//        }
//    }

//    public void AddDrink(Drink drink)
//    {
//        using (DrinkContext db = new DrinkContext())
//        {
//            try
//            {
//                db.Drinks.Add(drink);
//                db.SaveChanges();
//            }
//            catch (Exception e)
//            { Console.WriteLine(e.ToString()); }
//        }
//    }

//    public void AddReview(Review review)
//    {
//        using (DrinkContext db = new DrinkContext())
//        {
//            try
//            {
//                db.Reviews.Add(review);
//                db.SaveChanges();
//            }
//            catch (Exception e)
//            { Console.WriteLine(e.ToString()); }
//        }
//    }
// }

//namespace - FantasyFootballEuro2016 - Facebook loading page
//<script>
//  window.fbAsyncInit = function()
//{
//    FB.init({
//        appId: '583597945120738',
//      xfbml: true,
//      version: 'v2.5'
//    });
//};

//  (function(d, s, id)
//{
//    var js, fjs = d.getElementsByTagName(s)[0];
//    if (d.getElementById(id)) { return; }
//    js = d.createElement(s); js.id = id;
//    js.src = "//connect.facebook.net/en_US/sdk.js";
//    fjs.parentNode.insertBefore(js, fjs);
//}(document, 'script', 'facebook-jssdk'));
//</script>

//facebook like button  - anywhere in <body>
//    <div
//  class="fb-like"
//  data-share="true"
//  data-width="450"
//  data-show-faces="true">
//</div>

//classes - customer, fantasy team, fantasy league, credit card details, fantasy player, round, fixture, player, nation, player round stats, kp stats, fw s, def s, m s