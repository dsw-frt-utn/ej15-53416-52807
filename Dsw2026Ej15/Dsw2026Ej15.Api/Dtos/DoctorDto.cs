namespace Dsw2026Ej15.Api.Dtos
{
    public class DoctorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public string SpecialityName { get; set; } = string.Empty;
    }
}
