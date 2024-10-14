using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task.Controllers
{
    public partial class MathController
    {
        public MathController()
        {
        }

            public void ProcessInput(string input)
            {
                string[] parts = input.Split(' ');

                string number1 = parts[0];
                string operation = parts[1];
                string number2 = parts[2];

                switch (operation)
                {
                    case "+":
                        PerformAddition(number1, number2);
                        break;

                    case "-":
                        PerformSubtraction(number1, number2);
                        break;

                    case "*":
                        PerformMultiplication(number1, number2);
                        break;

                    case "/":
                        PerformDivision(number1, number2);
                        break;

                    default:
                        Console.WriteLine("Неизвестная операция.");
                        break;
                }
            }
        // Метод сложения (столбиком)
        private void PerformAddition(string number1, string number2)
        {
            // Убираем ведущие нули для корректной работы
            number1 = number1.TrimStart('0');
            number2 = number2.TrimStart('0');

            // Если оба числа пустые после удаления нулей, то результат будет "0"
            if (string.IsNullOrEmpty(number1) && string.IsNullOrEmpty(number2))
            {
                Console.WriteLine("Результат: 0");
                return;
            }

            // Определяем, какое число длиннее
            if (number1.Length < number2.Length)
            {
                (number1, number2) = (number2, number1); // Обмен значениями
            }

            // Переменная для хранения результата и переноса
            string result = "";
            int carry = 0;

            // Сложение начинается с конца строк
            int n1Length = number1.Length;
            int n2Length = number2.Length;

            for (int i = 0; i < n1Length; i++)
            {
                // Получаем цифры для сложения, если в number2 нет цифры, то используем 0
                int digit1 = number1[n1Length - 1 - i] - '0'; // Преобразование символа в цифру
                int digit2 = (i < n2Length) ? number2[n2Length - 1 - i] - '0' : 0;

                int sum = digit1 + digit2 + carry; // Суммируем цифры и перенос
                carry = sum / 10; // Вычисляем новый перенос
                result = (sum % 10) + result; // Добавляем текущую цифру к результату
            }

            // Если остался перенос, добавляем его к результату
            if (carry > 0)
            {
                result = carry + result;
            }

            Console.WriteLine($"Результат: {result}");
        }


       
        /// Метод для вычитания двух больших чисел, представленных в виде строк.
        private void PerformSubtraction(string number1, string number2)
        {
            // Убираем ведущие нули для корректной работы
            number1 = number1.TrimStart('0');
            number2 = number2.TrimStart('0');

            // Если оба числа пустые после удаления нулей, то результат будет "0"
            if (string.IsNullOrEmpty(number1) && string.IsNullOrEmpty(number2))
            {
                Console.WriteLine("Результат: 0");
                return;
            }

            // Проверяем, какое число больше
            bool isNegative = false;
            if (number1.Length < number2.Length ||
                (number1.Length == number2.Length && string.Compare(number1, number2) < 0))
            {
                // Если number1 меньше number2, меняем их местами и ставим флаг для отрицательного результата
                (number1, number2) = (number2, number1);
                isNegative = true;
            }

            // Переменная для хранения результата и займа
            string result = "";
            int borrow = 0;

            // Вычитание начинается с конца строк
            int n1Length = number1.Length;
            int n2Length = number2.Length;

            for (int i = 0; i < n1Length; i++)
            {
                // Получаем цифры для вычитания, если в number2 нет цифры, то используем 0
                int digit1 = number1[n1Length - 1 - i] - '0'; // Преобразование символа в цифру
                int digit2 = (i < n2Length) ? number2[n2Length - 1 - i] - '0' : 0;

                // Если необходимо, берем в долг (занимаем)
                if (digit1 - borrow < digit2)
                {
                    digit1 += 10; // Добавляем 10 к текущей цифре
                    borrow = 1; // Увеличиваем займ на 1
                }
                else
                {
                    borrow = 0; // Займ больше не нужен
                }

                int diff = digit1 - digit2 - borrow; // Разница
                result = diff + result; // Добавляем текущую цифру к результату
            }

            // Убираем ведущие нули в результате
            result = result.TrimStart('0');

            // Если результат пустой, это значит, что результат равен 0
            if (string.IsNullOrEmpty(result))
            {
                result = "0";
            }

            // Если результат был отрицательным, добавляем знак минус
            if (isNegative)
            {
                result = "-" + result;
            }

            Console.WriteLine($"Результат: {result}");
        }


        // Метод для умножения двух больших чисел, представленных в виде строк.
        private void PerformMultiplication(string number1, string number2)
        {
            // Убираем ведущие нули для корректной работы
            number1 = number1.TrimStart('0');
            number2 = number2.TrimStart('0');

            // Если одно из чисел пустое, результат будет "0"
            if (string.IsNullOrEmpty(number1) || string.IsNullOrEmpty(number2))
            {
                Console.WriteLine("Результат: 0");
                return;
            }

            // Результат умножения будет максимум длины number1 + длины number2
            int[] result = new int[number1.Length + number2.Length];

            // Умножаем каждую цифру одного числа на каждую цифру другого
            for (int i = number1.Length - 1; i >= 0; i--)
            {
                for (int j = number2.Length - 1; j >= 0; j--)
                {
                    int digit1 = number1[i] - '0'; // Преобразование символа в цифру
                    int digit2 = number2[j] - '0'; // Преобразование символа в цифру

                    // Умножаем цифры и добавляем к результату
                    int product = digit1 * digit2 + result[i + j + 1]; // Добавляем к текущей позиции

                    result[i + j + 1] = product % 10; // Записываем последнюю цифру в результат
                    result[i + j] += product / 10; // Добавляем оставшуюся часть к следующей позиции
                }
            }

            // Преобразуем массив результата в строку
            string resultStr = "";
            foreach (int num in result)
            {
                if (!(resultStr.Length == 0 && num == 0)) // Пропускаем ведущие нули
                {
                    resultStr += num;
                }
            }

            // Если результат пустой, это значит, что результат равен 0
            if (string.IsNullOrEmpty(resultStr))
            {
                resultStr = "0";
            }

            Console.WriteLine($"Результат: {resultStr}");
        }


        private void PerformDivision(string number1, string number2)
            {
                // Логика для деления (будет добавлена позже)
                Console.WriteLine($"Деление: {number1} / {number2}");
            }

        }
    
}
