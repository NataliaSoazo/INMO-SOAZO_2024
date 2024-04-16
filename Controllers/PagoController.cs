using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMO_SOAZO_2024.Models;
using ZstdSharp.Unsafe;

namespace INMO_SOAZO_2024.Controllers;

public class PagoController : Controller
{   RepositorioPago rp = new RepositorioPago();
    private readonly ILogger<HomeController> _logger;

    public PagoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()

    { 
        var lista = rp.GetAll();
        return View(lista);
    }
    public IActionResult Editar(int id)
         
    {    RepositorioContrato repo = new RepositorioContrato();
         ViewBag.Contratos = repo.GetContratos();
         
        if (id > 0){
           
            var pago = rp.getPago(id);
            return View(pago); 
        } else {
            return View();
        }
    }
    
    public IActionResult Guardar( Pago pago)
    {  
        if(pago.Id  > 0){
            rp.ModificarPago(pago);

        }else
        rp.AltaDePago(pago);
        return RedirectToAction(nameof(Index));
    }
     public IActionResult Eliminar( int id)
    {  
        rp.EliminarPago(id);
        TempData["Mensaje"] = "Eliminación realizada correctamente";
        return RedirectToAction(nameof(Index));
    }    

    public IActionResult Detalles( int id)
    {  
            var i = rp.getPago(id);
            return View(i); 
             try
            {
                rp.EliminarPago(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(Index);
            }
    }   
}
