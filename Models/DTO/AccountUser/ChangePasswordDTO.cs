namespace Social_Media.Models.DTO.AccountUser
{
    public class ChangePasswordDTO
    {
        public string userID {  get; set; }
        public string newPass { get; set; }
        public string currentPass { get; set; }
        public string verifyPass { get; set; }
    }
}
