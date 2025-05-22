using System.ComponentModel.DataAnnotations;

namespace Midgar.Application.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} field must be between 3 and 30 characters")]
        public string Local { get; set; }

        public string EventDate { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} field must be between 3 and 30 characters")]
        public string Theme { get; set; }

        [Range(1, 120000, ErrorMessage = "The number of people has to be between 1 and 120 thousand")]
        public int PeopleCount { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "The image field must be in gif, jpg, jpeg, bmp or png format")]
        public string ImageURL { get; set; }

        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<LoteDTO> Lotes { get; set; }

        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }

        public IEnumerable<SpeakerDTO> SpeakersEvents { get; set; }
    }
}