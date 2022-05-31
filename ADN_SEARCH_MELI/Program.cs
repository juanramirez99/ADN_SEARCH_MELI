using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADN_SEARCH_MELI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] dna = {
                            { "1", "W", "W", "W", "X", "X"},
                            { "X", "2", "X", "X", "Y", "Y"},
                            { "H", "Y", "3", "Y", "X", "X"},
                            { "G", "H", "W", "4", "X", "X"},
                            { "X", "G", "H", "X", "5", "W"},
                            { "Y", "Y", "G", "H", "X", "6"}
                           };

            bool z = IsMutant(dna);
            //HorizontalRevision(x);
            DiagonalRevision(dna);
            Console.ReadLine();
        }

        public static bool VerificacionSecuencia(string cadena)
        {
            bool encontrado = false;
            switch (cadena)
            {
                case "AAAA":
                    encontrado = true;
                    Console.WriteLine("secuencia AAAA encontrada");
                    break;
                case "TTTT":
                    encontrado = true;
                    Console.WriteLine("secuencia TTTT encontrada");
                    break;
                case "CCCC":
                    encontrado = true;
                    Console.WriteLine("secuencia CCCC encontrada");
                    break;
                case "GGGG":
                    encontrado = true;
                    Console.WriteLine("secuencia GGGG encontrada");
                    break;
                default:
                    encontrado = false;
                    break;
            }
            return encontrado;
        }

        public static void HorizontalRevision(string[,] dna)
        {
            Console.WriteLine("Matrix de " + dna.GetLength(0) + "X" + dna.GetLength(1) + " Recibida");
            int contadorSecuencia = 0;

            for (int i = 0; i < dna.GetLength(0); i++)
            {
                for (int j = 0; j < dna.GetLength(0); j++)
                {
                    Console.WriteLine("verificando posicion-> " + "[" + i + "," + j + "]");
                    if (j <= dna.GetLength(0) - 4)
                    {
                        bool encontrado = VerificacionSecuencia(dna[i, j] + dna[i, (j + 1)] + dna[i, (j + 2)] + dna[i, (j + 3)]);
                        if (encontrado)
                        {
                            contadorSecuencia++;
                            j = j + 4;
                            Console.WriteLine("saltando a posicion en x-> " + "[" + i + "," + j + "]");
                        }
                    }
                }
            }
            Console.WriteLine("Revision Horizontal finalizada " + contadorSecuencia + " secuencias horizontales encontradas");
        }

        public static void DiagonalRevision(string[,] dna)
        {
            int longitud = dna.GetLength(0);
            Console.WriteLine("longitud -> " + longitud);
            int j = 2;
            for (int i = 0; i <= 3; i++)
            {
                    Console.WriteLine("Posicion [" + j + "," + i + "]-> " + dna[j, i]);
                if (j<longitud)
                {
                    j++;
                }
                
            }
        }

        // matrix(row,column)

        public static bool IsMutant(string[,] dna)
        {
            string sec1 = "AAAA";
            string sec2 = "";
            string sec3 = "";
            string sec4 = "";

            Console.WriteLine("----------------------------------------------------\n");
            int arraylen = dna.GetLength(0);
            for (int i = 0; i < arraylen; i++)
            {
                for (int j = 0; j < arraylen; j++)
                {
                    Console.Write(dna[i, j].ToString() + " ");
                    /*if (dna[i,j] == sec1[0].ToString()
                        || dna[i, j] == sec1[0].ToString()
                        || dna[i, j] == sec2[0].ToString()
                        || dna[i, j] == sec3[0].ToString())
                    {
                        Console.WriteLine("Empieza por " + dna[i, j]);
                        //vecinos()
                    }*/
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("----------------------------------------------------\n");
            return true;
        }
    }
}
