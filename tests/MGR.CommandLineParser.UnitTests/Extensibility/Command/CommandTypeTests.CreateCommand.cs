﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MGR.CommandLineParser.Command;
using MGR.CommandLineParser.Extensibility;
using MGR.CommandLineParser.Extensibility.Command;
using MGR.CommandLineParser.Extensibility.Converters;
using Moq;
using Xunit;

namespace MGR.CommandLineParser.UnitTests.Extensibility.Command
{
    public partial class CommandTypeTests
    {
        public class CreateCommand
        {
            [Fact]
            public void PropertyWithNoConverterException()
            {
                // Arrange
                var testCommandType = new CommandType(typeof(TestBadConverterCommand),
                    new List<IConverter>
                    {
                        new StringConverter(),
                        new GuidConverter(),
                        new Int32Converter(),
                        new BooleanConverter()
                    }, new List<IOptionAlternateNameGenerator>());
                var serviceProviderMock = new Mock<IServiceProvider>();
                serviceProviderMock.Setup(_ => _.GetService(typeof(ICommandActivator)))
                    .Returns(BasicCommandActivator.Instance);
                var optionName = nameof(TestBadConverterCommand.PropertySimpleWithBadConverter);
                var expectedMessageException =
                    Constants.ExceptionMessages.ParserSpecifiedConverterNotValid(optionName,
                        testCommandType.Metadata.Name, typeof(int), typeof(bool));

                // Act
                var actualException =
                    Assert.Throws<CommandLineParserException>(
                        () => testCommandType.CreateCommand(serviceProviderMock.Object, new ParserOptions()));

                // Assert
                Assert.Equal(expectedMessageException, actualException.Message);
            }
            private class TestBadConverterCommand : ICommand
            {
                [Converter(typeof(BooleanConverter))]
                public int PropertySimpleWithBadConverter { get; set; }

                #region ICommand Members

                public Task<int> ExecuteAsync()
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
