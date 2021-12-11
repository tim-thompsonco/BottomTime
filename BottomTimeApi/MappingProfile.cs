using AutoMapper;
using BottomTimeApi.Models;

namespace BottomTimeApi {
	public class MappingProfile : Profile {
		public MappingProfile() {
			CreateMap<Dive, DivePost>().ReverseMap();
		}
	}
}
