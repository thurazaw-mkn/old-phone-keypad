using System;
using System.Collections.Generic;
using System.Text;

public class OldPhoneKeypad
{
    private static readonly Dictionary<char, string> keyMap = new Dictionary<char, string>
    {
        { '2', "ABC" },
        { '3', "DEF" },
        { '4', "GHI" },
        { '5', "JKL" },
        { '6', "MNO" },
        { '7', "PQRS" },
        { '8', "TUV" },
        { '9', "WXYZ" }
    };

    private static string ConvertInputToText(string input)
    {
        if (input.Length == 0) return string.Empty;

        char key = input[0];
        int count = input.Length;

        if (keyMap.ContainsKey(key))
        {
            string letters = keyMap[key];
            int index = (count - 1) % letters.Length;
            return letters[index].ToString();
        }
        return string.Empty;
    }
    
    public static string OldPhonePad(string input)
    {
        StringBuilder output = new StringBuilder();
        StringBuilder currentInput = new StringBuilder();
        char? lastKey = null;
        List<string> sequences = new List<string>();

        foreach (char c in input)
        {
            if (c == '#') 
            {
                if (currentInput.Length > 0)
                {
                    output.Append(ConvertInputToText(currentInput.ToString()));
                }
                break;
            }
            else if (c == ' ') 
            {
                if (currentInput.Length > 0)
                {
                    output.Append(ConvertInputToText(currentInput.ToString()));
                    sequences.Add(currentInput.ToString());
                    currentInput.Clear();
                }
                lastKey = null; 
            }
            else if (c == '*') 
            {
                if (sequences.Count > 0)
                {
                    string lastSequence = sequences[sequences.Count - 1];
                    if (currentInput.Length >= lastSequence.Length)
                    {
                        currentInput.Remove(currentInput.Length - lastSequence.Length, lastSequence.Length);
                    }
                    else
                    {
                        currentInput.Length--;
                    }
                    sequences.RemoveAt(sequences.Count - 1);
                }
            }
            else if (keyMap.ContainsKey(c))
            {
                if (lastKey == c) 
                {
                    currentInput.Append(c);
                }
                else
                {
                    if (currentInput.Length > 0)
                    {
                        output.Append(ConvertInputToText(currentInput.ToString()));
                        sequences.Add(currentInput.ToString());
                        currentInput.Clear();
                    }
                    currentInput.Append(c);
                }
                lastKey = c; 
            }
            else
            {
                output.Append("Invalid Input (" + c + ")");
            }
        }

        return output.ToString();
    }
    
    public static void Main(string[] args)
    {
        // Examples
        Console.WriteLine(OldPhonePad("33#")); // -> E
        Console.WriteLine(OldPhonePad("227*#")); // -> B
        Console.WriteLine(OldPhonePad("4433555 555666#")); // -> HELLO
        Console.WriteLine(OldPhonePad("8 88777444666*664#")); // -> TURING
    }
}