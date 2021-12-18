using AutoMapper;
using BottomTimeDives.Models;

namespace BottomTimeDives {
	public class MappingProfile : Profile {
		public MappingProfile() {
			CreateMap<Dive, DivePost>().ReverseMap();
		}
	}
}
