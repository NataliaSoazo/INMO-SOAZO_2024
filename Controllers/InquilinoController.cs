using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMO_SOAZO_2024.Models;
using ZstdSharp.Unsafe;

namespace INMO_SOAZO_2024.Controllers;

public class InquilinoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public InquilinoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        RepositorioInquilino rp = new RepositorioInquilino();
        var lista = rp.GetInquilinos();
        return View(lista);
    }
    public IActionResult Editar(int id)
    {
        if (id > 0){
            RepositorioInquilino rp = new RepositorioInquilino();
            var inquilino = rp.getInquilino(id);
            return View(inquilino); 
        } else {
            return View();
        }
    }
    
    public IActionResult Guardar( Inquilino inquilino)
    {  
        RepositorioInquilino rp = new RepositorioInquilino();
        if(inquilino.Id  > 0){
            rp.ModificarInquilino(inquilino);

        }else
        rp.AltaInquilino(inquilino);
        return RedirectToAction(nameof(Index));
    }
     public IActionResult Eliminar( int id)
    {  
        RepositorioInquilino rp = new RepositorioInquilino();
        rp.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }    
    public IActionResult Detalles(int id)
    {  
        RepositorioInquilino rp = new RepositorioInquilino();
            var inquilino = rp.getInquilino(id);
            return View(inquilino); 
    }    
}
