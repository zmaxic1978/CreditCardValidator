using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreditCardValidator.Models;
using CreditCardValidator.Controllers;
using System.Collections.Generic;

namespace CreditCardValidator.Test
{
    [TestClass]
    public class ModuleTest
    {
        [TestMethod]
        public void TestValidVisa()
        {
            ValidateController v = new ValidateController();
            TestCard tc = ModuleTest.TestCards[0];
            Card c = new Card() { CardNom = tc.CardNom, ExpDate = tc.ExpDate };
            ValidateResult vr = v.Check(c);
            Assert.AreEqual(tc.ExpectResult, vr.CardType + vr.Result);
        }

        [TestMethod]
        public void TestValidMasterCard()
        {
            ValidateController v = new ValidateController();
            TestCard tc = ModuleTest.TestCards[1];
            Card c = new Card() { CardNom = tc.CardNom, ExpDate = tc.ExpDate };
            ValidateResult vr = v.Check(c);
            Assert.AreEqual(tc.ExpectResult, vr.CardType + vr.Result);
        }

        [TestMethod]
        public void TestValidAmexCard()
        {
            ValidateController v = new ValidateController();
            TestCard tc = ModuleTest.TestCards[2];
            Card c = new Card() { CardNom = tc.CardNom, ExpDate = tc.ExpDate };
            ValidateResult vr = v.Check(c);
            Assert.AreEqual(tc.ExpectResult, vr.CardType + vr.Result);
        }

        [TestMethod]
        public void TestValidJCBCard()
        {
            ValidateController v = new ValidateController();
            TestCard tc = ModuleTest.TestCards[3];
            Card c = new Card() { CardNom = tc.CardNom, ExpDate = tc.ExpDate };
            ValidateResult vr = v.Check(c);
            Assert.AreEqual(tc.ExpectResult, vr.CardType + vr.Result);
        }

        [TestMethod]
        public void TestInvalidVisa()
        {
            ValidateController v = new ValidateController();
            TestCard tc = ModuleTest.TestCards[4];
            Card c = new Card() { CardNom = tc.CardNom, ExpDate = tc.ExpDate };
            ValidateResult vr = v.Check(c);
            Assert.AreEqual(tc.ExpectResult, vr.CardType + vr.Result);
        }

        [TestMethod]
        public void TestInvalidMasterCard()
        {
            ValidateController v = new ValidateController();
            TestCard tc = ModuleTest.TestCards[5];
            Card c = new Card() { CardNom = tc.CardNom, ExpDate = tc.ExpDate };
            ValidateResult vr = v.Check(c);
            Assert.AreEqual(tc.ExpectResult, vr.CardType + vr.Result);
        }

        [TestMethod]
        public void TestInvalidAmexCard()
        {
            ValidateController v = new ValidateController();
            TestCard tc = ModuleTest.TestCards[6];
            Card c = new Card() { CardNom = tc.CardNom, ExpDate = tc.ExpDate };
            ValidateResult vr = v.Check(c);
            Assert.AreEqual(tc.ExpectResult, vr.CardType + vr.Result);
        }

        [TestMethod]
        public void TestInvalidJCBCard()
        {
            ValidateController v = new ValidateController();
            TestCard tc = ModuleTest.TestCards[7];
            Card c = new Card() { CardNom = tc.CardNom, ExpDate = tc.ExpDate };
            ValidateResult vr = v.Check(c);
            Assert.AreEqual(tc.ExpectResult, vr.CardType + vr.Result);
        }

        private static List<TestCard> TestCards
        {
            get
            {
                var testProducts = new List<TestCard>();
                testProducts.Add(new TestCard { CardNom = 4000111122223333, ExpDate = "072020", ExpectResult = "VisaValid" });
                testProducts.Add(new TestCard { CardNom = 5000111122223333, ExpDate = "112027", ExpectResult = "MasterCardValid" });
                testProducts.Add(new TestCard { CardNom = 340111122223333, ExpDate = "032021", ExpectResult = "AmexValid" });
                testProducts.Add(new TestCard { CardNom = 3570111122223333, ExpDate = "102019", ExpectResult = "JCBValid" });
                testProducts.Add(new TestCard { CardNom = 4000111122223333, ExpDate = "072019", ExpectResult = "VisaInvalid" });
                testProducts.Add(new TestCard { CardNom = 5000111122223333, ExpDate = "112022", ExpectResult = "MasterCardInvalid" });
                testProducts.Add(new TestCard { CardNom = 340111122223333, ExpDate = "032020", ExpectResult = "AmexInvalid" });
                testProducts.Add(new TestCard { CardNom = 3570111122223333, ExpDate = "102020", ExpectResult = "JCBInvalid" });
                return testProducts;
            }
        }
    }

    public class TestCard
    {
        public long CardNom { get; set; }
        public string ExpDate { get; set; }
        public string ExpectResult { get; set; }
    }
}

