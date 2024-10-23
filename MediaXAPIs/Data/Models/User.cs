namespace MediaXAPIs.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }
        public int GlobalId { get; set; }
        [Required]
        [StringLength(45)]
        public string Username { get; set; }
        [Required]
        [StringLength(45)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(45)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(45)]
        public string Password { get; set; }
        public string Ispasschanged { get; set; } = "N";
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Role { get; set; }
        [StringLength(1)]
        public string Islogged { get; set; } = "N";
        [StringLength(1)]
        public string Newsletter { get; set; } = "N";
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }
        [StringLength(1)]
        public string Status { get; set; } = "A";
        public DateTime? LastLoginTime { get; set; } = DateTime.UtcNow;
        public string? Filename { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public bool DelFlag { get; set; } = false;
    }

    public class UserCreateDTO
    {
        [Required]
        public string Email { get; set; }
        [StringLength(45)]
        public string Firstname { get; set; }
        [StringLength(45)]
        public string Lastname { get; set; }
        [StringLength(45)]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }

    public class UserUpdateDTO
    {
        [Required]
        public int GlobalId { get; set; }
        [Required]
        [StringLength(45)]
        public string Username { get; set; }
        [StringLength(45)]
        public string Firstname { get; set; }
        [StringLength(45)]
        public string Password { get; set; }
        public string Role { get; set; }
        [StringLength(1)]
        public string Ispasschanged { get; set; } 
        public string Phone { get; set; }
        [StringLength(1)]
        public string Islogged { get; set; } 
        [StringLength(1)]
        public string Newsletter { get; set; } 
        public DateTime? ExpiryDate { get; set; }
        [StringLength(1)]
        public string Status { get; set; }
        public string Filename { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
