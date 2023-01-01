using System;

namespace Coding.Exercise
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        private int NextId = 0;

        public PersonFactory()
        {

        }

        public Person CreatePerson(string name)
        {
            var person = new Person
            {
                Name = name,
                Id = NextId
            };

            NextId++;

            return person;
        }
    }
}