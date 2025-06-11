using Threads.BusinessLogicLayer.DTO.PostExtenstions;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.Data.Entities;
using Threads.DataAccessLayer.RepositoryContracts;
using Threads.BusinessLogicLayer.DTO.PostDTO;

namespace Threads.BusinessLogicLayer.Services
{

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostResponse> AddPost(PostRequest postRequest)
        {

            var newPost = postRequest.ToPost();

            await _postRepository.AddAsync(newPost);
            await _postRepository.Save();
            return newPost.ToPostResponse();
        }
    }
}
