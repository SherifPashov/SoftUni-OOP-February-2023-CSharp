using NUnit.Framework;
using System;
using System.Numerics;

namespace FootballTeam.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void FootboalplayerConstructor()
        {
            FootballPlayer footballPlayer1 = new FootballPlayer("Az", 10, "Goalkeeper");

            Assert.AreEqual("Az",footballPlayer1.Name);
            Assert.AreEqual(10,footballPlayer1.PlayerNumber);
            Assert.AreEqual("Goalkeeper", footballPlayer1.Position);
            Assert.AreEqual(0, footballPlayer1.ScoredGoals);
        }

        [TestCase(null)]
        [TestCase("")]
        public void FootboalplayerNameNullOrEmpty(string name)
        {


            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new FootballPlayer(name, 10, "Goalkeeper"));
            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty!"));
        }

        [TestCase(0)]
        [TestCase(-30)]
        [TestCase(22)]
        [TestCase(220)]
        public void Footboalplayernumberbeetwin1and21(int numberPlayer)
        {


            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new FootballPlayer("az", numberPlayer, "Goalkeeper"));
            Assert.That(exception.Message, Is.EqualTo("Player number must be in range [1,21]"));
        }

        [TestCase("Goa")]
        [TestCase("Gadsoa")]
        [TestCase("Gosada")]

        public void FootboalplayerPositioUnCorrect(string position)
        {


            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new FootballPlayer("az", 10, position));
            Assert.That(exception.Message, Is.EqualTo("Invalid Position"));
        }


        [TestCase("Goalkeeper", "Forward", "Midfielder")]
        public void FootboalplayerPositioCorrect(string position1, string position2, string position3)
        {
            FootballPlayer footballPlayer1 = new FootballPlayer("Az", 10, position1);
            FootballPlayer footballPlayer2 = new FootballPlayer("Ti", 11, position2);
            FootballPlayer footballPlayer3 = new FootballPlayer("Toi", 12, position3);

            Assert.AreEqual("Az", footballPlayer1.Name);
            Assert.AreEqual(10, footballPlayer1.PlayerNumber);
            Assert.AreEqual("Goalkeeper", footballPlayer1.Position);
            Assert.AreEqual(0, footballPlayer1.ScoredGoals);

            Assert.AreEqual("Ti", footballPlayer2.Name);
            Assert.AreEqual(11, footballPlayer2.PlayerNumber);
            Assert.AreEqual("Forward", footballPlayer2.Position);
            Assert.AreEqual(0, footballPlayer2.ScoredGoals);

            Assert.AreEqual("Toi", footballPlayer3.Name);
            Assert.AreEqual(12, footballPlayer3.PlayerNumber);
            Assert.AreEqual("Midfielder", footballPlayer3.Position);
            Assert.AreEqual(0, footballPlayer3.ScoredGoals);

        }


        [Test]

        public void FootboalplayerScore()
        {
            FootballPlayer footballPlayer1 = new FootballPlayer("Az", 10, "Goalkeeper");
            Assert.AreEqual("Az", footballPlayer1.Name);
            Assert.AreEqual(10, footballPlayer1.PlayerNumber);
            Assert.AreEqual("Goalkeeper", footballPlayer1.Position);
            Assert.AreEqual(0, footballPlayer1.ScoredGoals);

            footballPlayer1.Score();
            Assert.AreEqual("Az", footballPlayer1.Name);
            Assert.AreEqual(10, footballPlayer1.PlayerNumber);
            Assert.AreEqual("Goalkeeper", footballPlayer1.Position);
            Assert.AreEqual(1, footballPlayer1.ScoredGoals);

            footballPlayer1.Score();
            Assert.AreEqual("Az", footballPlayer1.Name);
            Assert.AreEqual(10, footballPlayer1.PlayerNumber);
            Assert.AreEqual("Goalkeeper", footballPlayer1.Position);
            Assert.AreEqual(2, footballPlayer1.ScoredGoals);

        }

        [Test]

        public void FootboalTeamConstructor()
        {
            FootballTeam footballTeam = new FootballTeam("kachemak", 21);

            Assert.AreEqual("kachemak",footballTeam.Name);
            Assert.AreEqual(21,footballTeam.Capacity);
            Assert.AreEqual(0,footballTeam.Players.Count);

        }

        [TestCase(null)]
        [TestCase("")]
        public void FootboalTeamNameNullOrEmpty(string name)
        {


            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new FootballTeam(name, 21));
            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty!"));
        }

        [TestCase(0)]
        [TestCase(14)]
        [TestCase(-12)]
        [TestCase(10)]
        public void FootboalplayerCapasityLess15ThrowExseptiron(int numberPlayer)
        {


            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new FootballTeam("az", numberPlayer));
            Assert.That(exception.Message, Is.EqualTo("Capacity min value = 15"));
        }

        [Test]
        public void FootbollTeamAddNewPlayerCorrect() 
        {
            FootballTeam footballTeam = new FootballTeam("kachemak", 21);

            string result =footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            Assert.AreEqual(result, $"Added player Az in position Goalkeeper with number 10");
            Assert.AreEqual(footballTeam.Players[0].Name ,"Az");
            Assert.AreEqual(footballTeam.Players[0].PlayerNumber ,10);
            Assert.AreEqual(footballTeam.Players[0].Position , "Goalkeeper");
        }

        [Test]
        public void FootbollTeamAddNewPlayerUnCorrectMoreCapasity()
        {
            FootballTeam footballTeam = new FootballTeam("kachemak", 16);

            string result = footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));

            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            result = footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            string result2 = footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));
            Assert.AreEqual(result, "No more positions available!");
            Assert.AreEqual(result2, "No more positions available!");
            Assert.AreEqual(footballTeam.Players[0].Name, "Az");
            Assert.AreEqual(footballTeam.Players[0].PlayerNumber, 10);
            Assert.AreEqual(footballTeam.Players[0].Position, "Goalkeeper");
        }

        [Test]

        public void FootbollTeamPickPlayer()
        {
            FootballTeam footballTeam = new FootballTeam("kachemak", 16);
            FootballPlayer footballPlayer = footballTeam.PickPlayer("da");
            Assert.IsNull(footballPlayer);


            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));

            FootballPlayer footballPlayer1 = footballTeam.PickPlayer("Az");
            Assert.AreEqual("Az", footballPlayer1.Name);
            Assert.AreEqual(10, footballPlayer1.PlayerNumber);
            Assert.AreEqual("Goalkeeper", footballPlayer1.Position);
            Assert.AreEqual(0, footballPlayer1.ScoredGoals);
        }

        [Test]

        public void FootbollTeamPlayScore()
        {
            FootballTeam footballTeam = new FootballTeam("kachemak", 16);
            //NullReferenceException exception =Assert.Throws<NullReferenceException>(()=> footballTeam.PlayerScore(17));
            //Assert.That(exception.Message, Is.EqualTo("Object reference not set to an instance of an object."));
            


            footballTeam.AddNewPlayer(new FootballPlayer("Az", 10, "Goalkeeper"));

            string resulet2 = footballTeam.PlayerScore(10);

            Assert.AreEqual("Az", footballTeam.Players[0].Name);
            Assert.AreEqual(10, footballTeam.Players[0].PlayerNumber);
            Assert.AreEqual("Goalkeeper", footballTeam.Players[0].Position);
            Assert.AreEqual(1, footballTeam.Players[0].ScoredGoals);

            Assert.AreEqual(resulet2, $"Az scored and now has 1 for this season!");
        }


    }
}