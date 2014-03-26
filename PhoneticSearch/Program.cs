using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneticSearch
{
    class Program
    {
        static char[] charactersToIgnore = { 'a', 'e', 'i', 'h', 'o', 'u', 'y' };
        static char[][] equivalentLetterSets = new char[5][];

        static void Main(string[] args)
        {
            // Start a timer so we can work out how long the calculations took
            Stopwatch timer = Stopwatch.StartNew(); 

            // Initialise the equivalent letter sets as per task sheet
            equivalentLetterSets[0] = new char[5] { 'a', 'e', 'i', 'o', 'u' };
            equivalentLetterSets[1] = new char[9] { 'c', 'g', 'j', 'k', 'q', 's', 'x', 'y', 'z' };
            equivalentLetterSets[2] = new char[5] { 'b', 'f', 'p', 'v', 'w' };
            equivalentLetterSets[3] = new char[2] { 'd', 't' };
            equivalentLetterSets[4] = new char[2] { 'm', 'n' };

            ArrayList surnames = new ArrayList(); // Will hold all the surnames in the input text file
            string line;

            // Read file into an arraylist of strings
            while ((line = Console.In.ReadLine()) != null) { surnames.Add(line.Trim()); } // Trim to get rid of whitespace before and after 

            foreach (string nameA in args)
            {
                if (nameA == "<") { break; } // Needed when debugging in Visual Studio (standard input is seen as an argument)
                Console.Out.WriteLine(""); // Spacing

                Console.Out.Write(nameA + ": ");

                bool firstMatch = true; // Just used for working out if we need to precede the match with a comma
                foreach (string nameB in surnames)
                {
                    if (isPhoneticMatch(nameA, nameB))
                    {
                        // There is a match, write the result to the console
                        if (!firstMatch) { Console.Out.Write(", "); } // Write a comma and space if no the first match
                        Console.Out.Write(nameB);
                        firstMatch = false; // No longer the first match
                    }
                }
            }

            Console.Out.WriteLine(""); // Spacing
            Console.Out.WriteLine("");

            // Calculate the amount of time taken and write to the console
            Console.Out.WriteLine(timer.Elapsed + " | " + timer.ElapsedTicks + " ticks"); 
        }

        /// <summary>
        /// This method uses the matching algorithm outlined in the task sheet to compare the two provided names.
        /// 
        /// 1. All non-alphabetic characters are ignored.
        /// 2. Word case is not significant
        /// 3. After the first letter, any of the following letters are discarded: A, E, I, H, O, U, W, Y.
        /// 4. The following sets of letters are considered equivalent
        /// 
        ///     - A, E, I, O, U
        ///     - C, G, J, K, Q, S, X, Y, Z
        ///     - B, F, P, V, W
        ///     - D, T
        ///     - M, N
        ///     - All others have no equivalent
        ///     
        /// 5. Any consecutive occurrences of equivalent letters (after discarding letters in step 3) are considered as a single occurrence
        /// </summary>
        /// <param name="nameA">The surname from the arguments</param>
        /// <param name="nameB">The surname from the input file</param>
        /// <returns>a bool indicating if there was a match</returns>
        static bool isPhoneticMatch(string nameA, string nameB)
        {
            // Varialbles to build each string to match, start off empty
            var tempNameA = new List<char>();
            var tempNameB = new List<char>();

            char previousCharacter = '\0'; // The previous character starts off as '\0' (ascii null)
            for (int i = 0; i < nameA.Length; i++) // For every character in the name from arguments
            {
                char a = Char.ToLower(nameA[i]); // Get character and ignore case (lower case everything)
                bool validCharacter = false; // Not valid by default

                // If its the first character, only check that it is alphabetic
                if (i == 0 && Char.IsLetter(a)) { validCharacter = true; }
                // Otherwise check if it's not a character to ignore and alphabetic
                else if (!charactersToIgnore.Contains(a) && Char.IsLetter(a)) 
                {
                    // Check that it to make sure it isn't a consecutive occurence of an equivalent letter
                    validCharacter = !isEquivalent(a, previousCharacter);
                }

                // If the character was valid, add it to our array to be compared
                if (validCharacter) { tempNameA.Add(a); }

                // Save the previous character to be evaluated next loop
                previousCharacter = a;
            }

            previousCharacter = '\0'; // The previous character starts off as '\0' (ascii null)
            for (int i = 0; i < nameB.Length; i++) // For every character in the name from text input
            {
                char b = Char.ToLower(nameB[i]); // Get the character and ignore case (lower case everything)
                bool validCharacter = false; // Not valid by default

                // If its the first character, we only check that it is alphabetic
                if (i == 0 && Char.IsLetter(b)) { validCharacter = true; }
                // Otherwise check if it's not a character to ignore and alphabetic
                else if (!charactersToIgnore.Contains(b) && Char.IsLetter(b))
                {
                    // Check that it to make sure it isn't a consecutive occurence of an equivalent letter
                    validCharacter = !isEquivalent(b, previousCharacter);
                }

                // If it is a valid character and there is a matching character in name A to compare it to
                if (validCharacter && tempNameB.Count < tempNameA.Count)
                {
                    char a = Char.ToLower(tempNameA[tempNameB.Count]); // Get character and lowercase it
                    
                    // If characters aren't equal and they are considered equivalent
                    // use the letter in name A instead
                    if (!(a == b) && isEquivalent(a, b)) { b = a; }
                }

                // If it is valid, use it!
                if (validCharacter) { tempNameB.Add(b); }

                // Save the previous character to be evaluated next loop
                previousCharacter = b;
            }

            // Turn temp variables into proper strings ready for comparison
            nameA = new string(tempNameA.ToArray());
            nameB = new string(tempNameB.ToArray());

            // Return the match
            return (nameA == nameB);
        }

        /// <summary>
        /// This method loops through the sets to check to see if the two characters should be considered equivalent
        /// </summary>
        /// <param name="a">First character to compare</param>
        /// <param name="b">Second character to compare</param>
        /// <returns>true if they are considered equivalent</returns>
        static bool isEquivalent(char a, char b)
        {
            bool result = false;

            // Loop around each set of equivalent characters
            foreach (char[] set in equivalentLetterSets)
            {
                // If the preceding character and this character are in the same set, ignore this character
                if (set.Contains(a) && set.Contains(b))
                {
                    // These letters are equivalent
                    result = true; break;
                }
            }

            return result;
        }
    }
}
