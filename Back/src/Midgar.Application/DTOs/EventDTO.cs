using System.ComponentModel.DataAnnotations;

namespace Midgar.Application.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} field must be between 3 and 30 characters")]
        public string Local { get; set; }

        [RegularExpression(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{3}$", ErrorMessage = "The {0} field must be in the format yyyy-MM-ddTHH:mm:ss.fff")]
        public string EventDate { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "The {0} field must be between 3 and 30 characters")]
        public string Theme { get; set; }

        [Range(1, 120000, ErrorMessage = "The number of people has to be between 1 and 120 thousand")]
        public int PeopleCount { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "The image field must be in gif, jpg, jpeg, bmp or png format")]
        public string ImageURL { get; set; }

        [RegularExpression(@"\(\d{3}\)\d{5}-\d{4}", ErrorMessage = "Phone must be in the format (000)00000-0000")]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<LoteDTO> Lotes { get; set; }

        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }

        public IEnumerable<SpeakerDTO> SpeakersEvents { get; set; }
    }
}