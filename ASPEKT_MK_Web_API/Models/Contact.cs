namespace ASPEKT_MK_Web_API.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        public int  CompanyId { get; set; }
        public virtual Company Company { get; set; }

        
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
