namespace Social_Media.Models.DTO.AccountUser
{
    public class ChangePasswordDTO
    {
        public string userID {  get; set; }
        public string newPassword { get; set; }
        public string currentPassword { get; set; }
        public string verifyPaddword { get; set; }
    }
}
