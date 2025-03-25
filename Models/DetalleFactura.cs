using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semana5.Models
{
    public class DetalleFactura
    {
        [Key]
        public int DetalleFacturaId { get; set; }

        [ForeignKey("Factura")]
        [Required(ErrorMessage = "La factura es obligatoria.")]
        public int FacturaId { get; set; }

        public Factura Factura { get; set; }

        [ForeignKey("Producto")]
        [Required(ErrorMessage = "El producto es obligatorio.")]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero.")]
        public decimal PrecioUnitario { get; set; }

        public decimal Total => Cantidad * PrecioUnitario;
    }
}