namespace MoviesRecommendationSystem.Test.Mocks
{
    using AutoMapper;
    using Moq;
    using MoviesRecommendationSystem.Infrastructure;

    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(configuration
                    => configuration.AddProfile<MappingProfile>());

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
