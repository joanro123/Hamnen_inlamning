using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hamnen2
{
    class Program
    {        

        static void Main(string[] args)
        {
            List<Boat> inkommandeBåtar = new List<Boat>();
            List<Boat> BåtarUtanPlats = new List<Boat>();
            Boat[] hamnArray = new Boat[32];
            Boat[] rowboatArray = new Boat[32];

            Hamnen port = new Hamnen();
            int dag = 0;
            int platsnummer = 1;

            while (true)
            {


                

                Statistik(hamnArray, BåtarUtanPlats, port);

                MainWindow(hamnArray, rowboatArray, platsnummer, dag);

                DagariHamnenMetod(hamnArray, port, rowboatArray);

                BåtarDagligenMetod(5, inkommandeBåtar);

                Hamnplatser(hamnArray, rowboatArray, inkommandeBåtar, BåtarUtanPlats, port);



                dag++;
                Console.WriteLine();
                Console.WriteLine("Nästa dag, klicka enter");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    Console.Clear();
            }

        }

        private static void MainWindow(Boat[]array, Boat[]rowb, int plats, int dag)
        {
            plats = 1;

            if (dag == 0)
            {
                Console.WriteLine("Välkommen till hamnen!");
                Console.WriteLine();
            }
            Console.WriteLine("Lediga platser för tillfället: " + Hamnen.LedigaPlatser(array));

            Console.WriteLine($"Dag nummer {dag}\n");

            Console.WriteLine("Plats\tBåttyp\t\tNr\tVikt\tMaxhast\t\tÖvrigt");


            Boat[] test = array.Distinct().ToArray();
            

            using (StreamWriter sw = new StreamWriter("Memory.txt", true))
            {
                foreach (var item in array)
                {


                    if (item != null)
                    {
                        if (item.Tarplatser > 1)
                        {
                            Console.WriteLine($"{item.Index + 2 - item.Tarplatser}-{item.Index + 1}.\t{item.BoatType}\t{item.IdentityNumber}\t{item.Weight}\t{item.MaxSpeed} km/h\t\t{item.UniqueProperty}");
                            plats++;
                            sw.WriteLine($"{item.Index + 2 - item.Tarplatser}-{item.Index + 1}.\t{item.BoatType}\t{item.IdentityNumber}\t{item.Weight}\t{item.MaxSpeed} km/h\t\t{item.UniqueProperty}");

                        }
                        else
                        {
                            Console.WriteLine($"{item.Index + 1}.\t{item.BoatType}\t{item.IdentityNumber}\t{item.Weight}\t{item.MaxSpeed} km/h\t\t{item.UniqueProperty}");
                            sw.WriteLine($"{item.Index + 1}.\t{item.BoatType}\t{item.IdentityNumber}\t{item.Weight}\t{item.MaxSpeed} km/h\t\t{item.UniqueProperty}");
                            foreach (var row in rowb)
                            {
                                if (row != null)
                                {
                                    if (item.Index + 1 == row.Index + 1)
                                    {
                                        Console.WriteLine($"{row.Index + 1}.\t{row.BoatType}\t{row.IdentityNumber}\t{row.Weight}\t{row.MaxSpeed} km/h\t\t{row.UniqueProperty} ");
                                        sw.WriteLine($"{row.Index + 1}.\t{row.BoatType}\t{row.IdentityNumber}\t{row.Weight}\t{row.MaxSpeed} km/h\t\t{row.UniqueProperty} ");
                                    }
                                }
                            }
                            plats++;
                        }

                    }

                    else
                    {

                        Console.WriteLine(plats + ". Tomt");
                        plats++;
                    }

                }
                sw.Close();
            }
        }

        private static void Statistik(Boat[] array, List<Boat> list, Hamnen h)
        {
            
            Console.WriteLine($"Antal roddbåtar: {h.AmountOfRowboats}\nAntal motorbåtar: {h.AmountOfMotorboats}\nAntal segelbåtar: {h.AmountOfSailboats}\nAntal lastfartyg: {h.AmountOfCargoboats}");
            Console.WriteLine("Totalvikten är: " + Hamnen.CountWeight(array) + " kg");
            Console.WriteLine("Medelhastigheten är: " + Hamnen.CountVelocity(array) + " km/h");
            

            Console.WriteLine("Båtar som inte fick plats:");
            foreach (var item in list)
            {
                Console.WriteLine($"{item.BoatType} med id: {item.IdentityNumber}"); ;
            }
            Console.WriteLine();
        }

        private static void DagariHamnenMetod(Boat[] array, Hamnen h, Boat[] rowboat)
        {

            foreach (var item in array.ToList())
            {
                if (item != null)
                {
                    if (item.DagarIHamnen != 0)
                    {
                        item.DagarIHamnen--;

                    }

                    else
                    {
                        //  Console.WriteLine($"Den här båten lämnar hamnen: {it.IdentityNumber}");
                        //   it.Loop = true;

                        
                        if (item is Motorboat)
                        {
                            h.AmountOfMotorboats--;
                            array[item.Index] = null;
                        }
                        else if (item is Sailboat)
                        {
                            if (item.Loop)
                            {
                            h.AmountOfSailboats--;
                            array[item.Index] = null;
                            array[item.Index - 1] = null;
                            item.Loop = false;
                            }
                        }
                        else if (item is CargoShip)
                        {
                            if (item.Loop)
                            {
                            h.AmountOfCargoboats--;
                            array[item.Index] = null;
                            array[item.Index - 1] = null;
                            array[item.Index - 2] = null;
                            array[item.Index - 3] = null;
                            item.Loop = false;
                            }
                        }

                    }
                }
            }

            foreach (var item in array)
            {
                if (item != null)
                {

                    if (item is Rowboat)
                    {
                        if (item.DagarIHamnen == 0)
                        {
                            h.AmountOfRowboats--;
                            array[item.Index] = null;
                        }
                    }
                }
            }

            foreach (var row in rowboat.ToList())
            {
                if (row != null)
                {
                    
                        if (row is Rowboat)
                        {
                            row.DagarIHamnen--;
                            h.AmountOfRowboats--;
                            rowboat[row.Index] = null;

                        }
                    
                }
            }
        }

        private static void Hamnplatser(Boat[] hamnArray, Boat[] rowArray, List<Boat> inkommandeBåtar, List<Boat> båtarUtanPlats, Hamnen h)
        {
            for (int i = 0; i < hamnArray.Length; i++)
            {

                foreach (var item in inkommandeBåtar.ToList())
                {
                    if (hamnArray[i] == null)
                    {

                        if (item is Motorboat)
                        {
                            hamnArray[i] = item;
                            item.Index = i;
                            inkommandeBåtar.Remove(item);
                            h.AmountOfMotorboats++;
                            break;

                        }
                        else if (item is Rowboat)
                        {
                            for (int j = 0; j < hamnArray.Length; j++)
                            {
                                if (hamnArray[j] != null && hamnArray[j].BoatType == "Rowboat " && hamnArray[j].Shared == false)
                                {
                                    rowArray[j] = item;
                                    item.Index = j;
                                    inkommandeBåtar.Remove(item);
                                    h.AmountOfRowboats++;
                                    hamnArray[j].Shared = true;
                                    item.Shared = true;
                                    break;
                                }
                            }
                            if (item.Shared == false)
                            {
                                hamnArray[i] = item;
                                item.Index = i;
                                inkommandeBåtar.Remove(item);
                                h.AmountOfRowboats++;
                                break;
                            }

                        }
                        else if (item is Sailboat)
                        {
                            if (i < hamnArray.Length - item.Tarplatser)
                            {
                                if (hamnArray[i + 1] == null && i < hamnArray.Length - item.Tarplatser)
                                {
                                    hamnArray[i] = item;
                                    hamnArray[i + 1] = item;
                                    item.Index = i + 1;
                                    inkommandeBåtar.Remove(item);
                                    h.AmountOfSailboats++;
                                    break;
                                }
                            }
                        }
                        else if (item is CargoShip)
                        {
                            if (i < hamnArray.Length - item.Tarplatser)
                            {
                                if (hamnArray[i + 1] == null && hamnArray[i + 2] == null && hamnArray[i + 3] == null)
                                {
                                    hamnArray[i] = item;
                                    hamnArray[i + 1] = item;
                                    hamnArray[i + 2] = item;
                                    hamnArray[i + 3] = item;
                                    item.Index = i + 3;
                                    inkommandeBåtar.Remove(item);
                                    h.AmountOfCargoboats++;
                                    break;
                                }
                            }
                        }

                    }
                    
                    
                }

            }
            foreach (var item in inkommandeBåtar)
            {
                båtarUtanPlats.Add(item);
            }
        }

        private static void BåtarDagligenMetod(int båtarDagligen, List<Boat> boats)
        {
            Random rnd = new Random();
          //  List<Boat> boats = new List<Boat>();

            for (int i = 0; i < båtarDagligen; i++)
            {
                int randomNum = rnd.Next(1, 5);
                if (randomNum == 1)
                {
                    Rowboat rowboats = new Rowboat();
                    boats.Add(rowboats);
                }
                else if (randomNum == 2)
                {
                    Motorboat motorboats = new Motorboat();
                    boats.Add(motorboats);
                }
                else if (randomNum == 3)
                {
                    Sailboat sailboats = new Sailboat();
                    boats.Add(sailboats);
                }
                else if (randomNum == 4)
                {
                    CargoShip cargoships = new CargoShip();
                    boats.Add(cargoships);
                }
            }
            
        }
    }
    
}
