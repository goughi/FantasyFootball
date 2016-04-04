using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using testfan2.DAL;
using System.Web;

namespace testfan2.Models
{
    public class NationTeam
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [RegularExpression("([A-Z]{3})", ErrorMessage = "Invalid Nation Code")]
        public String NationCode { get; set; }

        //Nation Name
        [Display(Name = "Team")]
        public Nation Nation { get; set; }

        public String CrestPath
        {
            get
            {
                return "~/Images/" + NationCode + ".JPG";
            }
        }

        public TeamGoalWeight TeamGoalWeight { get; set; }
        public virtual ICollection<Fixture> Fixtures { get; set; }
        public virtual ICollection<Player> Players { get; set; }

    }
}