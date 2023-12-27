using NUnit.Framework;
using System.Diagnostics;
using System.Reflection;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RobotTestConstruktor()
        {
            Robot robot = new Robot("Ne", 1000, 5);
            Assert.AreEqual("Ne", robot.Model);
            Assert.AreEqual(1000, robot.Price);
            Assert.AreEqual(5, robot.InterfaceStandard);
            Assert.AreEqual(0, robot.Supplements.Count);
        }
        [Test]
        public void RobotToString()
        {
            Robot robot = new Robot("Ne", 1000, 5);
            Assert.AreEqual("Ne", robot.Model);
            Assert.AreEqual(1000, robot.Price);
            Assert.AreEqual(5, robot.InterfaceStandard);
            Assert.AreEqual(0, robot.Supplements.Count);
            Assert.AreEqual($"Robot model: Ne IS: 5, Price: 1000.00", robot.ToString());
            
        }

        [Test]
        public void SuppermentTestConstruktor()
        {
            Supplement supplement = new Supplement("Ne", 5);
            Assert.AreEqual("Ne", supplement.Name);
            
            Assert.AreEqual(5, supplement.InterfaceStandard);
            
        }

        [Test]
        public void SuppermentToString()
        {
            Supplement supplement = new Supplement("Ne", 5);
            Assert.AreEqual("Ne", supplement.Name);

            Assert.AreEqual(5, supplement.InterfaceStandard);

            Assert.AreEqual($"Supplement: Ne IS: 5", supplement.ToString());

        }
        [Test]
        public void FactoryConstrucktorTest()
        {
            Factory factory = new Factory("Da",5);

            Assert.AreEqual("Da", factory.Name);
            Assert.AreEqual(5, factory.Capacity);
            Assert.AreEqual(0, factory.Robots.Count);
            Assert.AreEqual(0, factory.Supplements.Count);
        }

        [Test]
        public void FactoryProduceRobotCurrect()
        {
            Factory factory = new Factory("Da", 5);
            string result = factory.ProduceRobot("Wili", 505, 8);

            Robot robot = factory.Robots[0];

            Assert.AreEqual($"Produced --> {robot}", result);

            Assert.AreEqual("Wili", robot.Model);
            Assert.AreEqual(505, robot.Price);
            Assert.AreEqual(8, robot.InterfaceStandard);
            Assert.AreEqual(0, robot.Supplements.Count);
        }
        [Test]
        public void FactoryProduceRobotUnCurrect()
        {
            Factory factory = new Factory("Da", 2);
            string result = factory.ProduceRobot("Wili", 505, 8);

            Robot robot = factory.Robots[0];

            Assert.AreEqual($"Produced --> {robot}", result);
            Assert.AreEqual("Wili", robot.Model);
            Assert.AreEqual(505, robot.Price);
            Assert.AreEqual(8, robot.InterfaceStandard);
            Assert.AreEqual(0, robot.Supplements.Count);

            string result2 = factory.ProduceRobot("Wil", 50, 3);

            Robot robot2 = factory.Robots[1];

            Assert.AreEqual($"Produced --> {robot}", result);
            Assert.AreEqual("Wil", robot2.Model);
            Assert.AreEqual(50, robot2.Price);
            Assert.AreEqual(3, robot2.InterfaceStandard);
            Assert.AreEqual(0, robot2.Supplements.Count);


            string result3 = factory.ProduceRobot("Wil", 50, 3);
            Assert.AreEqual($"The factory is unable to produce more robots for this production day!", result3);
        }
        [Test]
        public void FactoryProduceSupplement()
        {
            Factory factory = new Factory("Da", 5);
            string result = factory.ProduceSupplement("Wili", 8);

            Supplement supplement = factory.Supplements[0];

            Assert.AreEqual($"Supplement: Wili IS: 8", result);

            Assert.AreEqual("Wili", supplement.Name);
           
            Assert.AreEqual(8, supplement.InterfaceStandard);
            Assert.AreEqual(1, factory.Supplements.Count);

            
        }

        [Test]
        public void FactoryUpgradeRobot()
        {
            Factory factory = new Factory("Da", 5);
            
            string result = factory.ProduceSupplement("Wili", 8);
            string result2 = factory.ProduceRobot("Wil", 50, 3);
            Assert.AreEqual(1, factory.Supplements.Count);
            Assert.AreEqual(1, factory.Robots.Count);

            Robot robot = factory.Robots[0];
            Supplement supplement = new Supplement("da", 3);

            Assert.AreEqual(true,factory.UpgradeRobot(robot,supplement));
            Assert.AreEqual("da", robot.Supplements[0].Name);
            Assert.AreEqual(3, robot.Supplements[0].InterfaceStandard);

            Assert.AreEqual(false, factory.UpgradeRobot(robot, supplement));
            Supplement supplement2 = new Supplement("Ne", 4);
            Assert.AreEqual(false, factory.UpgradeRobot(robot, supplement2));
        }

        [Test]
        public void FactorySellRobot()
        {
            Factory factory = new Factory("Da", 5);

            
            string result1 = factory.ProduceRobot("Wil", 50, 3);
            string result2 = factory.ProduceRobot("Wili", 55, 4);
            Robot robot = factory.SellRobot(30);

            Assert.IsNull(robot);

            Robot robot1 = factory.SellRobot(60);
            Assert.AreEqual("Wili", robot1.Model);
            Assert.AreEqual(55, robot1.Price);
            Assert.AreEqual(4, robot1.InterfaceStandard);

        }
    }
}