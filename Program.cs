using System;
using System.Collections.Generic;
using LinQ.PeopleClasses;
using LinQ.LinQies;

namespace LinQ
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Human> People = new List<Human>()
            {
                new Human{ name = "Adam", surname = "West   ", age = 60, gender = HumanGender.male},

                new Human{ name = "Peter", surname = "Griffin", age = 40, gender = HumanGender.male},
                new Human{ name = "Lloys", surname = "Griffin", age = 43, gender = HumanGender.female},
                new Human{ name = "Stewie", surname = "Griffin", age = 2, gender = HumanGender.male},

                new Human{ name = "Bob", surname = "Belcher", age = 45, gender = HumanGender.male},
                new Human{ name = "Linda", surname = "Belcher", age = 42, gender = HumanGender.female},

                new Human{ name = "Rick", surname = "Sanchez", age = 70, gender = HumanGender.male},
                new Human{ name = "Betty", surname = "Smith  ", age = 34, gender = HumanGender.female},

                new Human{ name = "Betty", surname = "Wine    ", age = 42, gender = HumanGender.female},

                new Human{ name = "Francine", surname = "Smith  ", age = 40, gender = HumanGender.female}
            };
            Queries Query = new Queries();
            List<Human> sorted;

            Console.WriteLine("Список людей:");
            PrintList(People);
            // 1
            Console.WriteLine("\n1. Данные людей с минимальным и максимальным возрастом:\n" +
                "(Я не понял, наверное, в 1 и 9 нужно одно и то же, только в 9 ещё и имена)\n" +
                "Max:");
            sorted = Query.AgeMinAndMax(People, Aggregation.max);
            PrintList(sorted);
            Console.WriteLine("\nMin:");
            sorted = Query.AgeMinAndMax(People, Aggregation.min);
            PrintList(sorted);
            // 2
            Console.WriteLine("\n2.В алфавитном порядке людей определённого пола\nЖенщины:");
            sorted = Query.SortingNameOfOneGender(People, HumanGender.female);
            PrintList(sorted);
            Console.WriteLine("Мужчины:");
            sorted = Query.SortingNameOfOneGender(People, HumanGender.male);
            PrintList(sorted);
            // 3
            try
            {
                Console.WriteLine("\n3. Средний возраст людей с именем более коротким, чем 5 символов: " + Convert.ToInt32(Query.AverageAge(People)));
            }
            catch (InvalidOperationException empty)
            {
                Console.WriteLine(empty.Message);
            }
            // 4
            Console.WriteLine("\n4. Группы однофамильцев:\n");
            Query.Namesakes(People);
            // 5
            Console.WriteLine("\n5. Количество несовершеннолетних людей:\n ");
            Console.WriteLine(Query.MinorsCount(People));
            // 6
            Console.WriteLine("\n6. Все буквы, которые не встречаются в записи имен людей: \n");
            Dictionary<char, bool> letters = Query.UnusedLetters(People);
            bool first = true;
            foreach (KeyValuePair<char, bool> letter in letters)
            {
                if (letter.Value)
                {
                    if (first)
                    {
                        Console.Write($"{letter.Key}");
                        first = false;
                    }
                    else
                    {
                        Console.Write($", {letter.Key}");
                    }
                }
            }
            // 7
            Console.WriteLine("\n7. Содержимое коллекции в формате Json:\n");
            Console.WriteLine(Query.TransformToJson(People));
            // 8
            Console.WriteLine("\n8.Возрасты, встречающиеся у людей обоих полов.:\n");
            List<int> ages = Query.UnisexAges(People);
            first = true;
            foreach (int age in ages)
            {
                if (first)
                {
                    Console.Write($"{age}");
                    first = false;
                }
                else
                {
                    Console.Write($", {age}");
                }
            }
            // 9
            Console.WriteLine("\n9. Имя человека с минимальным/максимальным возрастом. смотрите в пункте 1\n");
            // 10 Пары из людей различного пола./////

        }
        static void PrintList(List<Human> people)
        {
            foreach (Human human in people)
            {
                Console.WriteLine($"Name: {human.name}\t Surname: {human.surname}\t Age: {human.age}\t Gender: {human.gender}");
            }
        }
    }
}
