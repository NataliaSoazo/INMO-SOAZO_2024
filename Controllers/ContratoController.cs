using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using INMO_SOAZO_2024.Models;
using ZstdSharp.Unsafe;
using System;
using System.Runtime.ConstrainedExecution;

namespace INMO_SOAZO_2024.Controllers;

public class ContratoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public ContratoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()

    {
        RepositorioContrato rp = new RepositorioContrato();
        var lista = rp.GetContratos();
        return View(lista);
    }
    public IActionResult Editar(int id)
    {
        RepositorioInmueble repoInmueble = new RepositorioInmueble();
        ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
        RepositorioInquilino repoInquilino = new RepositorioInquilino();
        ViewBag.Inquilinos = repoInquilino.GetInquilinos();

        if (id > 0)
        {
            RepositorioContrato rp = new RepositorioContrato();
            var contrato = rp.getContrato(id);
            return View(contrato);
        }
        else
        {
            return View();
        }
    }




    public ActionResult Guardar(Contrato contrato)
    {
        RepositorioContrato rp = new RepositorioContrato();
        if (contrato.Id > 0)
        {
            rp.ModificarContrato(contrato);

        }
        else
            rp.AltaContrato(contrato);
        return RedirectToAction(nameof(Index));

    }
    public IActionResult Eliminar(int id)
    {
        RepositorioContrato rp = new RepositorioContrato();
        rp.EliminarContrato(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Detalles( int id)
    {  
        RepositorioContrato rc = new RepositorioContrato();
            var c = rc.getContrato(id);
            return View(c); 
    }   
}
