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
        private readonly ICommentRepository _commentRepository;

        public PostService(IPostRepository postRepository, 
            ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
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
                .GetAsync(u => u.PostId.ToString() == Id,includeProperties: "Author");
            if (postFromDb is null)
                return null;

            var comments = await _commentRepository
                .GetAllAsync(c => c.PostId.ToString() == Id && c.ParentId == null, includeProperties: "Author");

            postFromDb.Comments = comments.ToList();

            return postFromDb.ToPostResponse();
        }

        public async Task<IEnumerable<PostResponse>> GetAllPosts()
        {
            var postsFromDb = await _postRepository.GetAllAsync(includeProperties:"Author");

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
