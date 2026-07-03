using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;

        // ← Constructor sin parámetros para EF
        protected Speciality() { }

        public Speciality(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Speciality(string name, string description, Guid id) : base(id)
        {
            Name = name;
            Description = description;
        }
    }
}
