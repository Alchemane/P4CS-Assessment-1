using System;
using System.Collections.Generic; // Importing list
using System.Text; // Importing StringBuilder

// Class Menu is the access point for the program and contains Main method with prompt, pointing to the other mini-app Classes
public class Menu
{
    // Main method runs the top-level menu, giving options to run different mini-apps
    public static void Main(string[] args)
    {
        // While loop using boolean control, allows for continuous prompts when incorrect prompt input
        bool exit = false;
        while (!exit)
        {
            // Using a verbatim string for multi-line menu options
            Console.WriteLine(@"Select an option:
            1. Run Energy Calculator
            2. Manage Products List
            3. Run Character Encoder
            4. Quit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": // Branch to Energy Calculator
                    EnergyCalculator energyCalculator = new EnergyCalculator();
                    energyCalculator.RunEnergyCalculator();
                    break;
                case "2": // Branch to Product List
                    ProductsList productsList = new ProductsList();
                    productsList.RunProductList();
                    break;
                case "3": // Branch to Character Encoder
                    CharacterEncoder characterEncoder = new CharacterEncoder();
                    characterEncoder.RunCharacterEncoder();
                    break;
                case "4":
                    exit = true; // Exit naturally
                    Console.WriteLine("Exiting the program.");
                    break;
                default: // Because we're in the while loop, allows for continuous prompting.
                    Console.WriteLine("Invalid option, please try again.");
                    break;

            }
        }
    }
}

public class ProductsList
{
    private List<Product> products = new List<Product>(); // List with data type <Product> as in the class Product

    public void RunProductList()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine(@"
            Select an option:
            1. Create a list of products
            2. List product details
            3. Quit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddProduct(); // Add to the products list
                    break;
                case "2":
                    ListProducts(); // Display all products in the list
                    break;
                case "3":
                    exit = true; // Exit the product list manager
                    Console.WriteLine("Exiting the program."); // Natural exit from products list app back into the top level menu
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again."); // Reprompting as in while loop
                    break;
            }
        }
    }

    public void AddProduct()
    {
        Console.WriteLine("Enter the product name:");
        string name = Console.ReadLine();

        double price = ValidateInput("Enter the price of the product:");
        int quantity = (int)ValidateInput("Enter the quantity of the product:"); // Type cast to int

        products.Add(new Product(name, price, quantity)); // Append new product to list
        Console.WriteLine("Product added successfully!");
    }

    public void ListProducts()
    {
        if (products.Count == 0) // A case for empty list
        {
            Console.WriteLine("No products available.");
        }
        else
        {
            foreach (var product in products) // Use of foreach loop is cleaner to output each item in products list
            {
                Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
            }
        }
    }

    public static double ValidateInput(string prompt) // Essential for ensuring correct input to products list
    {
        while (!validInput) // Loop until valid input is provided by the user
        {
            try
            {
                Console.WriteLine(prompt);
                value = Convert.ToDouble(Console.ReadLine()); // Attempt to convert user input to a double

                if (value <= 0 || value > max) // Check if the input is within the acceptable range
                {
                    // Throw exception if the value is out of specified range
                    throw new ArgumentOutOfRangeException($"Value must be between 0 and {max}.");
                }
                validInput = true; // Input is valid, exit the loop
            }
            catch (FormatException)
            {
                // Catch block to handle invalid format (non numeric input)
                Console.WriteLine("Invalid format. Please enter a number.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Catch block to handle out of range inputs and display the appropriate message
                Console.WriteLine(ex.Message);
            }
        }
        return value; // Return the valid numeric input
    }
}

public class Product // Product Class
{
    // Auto-Implemented Property; Getter and Setter with backing field managed by compiler
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, double price, int quantity) // Constructor for Product
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}

public class CharacterEncoder
{
    public void RunCharacterEncoder()
    {
        Console.WriteLine("Enter a string to encode:");
        string input = Console.ReadLine().ToUpper(); // Convert input to uppercase for uniform encoding
        string encoded = EncodeString(input); // Encode the input string
        Console.WriteLine($"Encoded string: {encoded}");
    }

