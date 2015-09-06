﻿using MGR.CommandLineParser;

namespace SimpleApp
{
    internal class Program
    {
        private static int Main()
        {
            var arguments = new[] { "pack", @"MGR.CommandLineParser\MGR.CommandLineParser.csproj", "-Build", "-Properties", "Configuration=Release", "-Exclude", "Test", "-Symbols" };
            var parserBuild = new ParserBuilder();
            var parser = parserBuild.BuildParser();
            var commandResult = parser.Parse(arguments);
            if (commandResult.IsValid)
            {
                return commandResult.Execute();
            }
            return (int)commandResult.ReturnCode;
        }
    }
}