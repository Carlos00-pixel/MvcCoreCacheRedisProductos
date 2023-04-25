using MvcCoreCacheRedisProductos.Helpers;
using MvcCoreCacheRedisProductos.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MvcCoreCacheRedisProductos.Services
{
    public class ServiceCacheRedis
    {
        private IDatabase database;

        public ServiceCacheRedis()
        {
            this.database =
            HelperCacheMultiplexer.GetConnection.GetDatabase();
        }

        public void AddProductoFavorito(Producto producto)
        {
            string jsonProductos = this.database.StringGet("favoritos");
            List<Producto> productoslist;
            if (jsonProductos == null)
            {
                productoslist = new List<Producto>();
            }
            else
            {
                productoslist =
                JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }
            productoslist.Add(producto);
            jsonProductos =
            JsonConvert.SerializeObject(productoslist);
            this.database.StringSet("favoritos", jsonProductos);
        }

        public List<Producto> GetProductosFavoritos()
        {
            string jsonProductos = this.database.StringGet("favoritos");
            if(jsonProductos == null)
            {
                return null;
            }
            else
            {
                List<Producto> favoritos =
                    JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                return favoritos;
            }
        }

        public void DeleteProductoFavorito(int idproducto)
        {
            List<Producto> favoritos = this.GetProductosFavoritos();
            if(favoritos != null)
            {
                Producto productoDelete =
                    favoritos.FirstOrDefault(z => z.IdProducto == idproducto);
                favoritos.Remove(productoDelete);

                if(favoritos.Count == 0)
                {
                    this.database.KeyDelete("favoritos");
                }
                else
                {
                    string jsonProductos =
                        JsonConvert.SerializeObject(favoritos);
                    this.database.StringSet("favoritos", jsonProductos, TimeSpan.FromMinutes(30));
                }
            }
        }
    }
}
