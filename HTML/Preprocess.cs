using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTML
{
    // An HTML parsing preprocessing channel to collapse white space and convert
    // escape sequences back to plain characters
    public class HtmlPreprocess
    {
        public const char NullChar = '\x00';

        string iEscapeSequence;
        bool iLastWasWhiteSpace;
        bool iAlreadyHadNonWhiteSpace;

        public HtmlPreprocess()
        {
            Reset();
        }

        public void Reset()
        {
            iEscapeSequence = string.Empty;
            iLastWasWhiteSpace = false;
            iAlreadyHadNonWhiteSpace = false;
        }

        // Add one char to the input stream.
        // Return one char or 0 after preprocessing.
        public char Add(char aIncoming)
        {
            if (char.IsWhiteSpace(aIncoming))
            {
                if (iLastWasWhiteSpace)
                {
                    // Collapse successive white space characters
                    return NullChar;
                }
                else if (char.IsControl(aIncoming) && !iAlreadyHadNonWhiteSpace)
                {
                    // Do not count control characters (particularly newline) as white space 
                    // unless we have already had white space.
                    // This is so that a leading newline does not cause extra space padding at
                    // the beginning of a line.
                    return NullChar;
                }
                else
                {
                    // Start of new white space sequence
                    iLastWasWhiteSpace = true;
                    return ' ';
                }
            }
            else if (aIncoming == '&')
            {
                // Start of escape sequence
                iEscapeSequence += aIncoming;
                iAlreadyHadNonWhiteSpace = true;
                return NullChar;
            }
            else if (iEscapeSequence.Length > 0)
            {
                // Building escape sequence
                if (aIncoming == ';')
                {
                    // End of escape sequence
                    if (iEscapeSequence.Equals("&amp", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Reset();
                        return '&';
                    }
                    else if (iEscapeSequence.Equals("&nbsp", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Reset();
                        return ' ';
                    }
                    else if (iEscapeSequence.Equals("&lt", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Reset();
                        return '<';
                    }
                    else if (iEscapeSequence.Equals("&gt", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Reset();
                        return '>';
                    }
                    else if (iEscapeSequence.Equals("&quot", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Reset();
                        return '"';
                    }
                    else
                    {
                        // There are plenty of others - not convinced they are used much
                        // (http://www.hybridelephant.com/computer/tutorial/spechar.html)
                        Reset();
                        return '?';
                    }
                }
                else
                {
                    iEscapeSequence += aIncoming;
                    return NullChar;
                }
            }
            else
            {
                // Pass non-escaped visible characters through
                iLastWasWhiteSpace = false;
                iAlreadyHadNonWhiteSpace = true;
                return aIncoming;
            }
        }
    }
}
