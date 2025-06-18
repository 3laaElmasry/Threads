using Threads.BusinessLogicLayer.DTO.PostExtenstions;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.Data.Entities;
using Threads.DataAccessLayer.RepositoryContracts;
using Threads.BusinessLogicLayer.DTO.PostDTO;
using System.Runtime.CompilerServices;

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

        public async Task<TweetResponse> AddPost(TweetRequest postRequest)
        {

            var newPost = postRequest.ToPost();




            await _postRepository.AddAsync(newPost);
            await _postRepository.Save();
            newPost = await _postRepository
                .GetAsync(p => p.TweetId == newPost.TweetId, includeProperties: "Author");
            return newPost!.ToPostResponse();
        }

        public async Task<bool> DeletePost(string postId, string userId)
        {
            var postFromDb = await _postRepository.GetAsync(p => p.TweetId.ToString() == postId);
            if (postFromDb is null || postFromDb.AuthorId.ToString() != userId)
                return false;

            var commentToRemove = await _commentRepository
                .GetAllAsync(c => c.TweetId == postFromDb.TweetId);
            _commentRepository.RemoveRange(commentToRemove);
            _postRepository.Remove(postFromDb);
            await _postRepository.Save();
            return true;    
        }

        public async Task<TweetResponse?> Get(string Id,string? includePropreties = null)
        {
            var postFromDb = await _postRepository
                .GetAsync(u => u.TweetId.ToString() == Id,includeProperties: includePropreties);

            if (postFromDb is null)
                return null;

            var comments = await _commentRepository
                .GetAllAsync(c => c.TweetId.ToString() == Id && c.ParentId == null, includeProperties: "Author");

            postFromDb.Comments = comments.ToList();

            return postFromDb.ToPostResponse();
        }

        public async Task<IEnumerable<TweetResponse>> GetAllPosts()
        {
            var postsFromDb = await _postRepository.GetAllAsync(includeProperties:"Author");

            return postsFromDb.Select(p => p.ToPostResponse());
        }

        public async Task<TweetResponse> UpdatePost(string postId, TweetRequest postDTO)
        {
            
            Tweet postFromDb = await _postRepository
                .GetAsync(u => u.TweetId.ToString() == postId)?? new();
            
            postFromDb.Text = postDTO.Text;
            postFromDb.UpdatedDate = DateTime.Now;

            _postRepository.Update(postFromDb);
            await _postRepository.Save();
            return postFromDb.ToPostResponse();
        }

        public async Task<bool> IsExist(string id)
        {
            var postFromDb = await _postRepository.GetAsync(p => p.TweetId.ToString() == id);
            return postFromDb is not null;
        }
    }
}
