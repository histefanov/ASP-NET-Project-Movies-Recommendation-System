﻿namespace MoviesRecommendationSystem.Services.Watchlists
{
    public interface IWatchlistService
    {
        bool Add(string userId, int movieId);

        bool Remove(string userId, int movieId);

        int Count(string userid);

        bool Exists(string userId, int movieId);
    }
}