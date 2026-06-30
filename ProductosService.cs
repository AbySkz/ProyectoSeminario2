using System;
using System.Collections.Generic;
using System.IO;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ProyectoSeminario2.Tests")]

namespace Proyecto1Seminario2Grupo13
{
    internal class ProductosService
    {
        private static string GetAppPath()
        {
            return AppContext.BaseDirectory;
        }

        public static void GuardarProducto(Producto unProducto)
        {
            string archivo = Path.Combine(GetAppPath(), "productos.txt");
            string datos = $"{unProducto.ID};{unProducto.Nombre};0"; // Inicialmente el stock es 0
            using (StreamWriter archivoSalida = new StreamWriter(archivo, true))
            {
                archivoSalida.WriteLine(datos);
            }
        }

        public static List<Producto> LeerProductos()
        {
            string archivo = Path.Combine(GetAppPath(), "productos.txt");
            List<Producto> productos = new List<Producto>();

            if (File.Exists(archivo))
            {
                string[] lineas = File.ReadAllLines(archivo);
                foreach (string productoComoTexto in lineas)
                {
                    var datos = productoComoTexto.Split(";");
                    Producto unProducto = new Producto(datos[0], datos[1]);
                    productos.Add(unProducto);
                }
            }

            return productos;
        }

        public static int ObtenerNuevoIDProducto()
        {
            string archivo = Path.Combine(GetAppPath(), "productos.txt");
            int ultimoID = 0;

            if (File.Exists(archivo))
            {
                string[] lineas = File.ReadAllLines(archivo);
                if (lineas.Length > 0)
                {
                    string ultimaLinea = lineas[lineas.Length - 1];
                    var datos = ultimaLinea.Split(";");
                    ultimoID = int.Parse(datos[0]);
                }
            }

            return ultimoID + 1;
        }
    }
}
