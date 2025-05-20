namespace Midgar.Application.DTOs
{
    public class LoteDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string InitialDate { get; set; }

        public string FinalDate { get; set; }

        public int Quantity { get; set; }

        public int EventId { get; set; }

        public EventDTO Event { get; set; }
    }
}