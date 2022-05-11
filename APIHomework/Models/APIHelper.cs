using Newtonsoft.Json;
using ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace APIHomework.Models
{
    public static class APIHelper
    {

        //private static HttpClient client = new HttpClient();

        /// <summary>
        /// GET
        /// </summary>
        public static void GetBooks()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:4598/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
            HttpResponseMessage response = client.GetAsync("api/books").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                List<Book> b = JsonConvert.DeserializeObject<List<Book>>(products);
                Console.WriteLine("СПИСОК КНИГ: ");
                foreach (var p in b)
                {
                    Console.WriteLine("ID: " + p.Id);
                    Console.WriteLine("TITLE: " + p.Title);
                    Console.WriteLine("AUTHOR: " + p.Author);
                    Console.WriteLine("YEAR OF ISSUE: " + p.YearOfIssue);
                    Console.WriteLine("____________");
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }


        /// <summary>
        /// PUT
        /// </summary>
        public static void EditBook(int id, string newTitle, string Author, int yearOfIssue)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:4598/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
            HttpResponseMessage response = client.GetAsync("api/books").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                List<Book> b = JsonConvert.DeserializeObject<List<Book>>(products);
                if (id < 0)
                {
                    Console.WriteLine("Вы ввели некорректный ID.");
                }
                else
                {
                    using (var cli = new HttpClient())
                    {
                        Book p = new Book { Id = id, Title = newTitle, Author = Author, YearOfIssue = yearOfIssue };
                        cli.BaseAddress = new Uri("http://localhost:4598/");
                        var resp = cli.PutAsJsonAsync("api/books", p).Result;
                        if (resp.IsSuccessStatusCode)
                        {
                            Console.Write($"Книга {newTitle} была изменена.");
                        }
                        else
                            Console.Write("Изменения не удались.");
                    }
                }
            }


                
        }

        /// <summary>
        /// POST
        /// </summary>
        public static void AddBook(string newTitle, string Author, int yearOfIssue)
        {
            using (var client = new HttpClient())
            {
                Book p = new Book { Title = newTitle, Author = Author, YearOfIssue = yearOfIssue };
                client.BaseAddress = new Uri("http://localhost:4598/");
                var response = client.PostAsJsonAsync("api/books", p).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write($"Книга {newTitle} была успешно добавлена.");
                }
                else
                    Console.Write("Добавление книги не удалось.");
            }
        }

        /// <summary>
        /// DELETE
        /// </summary>
        public static void DeleteBook(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:4598/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
            HttpResponseMessage response = client.GetAsync("api/books").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var books = response.Content.ReadAsStringAsync().Result;
                List<Book> b = JsonConvert.DeserializeObject<List<Book>>(books);
                if (id < 0)
                {
                    Console.WriteLine("Вы ввели некорректный ID.");
                }
                else
                {
                    using (var clo = new HttpClient())
                    {
                        clo.BaseAddress = new Uri("http://localhost:4598/");
                        var resp = clo.DeleteAsync($"api/books/{id}").Result;
                        if (resp.IsSuccessStatusCode)
                        {
                            Console.Write($"Книга {b.Where(x=>x.Id == id).FirstOrDefault().Title} была удалена.");
                        }
                        else
                            Console.Write("Произошла ошибка при удалении книги.");
                    }
                }
            }
        }

        /// <summary>
        /// api/files
        /// </summary>
        public static void GetFile(string pathToDownload)
        {
            HttpClient client = new HttpClient();
            var contentBytes = client.GetByteArrayAsync("http://localhost:4598/api/files").Result;
            MemoryStream stream = new MemoryStream(contentBytes);
            if (Directory.Exists(pathToDownload))
            {
                FileStream file = new FileStream($@"{pathToDownload}\testFileDown.txt", FileMode.Create, FileAccess.Write);
                stream.WriteTo(file);
                file.Close();
                stream.Close();
            }
            else
            {
                Directory.CreateDirectory(pathToDownload);
                FileStream file = new FileStream($@"{pathToDownload}\testFileDown.txt", FileMode.Create, FileAccess.Write);
                stream.WriteTo(file);
                file.Close();
                stream.Close();
            }

        }

    }
}
