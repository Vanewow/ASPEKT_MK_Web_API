namespace ASPEKT_MK_Web_API.Models
{
    public class Country
    {
        public Country()
        {
            this.Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }


    }
}
