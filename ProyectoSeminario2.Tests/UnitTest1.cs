using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using Proyecto1Seminario2Grupo13; // Tu namespace principal

namespace ProyectoSeminario2.Tests
{
    public class ProductosServiceTests : IDisposable
    {
        private readonly string _rutaArchivo;

        public ProductosServiceTests()
        {
            // Antes de cada test, definimos dónde se creará el archivo temporario
            _rutaArchivo = Path.Combine(AppContext.BaseDirectory, "productos.txt");
            LimpiarArchivo();
        }

        // Test real para LeerProductos
        [Fact]
        public void LeerProductos_DeberiaRetornarProductos_CuandoElArchivoTieneDatos()
        {
            // 1. Arrange: Creamos un escenario real escribiendo texto en productos.txt
            File.WriteAllLines(_rutaArchivo, new string[] {
                "1;Teclado Mecanico;0",
                "2;Mouse Gamer;0"
            });

            // 2. Act: Llamamos a TU método real del servicio
            List<Producto> resultado = ProductosService.LeerProductos();

            // 3. Assert: Verificamos que leyó los 2 productos correctamente
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Equal("1", resultado[0].ID);
            Assert.Equal("Teclado Mecanico", resultado[0].Nombre);
        }

        // Test real para ObtenerNuevoIDProducto
        [Fact]
        public void ObtenerNuevoIDProducto_DeberiaRetornarUno_CuandoElArchivoNoExiste()
        {
            // 1. Arrange: Nos aseguramos de que no exista el archivo
            LimpiarArchivo();

            // 2. Act: Ejecutamos tu función
            int siguienteID = ProductosService.ObtenerNuevoIDProducto();

            // 3. Assert: Si está vacío, el ID inicial tiene que ser 1
            Assert.Equal(1, siguienteID);
        }

        // Limpieza para no dejar basura entre ejecuciones de tests
        public void Dispose()
        {
            LimpiarArchivo();
        }

        private void LimpiarArchivo()
        {
            if (File.Exists(_rutaArchivo))
            {
                File.Delete(_rutaArchivo);
            }
        }
    }
}
