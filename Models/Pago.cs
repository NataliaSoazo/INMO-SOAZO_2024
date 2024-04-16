using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMO_SOAZO_2024.Models;
public class Pago

    {
    

            [Display(Name = "CÓDIGO")]
            public int Id { get; set; }
            
            [Display(Name = "N° DE PAGO")]
            [Required(ErrorMessage = "Campo obligatorio")]
            public int Nro { get; set; }

            [Display(Name = "FECHA DE PAGO")]
            [Required(ErrorMessage = "Campo obligatorio")]
            [DataType(DataType.Date)]
            public DateTime Fecha { get; set; }
            [Display(Name = "MES DE REFERENCIA")]
            [Required(ErrorMessage = "Campo obligatorio")]
            public string? Referencia { get; set; }
            [Required(ErrorMessage = "Campo obligatorio")]
            [Display(Name = "IMPORTE")]
            public double Importe { get; set; }

            [Display(Name = "ANULADO")]
            public String? Anulado { get; set; }
            [Display(Name = "CONTRATO REFERIDO")]
            [Required(ErrorMessage = "Campo obligatorio")]
            public int IdContrato { get; set; }
            
            public Contrato? DatosContrato { get; set; }

            
        }