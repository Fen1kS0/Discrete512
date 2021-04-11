using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Permutations512
{
    class Program
    {
        /// <summary>
        /// Кол-во различных слов, которые можно получить перестановкой букв слова alpha, подходящих под условие
        /// </summary>
        private static int _count;
        
        // Для 5 варианта (вынес ради экономии памяти)
        private const string Vowels = "аеёиоуыэюя";
        private const string Consonants = "бвгджзйклмнпрстфхцчшщьъ";
        private static readonly string Pattern = $@"\w*([{Vowels}][{Consonants}][{Consonants}][{Vowels}])\w*";
        // ============================================
        
        static void Main(string[] args)
        {
            string word = "пастух"; // Меняем на нужное нам слово
            int maxLength = word.Length;

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
            GetCountPermutations("", letters, countLetters, ref maxLength);
            Console.WriteLine($"Кол-во перестановок подходящее под условие = {Count}");
            stopWatch.Stop();
            
            ts = stopWatch.Elapsed;
            Console.WriteLine("Время выполнения = {0:00}:{1:00}.{2:00}",ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.ReadLine();
        }
        
        /// <summary>
        /// Тут без полного понимания лучше ничего не трогать
        /// </summary>
        static void GetCountPermutations(string per, char[] letters, int[] countLetters, ref int maxLength)
        {
            if (per.Length == maxLength)
            {
                if (Test(per))
                {
                    Count++;
                    Console.WriteLine(per); // Для вывода перестановки на консоль (Сильно влияет на быстродействие)
                }
                
                return;
            }

            for (int i = 0; i < countLetters.Length; i++)
            {
                if (countLetters[i] > 0)
                {
                    countLetters[i]--;
                    GetCountPermutations(per + letters[i], letters, countLetters, ref maxLength);
                    countLetters[i]++;
                }
            }
        }
        
        /// <summary>
        /// Здесь меняем на своё
        /// Метод принимает строку и проверяет её на выполнение условия
        /// </summary>
        /// <param name="s">Строка для проверки</param>
        /// <returns>true если слово проходит проверку,иначе false</returns>
        static bool Test(string s)
        {
            return Regex.IsMatch(s, Pattern, RegexOptions.IgnoreCase);
        }
    }
}
