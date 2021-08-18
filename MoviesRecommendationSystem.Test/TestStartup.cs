//namespace MoviesRecommendationSystem.Test
//{
//    using MyTested.AspNetCore.Mvc;
//    using Microsoft.Extensions.Configuration;
//    using Microsoft.Extensions.DependencyInjection;
//    using Moq;

//    using MoviesRecommendationSystem.Services.Movies;

//    public class TestStartup : Startup
//    {
//        public TestStartup(IConfiguration configuration)
//            : base(configuration)
//        {
//        }

//        public void ConfigureTestServices(IServiceCollection services)
//        {
//            base.ConfigureServices(services);

//            services.ReplaceTransient<IMovieService>(_ => Mock.Of<IMovieService>());
//        }
//    }
//}
