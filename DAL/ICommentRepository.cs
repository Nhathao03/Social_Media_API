﻿using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllComments();
        Task<Comment> GetCommentById(int id);
        Task AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(int id);
    }
}
