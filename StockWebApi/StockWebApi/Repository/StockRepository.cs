using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using StockWebApi.Data;
using StockWebApi.Dtos.Stock;
using StockWebApi.Helpers;
using StockWebApi.Interfaces;
using StockWebApi.Model;

namespace StockWebApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel=await  _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null) {
                return null;
            }
           _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            //return await _context.Stocks.Include(c=>c.Comment).ToListAsync();
            var stocks=_context.Stocks.Include(c=>c.Comment).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks=stocks.Where(s=>s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(x => x.Symbol):stocks.OrderBy(x => x.Symbol);
                }
            }
            //return await stocks.ToListAsync();

            var skipNumber =(query.PageNumber-1)*query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();

        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comment).FirstOrDefaultAsync(i=>i.Id==id);
        }

        public Task<bool> StockExist(int id)
        {
            return _context.Stocks.AnyAsync(x=>x.Id==id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            stock.Symbol = updateDto.Symbol;
            stock.MarketCap = updateDto.MarketCap;
            stock.LastDiv = updateDto.LastDiv;
            stock.CompanyName = updateDto.CompanyName;
            stock.Industry = updateDto.Industry;
            await _context.SaveChangesAsync();
            return stock;

        }
    }
}
