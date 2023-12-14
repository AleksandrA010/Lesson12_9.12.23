using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    internal class Program
    {
        static void PrintNumbers()
        {
            for (int i = 1; i < 11; i++)
            {
                Console.WriteLine($"Поток ID: {Thread.CurrentThread.ManagedThreadId}, порядковый номер: {i}");
            }
        }

        static void Factorial(object number1)
        {
            Thread.Sleep(8000);
            int number = (int)number1;
            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал числа {number1} = {result}\n");
        }
        static async Task MathNumberAsync(int number)
        {
            Console.WriteLine($"\nКорень числа {number} = {number * number}\n");
            await Task.Run(() => Factorial(number));
        }
        static void Main()
        {
            ThreadStart threadStart1 = new ThreadStart(PrintNumbers);
            Thread thread1 = new Thread(threadStart1);
            thread1.Start();
            Thread thread2 = new Thread(threadStart1);
            thread2.Start();
            PrintNumbers();
            Thread.Sleep(1000);
            Console.Write("Нажмите на любую кнопку...");
            Console.ReadKey();
            Console.Clear();

            Console.Write("Введите число: ");
            bool flag = int.TryParse(Console.ReadLine(), out int number);
            _ = MathNumberAsync(number);
            Console.WriteLine($"В момент вывода этого сообщения программа всё ещё считает факториал.\n");

            Thread.Sleep(9000);
            Type myType = typeof(Refl);

            foreach (MethodInfo method in myType.GetMethods())
            {
                string modificator = "";

                if (method.IsPublic)
                {
                    modificator += "public ";
                }
                else if (method.IsPrivate)
                {
                    modificator += "private ";
                }
                if (method.IsStatic)
                { 
                    modificator += "static "; 
                }
                else if (method.IsVirtual)
                {
                    modificator += "virtual "; 
                }
                Console.WriteLine($"{modificator}{method.ReturnType.Name} {method.Name} ()");
            }
            Console.ReadKey();
        }
    }
    public class Refl
    {
        public virtual void Main()
        {
            Console.WriteLine(Output());
            Console.WriteLine(AddInts(1, 2));
        }
        public string Output()
        {
            return "Test-Output";
        }
        public int AddInts(int i1, int i2)
        {
            return i1 + i2;
        }
    }
}
