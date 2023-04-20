using AutoMapper;
using SimpleBlog.Models;

namespace SimpleBlog
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Post, PostDto>();
            CreateMap<UserDto, User>();
            CreateMap<PostDto, Post>();
        }
    }

}
