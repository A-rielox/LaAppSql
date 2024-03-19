using App.DTOs;
using App.Entities;
using AutoMapper;

namespace App.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //           -------->
        CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.Pictures.FirstOrDefault(x => x.IsMain == 1).Url));

        CreateMap<Picture, PictureDto>();
        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<RegisterDto, AppUser>();

        //CreateMap<Message, MessageDto>();
        //.ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src =>
        //    src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
        //.ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src =>
        //    src.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
    }
}
