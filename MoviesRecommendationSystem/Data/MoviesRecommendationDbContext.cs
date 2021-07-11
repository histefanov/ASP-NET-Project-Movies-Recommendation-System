using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesRecommendationSystem.Data
{
    public class MoviesRecommendationDbContext : IdentityDbContext
    {
        public MoviesRecommendationDbContext(DbContextOptions<MoviesRecommendationDbContext> options)
            : base(options)
        {
        }
    }
}
