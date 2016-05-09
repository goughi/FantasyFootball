using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;

namespace testfan2.Models
{
    public class NationTeam
    {
        //Three letter code - all capital case - for administrative purposes when creating a new nation team
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [RegularExpression("([A-Z]{3})", ErrorMessage = "Invalid Nation Code")]
        public String NationCode { get; set; }

        //Nation Name
        [Display(Name = "Team")]
        public Nation Nation { get; set; }

        //used on nation details page
        public String CrestPath
        {
            get
            {
                return "~/Images/" + NationCode + ".JPG";
            }
        }
        //used for displaying team
        public String JerseyPath
        {
            get
            {
                return "~/Images/Jerseys/" + NationCode + ".JPG";
            }
        }
        public string Song
        {
            get
            {
                string s = "~/music/" + NationCode + ".mp3";
                return s;
            }
        }
        //each team have a weight(a-d) so when they play each other a B team would have a greater probability of beating a C team
        public TeamGoalWeight TeamGoalWeight { get; set; }
        public virtual ICollection<Fixture> Fixtures { get; set; }
        public virtual ICollection<Player> Players { get; set; }

    }
}