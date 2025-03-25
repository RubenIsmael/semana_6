using System.ComponentModel.DataAnnotations;

namespace Semana5.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }
    }
}