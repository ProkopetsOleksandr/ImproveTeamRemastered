using AutoMapper;
using ImproveTeam.Domain.Models;
using ImproveTeam.Models.Countries;

namespace ImproveTeam.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapDomainToViewModel();
            MapViewModelToDomain();
            MapViewModelToViewModel();
        }

        private void MapDomainToViewModel()
        {
            CreateMap<Country, CountryViewModel>();
            CreateMap<Country, EditCountryViewModel>();
            CreateMap<Region, RegionViewModel>();
            CreateMap<City, CityViewModel>();
        }

        private void MapViewModelToDomain()
        {
        }

        private void MapViewModelToViewModel()
        {
        }
    }
}
