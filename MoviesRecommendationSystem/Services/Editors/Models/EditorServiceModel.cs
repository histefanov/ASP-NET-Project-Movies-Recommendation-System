namespace MoviesRecommendationSystem.Services.Editors.Models
{
    public class EditorServiceModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDate { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }
    }
}
