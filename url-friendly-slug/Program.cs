/*
    Copyright (c) Johan Boström. All rights reserved.
    Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
*/

using System;

namespace UrlFriendlySlug
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string text = "ICH MUß EINIGE CRÈME BRÛLÉE HABEN,,,A..1/2\\3=4-5 6_7.AA.. 0..1..3.4..56.7 HÄR FÅR DU DET !\"#¤%&/()=? đàåáâäãåąèéêëęìíîïıòóôõöøőðùúûüŭůçćčĉżźžśşšŝñńýÿğĝřłđßÞĥĵ...";

            Console.WriteLine($"Original string: {text}");
            Console.WriteLine(text);

            Console.WriteLine($"Url Friendly (MAX):");
            Console.WriteLine(StringHelper.URLFriendly(text));

            Console.WriteLine($"Url Friendly (20 chars):");
            Console.WriteLine(StringHelper.URLFriendly(text, 20));

            Console.WriteLine($"Url Friendly (80 chars):");
            Console.WriteLine(StringHelper.URLFriendly(text, 80));

            Console.ReadLine();
        }
    }
}