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
            Serve(new SpicyMeal("Enchilada"));
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

        // Here we have an abstract method - it's only a method signature, no
        // method body. Defining an abstract method here forces all classes
        // that extend `Meal` to implement their own version of `GetSpiciness`.
        public abstract int GetSpiciness();

        public abstract int GetCheesiness();

        public abstract int GetSweetness();
    }

    // Here we see the syntax for extending a base class:
    // class <class-name> : <base-class-name>
    class CheesyMeal : Meal
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

        // Note the `override` keyword - this marks this method as having
        // _overidden_ a method in the base class.
        public override int GetCheesiness()
        {
            return 10;
        }

        public override int GetSpiciness()
        {
            return 0;
        }

        public override int GetSweetness()
        {
            return 0;
        }
    }

    class SpicyMeal : Meal
    {
        public SpicyMeal(string name) : base(name)
        {
        }

        public override int GetCheesiness()
        {
            return 0;
        }

        public override int GetSpiciness()
        {
            return 10;
        }

        public override int GetSweetness()
        {
            return 0;
        }
    }

    class Customer
    {
        // Because we've give `meal` the `Meal` type, we can pass any class
        // that extends the `Meal` class to this method.
        public void Eat(Meal meal)
        {
            Console.WriteLine("{0}? Sounds good!", meal.Name);

            if (meal.GetCheesiness() > 5)
            {
                Console.WriteLine("Very cheesy!");
            }

            if (meal.GetSpiciness() > 5)
            {
                Console.WriteLine("Very spicy!");
            }

            if (meal.GetSweetness() > 5)
            {
                Console.WriteLine("Very sweet!");
            }

            Console.WriteLine("5 Stars!");
        }
    }
}
