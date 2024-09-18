using System;
using System.Collections.Generic;

namespace JuegoDeCartas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al Juego de Cartas!");
            Console.WriteLine();

            int numJugadores = SeleccionarNumJugadores();

            Baraja baraja = new Baraja();

            Console.WriteLine("El juego ha comenzado con " + numJugadores + " jugadores.");
            Console.WriteLine();

            List<List<Carta>> manosJugadores = RepartirCartas(numJugadores, baraja);

            IniciarBatalla(manosJugadores);
        }

        /// <summary>
        /// Selección de Nº de jugadores en la partida
        /// </summary>
        /// <returns>numJugadores</returns>
        private static int SeleccionarNumJugadores()
        {
            int numJugadores = 0;

            while (numJugadores < 2 || numJugadores > 5)
            {
                Console.WriteLine("Selecciona el número de jugadores (2-5)");
                string input = Console.ReadLine();
                if (int.TryParse(input, out numJugadores) && numJugadores >= 2 && numJugadores <= 5)
                    return numJugadores;
                else
                    Console.WriteLine("Por favor, introduce un número válido de jugadores.");
            }
            return numJugadores;
        }

        /// <summary>
        /// Distribución de cartas entre los jugadores
        /// </summary>
        /// <param name="numJugadores"></param>
        /// <param name="baraja"></param>
        /// <returns>Cartas</returns>
        private static List<List<Carta>> RepartirCartas(int numJugadores, Baraja baraja)
        {
            int numCartaPorJug = 48 / numJugadores;
            List<List<Carta>> manosJugadores = new List<List<Carta>>();

            for (int i = 0; i < numJugadores; i++)
            {
                List<Carta> mano = new List<Carta>();
                for (int j = 0; j < numCartaPorJug; j++)
                {
                    Carta carta = baraja.RobarCarta();
                    mano.Add(carta);
                }
                manosJugadores.Add(mano);
            }

            int cartasRestantes = 48 % numJugadores;
            if (cartasRestantes > 0)
                Console.WriteLine($"Han sobrado {cartasRestantes} cartas en la baraja.");

            return manosJugadores;
        }

        /// <summary>
        /// Mecanismo de juego
        /// 
        /// Controlamos las rondas de 
        /// cada jugador y determinamos 
        /// si hay un ganador en cada ronda.
        /// 
        /// Llamamos al método MostrarPuntajesFinales
        /// para saber el resultado final.
        /// </summary>
        /// <param name="manosJugadores"></param>
        private static void IniciarBatalla(List<List<Carta>> manosJugadores)
        {
            bool juegoTerminado = false;
            int[] score = new int[manosJugadores.Count];

            while (!juegoTerminado)
            {
                List<Carta> cartasJugadas = new List<Carta>();
                Console.WriteLine("Ronda actual:");

                for (int i = 0; i < manosJugadores.Count; i++)
                {
                    List<Carta> manoActual = manosJugadores[i];
                    if (manoActual.Count == 0)
                        continue;

                    Carta cartaJugada = SeleccionarCarta(manoActual, i);
                    cartasJugadas.Add(cartaJugada);
                }

                int jugadorGanador = DeterminarGanador(cartasJugadas);
                score[jugadorGanador]++;
                Console.WriteLine($"La carta ganadora es: {cartasJugadas[jugadorGanador]} del Jugador {jugadorGanador + 1}");

                juegoTerminado = VerificarJuegoTerminado(manosJugadores);
            }

            MostrarPuntajesFinales(score);
        }

        /// <summary>
        /// Cada jugador tiene sus propias cartas
        /// </summary>
        /// <param name="manoActual"></param>
        /// <param name="jugador"></param>
        /// <returns>cartaJugada</returns>
        private static Carta SeleccionarCarta(List<Carta> manoActual, int jugador)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"Jugador {jugador + 1}, selecciona una carta para jugar (1-{manoActual.Count}):");
                    for (int j = 0; j < manoActual.Count; j++)
                        Console.WriteLine($"{j + 1}: {manoActual[j]}");

                    int seleccion = int.Parse(Console.ReadLine()) - 1;
                    if (seleccion < 0 || seleccion >= manoActual.Count)
                        throw new ArgumentOutOfRangeException();

                    Carta cartaJugada = manoActual[seleccion];
                    manoActual.RemoveAt(seleccion);
                    Console.WriteLine($"Jugador {jugador + 1} ha jugado: {cartaJugada}");
                    return cartaJugada;
                }
                catch (Exception)
                {
                    Console.WriteLine("Selección inválida. Por favor, intenta de nuevo.");
                }
            }
        }

        /// <summary>
        /// Quien tenga la carta más
        /// alta gana.
        /// </summary>
        /// <param name="cartasJugadas"></param>
        /// <returns>jugadorGanador</returns>
        private static int DeterminarGanador(List<Carta> cartasJugadas)
        {
            Carta cartaGanadora = cartasJugadas[0];
            int jugadorGanador = 0;
            for (int i = 1; i < cartasJugadas.Count; i++)
            {
                if (cartasJugadas[i].CompareTo(cartaGanadora) > 0)
                {
                    cartaGanadora = cartasJugadas[i];
                    jugadorGanador = i;
                }
            }
            return jugadorGanador;
        }

        /// <summary>
        /// Comprobamos si el juego ha terminado
        /// </summary>
        /// <param name="manosJugadores"></param>
        /// <returns>True</returns>
        private static bool VerificarJuegoTerminado(List<List<Carta>> manosJugadores)
        {
            foreach (var mano in manosJugadores)
            {
                if (mano.Count > 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Mostramos los puntajes finales
        /// </summary>
        /// <param name="score"></param>
        private static void MostrarPuntajesFinales(int[] score)
        {
            Console.WriteLine("El juego ha terminado. Score final:");
            for (int i = 0; i < score.Length; i++)
                Console.WriteLine($"Jugador {i + 1}: {score[i]} puntos");

            Console.ReadLine();
        }
    }
}
