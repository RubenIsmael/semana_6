using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Semana5.Models
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [ForeignKey("Cliente")]
        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        public ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
    }
}