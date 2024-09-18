using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoDeCartas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inicializamos una carta
            Carta carta = new Carta(new Random().Next(1, 13), Carta.Palo.Copas);
            Console.ReadKey();

            // Mostramos la carta
            
        }
    }
}
