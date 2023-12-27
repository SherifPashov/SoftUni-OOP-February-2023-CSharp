﻿using _05.BirthdayCelebrations.Interface;


namespace _05.BirthdayCelebrations.Model
{
    public class Citizen : ICitizen,IBirthdate,IIdentifiable
    {
        public Citizen(string name, int age, string id, string birthdate)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthdate;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public string Id { get; private set; }

        public string Birthdate { get; private set; }
    }
}
