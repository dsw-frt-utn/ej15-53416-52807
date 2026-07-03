using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; init; } = string.Empty;
        public string LicenseNumber { get; init; } = string.Empty;
        public bool IsActive { get; private set; }
        public Speciality Speciality { get; init; } = null!;

        // ← Constructor sin parámetros para EF
        protected Doctor() { }

        // ← Constructor normal para uso en la aplicación
        public Doctor(string name, string licenseNumber, Speciality speciality)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            Speciality = speciality;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}