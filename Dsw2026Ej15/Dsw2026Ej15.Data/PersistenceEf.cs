using Dsw2026Ej15.Data.EF;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly AppDbContext _context;

    public PersistenceEf(AppDbContext context)
    {
        _context = context;
    }

    // ─── Specialities ───
    public Speciality? GetSpeciality(Guid id) =>
        _context.Specialities.Find(id);

    public IEnumerable<Speciality> GetSpecialities() =>
        _context.Specialities.ToList();

    // ─── Doctors ───
    public void AddDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public Doctor? GetDoctor(Guid id) =>
        _context.Doctors
            .Include(d => d.Speciality)
            .FirstOrDefault(d => d.Id == id);

    public IEnumerable<Doctor> GetDoctors() =>
        _context.Doctors
            .Include(d => d.Speciality)
            .ToList();

    public void UpdateDoctor(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        _context.SaveChanges();
    }
}