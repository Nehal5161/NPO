using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2
{
    abstract class Enemy
    {

        // Defining variables which will be referenced by the properties
        protected int _health;
        protected string _type;

        // Defining the properties
        // health property
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        // type property
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        // Defining our speak and attack methods
        // marking it as abstract so that whoever inherits from our Enemy class must provide an implementation of speak and attack
        public abstract void Speak();
        public abstract void Attack(Player p);

    }

    // Creating 2 enemies
    // Note how they implemented the abstract methods defined in the Enemy class
    class Paladin : Enemy
    {
        


        public override void Speak()
        {
            Console.WriteLine("Player try to kill Enemy Paladin ");
            Console.WriteLine("");
        }
        public override void Attack(Player p)
        {
            //do something
            if (p.Health > 0)
                p.Health -= 10;
            else
                p.Health = 0;

        }
    }

    class Wizard : Enemy
    {
        public override void Speak()
        {
            // do something 
            Console.WriteLine("Player try to kill Enemy Wizard ");
            Console.WriteLine("");
        }
        public override void Attack(Player p)
        {
            //do something
            if (p.Health > 0)
                p.Health -= 10;
            else
                p.Health = 0;
        }
    }

    // Defining a player class per the homework
    class Player
    {
        protected int _health;

        // Defining the properties
        // health property
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public void Attack(Enemy e)
        {
            if (e.Health > 0)
                e.Health -= 15;
            else
                e.Health = 0;  
        }

        static void Main(string[] args)
        {
            Random r = new Random();

            const int SIZE = 2;
            Player p1 = new Player();
            p1.Health = r.Next(75,100);
            Enemy[] kill = new Enemy[SIZE]; 

            kill[0] = new Paladin();
            kill[0].Health = r.Next(75,100);

            kill[1] = new Wizard();
            kill[1].Health = r.Next(75,100);

            if (kill[0].Health > 0)
            {
                kill[0].Speak();
                p1.Attack(kill[0]); 
                kill[0].Attack(p1);
                Console.WriteLine("Player Health: " + p1.Health);
                Console.WriteLine("Enemy Health: " + kill[0].Health);
                Console.WriteLine("");

                if (p1.Health > 0 && (p1.Health > kill[0].Health))
                {
                    Console.WriteLine("You win");
                }
                else
                {
                    Console.WriteLine("You Loss");
                }
            }
            else
            {
                Console.WriteLine("Player health is weaker than enemy");
            }


            if (kill[1].Health > 0)
            {
                kill[1].Speak();
                p1.Attack(kill[1]);
                kill[1].Attack(p1);
                Console.WriteLine("Player Health:" + p1.Health);
                Console.WriteLine("Enemy Health: " + kill[1].Health);
                Console.WriteLine("");

                if (p1.Health > 0 && (p1.Health > kill[1].Health))
                {
                    Console.WriteLine("You win");
                }
                else
                {
                    Console.WriteLine("You Loss");
                }
            }
            else
            {
                Console.WriteLine("Player health is weaker than enemy");
            }
        }
    }
}
