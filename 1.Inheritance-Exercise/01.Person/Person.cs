

namespace _01.Person
{
    public class Person
    {
        private int age;

        public string Name { get; set; }
        public virtual int Age {
            get
            {
                return age;
            }
            set
            {
                if (value>0)
                {
                     age = value;
                }
            } 
        }
        public Person(string name,int age) 
        {
            Name = name;
            Age = age;
        
        }


        public override string ToString()
        {
            return $"Name: {Name}, Age: {age}";
        }
    }
}
