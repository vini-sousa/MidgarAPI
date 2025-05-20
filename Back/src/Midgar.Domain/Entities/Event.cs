namespace Midgar.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public string Local { get; set; }

        public DateTime? EventDate { get; set; }

        public string Theme { get; set; }

        public int PeopleCount { get; set; }

        public string ImageURL { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public IEnumerable<Lote> Lotes { get; set; }

        public IEnumerable<SocialMedia> SocialMedias { get; set; }

        public IEnumerable<SpeakerEvent> SpeakersEvents { get; set; }
    }
}