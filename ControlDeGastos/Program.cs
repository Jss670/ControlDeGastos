//Conexion MongoDB
using Amazon.Util;
using MongoDB.Driver;
using System.Diagnostics.Contracts;


namespace ControlDeGastos
{
    class Program
    {
        static void Main(string[] args)
        {

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("ControlDeGastos");
            IMongoCollection<Gasto> collection = database.GetCollection<Gasto>("Gasto");

            double saldoInicial = 0;

            while (true)
            {
                Console.WriteLine("Seleccione una Opcion");
                Console.WriteLine("1. Ingresar Saldo");
                Console.WriteLine("2. Registrar Gasto");
                Console.WriteLine("3. Mostrar Movimientos");
                Console.WriteLine("4. Obtener Saldo Disponible");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        saldoInicial = LeerSado();
                        break;
                    case 2:
                        RegistrarPago(collection);
                        break;
                    
                   
                        



                }


            }


        }

        static  double LeerSado()
        {
            Console.WriteLine("Ingresar Saldo Inicial");
            string leerSaldo = Console.ReadLine();
            return double.Parse(leerSaldo);
            
        }
        static void RegistrarPago(IMongoCollection<Gasto> collection)
        {
            Gasto RegistrarGasto = new Gasto();
            Console.WriteLine("Ingrese Descripcion");
            RegistrarGasto.Descripcion = Console.ReadLine();

            Console.WriteLine("Ingresa Importe");
            RegistrarGasto.Importe = double.Parse (Console.ReadLine());

            Console.WriteLine("Ingrese Fecha (dd/mm/aaaa)");
            RegistrarGasto.Fecha =DateTime.Parse (Console.ReadLine());

            collection.InsertOne(RegistrarGasto);
        }

    }

}