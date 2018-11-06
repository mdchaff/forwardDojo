using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace forwardDojo.Models {
    public class Joined {
        [Key]
        public int Joined_ID { get; set; }
        
        public int User_ID { get; set; }
        public int Job_ID { get; set; }
        public User User { get; set; }
        public Job Job { get; set; }
    }
}