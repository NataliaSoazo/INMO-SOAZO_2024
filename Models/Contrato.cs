using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMO_SOAZO_2024.Models;
    public class Contrato
    {
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de inicio del contrato")]
        public DateTime FechaInicio { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Culminación  contrato")]
        public DateTime FechaTerm { get; set; }


        [Display(Name = "Código del Locatario")]
        public int IdInquilino { get; set; }

        [Required]
        [ForeignKey(nameof(IdInquilino))]
        public Inquilino Locatario { get; set; }

        [Required]
        [Display(Name = "Código Inmueble")]
        public int IdInmueble { get; set; }


        [ForeignKey(nameof(IdInmueble))]

        [Display(Name = "Datos del inmueble")]
        public Inmueble Datos { get; set; }
    }
