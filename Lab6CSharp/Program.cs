using System;
using System.Collections;
using System.Linq;

namespace Lab6Charp
{
    // =================================================================
    // ЗАВДАННЯ 1.6: Інтерфейси (Антена)
    // =================================================================
    public interface IAntenn
    {
        void Show();
        double Power();
    }

    public class RoofAntenna : IAntenn
    {
        public double PowerValue { get; set; }
        public decimal Price { get; set; }
        public string Material { get; set; }

        public RoofAntenna(double power, decimal price, string material)
        {
            PowerValue = power;
            Price = price;
            Material = material;
        }

        public double Power() => PowerValue;

        public void Show()
        {
            Console.WriteLine($"[Дахова Антена] Матеріал: {Material}, Потужність: {Power()} дБ, Ціна: {Price} грн");
        }
    }

    // =================================================================
    // ЗАВДАННЯ 2.6: Ієрархія Person + Стандартні інтерфейси
    // =================================================================
    public interface IPerson
    {
        void Show();
        string GetRole();
    }

    // Абстрактний клас реалізує інтерфейси порівняння та клонування
    public abstract class Person : IPerson, IComparable<Person>, ICloneable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public abstract void Show();
        public abstract string GetRole();

        // Реалізація IComparable для сортування за віком
        public int CompareTo(Person other)
        {
            if (other == null) return 1;
            return this.Age.CompareTo(other.Age);
        }

        // Реалізація ICloneable для глибокого (або поверхневого) копіювання
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Worker : Person
    {
        public Worker(string name, int age) : base(name, age) { }
        public override string GetRole() => "Робітник";
        public override void Show() => Console.WriteLine($"[{GetRole()}] Ім'я: {Name,-12} | Вік: {Age}");
    }

    public class Employee : Person
    {
        public Employee(string name, int age) : base(name, age) { }
        public override string GetRole() => "Службовець";
        public override void Show() => Console.WriteLine($"[{GetRole()}] Ім'я: {Name,-12} | Вік: {Age}");
    }

    public class Engineer : Person
    {
        public string Specialty { get; set; }
        public Engineer(string name, int age, string specialty) : base(name, age) { Specialty = specialty; }
        public override string GetRole() => "Інженер";
        public override void Show() => Console.WriteLine($"[{GetRole()}] Ім'я: {Name,-12} | Вік: {Age} | Спеціальність: {Specialty}");
    }

    // =================================================================
    // ЗАВДАННЯ 3.6: Власні винятки (Exceptions) + OverflowException
    // =================================================================

    // Створюємо власний тип помилки
    public class InvalidFigureException : Exception
    {
        public InvalidFigureException(string message) : base(message) { }
    }

    public class MathTester
    {
        // Метод, який використовується для симуляції помилок
        public static void CalculateSquareArea(int side)
        {
            if (side < 0)
            {
                // Кидаємо власну помилку
                throw new InvalidFigureException("Сторона квадрата не може бути від'ємною!");
            }

            // Блок checked спеціально викликає OverflowException, якщо результат виходить за межі типу
            checked
            {
                int area = side * side;
                Console.WriteLine($"Площа квадрата: {area}");
            }
        }
    }

    // =================================================================
    // ЗАВДАННЯ 4: Додавання IEnumerable до класу з Лаб. 4 (VectorULong)
    // =================================================================
    public class VectorULong : IEnumerable
    {
        private ulong[] array;
        public int Size { get; private set; }

        public VectorULong(int size)
        {
            Size = size;
            array = new ulong[size];
            for (int i = 0; i < size; i++) array[i] = (ulong)(i * 10); // Заповнюємо тестовими даними
        }

        // Реалізація інтерфейсу IEnumerable для підтримки циклу foreach
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return array[i]; // yield автоматично генерує об'єкт IEnumerator
            }
        }
    }

    // =================================================================
    // ГОЛОВНЕ МЕНЮ ПРОГРАМИ
    // =================================================================
    class Program
    {
        static void Task1()
        {
            Console.WriteLine("\n--- Завдання 1.6: Антена ---");
            IAntenn myAntenna = new RoofAntenna(15.5, 1250.00m, "Алюміній");
            myAntenna.Show();
        }

        static void Task2()
        {
            Console.WriteLine("\n--- Завдання 2.6: База даних працівників (IPerson) ---");

            Person[] database = new Person[]
            {
                new Engineer("Олена", 28, "Програміст"),
                new Worker("Іван", 45),
                new Employee("Марія", 35),
                new Engineer("Петро", 30, "Енергетик")
            };

            Console.WriteLine("База ДО сортування:");
            foreach (var p in database) p.Show();

            // Використовуємо реалізований IComparable (сортування за віком)
            Array.Sort(database);

            Console.WriteLine("\nБаза ПІСЛЯ сортування за віком:");
            foreach (var p in database) p.Show();

            Console.WriteLine("\nДемонстрація ICloneable (Клонування об'єкта):");
            Engineer original = (Engineer)database[0];
            Engineer clone = (Engineer)original.Clone();
            clone.Name = "Олена_КОПІЯ";
            original.Show();
            clone.Show();

            Console.WriteLine("\nПошук всіх Інженерів у базі:");
            var engineers = database.Where(p => p is Engineer);
            foreach (var eng in engineers) eng.Show();
        }

        static void Task3()
        {
            Console.WriteLine("\n--- Завдання 3.6: Обробка винятків (Exceptions) ---");

            int[] testValues = { 5, -3, 50000 }; // 5 (ок), -3 (наш виняток), 50000 (переповнення int)

            foreach (int val in testValues)
            {
                Console.WriteLine($"\nСпроба порахувати площу квадрата зі стороною: {val}");
                try
                {
                    MathTester.CalculateSquareArea(val);
                }
                catch (InvalidFigureException ex) // Обробка нашого власного винятку
                {
                    Console.WriteLine($"[ПОМИЛКА ЛОГІКИ]: {ex.Message}");
                }
                catch (OverflowException ex) // Обробка стандартного винятку згідно з варіантом 3.6
                {
                    Console.WriteLine($"[КРИТИЧНА ПОМИЛКА]: Переповнення пам'яті (OverflowException). Число занадто велике! Деталі: {ex.Message}");
                }
                catch (Exception ex) // Відлов будь-яких інших помилок
                {
                    Console.WriteLine($"[НЕВІДОМА ПОМИЛКА]: {ex.Message}");
                }
            }
        }

        static void Task4()
        {
            Console.WriteLine("\n--- Завдання 4: Використання IEnumerable (foreach) ---");
            Console.WriteLine("Створено вектор типу VectorULong (з Лаб 4) на 5 елементів.");

            VectorULong vector = new VectorULong(5);

            Console.WriteLine("Перебір елементів вектору за допомогою foreach:");
            // Цикл працює ТІЛЬКИ тому, що ми реалізували інтерфейс IEnumerable
            foreach (ulong value in vector)
            {
                Console.Write($"{value}  ");
            }
            Console.WriteLine();
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("Оберіть завдання для запуску:");
                Console.WriteLine("1 - Завдання 1.6 (Інтерфейс Антена)");
                Console.WriteLine("2 - Завдання 2.6 (Інтерфейси Person, IComparable, ICloneable)");
                Console.WriteLine("3 - Завдання 3.6 (Обробка винятків + OverflowException)");
                Console.WriteLine("4 - Завдання 4 (IEnumerable для класу з Лаб.4)");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1": Task1(); break;
                    case "2": Task2(); break;
                    case "3": Task3(); break;
                    case "4": Task4(); break;
                    default: Console.WriteLine("Неправильне введення."); break;
                }
            }
        }
    }
}