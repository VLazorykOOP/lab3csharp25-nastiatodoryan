using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string? choice;

        do
        {
            Console.WriteLine("\nОберіть завдання:");
            Console.WriteLine("1. Масив точок та робота з класом Point");
            Console.WriteLine("2. Ієрархія класів: Корабель, Пароплав, Вітрильник, Корвет");
            Console.WriteLine("0. Вийти");
            Console.Write("Ваш вибір: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Task1();
                    break;
                case "2":
                    Task2();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                    break;
            }
        } while (choice != "0");
    }

    // Завдання 1: Клас Point
    static void Task1()
    {
        Console.Write("Введіть кількість точок: ");
        int n = int.Parse(Console.ReadLine()!);

        Point[] points = new Point[n];
        Random rand = new Random();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Введіть x координату для точки {i + 1}: ");
            int x = int.Parse(Console.ReadLine()!);
            Console.Write($"Введіть y координату для точки {i + 1}: ");
            int y = int.Parse(Console.ReadLine()!);
            int color = rand.Next(1, 6); // випадковий колір (1-5)
            points[i] = new Point(x, y, color);
        }

        Console.WriteLine("\nІнформація про точки:");
        double totalDistance = 0;
        foreach (var p in points)
        {
            p.Show();
            totalDistance += p.DistanceToOrigin();
        }

        double averageDistance = totalDistance / n;
        Console.WriteLine($"\nСередня відстань до початку координат: {averageDistance:F2}");

        Console.Write("\nВведіть вектор зміщення (x1): ");
        int x1 = int.Parse(Console.ReadLine()!);
        Console.Write("Введіть вектор зміщення (y1): ");
        int y1 = int.Parse(Console.ReadLine()!);

        Console.WriteLine("\nТочки після переміщення (лише ті, що більше за середню відстань):");
        foreach (var p in points)
        {
            if (p.DistanceToOrigin() > averageDistance)
            {
                p.Move(x1, y1);
                p.Show();
            }
        }
    }

    // Завдання 2: Ієрархія класів
    static void Task2()
    {
        List<Ship> ships = new List<Ship>
        {
            new Steamboat("SteamKing", 30, 500),
            new SailingShip("WindRider", 20, 3),
            new Corvette("Speedy", 40, 10),
            new Steamboat("Titan", 50, 800),
            new Corvette("Lightning", 45, 8)
        };

        Console.WriteLine("\nВсі кораблі:");
        foreach (var ship in ships)
        {
            ship.Show();
        }

        Console.WriteLine("\nСортування за швидкістю:");
        var sorted = ships.OrderBy(s => s.Speed).ToList();

        foreach (var ship in sorted)
        {
            ship.Show();
        }
    }
}

// ===== Завдання 1: Клас Point =====

class Point
{
    protected int x, y;
    protected int color;

    public Point()
    {
        x = 0;
        y = 0;
        color = 0;
    }

    public Point(int x, int y, int color)
    {
        this.x = x;
        this.y = y;
        this.color = color;
    }

    public int X
    {
        get => x;
        set => x = value;
    }

    public int Y
    {
        get => y;
        set => y = value;
    }

    public int Color => color;

    public void Show()
    {
        Console.WriteLine($"Точка ({x}, {y}), колір: {color}, відстань до (0,0): {DistanceToOrigin():F2}");
    }

    public double DistanceToOrigin()
    {
        return Math.Sqrt(x * x + y * y);
    }

    public void Move(int dx, int dy)
    {
        x += dx;
        y += dy;
    }
}

// ===== Завдання 2: Ієрархія кораблів =====

abstract class Ship
{
    public string Name { get; set; }
    public int Speed { get; set; }

    public Ship(string name, int speed)
    {
        Name = name;
        Speed = speed;
    }

    public abstract void Show();
}

class Steamboat : Ship
{
    public int EnginePower { get; set; }

    public Steamboat(string name, int speed, int enginePower)
        : base(name, speed)
    {
        EnginePower = enginePower;
    }

    public override void Show()
    {
        Console.WriteLine($"Пароплав: {Name}, Швидкість: {Speed} км/год, Потужність двигуна: {EnginePower} к.с.");
    }
}

class SailingShip : Ship
{
    public int MastCount { get; set; }

    public SailingShip(string name, int speed, int mastCount)
        : base(name, speed)
    {
        MastCount = mastCount;
    }

    public override void Show()
    {
        Console.WriteLine($"Вітрильник: {Name}, Швидкість: {Speed} км/год, Кількість щогл: {MastCount}");
    }
}

class Corvette : Ship
{
    public int WeaponCount { get; set; }

    public Corvette(string name, int speed, int weaponCount)
        : base(name, speed)
    {
        WeaponCount = weaponCount;
    }

    public override void Show()
    {
        Console.WriteLine($"Корвет: {Name}, Швидкість: {Speed} км/год, Озброєння: {WeaponCount} одиниць");
    }
}
