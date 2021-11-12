using System;
using System.Collections.Generic;

namespace DinnerDemo
{
    class DinerProgram
    {
        // Note something very important: the type of the `meal` parameter
        // is simply `Meal`! This allows us to pass in any class that extends
        // `Meal`.
        private static void Serve(Meal meal)
        {
            Console.WriteLine("");

            Customer customer = new Customer();
            customer.Eat(meal);
        }

        static void Main(string[] args)
        {
            List<Meal> menu = new List<Meal>();
            menu.Add(new CheesyMeal("Pastry"));
            menu.Add(new CheesySpicyMeal("Enchilada"));
            menu.Add(new SpicyMeal("Sichuan Noodles"));
            menu.Add(new SweetMeal("Ice-cream Sundae"));

            // Remember: if you're ever going to do something like this, make
            // sure there's something inside the loop that escapes it, or your
            // loop will run forever!
            while (true)
            {
                Console.WriteLine("Choose a menu item:");

                // While a "foreach" loop is a nicer way of iterating through a
                // List, if I use a "for" loop I get the index of each item as
                // well.
                for (int i = 0; i < menu.Count; ++i)
                {
                    Console.WriteLine("\t#{0} - {1}", i, menu[i].Name);
                }

                Console.Write("Enter choice (or \"exit\"): ");

                string input = Console.ReadLine();

                // Outside of "switch" statements, break immediately exits the
                // innermost loop we're in.
                if (input == "exit")
                    break;

                Console.WriteLine();

                // When exceptions occur they stop what the program was doing
                // _right at that moment_! This means later lines of code do
                // **not** run. (This is good because exceptions prevent our
                // program from entering into an invalid state.)
                //
                // "try/catch/finally" blocks are a way of intercepting these
                // exceptions and handling them, instead of letting the program
                // crash.
                try
                {
                    int choice = Convert.ToInt32(input);

                    // There's no real need to catch the potential
                    // "ArgumentOutOfRangeException" that might occur here
                    // because we can trivially handle it here.
                    if (choice < menu.Count)
                    {
                        Serve(menu[choice]);
                    }
                    else
                    {
                        Console.WriteLine("Hey, choose something *on* the menu!");
                    }
                }
                // You can see that we're "catching" the "FormatException" type
                // here. You can multiple "catch" clauses to a try/catch block
                // block just as long as each one matches against a unique
                // type of exception!
                catch (FormatException e)
                {
                    Console.WriteLine("Hey, write a *number*!");
                }
                finally
                {
                    // Finally blocks are guaranteed by C# to run, even if an
                    // exception happens. They're a good place to clean up
                    // resources that might be left open otherwise.
                    Console.WriteLine();
                    Console.WriteLine("Hurry up and choose something.");
                }

            }
        }
    }

    // Meal is an abstract class which mean we can't initialise objects of the
    // type `Meal` (i.e. `new Meal()` doesn't work). Instead we must create
    // classes that inherit or derive `Meal` and use those.
    //
    // The opposite of "abstract" is "concrete" so you could say that we can
    // only create explicit instances of "concrete" classes.
    abstract class Meal
    {
        // Abstract classes can still have all the features of concrete classes.

        private string _name;

        public Meal(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }

    // Interfaces are special abstract class "lite" things that define a
    // "contract" that implementers agree to follow: Implementing classes
    // must implement all of the features of the interface.
    interface ICheesy
    {
        // Note how this isn't abstract or virtual - it's just a method
        // signature!
        int GetCheesiness();
    }

    interface ISpicy
    {
        int GetSpiciness();
    }

    interface ISweet
    {
        int GetSweetness();
    }

    // Here we see the syntax for extending a base class:
    // class <class-name> : <base-class-name>
    class CheesyMeal : Meal, ICheesy
    {
        // Sub classes are required to call a constructor of their base class
        // in their constructor. You can see that here with the code
        // `: base(name)`. This allows us to pass values along from this
        // constructor to the base class's constructor.
        public CheesyMeal(string name) : base(name)
        {

        }

        // Sub-class constructors don't have to match the base class's
        // constructors! Here we can see a default constructor that passes a
        // fixed string to the base class.
        public CheesyMeal() : base("Cheesy Meal")
        {

        }

        public int GetCheesiness()
        {
            return 10;
        }
    }

    // While classes can only have a single "base class", they can implement
    // many interfaces as they like!
    class CheesySpicyMeal : Meal, ICheesy, ISpicy
    {
        public CheesySpicyMeal(string name) : base(name)
        {

        }

        public int GetCheesiness()
        {
            return 10;
        }

        public int GetSpiciness()
        {
            return 10;
        }
    }

    class SpicyMeal : Meal, ISpicy
    {
        public SpicyMeal(string name) : base(name)
        {
        }

        public int GetSpiciness()
        {
            return 10;
        }
    }

    class SweetMeal : Meal, ISweet
    {
        public SweetMeal(string name) : base(name)
        {
        }

        public int GetSweetness()
        {
            return 10;
        }
    }

    class Customer
    {
        // Because we've give `meal` the `Meal` type, we can pass any class
        // that extends the `Meal` class to this method.
        public void Eat(Meal meal)
        {
            Console.WriteLine("{0}? Sounds good!", meal.Name);

            // C#'s "as" operator either returns "meal" as the type "ICheesy"
            // or returns "null".
            ICheesy ch = meal as ICheesy;
            if (ch != null)
            {
                // As you can see we can use GetCheesiness on because the type
                // is now "ICheesy".
                if (ch.GetCheesiness() > 5)
                {
                    Console.WriteLine("Very cheesy!");
                }
            }

            ISpicy sp = meal as ISpicy;
            if (sp != null)
            {

                if (sp.GetSpiciness() > 5)
                {
                    Console.WriteLine("Very spicy!");
                }
            }

            ISweet sw = meal as ISweet;
            if (sw != null)
            {
                if (sw.GetSweetness() > 5)
                {
                    Console.WriteLine("Very sweet!");
                }
            }

            Console.WriteLine("5 Stars!");
        }
    }
}
