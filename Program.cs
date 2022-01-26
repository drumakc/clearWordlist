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
            String[] names = Directory.GetFiles(@"wordlist\", "x*");
            string pathB = @"wordlist\output.txt";
            FileInfo fiB = new FileInfo(pathB);

            if (fiB.Exists)
            {
                Console.WriteLine("Файл output.txt найден.");
            }
            else
            {
                File.Create(@"wordlist\output.txt");
                Console.WriteLine("Файл output.txt создан.");
            }

            foreach (string name in names)
            {
                Console.WriteLine($"Найден файл {name}.");
                FileInfo fia = new FileInfo(name);

                //копирую содержимое двух файлов в два списка
                List<string> bufferA = new List<string>();
                List<string> bufferB = new List<string>();
                
                using (StreamReader srA = new StreamReader(name, System.Text.Encoding.Default))
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
                        }
                    }
                }

                File.Delete(name);

                if (!File.Exists(name))
                {
                    Console.WriteLine($"Файл {name} отфильтрован и удален.");
                    Console.WriteLine("");
                }
            }
        }       
    }
}
