using ServerAPI.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading;
using APIHomework.Models;

namespace APIHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            int answer = -1;
            while (answer != 0)
            {
                Console.Clear();
                Console.WriteLine("ДОБРО ПОЖАЛОВАТЬ В ПРОГРАММУ ПО ТЕСТИРОВАНИЮ API\n" +
                    "Пожалуйста, выберите действие цифрой: \n" +
                    "[1] - получить список всех книг GET-запросом; \n" +
                    "[2] - редактировать книгу PUT-запросом; \n" +
                    "[3] - добавить книгу POST-запросом; \n" +
                    "[4] - удалить книгу DELETE-запросом; \n" +
                    "[5] - скачать файл GET-запросом; \n" +
                    "[0] - выход; \n");
                string userAnswer = Console.ReadLine();
                if (int.TryParse(userAnswer, out answer))
                {
                    switch (answer)
                    {
                        case 1:
                            APIHelper.GetBooks();
                            Console.ReadKey();
                            break;
                        case 2:
                            APIHelper.GetBooks();
                            Console.WriteLine("Через пробел укажите 4 параметра:\n" +
                                "[ID книги, которую хотите изменить] [новое название книги] [автор книги] [год выпуска]");
                            string[] newBookData = Console.ReadLine().Split(' ');
                            int id;
                            int year;
                            if (newBookData.Length < 4) Console.WriteLine("Вы указали недостаточное количество параметров.");
                            else if (!int.TryParse(newBookData[0], out id) || !int.TryParse(newBookData[3], out year)) Console.WriteLine("Вы указали неверное числовое значение." +
                                " ID и год выпуска должны быть указаны цифрами.");
                            else APIHelper.EditBook(id, newBookData[1], newBookData[2], year);
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.WriteLine("Через пробел укажите 3 параметра:\n" +
                            "[название книги] [автор книги] [год выпуска]");
                            string[] newBook = Console.ReadLine().Split(' ');
                            int newYear;
                            if (newBook.Length < 3) Console.WriteLine("Вы указали недостаточное количество параметров.");
                            else if (!int.TryParse(newBook[2], out newYear)) Console.WriteLine("Вы указали неверное числовое значение." +
                                " ID и год выпуска должны быть указаны цифрами.");
                            else APIHelper.AddBook(newBook[0], newBook[1], newYear);
                            Console.ReadKey();
                            break;
                        case 4:
                            APIHelper.GetBooks();
                            Console.WriteLine("Укажите ID книги, которую нужно удалить:");
                            int newID;
                            string userID = Console.ReadLine();
                            if (int.TryParse(userID, out newID))
                            {
                                APIHelper.DeleteBook(newID);
                            }
                            else
                                Console.WriteLine("Указан неверный ID.");
                            Console.ReadKey();
                            break;
                        case 5:
                            Console.WriteLine("Пожалуйста, укажите каталог, в который нужно сохранить файл:");
                            string path = Console.ReadLine();
                            APIHelper.GetFile(path);
                            Console.WriteLine("OK! Файл скачан и находится в указанной директории.");
                            Console.ReadKey();
                            break;
                        case 0:

                            break;
                        default:
                            Console.WriteLine("Введена неправильная команда. ");
                            break;
                    }
                }
            }
        }
    }
}
