using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace ConsoleApp16
{
  class Program
  {
    static void get_hash(string[] hash) 
    {
      string s = string.Empty;
      string h = string.Empty;
      for (int i = 97; i < 123; i++)
        for (int j = 97; j < 123; j++)
          for (int k = 97; k < 123; k++)
            for (int l = 97; l < 123; l++)
              for (int m = 97; m < 123; m++) 
              {
                s = Convert.ToString(Convert.ToChar(i))+Convert.ToString(Convert.ToChar(j))+Convert.ToString(Convert.ToChar(k))+Convert.ToString(Convert.ToChar(l))+ Convert.ToString(Convert.ToChar(m));
                h = ComputeSHA256(s);
                if (hash.Contains(h))
                  Console.WriteLine(s + "=" + h);
              }
    }
    static string ComputeSHA256(string s)
    {
      string hash = String.Empty;
      using (SHA256 sha256 = SHA256.Create())
      {
        byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
        foreach (byte b in hashValue)
        {
          hash += $"{b:X2}";
        }
      }
      return hash;
    }
    static void Main(string[] args)
    {
      Console.WriteLine("Если хотите считать хеши с файла напишите 'file'");
      string how = Console.ReadLine();
      string[] h;
      h = new string[3];
      if (how == "file")
      {
        Console.WriteLine("Напишите полное имя файла");
        string fname = Console.ReadLine();
        if (File.Exists(fname))
        {
          using (StreamReader sr = new StreamReader(fname))
          {
            string line;
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
              if ((i < 3) && (line.Length == 64))
              {
                h[i] = line.ToUpper();
              }
              i++;
            }
          }
        }
        else
        {
          Console.WriteLine("Неверное имя файла");
        }
      }
      else{
        for (int j = 0; j < 3; j++)
        {
          Console.WriteLine("Введите хэш");
          h[j] = Console.ReadLine().ToUpper();
        }
      }
      get_hash(h);
      Console.ReadKey();
    }
  }
}