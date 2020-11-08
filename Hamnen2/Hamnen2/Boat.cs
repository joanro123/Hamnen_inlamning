using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Hamnen2
{
    class Boat
    {
        public string BoatType { get; set; }
        public string IdentityNumber { get; set; }
        public int Weight { get; set; }
        public double MaxSpeed { get; set; }
        public string UniqueProperty { get; set; }
        public int DagarIHamnen { get; set; }
        public double Tarplatser { get; set; }
        public bool Loop { get; set; } = true;
        public int Index { get; set; }
        public bool Shared { get; set; } = false;


        public static string GetIdentityNumber()
        {

            string[] ar = new string[3];
            Random ran = new Random();

            for (int i = 0; i < 3; i++)
            {
                int numm = ran.Next(0, 26);
                char let = (char)('a' + numm);
                string svaret = let.ToString();
                ar[i] = svaret.ToUpper();

            }
            string toReturn = null;
            foreach (string item in ar)
            {
                toReturn += item;
            }

            return toReturn.ToUpper();
        }

        public static int AddWeight(int minWeight, int maxWeight)
        {
            Random r = new Random();
            int number = r.Next(minWeight, maxWeight + 1);
            int finalWeight = number;
            return finalWeight;
        }

        public static double AddMaxSpeed(int minSpeed, int maxSpeed)
        {
            Random r = new Random();
            int randomNumber = r.Next(minSpeed, maxSpeed + 1);
            int finalMaxSpeed = randomNumber;
            double kmPerHour = finalMaxSpeed * 1.852;
            return Math.Round(kmPerHour);
        }

        public virtual string AddUniqueProperty()
        {

            string s = "";
            return s;
        }

    }

    class Rowboat : Boat
    {
        public int MaxPassenger { get; set; }

        string identityPrefix = "R-";
        int minimumWeight = 100;
        int maximumWeight = 300;
        int minimumSpeed = 1;
        int maximumSpeed = 3;


        public Rowboat()
        {
            BoatType = "Rowboat ";
            IdentityNumber = identityPrefix + GetIdentityNumber();
            UniqueProperty = AddUniqueProperty();
            Weight = AddWeight(minimumWeight, maximumWeight);
            MaxSpeed = AddMaxSpeed(minimumSpeed, maximumSpeed);
            DagarIHamnen = 1;
            Tarplatser = 1;
        }

        public override string AddUniqueProperty()
        {
            Random r = new Random();
            int randomnumber = r.Next(1, 6 + 1);
            string s = $"Max antal personer: {randomnumber}";
            return s;
        }
    }

    class Motorboat : Boat
    {
        public int HorsePower { get; set; }

        string identityPrefix = "M-";
        int minimumWeight = 200;
        int maximumWeight = 3000;
        int minimumSpeed = 0;
        int maximumSpeed = 60;


        public Motorboat()
        {
            BoatType = "Motorboat";
            IdentityNumber = identityPrefix + GetIdentityNumber();
            UniqueProperty = AddUniqueProperty();
            Weight = AddWeight(minimumWeight, maximumWeight);
            MaxSpeed = AddMaxSpeed(minimumSpeed, maximumSpeed);
            DagarIHamnen = 3;
            Tarplatser = 1.0;
        }

        public override string AddUniqueProperty()
        {
            Random r = new Random();
            int randomnumber = r.Next(10, 1000 + 1);
            string s = $"Antal hästkrafter: {randomnumber}";
            return s;
        }

    }

    class Sailboat : Boat
    {
        public int BoatLength { get; set; }

        string identityPrefix = "S-";
        int minimumWeight = 800;
        int maximumWeight = 6000;
        int minimumSpeed = 1;
        int maximumSpeed = 12;


        public Sailboat()
        {
            BoatType = "Sailboat";
            IdentityNumber = identityPrefix + GetIdentityNumber();
            UniqueProperty = AddUniqueProperty();
            Weight = AddWeight(minimumWeight, maximumWeight);
            MaxSpeed = AddMaxSpeed(minimumSpeed, maximumSpeed);
            DagarIHamnen = 4;
            Tarplatser = 2.0;
        }

        public override string AddUniqueProperty()
        {
            Random r = new Random();
            int randomnumber = r.Next(10, 60 + 1);
            string s = $"Båtlängd: {randomnumber} m";
            return s;
        }
    }

    class CargoShip : Boat
    {
        public int Cargo { get; set; }

        string identityPrefix = "L-";
        int minimumWeight = 3000;
        int maximumWeight = 20000;
        int minimumSpeed = 1;
        int maximumSpeed = 20;

        public CargoShip()
        {
            BoatType = "CargoShip";
            IdentityNumber = identityPrefix + GetIdentityNumber();
            UniqueProperty = AddUniqueProperty();
            Weight = AddWeight(minimumWeight, maximumWeight);
            MaxSpeed = AddMaxSpeed(minimumSpeed, maximumSpeed);
            DagarIHamnen = 6;
            Tarplatser = 4.0;
        }

        public override string AddUniqueProperty()
        {
            Random r = new Random();
            int randomnumber = r.Next(500 + 1);
            string s = $"Antal containers på fartyget: {randomnumber}";
            return s;
        }

    }
    
    
}

