﻿using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;
using System.Xml.Linq;

namespace Social_Media.DAL
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _context.comments.ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.comments.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddComment(Comment comment)
        {
            _context.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            _context.comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComment(int id)
        {
            var comment = await _context.comments.FindAsync(id);
            if (comment != null)
            {
                _context.comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

    }
}
