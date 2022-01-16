/*
Копирует из файла input.txt уникальные слова длиннее семи символов в файл output.txt.
*/

using System;
using System.IO;
using System.Threading.Tasks;

namespace clearWordlist
{
    class Program
    {
        static async Task Main(String[] args)
        {
            do
            {
                string pathInput = @"input.txt";
                string pathOutput = @"output.txt";
                FileInfo fiInput = new FileInfo(pathInput);
                FileInfo fiOutput = new FileInfo(pathOutput);
                List<string> buffer = new List<string>();

                if (fiInput.Exists)
                {
                    Console.WriteLine("Файл input.txt найден.");                             
                }
                else 
                {
                    Console.WriteLine("Файл input.txt не найден.");
                    
                    String[] abc = {"a", "b", "c", "d", "e", "f", "g",
                    "h", "i", "j", "k", "l", "m", "n", "o", "p", "q",
                    "r", "s", "t", "u", "v", "w", "x", "y", "z"};

                    for (int i = 0; i < abc.Length; i++)
                    {
                        for (int j = 0; j < abc.Length; j++)
                        {                   
                            name = "x" + abc[i] + abc[j];
                            pathInput = @$"{name}";
                            fiInput = new FileInfo(pathInput);
                            
                            if (fiInput.Exists)
                            {
                                Console.WriteLine($"Файл {name} найден");
                                File.Move(name, "input.txt");
                                break;
                            }
                        }
                    }
                }

                //проверяю существование файла output.txt
                //при необходимости создаю новый               
                if (fiOutput.Exists)
                {
                    Console.WriteLine("Файл output.txt найден.");
                    //копирую содержимое файла output.txt в буффер
                    using (StreamReader srOutput = new StreamReader(pathOutput, System.Text.Encoding.Default))
                    {
                        string lineOutput;
                        while ((lineOutput = await srOutput.ReadLineAsync()) != null)
                        {
                            buffer.Add(lineOutput);                    
                        }
                    }
                }
                else 
                {
                    Console.WriteLine("Файл output.txt не найден.");
                    fiOutput.Create(); 
                    if (fiOutput.Exists)
                    {
                        Console.WriteLine("Файл output.txt создан.");
                    }              
                    else
                    {
                        Console.WriteLine("Файл output.txt не удалось создать.");
                    }
                }

                //асинхронное построчное чтение
                //добавляю в buffer слова длиннее семи символов и исключая дубликаты
                using (StreamReader srInput = new StreamReader(pathInput, System.Text.Encoding.Default))
                {
                    string lineInput;
                    while ((lineInput = await srInput.ReadLineAsync()) != null)
                    {
                        if (lineInput.Length > 7) {
                            if (!buffer.Exists(p => p == lineInput))
                            {
                                buffer.Add(lineInput);
                                Console.WriteLine($"{lineInput}");
                            }
                        }                     
                    }
                }
                //копирую результат фильтрации файла input.txt (содержимое buffer) в output.txt. 
                using (StreamWriter sw = new StreamWriter(pathOutput, false, System.Text.Encoding.Default))
                {
                    foreach (var item in buffer)
                    {
                        sw.WriteLine(item);
                    }
                } 
                //удаляю файл input.txt
                File.Delete(pathInput);
                FileInfo fi = new FileInfo(pathInput);
                
                if (fi.Exists)
                {
                Console.WriteLine("");
                Console.WriteLine("!!!!   Файл input.txt не был удален.   !!!!");
                }
                else
                {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Файл input.txt удален.");
                Console.WriteLine("");
                Console.WriteLine("XXXXXXXXXX  XX      XX");  
                Console.WriteLine("XXXXXXXXXX  XX    XX");
                Console.WriteLine("XX      XX  XX  XX");
                Console.WriteLine("XX      XX  XXXX");
                Console.WriteLine("XX      XX  XX  XX");  
                Console.WriteLine("XXXXXXXXXX  XX    XX"); 
                Console.WriteLine("XXXXXXXXXX  XX      XX");
                }
            }
            while (true);              
        }
    }
}