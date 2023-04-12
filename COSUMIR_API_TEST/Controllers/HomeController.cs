using COSUMIR_API_TEST.Models;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Text;


using Newtonsoft.Json;

using COSUMIR_API_TEST.Servicios;

namespace COSUMIR_API_TEST.Controllers
{
    public class HomeController : Controller
    {
        private IServicio_API _servicioApi;
        
        public   HomeController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> lista =  await _servicioApi.Lista();
            return View(lista);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> Producto(int idProducto) {

            Producto modelo_producto = new Producto();

            ViewBag.Accion = "Nuevo Producto";

            if (idProducto != 0) {

                ViewBag.Accion = "Editar Producto";
                modelo_producto = await _servicioApi.Obtener(idProducto);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Producto ob_producto) {

            bool respuesta;

            if (ob_producto.IdProducto == 0)
            {
                respuesta = await _servicioApi.Guardar(ob_producto);
            }
            else {
                respuesta = await _servicioApi.Editar(ob_producto);
            }


            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int idProducto) {

            var respuesta = await _servicioApi.Eliminar(idProducto);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}