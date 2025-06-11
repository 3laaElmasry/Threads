using Threads.BusinessLogicLayer.DTO.PostDTO;
using Threads.BusinessLogicLayer.DTO.PostExtenstions;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.ServiceContracts
{
    public interface IPostService
    {
        Task<PostResponse> AddPost(PostRequest postDTO);
    }
}
