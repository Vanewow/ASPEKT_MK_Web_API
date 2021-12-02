namespace ASPEKT_MK_Web_API.Models
{
    public class Company
    {
        public Company()
        {
            this.Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }



    }
}
