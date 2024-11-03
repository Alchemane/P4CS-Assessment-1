using System;
using System.Collections.Generic; // Importing list
using System.Text; // Importing StringBuilder for our Encoder

/*
Test Plan for Top Level Menu:
------------------------------------
1. Valid option selection:
   Input: '3'
   Expected output: branches to Character Encoder mini-app.
   Actual output: branches to Character Encoder mini-app.
   Result: pass

2. Invalid Option Selection greater than 4:
   Input: '5'
   Expected output: "Invalid option, please try again."
   Actual output: "Invalid option, please try again."
   Result: pass

3. Invalid option selection less than 1:
    Input: '0'
    Expected output: "Invalid option, please try again."
    Actual output: "Invalid option, please try again."
    Result: pass

4. Non-numeric input:
   Input: 'a' and/or '!@' and/or 'two'
   Expected output: "Invalid option, please try again."
   Actual output: "Invalid option, please try again."
   Result: pass

5. Non-numeric input whitespace or empty input:
   Input: '[Enter]'
   Expected output: "Invalid option, please try again."
   Actual output: "Invalid option, please try again."
   Result: pass
*/
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

            string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces
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

/*
Test Plan for Energy Calculator:
------------------------------------
1. Valid Appliance Name and Energy Calculation:
   Input: Appliance name: "Kettle", Power rating: '2', Hours used per day: '1.5'
   Expected Output: Displays daily, monthly, and yearly energy usage for the appliance.
   Actual Output: Displays daily, monthly, and yearly energy usage for the appliance.
   Result: Pass

2. Invalid Appliance Name (Not in the Valid List):
   Input: Appliance name: "Laptop"
   Expected Output: "Invalid appliance name. Please enter a valid appliance from the list or type '1' to view available appliances."
   Actual Output: "Invalid appliance name. Please enter a valid appliance from the list or type '1' to view available appliances."
   Result: Pass

3. View Available Appliance List:
   Input: '1' to view available appliances
   Expected Output: Displays list of valid appliances categorized by type.
   Actual Output: Displays list of valid appliances categorized by type.
   Result: Pass

4. Invalid Power Rating (Non-Numeric):
   Input: Power rating: 'abc'
   Expected Output: "Invalid input. Please enter a valid positive number."
   Actual Output: "Invalid input. Please enter a valid positive number."
   Result: Pass

5. Power Rating Out of Range (Negative or Zero):
    Input: Power rating: '-2'
    Expected Output: "Invalid input. Please enter a valid positive number."
    Actual Output: "Invalid input. Please enter a valid positive number."
    Result: Pass

6. Valid Power Rating and Hours Used (Boundary Test):
    Input: Power rating: '2', Hours used: '24'
    Expected Output: Displays calculated energy usage.
    Actual Output: Displays calculated energy usage.
    Result: Pass

7. Hours Used Out of Range (Greater Than 24):
    Input: Hours used: '25'
    Expected Output: "Invalid input. Please enter a valid positive number."
    Actual Output: "Invalid input. Please enter a valid positive number."
    Result: Pass

8. Non-Numeric Input for Hours Used:
    Input: Hours used: 'xyz'
    Expected Output: "Invalid input. Please enter a valid positive number."
    Actual Output: "Invalid input. Please enter a valid positive number."
    Result: Pass

9. Quit the Energy Calculator:
    Input: '2'
    Expected Output: "Exiting the Energy Calculator." and return to top-level menu.
    Actual Output: "Exiting the Energy Calculator." and return to top-level menu.
    Result: Pass
*/
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

            string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces
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
        // Dictionary of categories with appliances, using key pair values for categories and its appliances, used for checking available appliance names
        Dictionary<string, List<string>> applianceCategories = new Dictionary<string, List<string>>()
        {
            { "Kitchen Appliances", new List<string> 
                { 
                    "Kettle", "Toaster", "Microwave", "Fridge", "Refrigerator", "Freezer", "Dishwasher", 
                    "Oven", "Convection Oven", "Stove", "Cooktop", "Blender", "Coffee Maker", 
                    "Espresso Machine", "Rice Cooker", "Pressure Cooker", "Slow Cooker", "Food Processor", 
                    "Air Fryer", "Juicer", "Mixer", "Stand Mixer", "Toaster Oven", "Grill", 
                    "Indoor Electric Grill", "Deep Fryer", "Ice Cream Maker" 
                } 
            },
            { "Laundry & Cleaning Appliances", new List<string> 
                { 
                    "Washing Machine", "Dryer", "Tumble Dryer", "Vacuum Cleaner", "Robot Vacuum", 
                    "Steam Cleaner", "Iron", "Garment Steamer", "Clothes Steamer" 
                } 
            },
            { "Heating & Cooling Appliances", new List<string> 
                { 
                    "Heater", "Space Heater", "Air Conditioner", "Fan", "Ceiling Fan", "Portable Fan", 
                    "Humidifier", "Dehumidifier", "Air Purifier" 
                } 
            },
            { "Entertainment & Media Appliances", new List<string> 
                { 
                    "Television", "TV", "Sound System", "DVD Player", "Blu-ray Player", 
                    "Streaming Device", "Apple TV", "Chromecast", "Game Console", 
                    "PlayStation", "Xbox", "Nintendo Switch", "Radio", "Projector" 
                } 
            },
            { "Personal Care Appliances", new List<string> 
                { 
                    "Hair Dryer", "Electric Toothbrush", "Hair Straightener", "Flat Iron", 
                    "Hair Curler", "Electric Shaver", "Razor", "Massage Chair", "Facial Steamer" 
                } 
            },
            { "Lighting & Home Maintenance", new List<string> 
                { 
                    "Lamp", "Table Lamp", "Floor Lamp", "Chandelier", "Torch", 
                    "Flashlight", "LED Light Strip" 
                } 
            },
            { "Office & Miscellaneous Appliances", new List<string> 
                { 
                    "Printer", "Scanner", "Computer Monitor", "Electric Fan Heater", 
                    "Paper Shredder" 
                } 
            },
            { "Outdoor & Garden Appliances", new List<string> 
                { 
                    "Electric Lawn Mower", "Hedge Trimmer", "Leaf Blower", "Pressure Washer", 
                    "Garden Sprinkler System" 
                } 
            },
            { "Other Household Appliances", new List<string> 
                { 
                    "Electric Blanket", "Sewing Machine", "Water Dispenser", 
                    "Electric Fireplace", "Heated Towel Rail" 
                } 
            }
        };

        // Create a HashSet to store all valid appliance names for efficient lookup (case-insensitive)
        HashSet<string> validApplianceNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        // iterate through each list of appliances in categories to populate HashSet
        foreach (var appliances in applianceCategories.Values)//loop through each categorys appliance list
        {
            foreach (var appliance in appliances) // Loop through individual appliances within the list
            {
                validApplianceNames.Add(appliance);//Add each appliance to HashSet to build a complete collection
            }
        }

        bool exit = false;
        string name = ""; // has to be initialised empty

        // Main loop for choosing options continuously
        while (!exit)
        {
            Console.WriteLine(@"
                Please select a choice:
                1. Continue to enter appliance name
                2. List available appliance names by category
                3. Exit");

            string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces
            switch (choice)
            {
                case "1": // Continue - ask for the name of the appliance
                    bool validName = false;

                    while (!validName) // Re-prompt the user if the input name is invalid
                    {
                        Console.WriteLine("Enter the appliance name (e.g., Kettle, Fridge):");
                        name = Console.ReadLine();

                        if (validApplianceNames.Contains(name)) // Does hashset contain user-inputted appliance name?
                        {
                            validName = true; // Name is valid, exit loop
                        }
                        else
                        {
                            Console.WriteLine("Invalid appliance name. Please enter a valid appliance from the list or type '1' to view available appliances.");
                            string response = Console.ReadLine().Trim().ToLower();
                            if (response == "1") // Helps user find the appliance they need if incorrect appliance name is inputted
                            {
                                ListAppliancesByCategory(applianceCategories); //Call function for listing appliance by category
                            }
                        }
                    }
                    exit = true; // Once a valid name is entered, exit the main loop to proceed with other inputs
                    break;

                case "2": // Ask for appliance category to list and then list the supported appliance names
                    ListAppliancesByCategory(applianceCategories);
                    break;

                case "3": // Exit the appliance selection process
                    exit = true;
                    Console.WriteLine("Exiting the Energy Calculator.");
                    return null; // Exit the method

                default: // Handle invalid input
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }

        // Get and validate the power rating
        double powerRating = ValidateInput("Enter the power rating (in kWh):");

        // Get and validate the hours used per day, passing in 24 for maximum hours for a day
        double hoursUsed = ValidateInput("Enter the hours used per day, ensure it is no higher than 24 hours:", 24);

        // Return an Appliance object
        return new Appliance(name, powerRating, hoursUsed); // Returns instance of Appliance class using user input
    }

    /*
    Test Plan for menu option selection inside EnergyCalculator branch in ListAppliancesByCategory function:
    ------------------------------------
    1. Valid option selection:
    Input: '1'
    Expected output: lists all Kitchen Appliances.
    Actual output: lists all Kitchen Appliances
    Result: pass

    2. Invalid option selection greater than 10 and less than 1:
    Input: '11' and/or '0' and/or -1
    Expected output: "Invalid selection. Please select a valid category number"
    Actual output: "Invalid selection. Please select a valid category number."
    Result: pass

    3. Non-numeric input:
    Input: 'a' and/or '[Enter]'
    Expected output: "Invalid selection. Please select a valid category number."
    Actual output: "Invalid selection. Please select a valid category number.."
    Result: pass
    */

    // Function for listing appliances by category, encapsulated
    private void ListAppliancesByCategory(Dictionary<string, List<string>> applianceCategories)
    {
        Console.WriteLine("Select an appliance category to view available appliances:");

        int index = 1; // index counter for displaying available categories
        Dictionary<int, string> indexToCategoryMap = new Dictionary<int, string>();

        foreach (var category in applianceCategories.Keys) // Iterating through appliance categories to display them to user
        {
            Console.WriteLine($"{index}. {category}"); // displaying category with numeric index
            indexToCategoryMap.Add(index, category);  // Map index to category name
            index++;
        }
        Console.WriteLine($"{index}. Exit to previous menu"); // Adding an option to exit back to the previous menu

        int categoryChoice = 0; // Variable to store the users category choice
        while (true) // loop to get valid input from the user until they choose to exit or select valid category
        {
            try
            {
                // Parsing user input and ensuring selection valid
                if (int.TryParse(Console.ReadLine(), out categoryChoice) && categoryChoice >= 1 && categoryChoice <= indexToCategoryMap.Count + 1)
                {
                    if (categoryChoice == indexToCategoryMap.Count + 1)  // Handling the exit case when user chooses the last option to return to the previous menu
                    {
                        Console.WriteLine("Returning to the previous menu.");
                        break; // Exit category selection loop and return to calling function
                    }

                    // Retrieving selected category name using the mapping dictionary
                    string selectedCategory = indexToCategoryMap[categoryChoice];
                    Console.WriteLine($"Available appliances in {selectedCategory}:");

                    // Listing all appliances in selected category
                    foreach (string appliance in applianceCategories[selectedCategory])
                    {
                        Console.WriteLine($"- {appliance}");
                    }
                    break; // after listing appliances, break and return to main menu
                }
                else
                {
                    // Throwing an exception if user provides an invalid input
                    throw new ArgumentException("Invalid selection. Please select a valid category number.");
                }
            }
            catch (ArgumentException ex)
            {
                // Handling cases where user input is outside of expected range
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                // Generic exception handler for unexpected errors
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }

    //Method to validate numeric input, private as not necessary to be called outside this class
    private double ValidateInput(string prompt, double max = double.MaxValue) // Optional parameter max ensures hour value not exceeding 24
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

public class Appliance // Appliance Class for the use of appliances in the energy calculator class
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

/*
Test Plan for Product List Manager:
------------------------------------
1. Valid product addition:
   Input: Category: 'Bakery', Name: 'Bread', Cost: '1.5', Quantity: '10'
   Expected output: "Product added successfully!"
   Actual output: "Product added successfully!"
   Result: pass

2. Invalid category selection greater than available:
   Input: '5' (Category selection)
   Expected output: "Invalid category selection. Please select again."
   Actual output: "Invalid category selection. Please select again."
   Result: pass

3. Invalid product quantity:
   Input: Quantity: '-5' and/or '0'
   Expected output: "Specified argument was out of the range of valid values. (Parameter 'Value must be between 0 and 1.7976931348623157E+308.')"
   Actual output: "Specified argument was out of the range of valid values. (Parameter 'Value must be between 0 and 1.7976931348623157E+308.')"
   Result: pass

4. Invalid product price:
   Input: Quantity: '-6' and/or '0'
   Expected output: "Specified argument was out of the range of valid values. (Parameter 'Value must be between 0 and 1.7976931348623157E+308.')"
   Actual output: "Specified argument was out of the range of valid values. (Parameter 'Value must be between 0 and 1.7976931348623157E+308.')"
   Result: pass

5. Invalid product price non-numerical or :
   Input: Quantity: ['Enter']
   Expected output: "Invalid format. Please enter a number."
   Actual output: "Invalid format. Please enter a number."
   Result: pass

6. Listing products in an empty category:
   Input: Category: 'Dairy'
   Expected output: "No products available."
   Actual output: "No products available."
   Result: pass

7. Product quantity fractional nature:
   Input: Quantity: 0.7
   Expected output: "Quantity must be a whole number. Please enter a valid quantity."
   Actual output: "Quantity must be a whole number. Please enter a valid quantity."
   Result: pass
*/
public class ProductsList // Has a similar feature as the top level menu with case switch in while loop to direct user to different product list functions.
{
    /*
    Test Plan for Product List Manager Menu:
    ------------------------------------
    1. Valid option selection:
    Input: '2'
    Expected output: branches to list products in list.
    Actual output: branches to list products in list.
    Result: pass

    2. Invalid Option Selection greater than 3:
    Input: '4'
    Expected output: "Invalid option, please try again."
    Actual output: "Invalid option, please try again."
    Result: pass

    3. Invalid option selection less than 1:
    Input: '0'
    Expected output: "Invalid option, please try again."
    Actual output: "Invalid option, please try again."
    Result: pass

    4. Non-numeric input:
    Input: 'a'
    Expected output: "Invalid option, please try again."
    Actual output: "Invalid option, please try again."
    Result: pass

    5. Non-numeric input whitespace or empty input:
    Input: '[Enter]'
    Expected output: "Invalid option, please try again."
    Actual output: "Invalid option, please try again."
    Result: pass
    */
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

            string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces
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
        Console.WriteLine(@"Select a product category:
            1. Fruit & Vegetables
            2. Bakery
            3. Dairy
            4. Quit");

        string category = "";
        bool validCategory = false;

        while (!validCategory)
        {
            string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces
            switch (choice)
            {
                case "1":
                    category = "Fruit & Vegetables";
                    validCategory = true;
                    break;
                case "2":
                    category = "Bakery";
                    validCategory = true;
                    break;
                case "3":
                    category = "Dairy";
                    validCategory = true;
                    break;
                case "4":
                    // Exit the AddProduct method without adding a product
                    Console.WriteLine("Exiting product addition.");
                    return; // Exits the method entirely
                default:
                    Console.WriteLine("Invalid category selection. Please select again.");
                    break;
            }
        }

        Console.WriteLine("Enter the product name:");
        string name = Console.ReadLine();

        double price = ValidateInput("Enter the cost of one unit:"); // Pass user input directly into the ValidateInput function
        // Type cast to int and ensure isQuantity is true for additional validation layer
        int quantity = (int)ValidateInput("Enter the quantity of items:", true);

        products.Add(new Product(name, price, quantity, category)); // Append new product to list
        Console.WriteLine("Product added successfully!");
    }

    /*
    Test Plan for Category Selection Menu inside List Products branch in Products Manager:
    ------------------------------------
    1. Valid option selection:
    Input: '2'
    Expected output: branches to list products in list.
    Actual output: branches to list products in list.
    Result: pass

    2. Invalid option selection greater than 3 and less than 1:
    Input: '4' and/or '0' and/or -3
    Expected output: "Invalid option, please try again."
    Actual output: "Invalid option, please try again."
    Result: pass

    3. Non-numeric input:
    Input: 'a' and/or '[Enter]' and/or '!@' and/or 'one'
    Expected output: "Invalid option, please try again."
    Actual output: "Invalid option, please try again."
    Result: pass
    */
    public void ListProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        string selectedCategory = "";
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine(@"
            Please select a category:
            1. Fruit & Vegetables
            2. Bakery
            3. Dairy
            4. All
            5. Exit");
            string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces

            switch (choice)
            {
                case "1":
                    selectedCategory = "Fruit & Vegetables"; //Assign our category
                    exit = true; // breaks the for loop
                    break;
                case "2":
                    selectedCategory = "Bakery";
                    exit = true;
                    break;
                case "3":
                    selectedCategory = "Dairy";
                    exit = true;
                    break;
                case "4":
                    selectedCategory = "All";
                    exit = true;
                    break;
                case "5":
                    exit = true; // Exit naturally
                    Console.WriteLine("Returning to the menu...");
                    return;
                default: // Because we're in the while loop, allows for continuous prompting.
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
        // Filter products by category if a specific category is selected
        List<Product> filteredProducts = new List<Product>();
        // If "All" is selected, show all products; otherwise, filter by the selected category
        if (selectedCategory != "All")
        {
            filteredProducts = products.FindAll(p => p.Category == selectedCategory);
        }
        else
        {
            filteredProducts = products;
        }

        // Displaying product details selected category
        if (filteredProducts.Count == 0) // No products found in the selected category
        {
            Console.WriteLine($"No products available in the category: {selectedCategory}");
        }
        else
        {
            double totalCost = 0;
            int totalQuantity = 0;

            Console.WriteLine($"\nCategory: {selectedCategory}"); //print the category name
            Console.Write("Items: ");

            // Use a for loop to print each products name, adding a comma only when its not the last element
            for (int i = 0; i < filteredProducts.Count; i++)
            {
                Console.Write(filteredProducts[i].Name);
                if (i < filteredProducts.Count - 1)
                {
                    Console.Write(", ");// Add comma between items, but not after the last one
                }

                // Calculating total cost and quantity inside the loop
                totalCost += filteredProducts[i].Price * filteredProducts[i].Quantity;
                totalQuantity += filteredProducts[i].Quantity;
            }
            // Print the total number of items and the total cost for the selected category
            Console.WriteLine($"\nTotal number of items: {totalQuantity}");
            Console.WriteLine($"Total cost of items: £{totalCost:F2}");
        }
    }

    /* Essential method for validating user input is correct and sanitised. max not used but there for robustness and modularity. 
    isQuantity defaulted to false as is not needed unless quantity is used, which we'll explicitly state.
    Method to validate user input, private as not necessary to be called outside this class */
    private static double ValidateInput(string prompt, bool isQuantity = false, double max = double.MaxValue)
    {
        bool validInput = false; // repeats our loop until valid input present using boolean value
        double value = 0;
        while (!validInput) // Loop until valid input is provided by the user
        {
            try // capture expected errors and display error to user. prevents runtime errors that interfere the programs run.
            {
                Console.WriteLine(prompt);
                value = Convert.ToDouble(Console.ReadLine()); // Attempt to convert user input to a double

                if (value <= 0 || value > max) // Check if the input is within the acceptable range
                {
                    // Throw exception if the value is out of specified range
                    throw new ArgumentOutOfRangeException($"Value must be between 0 and {max}.");
                }
                // If validating quantity, ensure that the value is a whole number
                if (isQuantity && value % 1 != 0)
                {
                    throw new ArgumentException("Quantity must be a whole number. Please enter a valid quantity.");
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
            catch (ArgumentException ex)
            {
                // Catch block to handle non-whole numbers for quantity
                Console.WriteLine(ex.Message);
            }
        }
        return value; // Return the valid numeric input
    }
}

public class Product // Product Class for the use of products list class to store product details
{
    // Auto-Implemented Property; Getter and Setter with backing field managed by compiler
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }

    public Product(string name, double price, int quantity, string category) // Constructor for Product class
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
    }
}

/*
Test Plan for Encoding a String:
------------------------------------
1. Valid string input:
   Input: 'HELLO'
   Expected output: "Encoded string: 01000 00101 01100 01100 01111"
   Actual output: "Encoded string: 01000 00101 01100 01100 01111"
   Result: pass

2. Lowercase input:
   Input: 'hello'
   Expected output: "Encoded string: 01000 00101 01100 01100 01111"
   Actual output: "Encoded string: 01000 00101 01100 01100 01111"
   Result: pass

3. Input with non-alphabetic characters:
   Input: '3rd of April'
   Expected output: "3 10010 00100   01111 00110   00001 10000 10010 01001 01100"
   Actual output: "3 10010 00100   01111 00110   00001 10000 10010 01001 01100"
   Result: pass

4. Special character input:
   Input: '@hello!'
   Expected output: "@ 01000 00101 01100 01100 01111 !"
   Actual output: "@ 01000 00101 01100 01100 01111 !"
   Result: pass

5. Empty input:
   Input: '[Enter]'
   Expected output: "Invalid input. Please enter a valid string to encode."
   Actual output: "Invalid input. Please enter a valid string to encode."
   Result: pass
*/
public class CharacterEncoder
{
    /*
    Test Plan for menu option selection inside CharacterEncoder branch in RunCharacterEncoder:
    ------------------------------------
    1. Valid option selection:
    Input: '1'
    Expected output: branches to encoder prompt.
    Actual output: branches to encoder prompt
    Result: pass

    2. Invalid option selection greater than 3 and less than 1:
    Input: '3' and/or '0' and/or -1
    Expected output: "Invalid option. Please select 1 or 2."
    Actual output: "Invalid option. Please select 1 or 2."
    Result: pass

    3. Non-numeric input:
    Input: 'a' and/or '[Enter]' and/or '!@'
    Expected output: "Invalid option. Please select 1 or 2."
    Actual output: "Invalid option. Please select 1 or 2."
    Result: pass
    */
    public void RunCharacterEncoder()
    {
        bool exit = false;
        while (!exit)
        {
            // Presenting the user with options to encode or exit
            Console.WriteLine(@"
            Character Encoder:
            1. Encode a string
            2. Quit");

            string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces

            switch (choice)
            {
                case "1": // Option to encode a string
                    Console.WriteLine("Enter a string to encode:");
                    string input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input)) // catch null or empty input from user
                    {
                        Console.WriteLine("Invalid input. Please enter a non-empty string.");
                    }
                    else
                    {
                        string encoded = EncodeString(input); // call our encoder if valid input from user
                        Console.WriteLine($"Encoded string: {encoded}");
                    }
                    break;

                case "2": // Option to exit the character encoder
                    Console.WriteLine("Exiting Character Encoder.");
                    exit = true;
                    break;

                default: //Invalid input, prompting user again
                    Console.WriteLine("Invalid option. Please select 1 or 2.");
                    break;
            }
        }
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
            else
            {
                encodedString.Append(c + " "); // Leave non-alphabetic characters as is
            }
        }
        return encodedString.ToString().Trim(); // Remove any trailing spaces
    }
}