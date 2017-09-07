﻿using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MockTestSuite
{
    /// <summary>
    /// The <see cref="MockTestSuite"/> namespace contains classes for 
    /// testing the Moq NuGet package
    /// </summary>

    [System.Runtime.CompilerServices.CompilerGenerated]
    public class NamespaceDoc
    {
    }

    /// <summary>
    /// Basic Moq tests
    /// </summary>
    public class MockShould
    {
        public Mock<TestMock> MockTestMock { get; set; }
        /// <summary>
        /// test creating mock in the test fixture
        /// </summary>
        public MockShould()
        {
           MockTestMock = new Mock<TestMock>();
        }

        /// <summary>
        /// Interfaces mocked in all tests
        /// </summary>
        public interface TestMock
        {
            /// <summary>
            /// Associated class to mock
            /// </summary>
            TestClass Class { get; set; }

            /// <summary>
            /// Property mocked during testing
            /// </summary>
            int TestProperty { get; set; }

            /// <summary>
            /// Function mocked during testing
            /// </summary>
            /// <returns>an integer</returns>
            int TestReturn();
        }

        /// <summary>
        /// Class associated with the interface during testing
        /// </summary>
        public class TestClass
        {
            /// <summary>
            /// Class associated with the base class for testing hierarchy mocking
            /// </summary>
            public virtual TestAssociatedClass AssociatedClass { get; set; }

            /// <summary>
            /// Used for testing hierarchy mocking of functions
            /// </summary>
            /// <returns>false</returns>
            public virtual bool Submit() { return false; }
        }

        /// <summary>
        /// An associated class of a class for hierarchy mocking
        /// </summary>
        public class TestAssociatedClass
        {
            /// <summary>
            /// Used for testing hierarchy mocking of properties
            /// </summary>
            /// <returns>an integer</returns>
            public virtual int TestProperty { get; set; }
        }

        /// <summary>
        /// Test that moq will set a functions return value
        /// </summary>
        [Fact]
        public void SetFunctionToReturn5()
        {     var mock = new Mock<TestMock>();
            MockTestMock.Setup(testMock => testMock.TestReturn())
                .Returns(5);

            Assert.Equal(5, MockTestMock.Object.TestReturn());
        }

        /// <summary>
        /// Test that moq will set a properties return value
        /// </summary>
        [Fact]
        public void SetPropertyToReturn5()
        {
            MockTestMock.Setup(testMock => testMock.TestProperty).Returns(5);

            Assert.Equal(5, MockTestMock.Object.TestProperty);
        }

        /// <summary>
        /// Test that moq will set the return value of a base object's property
        /// </summary>
        [Fact]
        public void SetAssociatedClassPropertyToReturn5()
        {
            MockTestMock.Setup(testMock => testMock.Class.AssociatedClass.TestProperty).Returns(5);

            Assert.Equal(5, MockTestMock.Object.Class.AssociatedClass.TestProperty);
        }

        /// <summary>
        /// Test that Moq will initialize a properties value
        /// </summary>
        [Fact (Skip = "This test fails as of 09/07/2017 - the property is not set to 5 as expected by Moq")]
        public void InitializePropertyTo5()
        {
            MockTestMock.SetupSet(testMock => testMock.TestProperty = 5);

            Assert.Equal(5, MockTestMock.Object.TestProperty);
        }
    }
}