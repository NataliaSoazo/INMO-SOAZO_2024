namespace INMO_SOAZO_2024.Models;

public class Propietario
{
    
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Email { get; set; }
    
    public string? Dni { get; set; }
    public string? Telefono { get; set; }
    public string? Domicilio { get; set; }
    public string? Ciudad { get; set; }
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    
}