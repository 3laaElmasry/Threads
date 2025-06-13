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

        public async Task<bool> DeletePost(string postId, string userId)
        {
            var postFromDb = await _postRepository.GetAsync(p => p.PostId.ToString() == postId);
            if (postFromDb is null || postFromDb.AuthorId.ToString() != userId)
                return false;

            _postRepository.Remove(postFromDb);
            await _postRepository.Save();
            return true;    
        }

        public async Task<PostResponse?> Get(string Id)
        {
            Post? postFromDb = await _postRepository
                .GetAsync(u => u.PostId.ToString() == Id,includeProperties: "Author,Comments");

            return postFromDb?.ToPostResponse();
        }

        public async Task<IEnumerable<PostResponse>> GetAllPosts()
        {
            var postsFromDb = await _postRepository.GetAllAsync();

            return postsFromDb.Select(p => p.ToPostResponse());
        }

        public async Task<PostResponse> UpdatePost(string postId, PostRequest postDTO)
        {
            
            Post postFromDb = await _postRepository
                .GetAsync(u => u.PostId.ToString() == postId)?? new();
            
            postFromDb.Text = postDTO.Text;
            postFromDb.UpdatedDate = DateTime.Now;

            _postRepository.Update(postFromDb);
            await _postRepository.Save();
            return postFromDb.ToPostResponse();
        }
    }
}
