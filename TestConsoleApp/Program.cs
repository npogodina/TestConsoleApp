using System;

namespace Coding.Exercise
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {
        private int _age;
        private Bird _bird = new Bird();
        private Lizard _lizard = new Lizard();

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                _bird.Age = value;
                _lizard.Age = value;
            }
        }

        public string Fly()
        {
            return _bird.Fly();
        }

        public string Crawl()
        {
            return _lizard.Crawl();
        }
    }
}