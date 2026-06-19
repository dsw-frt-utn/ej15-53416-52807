using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Speciality? GetSpeciality(Guid id);
        IEnumerable<Speciality> GetSpecialities();

        void AddDoctor(Doctor doctor);
        Doctor? GetDoctor(Guid id);
        IEnumerable<Doctor> GetDoctors();
        void UpdateDoctor(Doctor doctor);
    }
}
