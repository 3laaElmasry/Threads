
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Threads.BusinessLogicLayer.DTO.CommentDTO;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.ServiceContracts
{
    public interface ICommentService
    {
        Task<List<CommentResponse>?> GetAll(string postId);

        Task<CommentResponse?> CreateComment(CommentRequest commentRequest);

        Task<CommentResponse?> GetCommentById(string commentId);

        Task <List<CommentResponse>> GetCommentReplies(string parentId);

    }
}
