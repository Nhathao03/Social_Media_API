﻿namespace Social_Media.Models.DTO
{
    public class CommentDTO
    {
        public string userID { get; set; }
        public int postID { get; set; }
        public string Content {  get; set; }
        public string? ImageUrl { get; set; }
        public int sticker {  get; set; }
    }
}
