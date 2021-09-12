using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace Knjizara.EndToEndTestovi
{
    [TestClass]
    public class KorisnickiInterfejsTestovi
    {
        private IWebDriver _driver;
        private readonly string baseUrl = "https://localhost:44316/";

        [TestInitialize]
        public void InicijalnaPostavka()
        {
            _driver = new ChromeDriver();
            _driver.Url = baseUrl;
        }

        [TestMethod]
        public void Testiranje_Navigacije_Na_Pocetnu_Stranicu()
        {
            //Arrange
            IWebElement homeBtn = _driver.FindElement(By.LinkText("Knjizara"));
            //Act
            homeBtn.Click();
            //Assert
            Assert.AreEqual(baseUrl, _driver.Url);
        }

        [TestMethod]
        public void Testiranje_Navigacije_Na_Stranicu_Za_Dodavanje_Knjige()
        {
            //Arrange
            IWebElement homeBtn = _driver.FindElement(By.PartialLinkText("Dodaj knjigu"));
            //Act
            homeBtn.Click();
            //Assert
            Assert.AreEqual($"{baseUrl}Knjizara/Dodaj", _driver.Url);
        }

        [TestMethod]
        public void Testiranje_Navigacije_Na_Stranicu_Za_Dodavanje_Kategorije()
        {
            //Arrange
            IWebElement homeBtn = _driver.FindElement(By.PartialLinkText("Dodaj kategoriju"));
            //Act
            homeBtn.Click();
            //Assert
            Assert.AreEqual($"{baseUrl}Knjizara/DodajKategoriju", _driver.Url);
        }

        [TestMethod]
        public void Testiranje_Dodavanja_Kategorija_Sa_Ispavnim_Nazivom_Popunjava_Select_Listu()
        {
            //Arrange
            _driver.Navigate().GoToUrl($"{baseUrl}Knjizara/DodajKategoriju");
            IWebElement naziv = _driver.FindElement(By.Id("Naziv"));
            IWebElement dugme = _driver.FindElement(By.Id("Dodaj"));
            //Act
            naziv.SendKeys("TestKategorija");
            dugme.Click();

            _driver.Navigate().GoToUrl($"{baseUrl}Knjizara/Dodaj");
            IWebElement kategorija = _driver.FindElement(By.Id("Kategorija"));
            SelectElement select = new SelectElement(kategorija);
            List<String> opcije = select.Options.Select(x => x.Text).ToList();
            //Assert
            CollectionAssert.Contains(opcije, "TestKategorija");
        }

        [TestMethod]
        public void Testiranje_Dodavanja_Kategorije_Sa_Praznim_Imenom_Izbacuje_Gresku()
        {
            //Arrange
            _driver.Navigate().GoToUrl($"{baseUrl}Knjizara/DodajKategoriju");
            IWebElement dugme = _driver.FindElement(By.Id("Dodaj"));
            //Act
            dugme.Click();

            IEnumerable<IWebElement> greske = _driver.FindElements(By.ClassName("field-validation-error"));
            //Assert
            Assert.AreNotEqual(0, greske.Count());
        }

        [TestMethod]
        public void Testiranje_Dodavanja_Knjige_Sa_Ispravnim_Podacima_Popunjava_Tabelu()
        {
            //Arrange
            _driver.Navigate().GoToUrl($"{baseUrl}Knjizara/Dodaj");
            IWebElement naziv = _driver.FindElement(By.Id("Naziv"));
            IWebElement cena = _driver.FindElement(By.Id("Cena"));
            IWebElement kategorija = _driver.FindElement(By.Id("Kategorija"));
            SelectElement select = new SelectElement(kategorija);
            IWebElement dugme = _driver.FindElement(By.Id("Potvrdi"));
            //Act
            naziv.SendKeys("TestKnjiga");
            cena.SendKeys("20.0");
            select.SelectByText("TestKategorija");
            dugme.Click();

            IWebElement nazivRezultat = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[1]"));
            IWebElement kategorijaRezultat = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[2]"));
            IWebElement cenaRezultat = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[3]"));

            //Assert
            Assert.AreEqual("TestKnjiga", nazivRezultat.Text);
            Assert.AreEqual("TestKategorija", kategorijaRezultat.Text);
            Assert.AreEqual("20", cenaRezultat.Text);
        }

        [TestMethod]
        public void Testiranje_Dodavanja_Knjige_Sa_Neispravnim_Podacima_Izbacuje_Gresku()
        {
            //Arrange
            _driver.Navigate().GoToUrl($"{baseUrl}Knjizara/Dodaj");
            IWebElement naziv = _driver.FindElement(By.Id("Naziv"));
            IWebElement cena = _driver.FindElement(By.Id("Cena"));
            IWebElement kategorija = _driver.FindElement(By.Id("Kategorija"));
            SelectElement select = new SelectElement(kategorija);
            IWebElement dugme = _driver.FindElement(By.Id("Potvrdi"));
            //Act
            dugme.Click();

            IEnumerable<IWebElement> greske = _driver.FindElements(By.ClassName("field-validation-error"));
            //Assert
            Assert.AreNotEqual(0, greske.Count());
        }

        [TestMethod]
        public void Testiranje_Izmene_Knjige_Sa_Ispravnim_Podacima_Menja_Podatke_U_Tabeli()
        {
            //Arrange
            _driver.Navigate().GoToUrl(baseUrl);
            IWebElement izmeniDugme = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[4]/a"));
            izmeniDugme.Click();

            IWebElement naziv = _driver.FindElement(By.Id("Naziv"));
            IWebElement cena = _driver.FindElement(By.Id("Cena"));
            IWebElement kategorija = _driver.FindElement(By.Id("Kategorija"));
            SelectElement select = new SelectElement(kategorija);
            IWebElement dugme = _driver.FindElement(By.Id("Potvrdi"));
            //Act
            naziv.Clear();
            cena.Clear();
            naziv.SendKeys("TestIzmena");
            cena.SendKeys("25.0");
            select.SelectByText("TestKategorija");
            dugme.Click();

            IWebElement nazivRezultat = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[1]"));
            IWebElement kategorijaRezultat = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[2]"));
            IWebElement cenaRezultat = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[3]"));

            //Assert
            Assert.AreEqual("TestIzmena", nazivRezultat.Text);
            Assert.AreEqual("TestKategorija", kategorijaRezultat.Text);
            Assert.AreEqual("25", cenaRezultat.Text);
        }

        [TestMethod]
        public void Testiranje_Izmena_Knjige_Sa_Neispravnim_Podacima_Izbacuje_Gresku()
        {
            //Arrange
            _driver.Navigate().GoToUrl(baseUrl);
            IWebElement izmeniDugme = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[4]/a"));
            izmeniDugme.Click();
            IWebElement naziv = _driver.FindElement(By.Id("Naziv"));
            IWebElement cena = _driver.FindElement(By.Id("Cena"));
            IWebElement kategorija = _driver.FindElement(By.Id("Kategorija"));
            SelectElement select = new SelectElement(kategorija);
            IWebElement dugme = _driver.FindElement(By.Id("Potvrdi"));
            //Act
            naziv.Clear();
            cena.Clear();
            dugme.Click();

            IWebElement greska = _driver.FindElement(By.ClassName("field-validation-error"));
            //Assert
            Assert.IsNotNull(greska);
        }

        [TestMethod]
        public void Testiranje_Brisanja_Knjige_Smanjuje_Tabelu()
        {
            //Arrange
            _driver.Navigate().GoToUrl(baseUrl);
            IWebElement obrisiDugme = _driver.FindElement(By.XPath("(//table[1]/tbody/tr)[last()]/td[5]/form/button"));
            IEnumerable<IWebElement> podaci = _driver.FindElements(By.XPath("//table/tbody/tr"));
            int kolicina = podaci.Count();

            //Act
            obrisiDugme.Click();
            podaci = _driver.FindElements(By.XPath("//table/tbody/tr"));

            //Assert
            Assert.AreEqual(kolicina - 1, podaci.Count());
        }

        [TestCleanup]
        public void ZatvaranjeDrajvera()
        {
            _driver.Close();
        }
    }
}
