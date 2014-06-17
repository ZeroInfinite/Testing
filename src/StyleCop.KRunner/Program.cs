﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using KCop.Core;
using Microsoft.Build.Framework;

namespace StyleCop.KRunner
{
    public class Program
    {

        public static int Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("StyleCop.KRunner - A StyleCop commandline runner");
            Console.ResetColor();

            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("\tStyleCop.KRunner <path/to/project.json>");
                Console.WriteLine();
                Console.ReadLine();
                return -1;
            }

            var projectFile = args[0];
            if (!File.Exists(projectFile))
            {
                Console.WriteLine("File '{0}' does not exist.", Path.GetFullPath(projectFile));
                return -2;
            }

            var runner = new Runner(projectFile);

            runner.OnViolationEncountered += Runner_ViolationEncountered;
            runner.OnOutputGenerated += Runner_OutputGenerated;

            var violations = runner.Run();

            if (violations.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine();
            Console.WriteLine("Finished Processing: {0}", projectFile);
            Console.WriteLine("{0} errors found.", violations.Count);
            Console.WriteLine();

            Console.ResetColor();

            Console.ReadLine();

            return violations.Count;
        }

        private static void Runner_ViolationEncountered(object sender, ViolationEventArgs e)
        {
            Console.WriteLine("{0}: {1} Line {2} - {3}", e.Violation.Rule.CheckId, e.SourceCode.Path, e.LineNumber, e.Message);
        }

        private static void Runner_OutputGenerated(object sender, OutputEventArgs e)
        {
            // There will be a bunch of message like "processing file Bleh.cs" with low importance,
            // we're intentionally excluding those.
            if (e.Importance != MessageImportance.Low)
            {
                Console.WriteLine(e.Output);
            }
        }
    }
}
