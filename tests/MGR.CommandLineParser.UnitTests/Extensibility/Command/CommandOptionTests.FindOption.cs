﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR.CommandLineParser.Command;
using MGR.CommandLineParser.Extensibility;
using MGR.CommandLineParser.Extensibility.Converters;
using MGR.CommandLineParser.Extensibility.DependencyInjection;
using Moq;
using Xunit;

namespace MGR.CommandLineParser.UnitTests.Extensibility.Command
{
    public partial class CommandOptionTests
    {
        public class FindOption
        {
            [Theory]
            [InlineData("property-list")]
            [InlineData("PropertyList")]
            [InlineData("pl")]
            public void D(string optionName)
            {
                // Arrange
                var testCommandType = new CommandType(typeof(FindOption.TestCommand),
                    new List<IConverter> { new StringConverter(), new GuidConverter(), new Int32Converter() });
                var dependencyResolverScopeMock = new Mock<IDependencyResolverScope>();
                dependencyResolverScopeMock.Setup(_ => _.ResolveDependency<ICommandActivator>())
                    .Returns(BasicCommandActivator.Instance);
                var expectedAlternateNames = new[]{"property-list"};
                var propertyName = nameof(TestCommand.PropertyList);
                var expectedPropertyInfo = typeof(FindOption.TestCommand).GetProperty(propertyName);

                // Act
                var actual = testCommandType.FindOption(optionName);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(propertyName, actual.DisplayInfo.Name);
                Assert.Equal(expectedAlternateNames, actual.DisplayInfo.AlternateNames);
                Assert.Equal(expectedPropertyInfo, actual.PropertyOption);

            }

            private class TestCommand : ICommand
            {
                [Display(ShortName = "pl")]
                public List<int> PropertyList { get; set; }
                public Dictionary<string, Guid> PropertyDictionary { get; set; }
                public int PropertySimple { get; set; }

                #region ICommand Members

                public int Execute()
                {
                    throw new NotImplementedException();
                }

                public IList<string> Arguments
                {
                    get { throw new NotImplementedException(); }
                }

                #endregion
            }
        }
    }
}
