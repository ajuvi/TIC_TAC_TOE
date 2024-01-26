using System;
using System.Threading;

namespace TIC_TAC_TOE
{

    /// <summary>
    /// https://www.c-sharpcorner.com/UploadFile/75a48f/tic-tac-toe-game-in-C-Sharp/
    /// </summary>
    internal class Program
    {
        const ConsoleColor COLOR_JUGADOR1 = ConsoleColor.Blue;
        const ConsoleColor COLOR_JUGADOR2 = ConsoleColor.Red;
        const ConsoleColor COLOR_BANNER = ConsoleColor.Green;
        static void Main(string[] args)
        {
            Random rand = new Random();

            char[] taulell = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int jugador;
            int pos;
            int ronda = 1;
            bool empat = false;

            //selecciona el primer jugador
            jugador = rand.Next(2) + 1;

            do
            {
                NetejarPantalla();

                //valora si ha finalitzat la ronda en empat
                if (TaulellPle(taulell))
                {
                    taulell = new char[]{ '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                    ronda++;
                    empat = true;
                } else empat = false;
                
                //mostra tota la informació del joc
                MostrarPantalla(ronda, jugador, taulell, empat);

                //espera un temps a demanar la nova posció
                EsperarSegons(0.5);

                //demana a l'usuari una posició
                pos = ObtenirPosicio(taulell);

                //assigna valor a la posció del taulell
                taulell[pos] = jugador == 2 ? 'O' : 'X';

                //canvia de jugador
                jugador = jugador % 2 + 1;
            }
            while (Guanyador(taulell) ==0);

            //Mostrar la pantalla final
            NetejarPantalla();
            MostrarPantalla(ronda, jugador, taulell);

            //mostrar el guanyador
            Console.WriteLine("El jugador {0} ha guanyat!", Guanyador(taulell));
            EsperarSegons(1);

            Console.ReadLine();
        }

        public static void EsperarSegons(double segons)
        {
            Thread.Sleep((int)(segons * 1000));
        }

        public static void MostrarPantalla(int ronda, int jugador, char[] taulell, bool empat=false)
        {
            MostrarBanner(empat);
            MostrarInfo(ronda, jugador);
            MostrarTaulell(taulell); 
        }
        public static void MostrarBanner(bool empat=false)
        {
            if (!empat)
                PintarLinia("*********TIC-TAC-TOE***********",COLOR_BANNER);
            else
                PintarLinia("************EMPAT**************", COLOR_BANNER);
        }

        private static void MostrarInfo(int ronda, int jugador)
        {

            Console.WriteLine($"RONDA {ronda} \nJugador1: X \nJugador2: O");
            PintarLinia($"\nTira el Jugador{jugador}\n",jugador==1?COLOR_JUGADOR1:COLOR_JUGADOR2);
        }

        private static void PintarLinia(String linia, ConsoleColor colorFons, ConsoleColor colorLletra=ConsoleColor.White)
        {
            Console.BackgroundColor = colorFons;
            Console.ForegroundColor = colorLletra;
            Console.WriteLine(linia);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PintarLiniaTauler(String linia)
        {
            for (int i = 0; i < linia.Length; i++)
            {
                if (linia[i] == 'X')
                {
                    Console.BackgroundColor = COLOR_JUGADOR1;
                    Console.Write(linia[i]);
                }
                else if (linia[i] == 'O')
                {
                    Console.BackgroundColor = COLOR_JUGADOR2;
                    Console.Write(linia[i]);
                }
                else
                {
                    Console.Write(linia[i]);
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }

        private static void MostrarTaulell(char[] arr)
        {
            Console.WriteLine("     |     |     ");
            PintarLiniaTauler(String.Format("  {0}  |  {1}  |  {2}  ", arr[0], arr[1], arr[2]));
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            PintarLiniaTauler(String.Format("  {0}  |  {1}  |  {2}  ", arr[3], arr[4], arr[5]));
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            PintarLiniaTauler(String.Format("  {0}  |  {1}  |  {2}  ", arr[6], arr[7], arr[8]));
            Console.WriteLine("     |     |     ");
        }

        public static void NetejarPantalla()
        {
            Console.Clear();
        }

        public static int ObtenirPosicio(char[] arr)
        {
            int pos;

            Console.Write("Entra el número de casella: ");

            //obtenir la posició a partir del número de casella
            try { pos = int.Parse(Console.ReadLine()) - 1; }
            catch (Exception e) { pos = -1; }

            while (!PosicioValida(arr, pos))
            {
                Console.WriteLine("Casella no vàlida!");
                Console.Write("Entra el número de casella: ");
                try { pos = int.Parse(Console.ReadLine()) - 1; }
                catch (Exception e) { pos = -1; }
            }

            return pos;
        }

        public static bool PosicioValida(char[] arr, int pos)
        {
            return pos >= 0 && pos < arr.Length && arr[pos]!='X' && arr[pos]!='O';
        }

        public static bool TaulellPle(char[] arr)
        {
            int i = 0;
            while (i<arr.Length && (arr[i]=='X' || arr[i]=='O')) i++;
            return i >= arr.Length;
        }

        //0 -> sense guanyador
        //1 -> guanya jugador1
        //2 -> guanya jugador2
        private static int Guanyador(char[] arr)
        {
            int jugador = 0;
            int i = 0;

            //files                       
            while (jugador==0 && i < 3)
            {
                if (arr[i * 3] == arr[i * 3 + 1] && arr[i * 3 + 1] == arr[i * 3 + 2] && (arr[i * 3] == 'X' || arr[i * 3] == 'O'))
                    jugador = arr[i * 3] == 'X' ? 1 : 2;
                i++;
            }


            //columnes
            i = 0;
            while(jugador==0 && i < 3)
            {
                if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6] && (arr[i] == 'X' || arr[i] == 'O'))
                    jugador = arr[i] == 'X' ? 1 : 2;
                i++;
            }


            //diagonals
            if (jugador == 0)
            {
                if (arr[0] == arr[4] && arr[4] == arr[8] && (arr[4] == 'X' || arr[4] == 'O') || 
                    arr[2] == arr[4] && arr[4] == arr[6] && (arr[4] == 'X' || arr[4] == 'O'))
                    jugador = arr[4] == 'X' ? 1 : 2;
            }

            return jugador;
        }
    }
}