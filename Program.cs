using System;
using System.Collections.Generic;

namespace Disco

{
    class Program
    {
        static List<bezoeker> bezoekers = new List<bezoeker>();
        static List<List<bezoeker>> tussenlist = new List<List<bezoeker>>();
        static List<numberzz> tussenlist2 = new List<numberzz>();
        static Int64 maxBezoekers = 0;

        static void Main()
        {
            Int64 totaalBezoekers = Int64.Parse(Console.ReadLine());

            for (Int64 i = 0; i < totaalBezoekers; i++)
            {
                bezoekers.Add(new bezoeker(Console.ReadLine()));
            }

            bezoekers.Sort(delegate (bezoeker x, bezoeker y)
            {
                int a = x.aangekomen.CompareTo(y.aangekomen);

                if (a == 0)
                    a = x.vertrek.CompareTo(y.vertrek);

                return a;
            });

            counting();
            counting4();

            Console.WriteLine(maxBezoekers);

            foreach (numberzz p in tussenlist2)
            {
                Console.WriteLine("Van " + p.max + " tot " + p.min);
            }
        }

        static void counting()
        {
            for (int b = 0; b < bezoekers.Count; b++)
            {
                List<Int64> listNumbers = new List<Int64>();
                listNumbers.Add(b);

                for (int i = 0; i < bezoekers.Count; i++)
                {
                    if (i > b)
                    {
                        if (bezoekers[i].aangekomen >= bezoekers[b].aangekomen && bezoekers[i].aangekomen <= bezoekers[b].vertrek && bezoekers[i].aangekomen != bezoekers[i].vertrek)
                        {
                            listNumbers.Add(i);
                        }
                    }
                    else if (i < b)
                    {
                        if (bezoekers[i].vertrek > bezoekers[b].aangekomen)
                        {
                            listNumbers.Add(i);
                        }
                    }
                }

                Console.WriteLine("-------A");
                for(int m = 0; m < listNumbers.Count; m++)
                {
                 Console.WriteLine(bezoekers[(int)listNumbers[m]].aangekomen + " " + bezoekers[(int)listNumbers[m]].vertrek);
                 }

                counting2(listNumbers);
            }

        }

        static void counting2(List<Int64> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                List<Int64> Numbers = new List<Int64>();
                Numbers.Add(l[0]);
                bool forAll = false;

                for (int j = 1 + i; j < l.Count; j++)
                {
                    foreach (Int64 n in Numbers)
                    {
                        if (l[j] < n)
                        {
                            if (bezoekers[(int)l[j]].vertrek > bezoekers[(int)n].aangekomen)
                            {
                                forAll = true;
                            }
                            else
                            {
                                forAll = false;
                                break;
                            }
                        }
                        else if (l[j] > n)
                        {
                            if (bezoekers[(int)l[j]].aangekomen > bezoekers[(int)n].aangekomen && bezoekers[(int)l[j]].aangekomen <= bezoekers[(int)n].vertrek)
                            {
                                forAll = true;
                            }
                            else
                            {
                                forAll = false;
                                break;
                            }
                        }
                    }

                    if (forAll)
                    {
                        Numbers.Add(l[j]);
                    }
                    else
                    {
                        break;
                    }

                    counting3(Numbers);

                     Console.WriteLine("-------B");
                    for(int m = 0; m < Numbers.Count; m++)
                     {
                    Console.WriteLine(bezoekers[(int)Numbers[m]].aangekomen + " " + bezoekers[(int)Numbers[m]].vertrek);
                    }
                }
            }
        }

        static void counting3(List<Int64> l)
        {
            List<bezoeker> m = new List<bezoeker>();

            l.Sort(delegate (Int64 x, Int64 y)
            {
                int a = x.CompareTo(y);

                if (a == 0)
                    a = x.CompareTo(y);

                return a;
            });

            for (int n = 0; n < l.Count; n++)
            {
                Int64 min = bezoekers[(int)l[n]].aangekomen;
                Int64 max = bezoekers[(int)l[n]].vertrek;

                for (int i = 1; i < l.Count; i++)
                {
                    if (bezoekers[(int)l[i]].aangekomen == bezoekers[(int)l[n]].vertrek && bezoekers[(int)l[i]].vertrek >= max)
                    {
                        max = bezoekers[(int)l[i]].vertrek;
                        l.Remove(i);
                    }
                }

                m.Add(new bezoeker("Anna" + " " + min + " " + max));
            }

            if (m.Count >= maxBezoekers)
            {
                maxBezoekers = m.Count;
                tussenlist.Add(m);
            }

             Console.WriteLine("-------C");
            for (int p = 0; p < m.Count; p++)
            {
              Console.WriteLine(m[p].aangekomen + " " + m[p].vertrek);
            }
        }

        static void counting4()
        {
            foreach (List<bezoeker> l in tussenlist)
            {
                Int64 min = 31622402; Int64 max = 0;

                foreach (bezoeker b in l)
                {
                    if (b.aangekomen > max)
                        max = b.aangekomen;

                    if (b.vertrek < min)
                        min = b.vertrek;
                }

                if (l.Count >= maxBezoekers)
                    tussenlist2.Add(new numberzz(min, max));
            }

            enkelvoud();

              foreach(numberzz p in tussenlist2)
             {
               Console.WriteLine("------D");
             Console.WriteLine(p.max + " " + p.min);
            }

            for (int c = 0; c < tussenlist2.Count; c++)
            {
                for (int v = 1; v < tussenlist2.Count; v++)
                {
                    if (tussenlist2[c].max == tussenlist2[v].min)
                    {
                        tussenlist2.Remove(tussenlist2[c]);
                        tussenlist2.Remove(tussenlist2[v]);
                    }
                }
            }

            //  foreach(numberzz p in tussenlist2)
            //{
            //Console.WriteLine("------");
            //Console.WriteLine(p.max + " " + p.min);
            //}

        }

        static void enkelvoud()
        {
            for (int z = 0; z < tussenlist2.Count; z++)
            {
                for (int x = 1; x < tussenlist2.Count; x++)
                {
                    if (tussenlist2[z].min == tussenlist2[x].min && tussenlist2[z].max == tussenlist2[x].max)
                    {
                        tussenlist2.Remove(tussenlist2[x]);
                    }
                }
            }
        }
    }



    class bezoeker
    {
        public string naam = "Anna";
        public Int64 aangekomen;
        public Int64 vertrek;

        public bezoeker(string info)
        {
            string[] p = info.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            this.aangekomen = Int64.Parse(p[1]);
            this.vertrek = Int64.Parse(p[2]);
        }
    }

    class numberzz
    {
        public Int64 min;
        public Int64 max;

        public numberzz(Int64 a, Int64 b)
        {
            this.min = a;
            this.max = b;
        }
    }
}
