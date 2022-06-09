using System.Reflection;

namespace ConsoleApp2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Hero hero = new Hero();

            List<Monstre> monstres = new List<Monstre>() { new Orc(), new Loup(), new Dragonnet() };

            foreach (Monstre monstre in monstres)
                hero.Loot(monstre);

        }
    }

    class Hero
    {
        public void Loot(Monstre monstre)
        {
            if(monstre == null)
                return;

            Type monstreType = monstre.GetType();

            IEnumerable<LootAttribute> lootAttributes = monstreType.GetCustomAttributes<LootAttribute>(true);

            foreach (LootAttribute item in lootAttributes)
            {
                Console.WriteLine($"Je ramasse de(s) '{item.Nom}' : {item.Quantite}");
            }
        }
    }


    class Monstre
    {

    }

    [Cuir]
    class Loup : Monstre
    {

    }

    [Or]
    class Orc : Monstre
    {

    }
    
    [Cuir("Ecaille")]
    [Or(1000)]
    [Gemme(Nom = "Topaze")]
    class Dragonnet : Monstre
    {

    }

    abstract class LootAttribute : Attribute
    {
        private int _maxValue;
        protected Random Random { get; init; }


        protected LootAttribute(string nom, int maxValue)
        {
            Random = new Random();
            Nom = nom;
            _maxValue = maxValue;
        }

        public virtual int Quantite { get { return Random.Next(_maxValue) + 1; } }
        public string Nom { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    class OrAttribute : LootAttribute
    {
        public OrAttribute() : base ("Or", 6)
        {

        }

        public OrAttribute(int maxValue) : base("Or", maxValue)
        {

        }
    }

    class CuirAttribute : LootAttribute
    {
        public CuirAttribute() : base("Cuir", 4)
        {

        }
        public CuirAttribute(string nom) : base(nom, 4)
        {

        }

    }

    class GemmeAttribute : LootAttribute
    {
        public GemmeAttribute() : base("Gemme", 10)
        {

        }
    }
}