using System.ComponentModel.DataAnnotations;

namespace PrimerParcialAPI.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(120)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime StartAt { get; set; }   

        public DateTime? EndAt { get; set; }    

        [Required]
        public bool IsOnline { get; set; }

        public string? Notes { get; set; }      
    }
}