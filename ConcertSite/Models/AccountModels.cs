using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ConcertSite.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [StringLength(32, ErrorMessage = "Value \"{0}\" must have minimum {2} symbols.", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "Value \"{0}\" must have minimum {2} symbols.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Value \"{0}\" must have minimum {2} symbols.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Value \"{0}\" must have minimum {2} symbols.", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "Value \"{0}\" must have minimum {2} symbols.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string pass { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Value \"{0}\" must have minimum {2} symbols.", MinimumLength = 5)]
        [DataType(DataType.Text)]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Value \"{0}\" must have minimum {2} symbols.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Phone")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone is not valid.")]
        public string phone { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid.")]
        [Display(Name = "Email")]
        //[RegularExpression("/^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9_.-])+_.-)+([a-zA-Z0-9]{2,4})+$/", ErrorMessage = "Email is not valid.")]
        public string email { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
