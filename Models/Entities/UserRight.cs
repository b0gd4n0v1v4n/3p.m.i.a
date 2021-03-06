﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class UserRight : Entity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string RightId { get; set; }
    }
}
