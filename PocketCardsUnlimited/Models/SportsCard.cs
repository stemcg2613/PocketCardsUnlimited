using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocketCardsUnlimited.Models
{
    public class SportsCard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Player Name")]
        public string PlayerName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Sport { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Team { get; set; }

        [StringLength(100)]
        public string? Manufacturer { get; set; }

        [Range(1800, 2100)]
        public int Year { get; set; }

        [StringLength(50)]
        [Display(Name = "Card Number")]
        public string? CardNumber { get; set; }

        [StringLength(50)]
        public string? Condition { get; set; }

        [Range(0, 100000000)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Estimated Value")]
        public decimal EstimatedValue { get; set; }

        // Card Owner
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}