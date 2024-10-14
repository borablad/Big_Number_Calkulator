using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task.Views
{
    public partial class CalculatorView
    {
        public CalculatorView()
        {
        }

        public void Start()
        {
            Console.WriteLine("Астрономический калькулятор. Для выхода введите End.");
            Console.WriteLine("Введите выражение в формате [число] [оператор] [число]. Например: 12345 + 67890");

            while (true)
            {


                Console.Write("Введите выражение: ");

                string input = Console.ReadLine();

                // Проверка на нажатие клавиши Esc для выхода
                if (input == "End")
                {
                    Console.WriteLine("\nПрограмма завершена.");
                    break;
                }

                // Проверка на корректность ввода
                if (IsValidInput(input))
                {
                    // Передаем выражение на обработку в контроллер
                    var controller = new Test_Task.Controllers.MathController();
                    controller.ProcessInput(input);
                }
                else
                {
                    Console.WriteLine("Неверный формат. Попробуйте еще раз.");
                }
            }

        }

        // Метод для проверки корректности ввода
        private bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            // Пример простейшей проверки на наличие двух чисел и оператора между ними
            string[] parts = input.Split(' ');

            if (parts.Length == 3 &&
                long.TryParse(parts[0], out _) &&
                long.TryParse(parts[2], out _) &&
                IsValidOperator(parts[1]))
            {
                return true;
            }

            return false;
        }

        // Проверка на допустимый оператор
        private bool IsValidOperator(string op)
        {
            return op == "+" || op == "-" || op == "*" || op == "/";
        }

    }
}
