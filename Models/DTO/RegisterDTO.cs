﻿namespace Social_Media.Models.DTO
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
