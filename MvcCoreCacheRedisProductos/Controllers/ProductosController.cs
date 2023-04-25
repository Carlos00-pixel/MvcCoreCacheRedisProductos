using Microsoft.AspNetCore.Mvc;
using MvcCoreCacheRedisProductos.Models;
using MvcCoreCacheRedisProductos.Repositories;
using MvcCoreCacheRedisProductos.Services;

namespace MvcCoreCacheRedisProductos.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;
        private ServiceCacheRedis service;

        public ProductosController(RepositoryProductos repo, ServiceCacheRedis service)
        {
            this.repo = repo;
            this.service = service;
        }

        public IActionResult Favoritos()
        {
            List<Producto> productosFavoritos =
                this.service.GetProductosFavoritos();
            return View(productosFavoritos);
        }

        public IActionResult SeleccionarFavorito(int idproducto)
        {
            Producto producto =
                this.repo.FindProducto(idproducto);
            this.service.AddProductoFavorito(producto);
            return RedirectToAction("Details", new { id = idproducto });
        }

        public IActionResult DeleteFavorito(int idproducto)
        {
            this.service.DeleteProductoFavorito(idproducto);
            return RedirectToAction("Favoritos");
        }

        public IActionResult Index()
        {
            List<Producto> productos = this.repo.GetProductos();
            return View(productos);
        }

        public IActionResult Details(int id)
        {
            Producto producto = this.repo.FindProducto(id);
            return View(producto);
        }
    }
}
