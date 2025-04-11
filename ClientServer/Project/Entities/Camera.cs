using System.ComponentModel.DataAnnotations;

namespace Project.Entities
{
    public class Camera
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } = null;

        public virtual ICollection<Snapshot> Snapshots { get; set; } = new List<Snapshot>();
    }
}
