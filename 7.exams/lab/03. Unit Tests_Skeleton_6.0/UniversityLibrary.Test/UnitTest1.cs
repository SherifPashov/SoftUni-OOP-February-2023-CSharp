namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextBookConstructorTest()
        {
            TextBook textBook = new TextBook("asd", "dsa", "actin");
            textBook.InventoryNumber = 1;
            textBook.Holder = "d";
            Assert.AreEqual("asd", textBook.Title);
            Assert.AreEqual("dsa", textBook.Author);
            Assert.AreEqual("actin", textBook.Category);
            Assert.AreEqual(1, textBook.InventoryNumber);
            Assert.AreEqual("d", textBook.Holder);
        }

        [Test]
        public void TextBookTostring()
        {
            TextBook textBook = new TextBook("asd", "dsa", "actin");
            Assert.AreEqual(textBook.ToString(), $"Book: asd - 0{Environment.NewLine}Category: actin{Environment.NewLine}Author: dsa".Trim());
        }

        [Test]

        public void TestUniversityLibralyConstructor()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            Assert.AreEqual(0,universityLibrary.Catalogue.Count);
        }

        [Test]
        public void TestUniversityLibralyAddBock()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("asd", "dsa", "actin");
            universityLibrary.AddTextBookToLibrary(textBook);

            Assert.AreEqual(1, universityLibrary.Catalogue.Count);

            Assert.AreEqual("asd", universityLibrary.Catalogue[0].Title);
            Assert.AreEqual("dsa", universityLibrary.Catalogue[0].Author);
            Assert.AreEqual("actin", universityLibrary.Catalogue[0].Category);

        }

        [Test]
        public void testCalogue()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("asd", "dsa", "actin");
            TextBook textBook1 = new TextBook("Asd", "Dsa", "Actin");
            TextBook textBook2 = new TextBook("Bsd", "Bsa", "Bctin");

            string result1 = universityLibrary.AddTextBookToLibrary(textBook);
            string result2 = universityLibrary.AddTextBookToLibrary(textBook1);
            string result3 = universityLibrary.AddTextBookToLibrary(textBook2);

            Assert.AreEqual(result1, $"Book: asd - 1{Environment.NewLine}Category: actin{Environment.NewLine}Author: dsa".Trim());
            Assert.AreEqual(result2, $"Book: Asd - 2{Environment.NewLine}Category: Actin{Environment.NewLine}Author: Dsa".Trim());
            Assert.AreEqual(result3, $"Book: Bsd - 3{Environment.NewLine}Category: Bctin{Environment.NewLine}Author: Bsa".Trim());

            Assert.AreEqual(3, universityLibrary.Catalogue.Count);
            Assert.AreEqual("asd", universityLibrary.Catalogue[0].Title);
            Assert.AreEqual("dsa", universityLibrary.Catalogue[0].Author);
            Assert.AreEqual("actin", universityLibrary.Catalogue[0].Category);
            Assert.AreEqual(1, universityLibrary.Catalogue[0].InventoryNumber);

            Assert.AreEqual("Asd", universityLibrary.Catalogue[1].Title);
            Assert.AreEqual("Dsa", universityLibrary.Catalogue[1].Author);
            Assert.AreEqual("Actin", universityLibrary.Catalogue[1].Category);
            Assert.AreEqual(2, universityLibrary.Catalogue[1].InventoryNumber);

            Assert.AreEqual("Bsd", universityLibrary.Catalogue[2].Title);
            Assert.AreEqual("Bsa", universityLibrary.Catalogue[2].Author);
            Assert.AreEqual("Bctin", universityLibrary.Catalogue[2].Category);
            Assert.AreEqual(3, universityLibrary.Catalogue[2].InventoryNumber);
        }

        [Test]
        public void TestUniversityLibralyLoanTextBookTeatleravni()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("asd", "dsa", "actin");
            textBook.InventoryNumber = 1;
            textBook.Holder = "Az";
            universityLibrary.AddTextBookToLibrary(textBook);



            string resulet = universityLibrary.LoanTextBook(1, "Az");
            Assert.AreEqual(resulet, "Az still hasn't returned asd!");

        }
        public void TestUniversityLibralyLoanTextBookTeatleravniDefalt()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("asd", "dsa", "actin");

            string resulet = universityLibrary.LoanTextBook(2, "ti");
            Assert.AreEqual(resulet, "asd loaned to ti.");
        }
            [Test]
        public void TestUniversityLibralyLoanTextBookelse()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("asd", "dsa", "actin");
            textBook.InventoryNumber = 1;
            
            universityLibrary.AddTextBookToLibrary(textBook);

            string resulet = universityLibrary.LoanTextBook(1, "ti");
            Assert.AreEqual("ti", universityLibrary.Catalogue[0].Holder);
            Assert.AreEqual(resulet, "asd loaned to ti.");
        }

        [Test]
        public void TestUniversityLibralyReturnTextBook()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("asd", "dsa", "actin");
            textBook.InventoryNumber = 1;
            textBook.Holder = "Az";
            universityLibrary.AddTextBookToLibrary(textBook);

            string result = universityLibrary.ReturnTextBook(1);
            Assert.AreEqual(string.Empty, textBook.Holder);
            Assert.AreEqual("asd is returned to the library.", result);


        }


    }

}