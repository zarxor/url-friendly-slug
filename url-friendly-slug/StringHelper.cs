/*
    Copyright (c) Johan Boström. All rights reserved.
    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
*/

using System.Globalization;
using System.Text;

namespace UrlFriendlySlug
{
    public static class StringHelper
    {
        /// <summary>
        /// Creates a URL And SEO friendly slug
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string URLFriendly(string text, int maxLength = 0)
        {
            // Return empty value if text is null
            if (text == null) return "";

            // Normalize the text
            var normalizedString = text
                .ToLowerInvariant()
                .Normalize(NormalizationForm.FormD);

            var stringBuilder = new StringBuilder();
            var stringLength = normalizedString.Length;
            bool prevdash = false;
            char c;
            var trueLength = 0;

            for (int i = 0; i < stringLength; i++)
            {
                c = normalizedString[i];

                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    // Check if the character is a letter or a digit
                    // if the character is a international character
                    // remap it to an ascii valid character
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (c < 128)
                            stringBuilder.Append(c);
                        else
                            stringBuilder.Append(ConstHelper.RemapInternationalCharToAscii(c));

                        prevdash = false;
                        trueLength = stringBuilder.Length;
                        continue;
                    
                    // Check if the character is to be replaced by a -
                    // but only if the last character wasn't
                    case UnicodeCategory.SpaceSeparator:
                    case UnicodeCategory.ConnectorPunctuation:
                    case UnicodeCategory.DashPunctuation:
                    case UnicodeCategory.OtherPunctuation:
                    case UnicodeCategory.MathSymbol:
                        if (!prevdash)
                        {
                            stringBuilder.Append('-');
                            prevdash = true;
                            trueLength = stringBuilder.Length;
                        }
                        continue;
                }
                
                // If we are at max length, stop parsing
                if (maxLength > 0 && trueLength >= maxLength)
                    break;
            }

            var result = stringBuilder.ToString().Trim('-');

            // Remove any excess character to meet maxlength criteria
            return maxLength <= 0 || result.Length <= maxLength ? result : result.Substring(0, maxLength);
        }
    }
}