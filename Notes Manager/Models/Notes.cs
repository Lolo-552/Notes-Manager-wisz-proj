using System.ComponentModel.DataAnnotations.Schema;

namespace Notes_Manager.Models
{
    public class Notes
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser user { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
