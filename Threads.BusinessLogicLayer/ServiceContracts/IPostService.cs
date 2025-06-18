using Threads.BusinessLogicLayer.DTO.PostDTO;
using Threads.BusinessLogicLayer.DTO.PostExtenstions;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.ServiceContracts
{
    public interface IPostService
    {
        Task<TweetResponse> AddPost(TweetRequest postDTO);
        Task<TweetResponse?> Get(string Id,string? includePropreties = null);
        Task<TweetResponse> UpdatePost(string postId, TweetRequest postDTO);
        Task<bool> IsExist(string id);
        Task<bool> DeletePost(string postId,string userId);
        Task<IEnumerable<TweetResponse>> GetAllPosts();
    }
}
