﻿using System.ComponentModel.DataAnnotations;
using MGR.CommandLineParser.Command;

namespace MGR.CommandLineParser.Tests.Commands
{
    [CommandDisplay(Description = "SourcesCommandDescription", Usage = "SourcesCommandUsageSummary")]
    public class SourcesCommand : CommandBase
    {
        [Display(Description = "SourcesCommandNameDescription")]
        public string Name { get; set; }

        [Display(Description = "SourcesCommandSourceDescription", ShortName = "src")]
        public string Source { get; set; }

        [Display(Description = "SourcesCommandUserNameDescription")]
        public string UserName { get; set; }

        [Display(Description = "SourcesCommandPasswordDescription")]
        public string Password { get; set; }

        protected override int ExecuteCommand() => 0;
    }
}