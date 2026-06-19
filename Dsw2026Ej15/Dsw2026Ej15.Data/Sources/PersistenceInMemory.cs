using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Sources
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Speciality> _specialities = new List<Speciality>();
        private readonly List<Doctor> _doctors = new List<Doctor>();

        public PersistenceInMemory()
        {
            LoadSpecialities().Wait();
        }

        private async Task LoadSpecialities()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
            string json = await File.ReadAllTextAsync(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<SpecialityDto>? dtos = JsonSerializer.Deserialize<List<SpecialityDto>>(json, options);

            if (dtos != null)
            {
                foreach (SpecialityDto dto in dtos)
                {
                    Speciality speciality = new Speciality(dto.Name, dto.Description, dto.Id);
                    _specialities.Add(speciality);
                }
            }
        }

        // Specialities
        public Speciality? GetSpeciality(Guid id) => _specialities.Find(s => s.Id == id);
        public IEnumerable<Speciality> GetSpecialities() => _specialities;

        // Doctors
        public void AddDoctor(Doctor doctor) => _doctors.Add(doctor);
        public Doctor? GetDoctor(Guid id) => _doctors.Find(d => d.Id == id);
        public IEnumerable<Doctor> GetDoctors() => _doctors;
        public void UpdateDoctor(Doctor doctor)
        {
            int index = _doctors.FindIndex(d => d.Id == doctor.Id);
            if (index >= 0)
                _doctors[index] = doctor;
        }

        // DTO interno para deserializar el JSON
        private class SpecialityDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description {  get; set; } = string.Empty;
        }
    }
}
