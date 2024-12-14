namespace Notes_Manager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual ICollection<Notes> Notes { get; set; }
    }
}
