namespace StreetWorkout.Test.Mocks
{
    using System;
    using Microsoft.EntityFrameworkCore;

    using MoviesRecommendationSystem.Data;

    public static class DataMock
    {
        public static MoviesRecommendationDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<MoviesRecommendationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new MoviesRecommendationDbContext(dbContextOptions);
            }
        }
    }
}
