namespace Social_Media.Models.DTO.AccountUser
{
    public class PersonalInformationDTO
    {
        public string userID { get; set; }
        public string fullname { get; set; }
        public List<Address>? Addresses { get; set; }
        public DateTime Birth {  get; set; }
        public string gender { get; set; }
        public string avatar { get; set; }

    }
}
