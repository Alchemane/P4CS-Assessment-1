using System;
using System.Collections.Generic; // Importing list
using System.Text; // Importing StringBuilder for our Encoder
using System.Linq; // Language Integrated Query (LINQ) Using Any()

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
        /* This logic was previously controlled by a boolean variable set to false and the argument for the while loop 
            was ![variable_name] but this method cut the code down by a few lines. the exit case used a reassignment of the 
            variable line so [variable_name] = true; but i realised that the ![variable_name] or not false, is essentially 
            just true, and the while loop could be controlled using a return; statement in case 4 to naturally converge 
            and end the program from the top level menu. this reduces memory allocation from the removal of loop bool variable, and 
            the code looks cleaner and more readable, as well as less lines for the same purpose. all other instances of the while
            loops have utilised the same method as here.
        */

        // top level try catch to catch unexpected errors in our top level menu
        try
        {
            while (true)
            {
                Console.WriteLine("\n" +
                    "Select an option:\n" +
                    "1. Run Energy Calculator\n" +
                    "2. Manage Products List\n" +
                    "3. Run Character Encoder\n" +
                    "4. Quit"); // String concatenation for convenience

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
                        Console.WriteLine("Exiting the program.");
                        return; // Exit naturally
                    default: // Because we're in the while loop, allows for continuous prompting.
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
        catch (Exception ex) // Catch any unhandled exceptions
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            Console.WriteLine("The program will now exit.");
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
        try
        {
            while (true)
            {
                Console.WriteLine("\n" + 
                "Energy Calculator:\n" +
                "1. Calculate energy usage\n" +
                "2. Quit"); // Reason for this menu option is to continuously add numerous products without exiting unless user states

                string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces
                switch (choice) // Switch statement for limited options
                {
                    case "1":
                        Appliance appliance = GetApplianceData();
                        CalculateEnergyUsage(appliance); // Calculates and returns data regarding appliance
                        break;
                    case "2":
                        // Exits the app and returns to Main method in Menu class to reprompt top level menu
                        Console.WriteLine("Exiting the Energy Calculator.");
                        return;
                    default: // Accounts for invalid options again, with while loop with boolean conditional for reprompting
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
        catch (Exception ex) // ensure program is stable during runtime, catch and print any unexpected errors
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    private Appliance GetApplianceData() // Privatised and encapuslated as is only used by this class.
    {
        /* Dictionary of categories with appliances, using key pair values for categories and its appliances, used for checking available appliance names
        using our data provider class dedicated for storing our products and categories */
        var applianceCategories = ProductDataProvider.GetApplianceCategories();
        // select a category
        string selectedCategory = Utilities.SelectCategory(applianceCategories);
        if (selectedCategory == null)
        {
            Console.WriteLine("Exiting the Energy Calculator.");
            return null;
        }
        // select an appliance from the chosen category
        string applianceName = Utilities.SelectProduct(selectedCategory, applianceCategories);
        // get and validate the power rating and hours used
        double powerRating = Utilities.ValidateInput("Enter the power rating (in kWh):");
        double hoursUsed = Utilities.ValidateInput("Enter the hours used per day (out of 24):", 24);

        // return the appliance object
        return new Appliance(applianceName, powerRating, hoursUsed);
    }

    // Method to calculate and display the energy usage
    private void CalculateEnergyUsage(Appliance appliance) // Privatised and encapuslated as is only used by this class.
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
        try
        {
            while (true)
            {
                Console.WriteLine("\n" +
                "Select an option:\n" +
                "1. Create a list of products\n" +
                "2. List product details\n" +
                "3. Quit");

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
                        Console.WriteLine("Exiting the program."); // Natural exit from products list app back into the top level menu
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again."); // Reprompting as in while loop
                        break;
                }
            }
        }
        catch (Exception ex) // ensure every part of the program is stable during runtime, catch and print any unexpected errors
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    private void AddProduct() // Privatised and encapsulated as is only used by this class.
    {
        // Dictionary for product categories and available items within those categories using our dedicated class for dictionary data
        var productCategories = ProductDataProvider.GetProductCategories();
        // select a category
        string selectedCategory = Utilities.SelectCategory(productCategories);
        if (selectedCategory == null)
        {
            Console.WriteLine("Exiting product addition.");
            return;
        }
        // select a product from chosen category
        string productName = Utilities.SelectProduct(selectedCategory, productCategories);

        // get and validate price and quantity
        double price = Utilities.ValidateInput("Enter the cost of one unit:");
        int quantity = (int)Utilities.ValidateInput("Enter the quantity of items:", double.MaxValue, true);

        // add new product to list
        products.Add(new Product(productName, price, quantity, selectedCategory));
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
    private void ListProducts() // Privatised and encapuslated as is only used by this class.
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }
        string selectedCategory = "";
        while (true)
        {
            Console.WriteLine("\n" +
            "Please select a category:\n" +
            "1. Fruit & Vegetables\n" +
            "2. Bakery\n" +
            "3. Dairy\n" +
            "4. All\n" +
            "5. Exit");
            string choice = Console.ReadLine()?.Trim(); // Remove any trailing white spaces

            switch (choice)
            {
                case "1":
                    selectedCategory = "Fruit & Vegetables"; //Assign our category
                    break;
                case "2":
                    selectedCategory = "Bakery";
                    break;
                case "3":
                    selectedCategory = "Dairy";
                    break;
                case "4":
                    selectedCategory = "All";
                    break;
                case "5":
                    Console.WriteLine("Returning to the menu...");
                    return;
                default: // Because we're in the while loop, allows for continuous prompting.
                    Console.WriteLine("Invalid option, please try again.");
                    continue; // Skip the rest of the loop and re-prompt
            }
            break; // Exit the loop once a valid category is selected
        }
        /* Filter products by category if a specific category is selected
            If "All" is selected, show all products; otherwise, filter by the selected category */
        List<Product> filteredProducts = selectedCategory == "All" ? products.ToList() 
        : products.Where(p => p.Category == selectedCategory).ToList();

        // Displaying product details selected category
        if (filteredProducts.Count == 0) // No products found in the selected category
        {
            Console.WriteLine($"No products available in the category: {selectedCategory}");
        }
        else
        {
            Console.WriteLine($"\nCategory: {selectedCategory}");
            // Use string.Join to concatenate product names separated by a comma
            Console.WriteLine("Items: " + string.Join(", ", filteredProducts.Select(p => p.Name)));
            // Calculate total cost and total quantity using LINQ.
            double totalCost = filteredProducts.Sum(p => p.Price * p.Quantity);
            int totalQuantity = filteredProducts.Sum(p => p.Quantity);
            // Print the total number of items and the total cost for the selected category
            Console.WriteLine($"\nTotal number of items: {totalQuantity}");
            Console.WriteLine($"Total cost of items: £{totalCost:F2}");
        }
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
        try
        {
            while (true)
            {
                // Presenting the user with options to encode or exit
                Console.WriteLine("\n" +
                "Character Encoder:\n" +
                "1. Encode a string\n" +
                "2. Quit");

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
                        return;
                    default: //Invalid input, prompting user again
                        Console.WriteLine("Invalid option. Please select 1 or 2.");
                        break;
                }
            }
        }
        catch (Exception ex) // ensure program is stable during runtime, catch and print any unexpected errors
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    private static string EncodeString(string input) // Privatised and encapuslated as is only used by this class.
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

public static class Utilities
{
    /* Method to validate numeric input with optional constraints for maximum value and checking if the input should be a whole number.
    prompt parameter specifies the message to be displayed to the user.
    max parameter is an optional constraint for the maximum allowed value (default is double.MaxValue).
    requiresWholeNum parameter, when true, ensures that the input is a whole number (e.g., for quantities). */
    public static double ValidateInput(string prompt, double max = double.MaxValue, bool requiresWholeNum = false)
    {
        bool validInput = false; // flag to track if valid input is obtained
        double value = 0; // variable to store the user input value
        
        while (!validInput) // loop until valid input is provided by the user
        {
            try
            {
                Console.WriteLine(prompt); // prompt the user for input and convert it to a double
                value = Convert.ToDouble(Console.ReadLine());
                // check if the value is within the specified range (greater than 0 and less than or equal to the max value)
                if (value <= 0 || value > max)
                {
                    // Throw exception if value is not within the specified range
                    throw new ArgumentOutOfRangeException($"Value must be between 0 and {max}.");
                }

                // If the value should be a whole number (e.g., quantity), check for modular arithmetic to see if there's a fraction
                if (requiresWholeNum && value % 1 != 0) // use of modular arithmetic
                {
                    throw new ArgumentException("Value must be a whole number.");
                }

                validInput = true; // Exit the loop when the input is valid
            }
            catch (FormatException)
            { // handle the case where the input cannot be converted to a double
                Console.WriteLine("Invalid format. Please enter a number.");
            }
            catch (ArgumentOutOfRangeException ex)
            { // handle the case where the input value is out of the specified range
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            { // handle the case where the value must be a whole number but isn't
                Console.WriteLine(ex.Message);
            }
        }
        return value; // Return the valid numeric input
    }

    /* method to select a category from a dictionary of categories provided by the user.
    The categories parameter is a dictionary of category names and their associated lists.
    Prompts the user to choose from the available categories or exit.
    Returns the name of the selected category, or null if the user chooses to exit. */
    public static string SelectCategory(IReadOnlyDictionary<string, List<string>> categories)
    {
        Console.WriteLine("Select a category:");
        int index = 1;
        Dictionary<int, string> indexToCategoryMap = new Dictionary<int, string>();

        // Display available categories to user
        foreach (var category in categories.Keys)
        {
            Console.WriteLine($"{index}. {category}");
            indexToCategoryMap[index] = category;
            index++;
        }
        Console.WriteLine($"{index}. Exit");  // Add an option for the user to exit the category selection

        while (true)
        {
             // read user input and try to parse it as an integer
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= indexToCategoryMap.Count + 1)
            {
                if (choice == index)// check if the user selected the last option (exit)
                {
                    return null; // indicating exit
                }
                return indexToCategoryMap[choice]; // return the selected category from the mapping dictionary
            }
            else
            { // if input is invalid, prompt the user again
                Console.WriteLine("invalid choice. please select a valid category number.");
            }
        }
    }

    /* Method to select a product from a list of available products for a given category.
    The category parameter is the name of the category from which the user wants to select a product.
    The productCategories parameter is a dictionary of categories with their associated products.
    Prompts the user to choose a product from the given category or list the available products again.
    Returns the name of the selected product if found in the available products list. 
    This function is used for both ProductsList class and EnergyCalculator class*/
    public static string SelectProduct(string category, IReadOnlyDictionary<string, List<string>> productCategories)
    {
        // get the list of available products for the selected category
        List<string> availableProducts = productCategories[category];
        
        Console.WriteLine($"available products in {category}:"); // display available products to the user
        foreach (string product in availableProducts)
        {
            Console.WriteLine($"- {product}");
        }
        while (true) // loop until a valid product selection is made
        {
            // prompt the user to enter the product name or type 'list' to see the products again
            Console.WriteLine("enter the product name (or type 'list' to see available products again):");
            string productName = Console.ReadLine()?.Trim();
            
            // if the user types 'list', display the available products again
            if (productName.Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                foreach (string product in availableProducts) // loop through the list of available products and print each product
                {
                    Console.WriteLine($"- {product}");
                }
            }
            else if (availableProducts.Any(product => product.Equals(productName, StringComparison.OrdinalIgnoreCase)))
            {
                // if a valid product name is found, return it
                return productName;
            }
            else
            {
                // if no valid product name is found, notify the user
                Console.WriteLine("invalid product name. please enter a valid product from the list.");
            }
        }
    }
}

/* Dedicated utility class for our dictionaries which hold all our information. Placed in separate class in order to adhere to
    the encapsulation and abstraction principles, ease of development and editing, and readability of the program. This class
    utilizes private dictionaries with getters in order to decouple the dictionaries which can only be edited via code. justification
    for this is as follows: either the program reads from an external file, or if the options are basic or limited (perhaps due to
    development cycle constraints or future development), i have opted for this method. this method allows for the basic and sufficient
    use of the program without having dictionaries be ridiculously long. this can be edited simply by inserting another value into the 
    key that is categories. this method showcases important oop principles and neatly factors the code efficiently without the need for
    an external file. i believe, unless the program because any larger in scale than what is shown, the use of external files are not strictly
    necessary. static and readonly means code in other parts of the program will not be able to add to this so developers can exercise their
    privilege to extend the programs scope for products.
*/
public static class ProductDataProvider
{
    // Dictionary of categories with available products in each category
    private static readonly Dictionary<string, List<string>> productCategories = new Dictionary<string, List<string>>()
    {
        { "Fruit & Vegetables", new List<string> { "Apple", "Banana", "Orange", "Carrot", "Broccoli", "Potato", "Tomato", "Onion", "Lettuce" } },
        { "Bakery", new List<string> { "Bread", "Croissant", "Bagel", "Muffin", "Scone", "Cake", "Baguette" } },
        { "Dairy", new List<string> { "Milk", "Cheese", "Yogurt", "Butter", "Cream", "Ice Cream" } }
    };
    // Dictionary of appliance categories with available appliances in each category.
    private static readonly Dictionary<string, List<string>> applianceCategories = new Dictionary<string, List<string>>()
    {
        { "Kitchen Appliances", new List<string> { "Kettle", "Toaster", "Microwave", "Fridge", "Oven", "Blender", "Coffee Maker", "Slow Cooker", "Rice Cooker" } },
        { "Laundry & Cleaning Appliances", new List<string> { "Washing Machine", "Dryer", "Vacuum Cleaner", "Robot Vacuum", "Steam Cleaner", "Iron" } },
        { "Heating & Cooling Appliances", new List<string> { "Heater", "Air Conditioner", "Fan", "Humidifier", "Dehumidifier", "Air Purifier" } }
    };
    // Provide methods to return read-only versions of the dictionaries
    public static IReadOnlyDictionary<string, List<string>> GetProductCategories()
    {
        return productCategories;
    }
    public static IReadOnlyDictionary<string, List<string>> GetApplianceCategories()
    {
        return applianceCategories;
    }
}