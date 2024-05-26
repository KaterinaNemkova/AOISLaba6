using Microsoft.VisualStudio.TestTools.UnitTesting;
using LABA6AOIOS_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LABA6AOIOS_.Tests
{
    [TestClass()]
  
    public class LiteratureDictionaryTests
        {
            [TestMethod()]
            public void AddTerm_ShouldAddTermSuccessfully()
            {
                // Arrange
                var dictionary = new LiteratureDictionary();

                // Act
                dictionary.AddTerm("Pride and Prejudice", "A novel by Jane Austen");
                var result = dictionary.SearchTerm("Pride and Prejudice");

                // Assert
                Assert.AreEqual("A novel by Jane Austen", result);
            }

            [TestMethod()]
            public void AddTerm_ShouldHandleCollisions()
            {
                // Arrange
                var dictionary = new LiteratureDictionary();

                // Act
                dictionary.AddTerm("AA", "First definition");
                dictionary.AddTerm("BB", "Second definition");
                var result1 = dictionary.SearchTerm("AA");
                var result2 = dictionary.SearchTerm("BB");

                // Assert
                Assert.AreEqual("First definition", result1);
                Assert.AreEqual("Second definition", result2);
            }

            [TestMethod()]
            public void SearchTerm_ShouldReturnTermNotFound()
            {
                // Arrange
                var dictionary = new LiteratureDictionary();

                // Act
                var result = dictionary.SearchTerm("NonExistentTerm");

                // Assert
                Assert.AreEqual("The term is not found in the dictionary.", result);
            }

            [TestMethod()]
            public void DeleteTerm_ShouldDeleteTermSuccessfully()
            {
                // Arrange
                var dictionary = new LiteratureDictionary();
                dictionary.AddTerm("Moby Dick", "A novel by Herman Melville");

                // Act
                var deleteResult = dictionary.DeleteTerm("Moby Dick");
                var searchResult = dictionary.SearchTerm("Moby Dick");

                // Assert
                Assert.AreEqual("The term has been successfully deleted.", deleteResult);
                Assert.AreEqual("The term is not found in the dictionary.", searchResult);
            }

            [TestMethod()]
            public void DisplayAllTerms_ShouldDisplayAllTerms()
            {
                // Arrange
                var dictionary = new LiteratureDictionary();
                dictionary.AddTerm("Pride and Prejudice", "A novel by Jane Austen");
                dictionary.AddTerm("War and Peace", "A novel by Leo Tolstoy");

                // Act
                var stringWriter = new StringWriter();
                Console.SetOut(stringWriter);
                dictionary.DisplayAllTerms();
                var output = stringWriter.ToString();

                // Assert
                Assert.IsTrue(output.Contains("Pride and Prejudice: A novel by Jane Austen"));
                Assert.IsTrue(output.Contains("War and Peace: A novel by Leo Tolstoy"));
            }

        }
    }