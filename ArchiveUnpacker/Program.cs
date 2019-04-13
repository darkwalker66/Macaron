﻿using System;
using System.IO;
using ArchiveUnpacker.Framework;
using ArchiveUnpacker.Unpackers;

namespace ArchiveUnpacker
{
    internal static class Program
    {
        private const string ExtractDirectory = "Extracted";

        private static void Main(string[] args)
        {
            // Register all types
            UnpackerRegistry.Register<ArtemisUnpacker>(ArtemisUnpacker.IsGameFolder);
            UnpackerRegistry.Register<AIMSUnpacker>(AIMSUnpacker.IsGameFolder);

            Console.Write("Gimme directory: ");
#if DEBUG
            string directory = @"testDirs\Artemis";
            Console.WriteLine(directory);
#else
            string directory = Console.ReadLine();
#endif

            // Get unpacker
            var unpacker = UnpackerRegistry.Get(directory);
            foreach (IExtractableFile file in unpacker.LoadFiles(directory)) {
                if (file.Path is null) {
                    // TODO: make up your own path I guess
                    Console.WriteLine("File had no path, not extracting for now!");
                    continue;
                }

                Console.WriteLine("Extracting " + file.Path);

                // could add another directory to this for the game or something
                string fullPath = Path.Combine(Environment.CurrentDirectory, ExtractDirectory, file.Path);

                string fileDir = Path.GetDirectoryName(fullPath) ?? string.Empty;
                if (!Directory.Exists(fileDir))
                    Directory.CreateDirectory(fileDir);

                using (var stream = File.OpenWrite(fullPath))
                    file.WriteToStream(stream);
            }
        }
    }
}