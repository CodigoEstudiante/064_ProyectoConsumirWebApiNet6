using COSUMIR_API_TEST.Models;

namespace COSUMIR_API_TEST.Servicios
{
    public interface IServicio_API
    {
        Task<List<Producto>> Lista();
        Task<Producto> Obtener(int idProducto);

        Task<bool> Guardar(Producto objeto);

        Task<bool> Editar(Producto objeto);

        Task<bool> Eliminar(int idProducto);
    }
}
