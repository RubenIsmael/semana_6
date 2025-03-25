using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Semana5.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres.")]
        public string Email { get; set; }

        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}