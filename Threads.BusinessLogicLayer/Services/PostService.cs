using Threads.BusinessLogicLayer.DTO.PostExtenstions;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.Data.Entities;
using Threads.DataAccessLayer.RepositoryContracts;
using Threads.BusinessLogicLayer.DTO.PostDTO;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

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

        public async Task<PostResponse?> Get(string Id)
        {
            Post? postFromDb = await _postRepository
                .GetAsync(u => u.PostId.ToString() == Id,includeProperties: "Author,Comments");

            return postFromDb?.ToPostResponse();
        }
    }
}
