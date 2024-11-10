using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class Role
    {
        [Key]
        [Column("Role_ID")]
        [StringLength(25)]
        public string RoleId { get; set; }

        [Required]
        [Column("Role")]
        [StringLength(25)]
        public string Name { get; set; }

        [Column("Role_Description")]
        [StringLength(255)]
        public string RoleDescription { get; set; }

        // Navigation Properties
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
