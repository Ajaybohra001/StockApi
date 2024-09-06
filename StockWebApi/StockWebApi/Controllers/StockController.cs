using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockWebApi.Data;
using StockWebApi.Dtos.Stock;
using StockWebApi.Helpers;
using StockWebApi.Interfaces;
using StockWebApi.Mappers;
using StockWebApi.Model;

namespace StockWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDbContext context,IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var stocks=await _context.Stocks.ToListAsync();
            //var stocks = await _stockRepository.GetAllAsync();

            //Now we using filtering
            var stocks = await _stockRepository.GetAllAsync(query);

            var stockDto = stocks.Select(s => s.toStockDto());
        return Ok(stockDto);
        }

        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetById(int id) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var stock= await _context.Stocks.FindAsync(id);
            var stock=await _stockRepository.GetByIdAsync(id);
            if(stock==null)
            {
                return NotFound();
            }

            return Ok(stock.toStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel=stockDto.toStockFromCreateDTO();
           //await _context.Stocks.AddAsync(stockModel);    
           //await _context.SaveChangesAsync();
          await _stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById),new {id=stockModel.Id},stockModel.toStockDto());  
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateStockRequestDto updateDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var stock=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            // if(stock==null)
            // {
            //     return NotFound();
            // }
            // stock.Symbol= updateDto.Symbol;
            // stock.MarketCap= updateDto.MarketCap;
            // stock.LastDiv= updateDto.LastDiv;
            // stock.CompanyName= updateDto.CompanyName;
            // stock.Industry= updateDto.Industry;
            //await _context.SaveChangesAsync();
            var stock = await _stockRepository.UpdateAsync(id,updateDto);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.toStockDto());

        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var stock=await _context.Stocks.FirstOrDefaultAsync(x=> x.Id==id);

            //if(stock==null)
            //{
            //    return NotFound();
            //}

            //_context.Remove(stock);
            // await _context.SaveChangesAsync();
            var stock= await _stockRepository.DeleteAsync(id);
            if (stock == null)
            {    return NotFound();
            }    
            return Ok();
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id , [FromBody] JsonPatchDocument<Stock> patchDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var resource=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(resource==null)
            {
                return NotFound();
            }
            patchDocument.ApplyTo(resource, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(resource);
        }
    }
}
