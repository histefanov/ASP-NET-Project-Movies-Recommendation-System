namespace MoviesRecommendationSystem.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 4;
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }

        public class Movie
        {
            public const int TitleMinLength = 1;
            public const int TitleMaxLength = 150;
            public const int YearMinValue = 1900;
            public const int YearMaxValue = 2050;
            public const int PlotMinLength = 20;
            public const int PlotMaxLength = 200;
            public const int RuntimeMinValue = 10;
            public const int RuntimeMaxValue = 600;
            public const int LanguageMinLength = 2;
            public const int LanguageMaxLength = 30;
            public const int StudioNameMinLength = 3;
            public const int StudioNameMaxLength = 40;
            public const int SeasonCountMinValue = 1;
            public const int SeasonCountMaxValue = 30;
        }

        public class Genre
        {
            public const int NameMaxLength = 30;
        }
        
        public class Person
        {
            public const int FullNameMinLength = 4;
            public const int FullNameMaxLength = 50;
            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 50;
            public const int MiddleNameMaxLength = 50;
            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 50;
            public const int EditorMinAge = 16;
            public const int EditorMaxAge = 100;
        }    
    }
}
