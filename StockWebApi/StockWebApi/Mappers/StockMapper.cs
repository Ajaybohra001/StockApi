using StockWebApi.Dtos.Stock;
using StockWebApi.Model;
using System.Runtime.CompilerServices;

namespace StockWebApi.Mappers
{
    public static class StockMapper
    {
        public static StockDto toStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                LastDiv=stockModel.LastDiv,
                Industry=stockModel.Industry,
                MarketCap=stockModel.MarketCap,
                Comments=stockModel.Comment.Select(c=>c.toCommentDto()).ToList()

            };

        }
        public static Stock toStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }

    }
}
