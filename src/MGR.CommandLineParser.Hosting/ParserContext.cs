﻿using System.Collections.Generic;

namespace MGR.CommandLineParser.Hosting
{
    internal class ParserContext
    {
        internal IEnumerable<string> Arguments { get; set; }
        internal ParsingResult ParsingResult { get; set; }
    }
}