using System;
using System.Text;

namespace CarShowroom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;

            while (running)
            {
                Console.WriteLine("AВТОСАЛОН");
                Console.WriteLine("1. Показати всі записи (буде пізніше)");
                Console.WriteLine("2. Додати запис (буде пізніше)");
                Console.WriteLine("3. Знайти за умовою (буде пізніше)");
                Console.WriteLine("4. Змінити запис (буде пізніше)");
                Console.WriteLine("5. Видалити запис (буде пізніше)");
                Console.WriteLine("6. Зведена таблиця");
                Console.WriteLine("7. Зберегти у файл (буде пізніше)");
                Console.WriteLine("8. Завантажити з файлу (буде пізніше)");
                Console.WriteLine("9. Експорт у CSV (буде пізніше)");
                Console.WriteLine("10. Довідка про програму");
                Console.WriteLine("11. Табулювання функції (вартість залежно від пробігу)");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "7":
                    case "8":
                    case "9":
                        Console.WriteLine("Далі буде");
                        Pause();
                        break;

                    case "6":
                        ShowSummaryTable();
                        Pause();
                        break;

                    case "10":
                        ShowAbout();
                        Pause();
                        break;

                    case "11":
                        ShowTabulation();
                        Pause();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("До побачення!");
                        break;

                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }
        static void ShowAbout()
        {
            Console.WriteLine("ДОВІДКА ПРО ПРОГРАМУ");
            Console.WriteLine("Тема проєкту: Автосалон (Варіант 16)");
            Console.WriteLine("Головна сутність: Автомобіль");
            Console.WriteLine("Поля сутності «Автомобіль»:");
            Console.WriteLine("1. VIN (string) - унікальний ідентифікаційний номер");
            Console.WriteLine("2. Марка (string) - виробник автомобіля");
            Console.WriteLine("3. Код моделі (int) - посилання на довідник моделей");
            Console.WriteLine("4. Рік випуску (int) - від 1900 до поточного");
            Console.WriteLine("5. Пробіг (int) - в кілометрах, не від'ємний");
            Console.WriteLine("6. Колір (string) - назва кольору");
            Console.WriteLine("7. Ціна (double) - вартість у доларах");
            Console.WriteLine("8. Чи проданий (bool) - стан продажу (так або ні)");
            Console.WriteLine("Підсумкова величина: середня ціна за марками та скільки авто продано за місяць.");
        }

        static void ShowTabulation()
        {
            Console.WriteLine("=== Таблиця розрахунку орієнтовної ціни залежно від пробігу ===");
            Console.WriteLine(" Пробіг (тис. км)   Ціна зі знижкою ($)");
            Console.WriteLine("----------------------------------------");

            double basePrice = 25000.0;
            double from = 0.0;
            double to = 150.0;
            double step = 15.0;

            for (double mileage = from; mileage <= to + 1e-9; mileage += step)
            {
                double currentPrice = basePrice * Math.Exp(-0.003 * mileage);
                Console.WriteLine($"{mileage,16:F1}   {currentPrice,18:F2}");
            }
        }

        static void ShowSummaryTable()
        {
            Console.WriteLine("Зведена таблиця (статистика продажів)");

            Console.Write("Введіть кількість рядків (категорій/моделей): ");
            string rowsInput = Console.ReadLine();
            int rows;
            bool checkRows = int.TryParse(rowsInput, out rows);

            if (!checkRows || rows <= 0)
            {
                Console.WriteLine("Помилка: введено некоректне число рядків.");
                return;
            }

            Console.Write("Введіть кількість стовпців (місяців): ");
            string colsInput = Console.ReadLine();
            int cols;
            bool checkCols = int.TryParse(colsInput, out cols);

            if (!checkCols || cols <= 0)
            {
                Console.WriteLine("Помилка: введено некоректне число стовпців.");
                return;
            }

            double[] rowTotals = new double[rows];
            double[] colTotals = new double[cols];
            double grandTotal = 0;

            Console.Write("\n    Модель");
            for (int c = 1; c <= cols; c++)
            {
                Console.Write($"    Місяць {c}");
            }
            Console.WriteLine("    Підсумок");

            for (int i = 0; i < 12 + cols * 12 + 12; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            for (int r = 0; r < rows; r++)
            {
                Console.Write($"{("Модель " + (r + 1)),10}");
                double currentRowSum = 0;

                for (int c = 0; c < cols; c++)
                {
                    double val = (r + 1) * (c + 1) * 2;
                    Console.Write($"{val,12:F0}");

                    currentRowSum = currentRowSum + val;
                    colTotals[c] = colTotals[c] + val;
                }

                rowTotals[r] = currentRowSum;
                grandTotal = grandTotal + currentRowSum;

                Console.WriteLine($"{currentRowSum,12:F0}");
            }

            for (int i = 0; i < 12 + cols * 12 + 12; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            Console.Write($"{"Підсумок",10}");
            for (int c = 0; c < cols; c++)
            {
                Console.Write($"{colTotals[c],12:F0}");
            }

            Console.WriteLine($"{grandTotal,12:F0}");
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}