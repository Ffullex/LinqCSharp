using System;
using System.Collections.Generic;
using LinQ.PeopleClasses;
using System.Linq;

namespace LinQ.LinQies
{
    class Queries
    {


        // 1
        // Запрос "Минимальный/максимальный возраст."
        public List<Human> AgeMinAndMax(List<Human> People, Aggregation aggregation/*, Aggregation aggregationMin*/)
        {
            switch (aggregation)
            {
                case Aggregation.max:
                    var maxAge = People.GroupBy(human => human.age).Max(human => human.Key);
                    List<Human> maxAgeHuman = People.Where(age => age.age == maxAge).ToList();
                    return maxAgeHuman;

                case Aggregation.min:
                    var minAge = People.GroupBy(human => human.age).Min(human => human.Key);
                    List<Human> minAgeHuman = People.Where(age => age.age == minAge).ToList();
                    return minAgeHuman;

                default:
                    return new List<Human> { };
            }

        }
        // 2
        // Запрос "В алфавитном порядке только людей определенного пола."
        public List<Human> SortingNameOfOneGender(List<Human> People, HumanGender gender)
        {
            switch (gender)
            {
                case HumanGender.male:
                    var sorted = from human in People
                                 where human.gender == HumanGender.male
                                 orderby human.name
                                 select human;
                    return FillList(sorted);

                case HumanGender.female:
                    sorted = from human in People
                             where human.gender == HumanGender.female
                             orderby human.name
                             select human;
                    return FillList(sorted);

                default:
                    return new List<Human> { };
            }
        }
        private List<Human> FillList(IOrderedEnumerable<Human> select)
        {
            List<Human> newList = new List<Human> { };

            foreach (Human human in select)
            {
                newList.Add(human);
            }

            return newList;
        }
        // 3
        // Запрос "Средний возраст людей с именем короче 5 символов."
        public double AverageAge(List<Human> People)
        {
            try
            {
                return People.Where(human => human.name.Length < 5).Average(human => human.age);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Такие люди отсутствуют");
            }
        }
        // 4
        // Запрос "Группы однофамильцев."
        public void Namesakes(List<Human> People)
        {
            var namesakes = from human in People
                            group human by human.surname;

            foreach (IGrouping<string, Human> namesake in namesakes)
            {
                if (namesake.Count() == 1)
                {
                    continue;
                }
                else
                {

                    foreach (var human in namesake)
                        Console.WriteLine($"{namesake.Key} {human.name}");

                    Console.WriteLine();
                }
            }
        }
        // 5
        // Запрос "Количество несовершеннолетних людей." 
        public int MinorsCount(List<Human> People)
        {
            return People.Where(human => human.age < 18).Count();
        }
        // 6
        // Запрос "Все буквы, которые не встречаются в записи имен людей."(???)
        public Dictionary<char, bool> UnusedLetters(List<Human> People)
        {
            Dictionary<char, bool> letters = new Dictionary<char, bool>
            {
                { 'A', true }, 
                { 'B', true },
                { 'C', true }, 
                { 'D', true }, 
                { 'E', true },
                { 'F', true }, 
                { 'G', true }, 
                { 'H', true }, 
                { 'I', true }, 
                { 'J', true }, 
                { 'K', true },
                { 'L', true }, 
                { 'M', true }, 
                { 'N', true }, 
                { 'O', true }, 
                { 'P', true }, 
                { 'Q', true }, 
                { 'R', true },
                { 'S', true }, 
                { 'T', true }, 
                { 'U', true }, 
                { 'V', true }, 
                { 'W', true }, 
                { 'X', true }, 
                { 'Y', true }, 
                { 'Z', true }
            };

            var linqHuman = from human in People
                             select human;

            foreach (Human human in People)
            {
                foreach (char letter in human.name.ToUpper())
                {
                    if (letters[letter])
                    {
                        letters[letter] = false;
                    }
                }
            }

            return letters;
        }
        // 7
        // Запрос "Содержимое коллекции в формате Json."
        public string TransformToJson(List<Human> People)
        {
            var collHuman = from human in People
                             select human;

            string jsonString = "{\n\"People\":[\n";

            foreach (Human human in collHuman)
            {
                jsonString +=
                    "\t{\n" +
                    $"\t\"name\": \"{human.name}\"\n" +
                    $"\t\"surname\": \"{human.surname}\"\n" +
                    $"\t\"age\": {human.age}\n" +
                    $"\t\"gender\": {human.gender}\n";

                if (human == collHuman.Last())
                {
                    jsonString += "\t}\n";
                }
                else
                {
                    jsonString += "\t},\n\n";
                }
            }

            jsonString = jsonString + "\t]\n}";

            return jsonString;
        }
        // 8
        // Запрос "Возрасты, встречающиеся у людей обоих полов." 
        public List<int> UnisexAges(List<Human> People)
        {
            List<int> ages = new List<int> { };

            var ageMaleHuman = People
                    .Where(male => male.gender == HumanGender.male)
                    .Select(male => male);

            foreach (Human ageMale in ageMaleHuman)
            {
                var ageFemaleAge = People
                    .Where(female => female.gender == HumanGender.female && female.age == ageMale.age)
                    .Select(female => female.age);

                if (ageFemaleAge.Count() > 0)
                {
                    ages.Add(ageMale.age);
                }
            }

            return ages;
        }
        // 9
        // Запрос "Имя человека с минимальным/максимальным возрастом." ????????????

        // 10
        // Запрос "Пары из людей различного пола."????????????

    }
}
