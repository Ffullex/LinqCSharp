using System.Collections;

namespace LinQ.PeopleClasses
    {
    public class Human : IEnumerable
    {
        public string name { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        public HumanGender gender { get; set; }

        public IEnumerator GetEnumerator()
        {
        return GetEnumerator();
        }


    }
}

