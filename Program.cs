﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Permutations512
{
    class Program
    {
        /// <summary>
        /// Кол-во различных слов, которые можно получить перестановкой букв слова a, подходящих под условие
        /// </summary>
        private static int Count = 0;
        
        // Для 5 варианта (вынес ради экономии памяти)
        private static readonly string vowels = "аеёиоуыэюя";
        private static readonly string consonants = "бвгджзйклмнпрстфхцчшщьъ";
        private static readonly string pattern = $@"\w*([{vowels}][{consonants}][{consonants}][{vowels}])\w*";
        // ============================================
        
        static void Main(string[] args)
        {
            string word = "пастух"; // Меняем на нужное нам слово
            int len = word.Length;

            var group = word.GroupBy(g => g);
            
            char[] letters = new char[group.Count()]; // Все буквы в определённом порядке
            int[] countLetters = new int[group.Count()]; // Кол-во букв letters[i] в слове

            for (int i = 0; i < group.Count(); i++)
            {
                letters[i] = group.ElementAt(i).Key;
                countLetters[i] = group.ElementAt(i).Count();
            }

            Stopwatch stopWatch = new Stopwatch();
            TimeSpan ts;

            stopWatch.Start();
            GetCountPermutations("", ref letters, ref countLetters, ref len);
            Console.WriteLine($"Кол-во перестановок подходящее под условие = {Count}");
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine("Время выполнения = {0:00}:{1:00}.{2:00}",ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            
            Console.ReadLine();
        }
        
        /// <summary>
        /// Тут без полного понимания лучше ничего не трогать
        /// </summary>
        static void GetCountPermutations(string per, ref char[] letters, ref int[] countLetters, ref int maxLength)
        {
            if (per.Length == maxLength)
            {
                if (Test(per))
                {
                    Count++;
                    // Console.WriteLine(per); // Для вывода перестановки на консоль (Сильно влияет на быстродействие)
                }
                
                return;
            }

            for (int i = 0; i < countLetters.Length; i++)
            {
                if (countLetters[i] > 0)
                {
                    countLetters[i]--;
                    GetCountPermutations(per + letters[i], ref letters, ref countLetters, ref maxLength);
                    countLetters[i]++;
                }
            }
        }
        
        /// <summary>
        /// Здесь меняем на своё
        /// Метод принимает строку и проверяет её на выполнение условия
        /// </summary>
        /// <param name="s">Строка для проверки</param>
        /// <returns>true или false</returns>
        static bool Test(string s)
        {
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
        }
    }
}