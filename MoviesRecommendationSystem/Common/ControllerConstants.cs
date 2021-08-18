namespace MoviesRecommendationSystem.Common
{
    public class ControllerConstants
    {
        public class Editors
        {
            public const string SubmissionSentMessage = "Your submission was sent and is awaiting approval!";
            public const string HomeControllerName = "Home";
        }

        public class Movies
        {
            public const string HomeControllerName = "Home";
            public const string AdminPanelRoute = "/Admin/Movies/All";
            public const string EditorsControllerName = "Editors";
            public const string GenreDoesNotExistMessage = "Selected genre does not exist.";
            public const string MovieAddedPublicMessage = "Your movie was added successfully and is now public!";
            public const string MovieAddedAwaitingApprovalMessage = "Your movie was added successfully and is awaiting approval!";
            public const string MovieEditedPublicMessage = "Movie was edited successfully and is now public!";
            public const string MovieEditedAwaitingApprovalMessage = "Your movie was edited successfully and is awaiting approval!";
        }

        public class Reviews
        {
            public const string MoviesControllerName = "Movies";
            public const string ReviewInvalidMessage = "Rating and content must be between 1 and 5 stars and between 5 and 200 characters respectively!";
            public const string UserHasReviewMessage = "You already have a review for this movie!";
        }

        public class Watchlists
        {
            public const string MoviesControllerName = "Movies";
            public const string AddRoute = "Watchlist/Add/{movieId}";
            public const string RemoveRoute = "Watchlist/Remove/{movieId}";
        }
    }
}
