using System;
using System.Collections.Generic;

namespace Personalregister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<Employee> repo = new InMemoryListEmployees();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Personalregister - välj ett alternativ:");
                Console.WriteLine("1) Lägg till anställd");
                Console.WriteLine("2) Skriv ut register");
                Console.WriteLine("3) Avsluta");
                Console.Write("Val: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Namn: ");
                    var name = Console.ReadLine()?.Trim() ?? string.Empty;
                    if (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Ogiltigt namn. Försök igen.");
                        continue;
                    }

                    Console.Write("Lön: ");
                    var salaryInput = Console.ReadLine()?.Trim() ?? string.Empty;
                    if (!decimal.TryParse(salaryInput, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out var salary))
                    {
                        // Try invariant culture (accept dot as decimal separator)
                        if (!decimal.TryParse(salaryInput, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out salary))
                        {
                            Console.WriteLine("Ogiltig lön. Ange ett tal (t.ex. 25000.50). Försök igen.");
                            continue;
                        }
                    }

                    try
                    {
                        repo.Add(new Employee { Name = name, Salary = salary });
                        Console.WriteLine($"Anställd '{name}' tillagd.");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine();
                    Console.WriteLine("Register:");
                    var list = repo.List();
                    if (list.Count == 0)
                    {
                        Console.WriteLine("Inga anställda registrerade.");
                    }
                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            var e = list[i];
                            Console.WriteLine($"{i + 1}. {e}");
                        }
                    }
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt val. Ange 1, 2 eller 3.");
                }
            }
        }
    }
}
