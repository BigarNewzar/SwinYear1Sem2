using System;

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
            Serve(new CheesyMeal("Pastry"));
            Serve(new CheesySpicyMeal("Enchilada"));
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
            if(sw != null)
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
