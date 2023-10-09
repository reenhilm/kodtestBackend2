using AutoMapper;
using backend.Core.Models;
using backend.Shared.Dtos;

namespace backend.Data.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Transaction, TransactionForCreateDto>().ReverseMap();
        }
    }
}
