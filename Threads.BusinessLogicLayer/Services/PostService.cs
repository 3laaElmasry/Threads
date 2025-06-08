

using Threads.BusinessLogicLayer.DTO;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.Data1.Entities;
using Threads.DataAccessLayer.RepositoryContracts;

namespace Threads.BusinessLogicLayer.Services
{
    
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> AddPost(PostDTO postDTO)
        {

            Post newPost = new Post()
            {
                Text = postDTO.Text,
                AuthorId = postDTO.AuthorId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _postRepository.AddAsync(newPost);
            await _postRepository.Save();
            return newPost;
        }
    }
}
