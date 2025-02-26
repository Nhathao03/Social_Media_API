﻿namespace Social_Media.Models.DTO
{
    public class PostDTO
    {
        public string? UserID { get; set; }
        public string? Content { get; set; }
        public int? Views { get; set; }
        public int? Share { get; set; }
        public string? ImageUrl { get; set; }
        public int PostCategoryID { get; set; }
    }
}
