using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMO_SOAZO_2024.Models;
using ZstdSharp.Unsafe;

namespace INMO_SOAZO_2024.Controllers;

public class InmuebleController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public InmuebleController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()

    { 
        RepositorioInmueble rp = new RepositorioInmueble();
        var lista = rp.ObtenerTodos();
        return View(lista);
    }
    public IActionResult Editar(int id)
         
    {    RepositorioPropietario repoPropietario = new RepositorioPropietario();
        ViewBag.Propietarios = repoPropietario.GetPropietarios();

        if (id > 0){
            RepositorioInmueble rp = new RepositorioInmueble();
            var inmueble = rp.getInmueble(id);
            return View(inmueble); 
        } else {
            return View();
        }
    }
    
    public IActionResult Guardar( Inmueble inmueble)
    {  
       RepositorioInmueble rp = new RepositorioInmueble();
        if(inmueble.Id  > 0){
            rp.ModificarInmueble(inmueble);

        }else
        rp.AltaInmueble(inmueble);
        return RedirectToAction(nameof(Index));
    }
     public IActionResult Eliminar( int id)
    {  RepositorioInmueble rp = new RepositorioInmueble();
        rp.EliminarInmueble(id);
        return RedirectToAction(nameof(Index));
    }    

    public IActionResult Detalles( int id)
    {  
        RepositorioInmueble rp = new RepositorioInmueble();
            var i = rp.getInmueble(id);
            return View(i); 
    }   
}
