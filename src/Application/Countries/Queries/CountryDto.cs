namespace CleanArchitecture.Application.Countries.Queries;

public class CountryDto 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PhoneAreaCode { get; set; }
    }

