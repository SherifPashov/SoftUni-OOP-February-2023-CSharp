namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database _database;
        [SetUp]
        public void Setup()
        {
            _database = new Database();
        }

        [TearDown]

        public void TearDown()
        {
            _database = null;
        }

        [Test]

        public void AddElementTest()
        {

            _database.Add(new Person(1, "Pesho"));
            Person result = _database.FindById(1);

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Pesho", result.UserName);
        }



        [Test]
        public void ShouldThrowIfMoreThanMaximumLenght()
        {
            Person[] people = CreateFullArray();
            _database = new Database(people);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => _database.Add(new Person(25, "Az")));

            Assert.That(exception.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));

        }

        public Person[] CreateFullArray()
        {
            Person[] people = new Person[16];

            for (int i = 0; i < 16; i++)
            {
                people[i] = new Person(i, i.ToString());
            }
            return people;
        }


        [Test]

        public void AddShouldThrowIfnotUniqueUserName()
        {
            _database.Add(new Person(1, "Pesho"));

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => _database.Add(new Person(2, "Pesho")));
            Assert.That(exception.Message, Is.EqualTo("There is already user with this username!"));


        }
        [Test]
        public void AddShouldThrowIfnotUniqueId()
        {
            _database.Add(new Person(1, "Pesho"));

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => _database.Add(new Person(1, "Gosho")));
            Assert.That(exception.Message, Is.EqualTo("There is already user with this Id!"));


        }
        [Test]

        public void ConstructorDataBase()
        {
            _database = new Database(new Person(1, "Gosho"), new Person(2, "Pesho"));

            Person first = _database.FindById(1);
            Person second = _database.FindById(2);

            Assert.AreEqual(2, _database.Count);
            Assert.AreEqual("Gosho", first.UserName);
            Assert.AreEqual("Pesho", second.UserName);

        }

        [Test]

        public void RemoveFromEmptyDatebaseShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() => _database.Remove());

        }

        [Test]

        public void RemoveFromDateebase()
        {

            _database = new Database(new Person(1, "Gosho"), new Person(2, "Pesho"));
            _database.Remove();

            Person first = _database.FindById(1);


            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual("Gosho", first.UserName);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => _database.FindByUsername("Pesho"));
            Assert.That(exception.Message, Is.EqualTo("No user is present by this username!"));

        }

        [TestCase(null)]
        [TestCase("")]
        public void FindByUsernameShouldThrowIfUserNameNullOrEmpty(string name)
        {


            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => _database.FindByUsername(name));
            Assert.That(exception.ParamName, Is.EqualTo("Username parameter is null!"));
        }

        [Test]
        public void FindByUsernameShouldThrowIfUserNameDoesNotExist()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => _database.FindByUsername("Gosho"));

            Assert.That(exception.Message, Is.EqualTo("No user is present by this username!"));
        }

        [Test]

        public void FindByUserNameReturnCorrectUser()
        {
            _database = new Database(new Person(1, "Gosho"), new Person(2, "Pesho"));

            Person person = _database.FindByUsername("Pesho");

            Assert.AreEqual("Pesho", person.UserName);
            Assert.AreEqual(2, person.Id);
        }


        [TestCase(-10)]
        [TestCase(-1)]
        public void FindByUsernameShouldThrowIfIdLessThanZero(int id)
        {


            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => _database.FindById(id));
            Assert.That(exception.ParamName, Is.EqualTo("Id should be a positive number!"));
        }

        [Test]
        public void FindByUsernameShouldThrowIfIdDoesNotExist()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => _database.FindById(1));

            Assert.That(exception.Message, Is.EqualTo("No user is present by this ID!"));
        }

        [Test]

        public void FindByIdReturnCorrectUser()
        {
            _database = new Database(new Person(1, "Gosho"), new Person(2, "Pesho"));

            Person person = _database.FindById(2);

            Assert.AreEqual("Pesho", person.UserName);
            Assert.AreEqual(2, person.Id);
        }
    }
}