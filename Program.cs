using System;
using System.Collections.Generic;

namespace Disco

{
    class Program
    {
        static List<Int32> Aankomst = new List<Int32>();
        static List<Int32> vertrek = new List<Int32>();

        static List<int> aantalen = new List<int>();
        static Int64 maxBezoekers = 0;
        static int totaal = 0;

        static void Main()
        {
            int totaalBezoekers = int.Parse(Console.ReadLine());

            for (int i = 0; i < totaalBezoekers; i++)
            {
                string a = Console.ReadLine();
                string[] p = a.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (Int64.Parse(p[1]) != Int64.Parse(p[2]))
                {
                    Aankomst.Add(Int32.Parse(p[1]));
                    vertrek.Add(Int32.Parse(p[2]));
                }

            }

            Aankomst.Sort(delegate (Int32 x, Int32 y)
            {
                int a = x.CompareTo(y);

                return a;
            });

            vertrek.Sort(delegate (Int32 x, Int32 y)
            {
                int a = x.CompareTo(y);

                return a;
            });

            for (int i = Aankomst[0]; i < vertrek[vertrek.Count - 1]; i++)
            {
                int bij = 0; int af = 0;
                int begin = 0; int eind = Aankomst.Count;
                int countUp = 1000;

                if (Aankomst.Count >= 5000)
                {
                    while (Aankomst[begin] < i)
                    {
                        if (begin + countUp >= Aankomst.Count)
                            break;

                        if (Aankomst[begin + countUp] < i)
                            begin += countUp;
                        else
                            break;
                    }
                }

                for (int j = begin; j < eind; j++)
                {
                    if (Aankomst[j] == i)
                    {
                        bij++;
                    }

                    if (vertrek[j] == i)
                    {
                        af++;
                    }

                    if (Aankomst[j] > i && vertrek[j] > i)
                    {
                        break;
                    }
                }

                totaal += bij - af;

                aantalen.Add(totaal);

                if (totaal > maxBezoekers)
                {
                    maxBezoekers = totaal;
                }

            }

            Console.WriteLine(maxBezoekers);
        }
    }
}