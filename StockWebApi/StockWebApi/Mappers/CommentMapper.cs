using StockWebApi.Dtos.Comment;
using StockWebApi.Model;

namespace StockWebApi.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto toCommentDto(this Comment comment)
        {
            return new CommentDto()
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                Title = comment.Title,


            };

        }
        public static Comment toCommentFromCreateCommentDto(this CreateCommentDto createCommentDto,int stockId)
        {
            return new Comment()
            {
                Content = createCommentDto.Content,
                Title = createCommentDto.Title,
                StockId = stockId,
               
            };
        }
        public static Comment toCommentFromUpdateCommentDto(this UpdateCommentRequestDto updateCommentDto)
        {
            return new Comment()
            {
                Content = updateCommentDto.Content,
                Title = updateCommentDto.Title,
       

            };
        }
    }
}
