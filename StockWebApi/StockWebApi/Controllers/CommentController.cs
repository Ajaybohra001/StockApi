using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockWebApi.Dtos.Comment;
using StockWebApi.Interfaces;
using StockWebApi.Mappers;
using StockWebApi.Model;

namespace StockWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;

            _stockRepository = stockRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()

        {//Implement all the validations of the dtos we use modelstate
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments=await _commentRepository.GetAllAsync();
            var commentDto= comments.Select(x=>x.toCommentDto());
            return Ok(commentDto);

        }
        [HttpGet("{id:int}")]
      
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepository.GetByIdAsync(id);
            if(comments==null)
            {
                return NotFound();
            }
            return Ok(comments.toCommentDto());
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepository.StockExist(stockId))
            {
                return BadRequest("Stock not exist");
            }

            var commentModel = commentDto.toCommentFromCreateCommentDto(stockId);
            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id=commentModel.Id},commentModel.toCommentDto());

        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepository.UpdateAsync(id,commentDto.toCommentFromUpdateCommentDto());
            if(commentModel==null)
            {
                return BadRequest();
            }
            return Ok(commentModel.toCommentDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel=await _commentRepository.DeleteAsync(id);
            if(commentModel==null)
                return NotFound();
            return Ok("Comment Deleted Successfully");
        }

    }
}
