﻿namespace Social_Media.Models.DTO.AccountUser
{
    public class AuthResultDTO
    {
        public bool Success { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public IEnumerable<string> Errors { get; set; }

    }
}
