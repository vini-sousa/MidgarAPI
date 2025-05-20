namespace Midgar.Domain.Entities
{
    public class Speaker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SpeakerInformation { get; set; }

        public string ImageURL { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public IEnumerable<SocialMedia> SocialMedias { get; set; }

        public IEnumerable<SpeakerEvent> SpeakerEvents { get; set; }
    }
}