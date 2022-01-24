/*
Соединяет словари очищая их от слов короче восьми символов и дубликатов
*/

using System;
using System.IO;
using System.Threading.Tasks;

namespace clearWordlist
{
    class Program
    {
        static async Task Main(String[] arr)
        { 
            //ищу внутри папки wordlist файл из трех символов, 
            //первый из которых "х"
            String[] abc = {"a", "b", "c", "d", "e", "f", "g",
                "h", "i", "j", "k", "l", "m", "n", "o", "p", "q",
                "r", "s", "t", "u", "v", "w", "x", "y", "z"};

            for (int i = 0; i < abc.Length; i++)
            {
                for (int j = 0; j < abc.Length; j++)
                {
                    string name = "x" + abc[i] + abc[j];
                    string pathA = @$"wordlist\{name}.txt";
                    FileInfo fiA = new FileInfo(pathA);
                    
                    if (fiA.Exists)
                    {   
                        Console.WriteLine($"Найден файл {name}.txt");
                     // ищу файл output.txt
                        string pathB = @"wordlist\output.txt";
                        FileInfo fiB = new FileInfo(pathB);
                        
                        if (fiB.Exists)
                        {   
                            Console.WriteLine("Найден файл output.txt");
                         //копирую содержимое двух файлов в два списка
                            List<string> bufferA = new List<string>();
                            List<string> bufferB = new List<string>();
                            
                            using (StreamReader srA = new StreamReader(pathA, System.Text.Encoding.Default))
                            {
                                string lineA;
                                while ((lineA = await srA.ReadLineAsync()) != null)
                                {
                                    if (lineA.Length > 7) {                                        
                                        if (!bufferA.Exists(p => p == lineA))
                                        {
                                            bufferA.Add(lineA);
                                        }
                                    }                                       
                                }
                            }

                            Console.WriteLine($"bufferA = {bufferA.Count}");
                            
                            using (StreamReader srB = new StreamReader(pathB, System.Text.Encoding.Default))
                            {
                                string lineB;
                                while ((lineB = await srB.ReadLineAsync()) != null)
                                {
                                    bufferB.Add(lineB);
                                }
                            }

                            Console.WriteLine($"bufferB = {bufferB.Count}");
                            
                            //Извлекаю из первого файла слова больше семи символов и без дубликатов
                            foreach (string itemA in bufferA)
                            {
                                if (itemA.Length > 7)
                                {
                                    if (!bufferB.Exists(p => p == itemA))
                                    {
                                        using (StreamWriter sw = new StreamWriter(pathB, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine(itemA);
                                        }
                                        //Console.WriteLine(itemA);
                                    }
                                }
                            }

                            File.Delete(pathA);

                            if (!File.Exists(pathA))
                            {
                                Console.WriteLine($"Файл {name} отфильтрован и удален.");
                                Console.WriteLine("");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Файл output.txt не найден.");
                        }
                    }
                }
            }
        }       
    }
}
