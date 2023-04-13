using AutoMapper;
using ImproveTeam.Domain.Models;
using ImproveTeam.Models.Countries;
using ImproveTeam.Models.Products;

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
            CreateMap<Country, UpdateCountryViewModel>();
            CreateMap<Region, RegionViewModel>();
            CreateMap<City, CityViewModel>();

            CreateMap<Product, ProductViewModel>();
        }

        private void MapViewModelToDomain()
        {
        }

        private void MapViewModelToViewModel()
        {
        }
    }
}
