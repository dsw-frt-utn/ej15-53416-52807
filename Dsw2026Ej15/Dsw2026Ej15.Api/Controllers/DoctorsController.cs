using Dsw2026Ej15.Api.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]

public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistencia;

    public DoctorsController(IPersistence persistencia)
    {
        _persistencia = persistencia;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateDoctorDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ValidationException("Name es requerido.");

        if (string.IsNullOrWhiteSpace(dto.LicenseNumber))
            throw new ValidationException("LicenseNumber es requerido.");

        Speciality? specialty = _persistencia.GetSpeciality(dto.SpecialityId);
        if (specialty == null)
            throw new ValidationException($"La especialidad con el id '{dto.SpecialityId}' no existe.");

        Doctor doctor = new Doctor(dto.Name, dto.LicenseNumber, specialty);
        _persistencia.AddDoctor(doctor);

        return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, new DoctorDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<DoctorDto> doctors = _persistencia.GetDoctors()
            .Where(d => d.IsActive)
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                LicenseNumber = d.LicenseNumber,
                SpecialityName = d.Speciality.Name
            });

        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        Doctor? doctor = _persistencia.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
            return NotFound();

        return Ok(new DoctorDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        Doctor? doctor = _persistencia.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
            return NotFound();

        doctor.Deactivate();
        _persistencia.UpdateDoctor(doctor);

        return NoContent();
    }
}
