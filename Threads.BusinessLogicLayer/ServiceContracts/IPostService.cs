

using Threads.BusinessLogicLayer.DTO;
using Threads.DataAccessLayer.Data1.Entities;

namespace Threads.BusinessLogicLayer.ServiceContracts
{
    public interface IPostService
    {
        Task<Post> AddPost(PostDTO postDTO);
    }
}
