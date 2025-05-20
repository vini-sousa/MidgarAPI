namespace Midgar.Application.DTOs
{
    public class SpeakerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SpeakerInformation { get; set; }

        public string ImageURL { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }

        public IEnumerable<SpeakerDTO> SpeakerEvents { get; set; }
    }
}