using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using testfan2.DAL;

namespace testfan2.Models
{

    public class Player
    {

        //Player ID
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlayerID { get; set; }

        //Player's name
        [Display(Name = "Player's Name")]
        [Required(ErrorMessage = "You must enter a Player's name")]
        public String PlayerSurname { get; set; }

        //Player's name
        [Display(Name = "Player's Name")]
        [Required(ErrorMessage = "You must enter a Player's name")]
        public String PlayerFirstname { get; set; }

        //Player's position
        public Position Position { get; set; }

        //Date of birth
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }

        //player's value
        [Display(Name = "Value(million")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You must enter a value for player's value")]
        [Range(3, 15, ErrorMessage = "Player's value ranges from 3 to 15 million Euros")]
        public double PlayerValue { get; set; }

        //player's goal weight
        [Range(0,1)]
        public double GoalWeight{get;set;} 

        //get player's total points score so far
        public int TotalPoints
        {
            get
            {
                using (FantasyFootballContext db = new FantasyFootballContext())
                {
                    //int total;
                    // var points = db.PlayerRoundStats.ToList().(r => r.PlayerID == PlayerID);
                    //foreach(PlayerRoundStat r in stats)
                    //{
                    //     total += r.GetTotalPoints();
                    //}
                    //double points = 8;
                    //if (points != 0)
                    //{
                    //    int totalPoints = points.Sum(p => p.GetTotalPoints()) ?? 0;
                    //    return totalPoints;
                    //}

                    return 0;
                }
            }

        }

        //NationCode is the foreign key
        [Required]
        public String NationCode { get; set; }
        public virtual NationTeam NationTeam { get; set; }
        public virtual ICollection<FantasyTeam> FantasyTeams { get; set; }
    }
}