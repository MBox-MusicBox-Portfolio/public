﻿using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MBox.Models.Db
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
        [Required]
        public Role? Role { get; set; }
        [JsonIgnore]
        public List<Band>? FollowingsBands { get; set; } = new();
        [Required]
        public string? Password { get; set; }
        public bool IsEmailVerify { get; set; } = false;
        public bool IsBlocked { get; set; } = false;
    }
}
