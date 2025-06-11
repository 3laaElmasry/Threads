using Threads.BusinessLogicLayer.DTO.PostDTO;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.PostExtenstions
{
    public static class PostExtenstions
    {
        public static Post ToPost(this PostRequest postRegisterDTO)
        {
            return new Post()
            {
                Text = postRegisterDTO.Text,
                CreatedDate = postRegisterDTO.CreatedDate,
                UpdatedDate = postRegisterDTO.UpdatedDate,
                AuthorId = postRegisterDTO.AuthorId,
                PostId = Guid.NewGuid(),
            };
        }
       

        public static PostResponse ToPostResponse(this Post postRegisterDTO)
        {
            return new PostResponse()
            {
                Text = postRegisterDTO.Text,
                CreatedDate = postRegisterDTO.CreatedDate,
                UpdatedDate = postRegisterDTO.UpdatedDate,
                AuthorId = postRegisterDTO.AuthorId,
                PostId = Guid.NewGuid(),
                Comments = postRegisterDTO.Comments,
                Author = postRegisterDTO.Author

            };
        }
    }
}
