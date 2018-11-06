using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace forwardDojo.Models {
        public class Job {
                [Key]
                public int Job_ID { get; set; }
                //====================================================
                public string legal { get; set; }
                public string slug { get; set; }
                public string epoch { get; set; }
                public DateTime? date { get; set; }
                public string company { get; set; }
                public string position { get; set; }
                //     public List<object> tags { get; set; }
                public string description { get; set; }
                public string url { get; set; }
                public string logo { get; set; }

                //===========  ACTIVITIES JOINED/CREATED  ===========
                public bool applied { get; set; }

                public List<Joined> Joineds { get; set; }
                public Job () {
                        Joineds = new List<Joined> ();
                }
                //====================================================
        }
}