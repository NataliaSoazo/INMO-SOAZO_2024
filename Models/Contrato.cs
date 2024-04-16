using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMO_SOAZO_2024.Models;
    public class Contrato
    {
        [Display(Name = "CÓDIGO")]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "INICIO DEL CONTRATO")]
        public DateTime FechaInicio { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "CULMINACIÓN")]
        public DateTime FechaTerm { get; set; }
        [Display(Name = "MONTO MENSUAL")]
        public double MontoMensual { get; set; }
        public int IdInquilino { get; set; }
        [Display(Name = "ARRENDATARIO")]
        [Required]
        [ForeignKey(nameof(IdInquilino))]
        public Inquilino? Arrendatario { get; set; }
        [Required]
        public int IdInmueble { get; set; }
        [ForeignKey(nameof(IdInmueble))]

        [Display(Name = "DATOS DEL INMUEBLE")]
        public Inmueble? Datos { get; set; }
    }
