using AutoMapper;
using backend.Core.Models;
using backend.Shared.Dtos;

namespace backend.Data.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.Account, from => from.MapFrom(m => m.Account))
                .ReverseMap();

            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
