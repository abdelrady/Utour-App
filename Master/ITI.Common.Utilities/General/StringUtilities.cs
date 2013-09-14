using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace ITI.Common.Utilities.General
{
    /// <summary>
    /// Miscellaneous <see cref="System.String"/> utility methods.
    /// </summary>
    /// <remarks>
    /// <p>
    /// Mainly for string validation.
    /// </p>
    /// </remarks>
    /// <author>Ahmed Al Amir</author>	
    public static class StringUtilities
    {
        #region -- Constants --

        /// <summary>
        /// The string that signals the start of an Ant-style expression.
        /// </summary>
        private const string AntExpressionPrefix = "${";

        /// <summary>
        /// The string that signals the end of an Ant-style expression.
        /// </summary>
        private const string AntExpressionSuffix = "}";

        #endregion

        #region -- Global Varaibles --
        /// <summary>
        /// An empty array of <see cref="System.String"/> instances.
        /// </summary>
        public static readonly string[] EmptyStrings = new string[] { };
        #endregion        

        #region -- Static Methods --

        /// <summary>
        /// Extentions method that Tokenize the given <see cref="System.String"/> into a
        /// <see cref="System.String"/> array.
        /// </summary>
        /// <remarks>
        /// <p>
        /// If <paramref name="s"/> is <see langword="null"/>, returns an empty
        /// <see cref="System.String"/> array.
        /// </p>
        /// <p>
        /// If <paramref name="delimiters"/> is <see langword="null"/> or the empty
        /// <see cref="System.String"/>, returns a <see cref="System.String"/> array with one
        /// element: <paramref name="s"/> itself.
        /// </p>
        /// </remarks>
        /// <param name="s">The <see cref="System.String"/> to tokenize.</param>
        /// <param name="delimiters">
        /// The delimiter characters, assembled as a <see cref="System.String"/>.
        /// </param>
        /// <param name="trimTokens">
        /// Trim the tokens via <see cref="System.String.Trim()"/>.
        /// </param>
        /// <param name="ignoreEmptyTokens">
        /// Omit empty tokens from the result array.</param>
        /// <returns>An array of the tokens.</returns>
        public static string[] Split(
            this string s, string delimiters, bool trimTokens, bool ignoreEmptyTokens)
        {
            if (s == null)
            {
                return new string[0];
            }
            if (StringUtilities.IsNullOrEmptyString(delimiters))
            {
                return new string[] { s };
            }
            string[] tmp = s.Split(delimiters.ToCharArray());
            // short circuit if String.Split default behavior is ok
            if (!trimTokens && !ignoreEmptyTokens)
            {
                return tmp;
            }
            else
            {
                ArrayList tokens = new ArrayList(tmp.Length);
                for (int i = 0; i < tmp.Length; ++i)
                {
                    string token = (trimTokens ? tmp[i].Trim() : tmp[i]);
                    if (!(ignoreEmptyTokens && token.Length == 0))
                    {
                        tokens.Add(token);
                    }
                }
                return (string[])tokens.ToArray(typeof(string));
            }
        }

        /// <summary>
        /// Extentions method that Convert a CSV list into an array of <see cref="System.String"/>s.
        /// </summary>
        /// <param name="s">A CSV list.</param>
        /// <returns>
        /// An array of <see cref="System.String"/>s, or the empty array
        /// if <paramref name="s"/> is <see langword="null"/>.
        /// </returns>
        public static string[] CommaDelimitedListToStringArray(this string s)
        {
            return DelimitedListToStringArray(s, ",");
        }

        /// <summary>
        /// Extentions method that Take a <see cref="System.String"/> which is a delimited list
        /// and convert it to a <see cref="System.String"/> array.
        /// </summary>
        /// <remarks>
        /// <p>
        /// If the supplied <paramref name="delimiter"/> is a
        /// <cref lang="null"/> or zero-length string, then a single element
        /// <see cref="System.String"/> array composed of the supplied
        /// <paramref name="input"/> <see cref="System.String"/> will be 
        /// eturned. If the supplied <paramref name="input"/>
        /// <see cref="System.String"/> is <cref lang="null"/>, then an empty,
        /// zero-length <see cref="System.String"/> array will be returned.
        /// </p>
        /// </remarks>
        /// <param name="input">
        /// The <see cref="System.String"/> to be parsed.
        /// </param>
        /// <param name="delimiter">
        /// The delimeter (this will not be returned). Note that only the first
        /// character of the supplied <paramref name="delimiter"/> is used.
        /// </param>
        /// <returns>
        /// An array of the tokens in the list.
        /// </returns>
        public static string[] DelimitedListToStringArray(this string input,string delimiter)
        {
            if (input == null)
            {
                return new string[0];
            }
            if (!HasLength(delimiter))
            {
                return new string[] { input };
            }
            return input.Split(delimiter[0]);
        }

        /// <summary>
        /// Extentions method that Convenience method to return an
        /// <see cref="System.Collections.ICollection"/> as a delimited
        /// (e.g. CSV) <see cref="System.String"/>.
        /// </summary>
        /// <param name="c">
        /// The <see cref="System.Collections.ICollection"/> to parse.
        /// </param>
        /// <param name="delimiter">
        /// The delimiter to use (probably a ',').
        /// </param>
        /// <returns>The delimited string representation.</returns>
        public static string CollectionToDelimitedString(
            this string delimiter ,ICollection c)
        {
            if (c == null)
            {
                return "null";
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (object obj in c)
            {
                if (i++ > 0)
                {
                    sb.Append(delimiter);
                }
                sb.Append(obj);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convenience method to return an
        /// <see cref="System.Collections.ICollection"/> as a CSV
        /// <see cref="System.String"/>.
        /// </summary>
        /// <param name="collection">
        /// The <see cref="System.Collections.ICollection"/> to display.
        /// </param>
        /// <returns>The delimited string representation.</returns>
        public static string CollectionToCommaDelimitedString(
            this ICollection collection)
        {
            return CollectionToDelimitedString(",", collection);
        }

        /// <summary>
        /// Convenience method to return an array as a CSV
        /// <see cref="System.String"/>.
        /// </summary>
        /// <param name="source">
        /// The array to parse. Elements may be of any type (
        /// <see cref="System.Object.ToString"/> will be called on each
        /// element).
        /// </param>
        public static string ArrayToCommaDelimitedString(this object[] source)
        {
            return ArrayToDelimitedString("," ,source);
        }

        /// <summary>
        /// Convenience method to return a <see cref="System.String"/>
        /// array as a delimited (e.g. CSV) <see cref="System.String"/>.
        /// </summary>
        /// <param name="source">
        /// The array to parse. Elements may be of any type (
        /// <see cref="System.Object.ToString"/> will be called on each
        /// element).
        /// </param>
        /// <param name="delimiter">
        /// The delimiter to use (probably a ',').
        /// </param>
        public static string ArrayToDelimitedString(
            this string delimiter, object[] source)
        {
            if (source == null)
            {
                return "null";
            }
            else
            {
                return StringUtilities.CollectionToDelimitedString(delimiter, source);
            }
        }

        /// <summary>Checks if a string has length.</summary>
        /// <param name="target">
        /// The string to check, may be <see langword="null"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the string has length and is not
        /// <see langword="null"/>.
        /// </returns>
        /// <example>
        /// <code lang="C#">
        /// StringUtilities.HasLength(null) = false
        /// StringUtilities.HasLength("") = false
        /// StringUtilities.HasLength(" ") = true
        /// StringUtilities.HasLength("Hello") = true
        /// </code>
        /// </example>
        public static bool HasLength(this string target)
        {
            return (target != null && target.Length > 0);
        }

        /// <summary>
        /// Checks if a <see cref="System.String"/> has text.
        /// </summary>
        /// <remarks>
        /// <p>
        /// More specifically, returns <see langword="true"/> if the string is
        /// not <see langword="null"/>, it's <see cref="String.Length"/> is >
        /// zero <c>(0)</c>, and it has at least one non-whitespace character.
        /// </p>
        /// </remarks>
        /// <param name="target">
        /// The string to check, may be <see langword="null"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="target"/> is not
        /// <see langword="null"/>,
        /// <see cref="String.Length"/> > zero <c>(0)</c>, and does not consist
        /// solely of whitespace.
        /// </returns>
        /// <example>
        /// <code language="C#">
        /// StringUtilities.HasText(null) = false
        /// StringUtilities.HasText("") = false
        /// StringUtilities.HasText(" ") = false
        /// StringUtilities.HasText("12345") = true
        /// StringUtilities.HasText(" 12345 ") = true
        /// </code>
        /// </example>
        public static bool HasText(this string target)
        {
            if (target == null)
            {
                return false;
            }
            else
            {
                return HasLength(target.Trim());
            }
        }

        /// <summary>
        /// Checks if a <see cref="System.String"/> is <see langword="null"/>
        /// or an empty string.
        /// </summary>
        /// <remarks>
        /// <p>
        /// More specifically, returns <see langword="false"/> if the string is
        /// <see langword="null"/>, it's <see cref="String.Length"/> is equal
        /// to zero <c>(0)</c>, or it is composed entirely of whitespace
        /// characters.
        /// </p>
        /// </remarks>
        /// <param name="target">
        /// The string to check, may (obviously) be <see langword="null"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="target"/> is
        /// <see langword="null"/>, has a length equal to zero <c>(0)</c>, or
        /// is composed entirely of whitespace characters.
        /// </returns>
        /// <example>
        /// <code language="C#">
        /// StringUtilities.IsNullOrEmpty(null) = true
        /// StringUtilities.IsNullOrEmpty("") = true
        /// StringUtilities.IsNullOrEmpty(" ") = true
        /// StringUtilities.IsNullOrEmpty("12345") = false
        /// StringUtilities.IsNullOrEmpty(" 12345 ") = false
        /// </code>
        /// </example>
        public static bool IsNullOrEmptyString(this string target)
        {
            return !HasText(target);
        }

        /// <summary>
        /// Strips first and last character off the string.
        /// </summary>
        /// <param name="text">The string to strip.</param>
        /// <returns>The stripped string.</returns>
        public static string StripFirstAndLastCharacter(this string text)
        {
            if (text != null
                && text.Length > 2)
            {
                return text.Substring(1, text.Length - 2);
            }
            else
            {
                return System.String.Empty;
            }
        }

        /// <summary>
        /// Returns a list of Ant-style expressions from the specified text.
        /// </summary>
        /// <param name="text">The text to inspect.</param>
        /// <returns>
        /// A list of expressions that exist in the specified text.
        /// </returns>
        /// <exception cref="System.FormatException">
        /// If any of the expressions in the supplied <paramref name="text"/>
        /// is empty (<c>${}</c>).
        /// </exception>
        public static IList GetAntExpressions(this string text)
        {
            IList expressions = new ArrayList();
            if (StringUtilities.HasText(text))
            {
                int start = text.IndexOf(AntExpressionPrefix);
                while (start >= 0)
                {
                    int end = text.IndexOf(AntExpressionSuffix, start + 2);
                    if (end == -1)
                    {
                        // terminator character not found, so let's quit...
                        start = -1;
                    }
                    else
                    {
                        string exp = text.Substring(start + 2, end - start - 2);
                        if (StringUtilities.IsNullOrEmptyString(exp))
                        {
                            throw new FormatException(
                                string.Format("Empty {0}{1} value found in text : '{2}'.",
                                              AntExpressionPrefix,
                                              AntExpressionSuffix,
                                              text));
                        }
                        if (expressions.IndexOf(exp) < 0)
                        {
                            expressions.Add(exp);
                        }
                        start = text.IndexOf(AntExpressionPrefix, end);
                    }
                }
            }
            return expressions;
        }

        /// <summary>
        /// Replaces Ant-style expression placeholder with expression value.
        /// </summary>
        /// <remarks>
        /// <p>
        /// 
        /// </p>
        /// </remarks>
        /// <param name="text">The string to set the value in.</param>
        /// <param name="expression">The name of the expression to set.</param>
        /// <param name="expValue">The expression value.</param>
        /// <returns>
        /// A new string with the expression value set; the
        /// <see cref="String.Empty"/> value if the supplied
        /// <paramref name="text"/> is <see langword="null"/>, has a length
        /// equal to zero <c>(0)</c>, or is composed entirely of whitespace
        /// characters.
        /// </returns>
        public static string SetAntExpression(this string text, string expression, object expValue)
        {
            if (StringUtilities.IsNullOrEmptyString(text))
            {
                return System.String.Empty;
            }
            if (expValue == null)
            {
                expValue = System.String.Empty;
            }
            return text.Replace(
                StringUtilities.Surround(AntExpressionPrefix, expression, AntExpressionSuffix), expValue.ToString());
        }

        /// <summary>
        /// Surrounds (prepends and appends) the string value of the supplied
        /// <paramref name="fix"/> to the supplied <paramref name="target"/>.
        /// </summary>
        /// <remarks>
        /// <p>
        /// The return value of this method call is always guaranteed to be non
        /// <see langword="null"/>. If every value passed as a parameter to this method is
        /// <see langword="null"/>, the <see cref="System.String.Empty"/> string will be returned.
        /// </p>
        /// </remarks>
        /// <param name="fix">
        /// The pre<b>fix</b> and suf<b>fix</b> that respectively will be prepended and
        /// appended to the target <paramref name="target"/>. If this value
        /// is not a <see cref="System.String"/> value, it's attendant
        /// <see cref="System.Object.ToString()"/> value will be used.
        /// </param>
        /// <param name="target">
        /// The target that is to be surrounded. If this value is not a
        /// <see cref="System.String"/> value, it's attendant
        /// <see cref="System.Object.ToString()"/> value will be used.
        /// </param>
        /// <returns>The surrounded string.</returns>
        public static string Surround(this object fix, object target)
        {
            return StringUtilities.Surround(fix, target, fix);
        }

        /// <summary>
        /// Surrounds (prepends and appends) the string values of the supplied
        /// <paramref name="prefix"/> and <paramref name="suffix"/> to the supplied
        /// <paramref name="target"/>.
        /// </summary>
        /// <remarks>
        /// <p>
        /// The return value of this method call is always guaranteed to be non
        /// <see langword="null"/>. If every value passed as a parameter to this method is
        /// <see langword="null"/>, the <see cref="System.String.Empty"/> string will be returned.
        /// </p>
        /// </remarks>
        /// <param name="prefix">
        /// The value that will be prepended to the <paramref name="target"/>. If this value
        /// is not a <see cref="System.String"/> value, it's attendant
        /// <see cref="System.Object.ToString()"/> value will be used.
        /// </param>
        /// <param name="target">
        /// The target that is to be surrounded. If this value is not a
        /// <see cref="System.String"/> value, it's attendant
        /// <see cref="System.Object.ToString()"/> value will be used.
        /// </param>
        /// <param name="suffix">
        /// The value that will be appended to the <paramref name="target"/>. If this value
        /// is not a <see cref="System.String"/> value, it's attendant
        /// <see cref="System.Object.ToString()"/> value will be used.
        /// </param>
        /// <returns>The surrounded string.</returns>
        public static string Surround(this object prefix, object target, object suffix)
        {
            return string.Format(
                CultureInfo.InvariantCulture, "{0}{1}{2}", prefix, target, suffix);
        }

        /// <summary>
        /// Converts escaped characters (for example "\t") within a string
        /// to their real character.
        /// </summary>
        /// <param name="inputString">The string to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ConvertEscapedCharacters(this string inputString)
        {
            StringBuilder sb = new StringBuilder(inputString.Length);
            for (int i = 0; i < inputString.Length; i++)
            {
                if (inputString[i].Equals('\\'))
                {
                    i++;
                    if (inputString[i].Equals('t'))
                    {
                        sb.Append('\t');
                    }
                    else if (inputString[i].Equals('r'))
                    {
                        sb.Append('\r');
                    }
                    else if (inputString[i].Equals('n'))
                    {
                        sb.Append('\n');
                    }
                    else if (inputString[i].Equals('\\'))
                    {
                        sb.Append('\\');
                    }
                    else
                    {
                        sb.Append("\\" + inputString[i]);
                    }
                }
                else
                {
                    sb.Append(inputString[i]);
                }
            }
            return sb.ToString();
        }

        #endregion
    }
}