    public static string EncodeString(string input)
    {
        // Dictionary with Alphabet as Key and binary encodings as Value
        // Maps each letter to its corresponding binary encoding
        Dictionary<char, string> encodingMap = new Dictionary<char, string>
        {
            {'A', "00001"}, {'B', "00010"}, {'C', "00011"}, {'D', "00100"}, {'E', "00101"},
            {'F', "00110"}, {'G', "00111"}, {'H', "01000"}, {'I', "01001"}, {'J', "01010"},
            {'K', "01011"}, {'L', "01100"}, {'M', "01101"}, {'N', "01110"}, {'O', "01111"},
            {'P', "10000"}, {'Q', "10001"}, {'R', "10010"}, {'S', "10011"}, {'T', "10100"}, 
            {'U', "10101"}, {'V', "10110"}, {'W', "10111"}, {'X', "11000"}, {'Y', "11001"},
            {'Z', "11010"}
        };

        StringBuilder encodedString = new StringBuilder(); // Efficient string manipulation
        foreach (char c in input)
        {
            if (encodingMap.ContainsKey(c)) // Encode each letter from user input using foreach loop
            {
                encodedString.Append(encodingMap[c] + " ");  // Append encoded letter with space separator
            }
            else if (char.IsLetter(c))
            {
                encodedString.Append("Invalid "); // Mark non-alphabetic characters as invalid
            }
        }
        return encodedString.ToString().Trim(); // Remove any trailing spaces
    }
}

public class EnergyCalculator
{
    public void RunEnergyCalculator()
    {
        bool exit = false;
        while (!exit)
        {
            // Verbatim used to reduce amount of print statements
            Console.WriteLine(@"
            Energy Calculator:
            1. Calculate energy usage
            2. Quit"); // Reason for this menu option is to continuously add numerous products without exiting unless user states

            string choice = Console.ReadLine();
            switch (choice) // Switch statement for limited options
            {
                case "1":
                    Appliance appliance = GetApplianceData();
                    CalculateEnergyUsage(appliance); // Calculates and returns data regarding appliance
                    break;
                case "2":
                    exit = true; // Exits the app and returns to Main method in Menu class to reprompt top level menu
                    Console.WriteLine("Exiting the Energy Calculator.");
                    break;
                default: // Accounts for invalid options again, with while loop with boolean conditional for reprompting
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    public Appliance GetApplianceData()
    {
        Console.WriteLine("Enter the appliance name:");
        string name = Console.ReadLine();

        // Get and validate the power rating
        double powerRating = ValidateInput("Enter the power rating (in kWh):");

        // Get and validate the hours used per day
        double hoursUsed = ValidateInput("Enter the hours used per day:", 24);

        // Return an Appliance object
        return new Appliance(name, powerRating, hoursUsed); // Returns instance of Appliance class using user input
    }

    // Method to validate numeric input
    public double ValidateInput(string prompt, double max = double.MaxValue) // Optional parameter max ensures hour value not exceeding 24
    {
        double value;
        Console.WriteLine(prompt);
        // Try to parse user input and ensure it is within the valid range as in 24 hours for days
        while (!double.TryParse(Console.ReadLine(), out value) || value <= 0 || value > max)
        {
            Console.WriteLine("Invalid input. Please enter a valid positive number."); // None negative number only
        }
        return value;
    }

    // Method to calculate and display the energy usage
    public void CalculateEnergyUsage(Appliance appliance)
    {
        /*
            Daily Energy Usage = power rating x hours used per day
            Monthly Energy Usage = Daily Energy Usage x 30
            Yearly Energy Usage = Daily Energy Usage x 365
        */
        // Energy usage calculations based on daily usage, then extrapolated to monthly and yearly usage
        double dailyUsage = appliance.PowerRating * appliance.HoursUsed;
        double monthlyUsage = dailyUsage * 30; // Approximate monthly usage (30 days)
        double yearlyUsage = dailyUsage * 365; // Approximate yearly usage (365 days)

        // Display the results to the user
        Console.WriteLine($"\nThe daily energy usage for your {appliance.Name} is: {dailyUsage} kWh\n" +
                  $"Its monthly energy usage is: {monthlyUsage} kWh\n" +
                  $"Its yearly energy usage is: {yearlyUsage} kWh");
    }
}

public class Appliance // Appliance Class
{
    // Auto-Implemented Property; Getter and Setter with backing field managed by compiler
    public string Name { get; set; }
    public double PowerRating { get; set; }
    public double HoursUsed { get; set; }

    public Appliance(string name, double powerRating, double hoursUsed) // Constructor
    {
        Name = name;
        PowerRating = powerRating;
        HoursUsed = hoursUsed;
    }
}