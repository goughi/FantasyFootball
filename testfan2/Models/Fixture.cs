using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace testfan2.Models
{

    public class Fixture
    {
        ApplicationDbContext db = new ApplicationDbContext();
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int FixtureId { get; set; }
        public Venue Venue { get; set; }
        // public DateTime FixtureDate { get; set; }
        [Required]
        [ForeignKey("HomeTeam"), Column(Order = 0)]
        public String HomeTeamNationCode { get; set; }
        [Required]
        [ForeignKey("AwayTeam"), Column(Order = 1)]
        public String AwayTeamNationCode { get; set; }
        public virtual NationTeam HomeTeam { get; set; }
        
        public virtual NationTeam AwayTeam { get; set; }
        
   

        public RoundStage RoundStage { get; set; }


        public virtual ICollection<PlayerRoundStat> PlayerRoundStats { get; set; }

        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public List<Player> HomeTeamScorer { get; set; }
        public List<Player> AwayTeamScorer { get; set; }
        public List<Player> YellowCards { get; set; }
        public List<Player>  RedCards { get; set; }

        public bool gamePlayed { get; set; }
    }

    



}
