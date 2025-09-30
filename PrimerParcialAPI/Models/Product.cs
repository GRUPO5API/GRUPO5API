using System;

namespace PrimerParcialAPI.Models
{
    public class Product
    {
        public int Id { get; set; }                     // PK
        public string Name { get; set; } = null!;       // requerido
        public string? Description { get; set; }        // nullable
        public decimal Price { get; set; }              // precio
        public int Stock { get; set; }                  // cantidad
        public bool IsActive { get; set; } = true;      // activo/inactivo
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // fecha creación
        public DateTime? UpdatedAt { get; set; }        // fecha actualización (nullable)
    }
}