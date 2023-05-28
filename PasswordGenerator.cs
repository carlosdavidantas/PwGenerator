using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class PasswordGenerator
{
    private int _PasswordLength;
    private int _SpecialChars;
    private int _HighCaseChars;
    private int _NumbersChars;
    private int _LowCaseChars;
    private Random random;
    private List<int> FreePasswordPositions;
    private readonly List<string> Chars = new List<string>
    {
        "abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "@#$%&*", "0123456789"
    };

    public PasswordGenerator(int passwordLength, int specialCharCount, int highCaseCharCount, int numbersCount)
    {
        _PasswordLength = passwordLength;
        _SpecialChars = specialCharCount;
        _HighCaseChars = highCaseCharCount;
        _NumbersChars = numbersCount;
        random = new Random();
        FreePasswordPositions = new List<int>();

        for (int i = 0; i < passwordLength; i++)
        {
            FreePasswordPositions.Add(i);
        }

        int n = specialCharCount + highCaseCharCount + numbersCount;
        if (n > passwordLength)
        {
            throw new ArgumentException("Password count exceeded!");
        }
        _LowCaseChars = _PasswordLength - (_SpecialChars + _HighCaseChars + _NumbersChars);
    }

    public string Generate()
    {
        char[] password = new char[_PasswordLength];
        while (_LowCaseChars > 0)
            InsertRandomChar(password, 0);
        while (_HighCaseChars > 0)
            InsertRandomChar(password, 1);
        while (_SpecialChars > 0)
            InsertRandomChar(password, 2);
        while (_NumbersChars > 0)
            InsertRandomChar(password, 3);

        string pw = "";
        for (int i = 0; i < password.Length; i++)
        {
            pw += password[i];
        }
        return pw;
    }

    private char PickChar(int charType)
    {
        char c = Chars[charType][random.Next(0, Chars[charType].Length)];
        return c;
    }

    private void InsertRandomChar(char[] password, int charType)
    {
        if (charType > 3 || charType < 0)
        {
            throw new ArgumentOutOfRangeException("The argument 'charType' can't be higher than 3 or smaller than 0!");
        }

        int position = random.Next(0, FreePasswordPositions.Count);
        char newChar = PickChar(charType);
        password[FreePasswordPositions[position]] = newChar;
        FreePasswordPositions.Remove(FreePasswordPositions[position]);

        if (charType == 0)
            _LowCaseChars -= 1;
        if (charType == 1)
            _HighCaseChars -= 1;
        if (charType == 2)
            _SpecialChars -= 1;
        if (charType == 3)
            _NumbersChars -= 1;
    }
}

