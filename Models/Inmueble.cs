using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMO_SOAZO_2024.Models;
public class Inmueble

    {
    

             [Display(Name = "CÓDIGO")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Campo obligatorio")]
            [StringLength(50, MinimumLength = 5, ErrorMessage = "Ingrese una dirección válida")]
    
            [Display(Name = "DIRECCIÓN")]
            public string? Direccion { get; set; }

            [Display(Name = "AMBIENTES")]
            [Required(ErrorMessage = "Campo obligatorio")]
            public int Ambientes { get; set; }
            [Display(Name = "USO")]
            [Required(ErrorMessage = "Campo obligatorio")]
            [StringLength(8, MinimumLength = 4, ErrorMessage = "Ingrese un uso válido válido")]
            public string? Uso { get; set; }

            [Display(Name = "PRECIO APROX.")]
            public double? Valor { get; set; }

            [Display(Name = "DISPONIBLE")]
            [Required(ErrorMessage = "Campo obligatorio")]
            public string? Disponible { get; set; }
            [Display(Name = "DUEÑO")]
            [Required(ErrorMessage = "Campo obligatorio")]
            public int PropietarioId { get; set; }
            [ForeignKey(nameof(PropietarioId))] 
            public Propietario? Duenio { get; set; }

            
        }