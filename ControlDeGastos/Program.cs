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
                    case 3: MostrarMovimientos(collection);
                        break;
                    case 4: ObtenerSaldo(collection, saldoInicial);
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
            RegistrarGasto.Fecha = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            collection.InsertOne(RegistrarGasto);
        }

        static void MostrarMovimientos(IMongoCollection<Gasto> collection)
        {
            Console.WriteLine("Los Movimientos Guardados son: ");

            var filter = Builders<Gasto>.Filter.Empty;
            List<Gasto> gastos = collection.Find(filter).ToList();

            foreach (var gasto in gastos)
            {
                Console.WriteLine(" Importe: " + gasto.Importe + " Descripcion:  " + gasto.Descripcion+ " Fecha: "+ gasto.Fecha.ToShortTimeString());
            }
            
        }

        static void ObtenerSaldo(IMongoCollection<Gasto> collection, double saldoInicial)
        {
            Console.WriteLine(" El Saldo en tu cuenta es $ ");
            var filter = Builders<Gasto>.Filter.Empty;
            List<Gasto> gastos = collection.Find(filter).ToList();
            double GastoTotal = 0;


            foreach ( var gasto in gastos)
            {
                GastoTotal = GastoTotal + gasto.Importe;


            }

            Console.WriteLine(saldoInicial - GastoTotal);



        }

    }

}