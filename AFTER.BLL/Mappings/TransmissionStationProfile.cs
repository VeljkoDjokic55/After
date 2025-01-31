using AutoMapper;
using AFTER.DAL.Model;
using AFTER.Shared.DTOs.TS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.BLL.Mappings
{
    public class TransmissionStationProfile : Profile
    {
        public TransmissionStationProfile()
        {
            CreateMap<TransmissionStation, TransmissionStationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));

            CreateMap<TransmissionStationDto, TransmissionStation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));

        }
    }
}
