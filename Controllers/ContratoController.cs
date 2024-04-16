using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMO_SOAZO_2024.Models;
using ZstdSharp.Unsafe;
using System;
using System.Runtime.ConstrainedExecution;
using System.Linq.Expressions;

namespace INMO_SOAZO_2024.Controllers;

public class ContratoController : Controller
{
    RepositorioContrato rc = new RepositorioContrato();
     private readonly ILogger<HomeController> _logger;

    public ContratoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()

    {    var lista = rc.GetContratos();
        return View(lista);
    }

    public IActionResult Editar(int id)
    {
        try
        {
            RepositorioInmueble repoInmueble = new RepositorioInmueble();
            ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
            RepositorioInquilino repoInquilino = new RepositorioInquilino();
            ViewBag.Inquilinos = repoInquilino.GetInquilinos();

            if (id > 0)
            {
                var contrato = rc.getContrato(id);
                return View(contrato);
            }
            else
            {
                return View();
            }
        }
        catch (Exception ex)
        {
            TempData["Mensaje"] = "No se pudo  realizar la acción. ";
            return View();
        }
    }




    public ActionResult Guardar(Contrato contrato)
    {   
        
        Boolean validado = rc.validarContrato(contrato);
        
            if (validado == true)
            {

                if (contrato.Id > 0)
                {
                    rc.ModificarContrato(contrato);
                    return RedirectToAction(nameof(Index));

                }
                else
                    rc.AltaContrato(contrato);
                    return RedirectToAction(nameof(Index));
                 
            }else  ViewBag.Error = "El contrato debe tener una duración mínima de dos años.";
                return View("Error");
    

    }



public IActionResult Eliminar(int id)
    {   try{
        rc.EliminarContrato(id);
        return RedirectToAction(nameof(Index));
    }  catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }

    }

    public IActionResult Detalles( int id)
    {   try{
            var c = rc.getContrato(id);
            return View(c); 
    }  catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }
    }   
}
