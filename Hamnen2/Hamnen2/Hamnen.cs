using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hamnen2
{
    class Hamnen
    {


        public int AmountOfRowboats { get; set; }
        public int AmountOfMotorboats { get; set; }
        public int AmountOfSailboats { get; set; }
        public int AmountOfCargoboats { get; set; }




        public static int LedigaPlatser(Boat[] array)
        {
            int counter = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    counter++;
                }
            }

            return counter;
        }

        

       //public static int CountWeight(Boat[] array)
       // {
       //     int weight = 0;
       //     foreach (var item in array)
       //     {
       //         if (item != null)
       //         weight += item.Weight;
       //     }

       //     return weight;
       // }

        //public static double CountVelocity(Boat[] array)
        //{
        //    double speed = 0;
        //    int counter = 1;
        //    foreach (var item in array)
        //    {
        //        if (item != null)
        //        {
        //            speed += item.MaxSpeed;
        //            counter++;
        //        }
        //    }

        //    return Math.Round(speed / counter, 1);
        //}

        public static int? CountVelocity(Boat[] array)
        {
            double? q = array?.Select(a => a?.MaxSpeed)?.Average();

            return (int?)q;
        }

        public static int? CountWeight(Boat[] array)
        {
            int? q = array?.Select(a => a?.Weight)?.Sum();
            return q;
        }






    }


}

