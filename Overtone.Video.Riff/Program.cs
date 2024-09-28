using Kaitai;
using RiffParserDemo2;

namespace Overtone.Video.Riff;

public class Program
{
    static void Main()
    {
        string filename = @"D:\Games\TONE\INTRO.AVI";
        var avi = Avi.FromFile(filename);
        var i = 0;
        foreach (var dataEntry in avi.Data.Entries)
        {
            var name = @$"T:\Temp\block{i++}.dat";
            Console.WriteLine(dataEntry);
            Console.WriteLine(dataEntry.Data);
            Console.WriteLine(dataEntry.FourCc);

            if (dataEntry.M_RawData != null)
                File.WriteAllBytes(name, dataEntry.M_RawData);

            switch (dataEntry.Data)
            {
                case Avi.ListBody x:
                {
                    foreach (var entry in x.Data.Entries)
                    {
                        Console.WriteLine(entry);
                    }
                    break;
                }
                case Avi.StrhBody x:
                {
                    Console.WriteLine(x);
                    Console.WriteLine(x.FccHandler);
                    break;
                }
            }
        }
    }

    static void Main3()
    {
        string filename = @"D:\Games\TONE\INTRO.AVI";
        var data = Kaitai.Riff.FromFile(filename);
        processRiff(data);
    }

    private static void processRiff(Kaitai.Riff data)
    {
        var root = data.M_Root;
        foreach (var subchunk in root.Subchunks)
        {
            Console.WriteLine($"{subchunk.ChunkId}: {subchunk.ChunkIdReadable}");
            var data2 = subchunk.Chunk.M_Root;
            Console.WriteLine($"Data: {data2.ChunkId}");
            if (data2.IsRiffChunk)
            {
                processRiff(data2);
            }
        }
    }

    // Parse a RIFF file
    static void Main2(string[] args)
    {
        // Create a parser instance
        RiffParser rp = new RiffParser();
        try
        {
            string filename = @"D:\Games\TONE\INTRO.AVI";
            //string filename = @"C:\WINNT\Media\Chimes.wav"
            if (0 != args.Length)
            {
                filename = args[0];
            }

            // Specify a file to open
            rp.OpenFile(filename);

            // If we got here - the file is valid.
            //Output information about the file
            Console.WriteLine("File " + rp.ShortName +
                              " is a \"" + RiffParser.FromFourCC(rp.FileRIFF) +
                              "\" with a specific type of \"" +
                              RiffParser.FromFourCC(rp.FileType) + "\"");

            // Store the size to loop on the elements
            int size = rp.DataSize;

            // Define the processing delegates
            RiffParser.ProcessChunkElement pc =
                new RiffParser.ProcessChunkElement(ProcessChunk);
            RiffParser.ProcessListElement pl =
                new RiffParser.ProcessListElement(ProcessList);

            // Read all top level elements and chunks
            while (size > 0)
            {
                // Prefix the line with the current top level type
                Console.Write(RiffParser.FromFourCC(rp.FileType) +
                              " (" + size.ToString() + "): ");
                // Get the next element (if there is one)
                if (false == rp.ReadElement(ref size, pc, pl)) break;
            }

            // Close the stream
            rp.CloseFile();
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Problem: " + ex.ToString());
        }

        Console.WriteLine("\n\rDone. Press 'Enter' to exit.");
        Console.ReadLine();
    }

    // Process a RIFF list element (list sub elements)
    public static void ProcessList(RiffParser rp, int FourCC, int length)
    {
        string type = RiffParser.FromFourCC(FourCC);
        Console.WriteLine("Found list element of type \"" +
                          type + "\" and length " + length.ToString());

        // Define the processing delegates
        RiffParser.ProcessChunkElement pc =
            new RiffParser.ProcessChunkElement(ProcessChunk);
        RiffParser.ProcessListElement pl =
            new RiffParser.ProcessListElement(ProcessList);

        // Read all the elements in the current list
        try
        {
            while (length > 0)
            {
                // Prefix each line with the type of the current list
                Console.Write(type + " (" + length.ToString() + "): ");
                // Get the next element (if there is one)
                if (false == rp.ReadElement(ref length, pc, pl)) break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Problem: " + ex.ToString());
        }
    }

    // Process a RIFF chunk element (skip the data)
    public static void ProcessChunk(RiffParser rp,
        int FourCC, int length, int paddedLength)
    {
        string type = RiffParser.FromFourCC(FourCC);
        Console.WriteLine("Found chunk element of type \"" +
                          type + "\" and length " + length.ToString());

        // Skip data and update bytesleft
        rp.SkipData(paddedLength);
    }
}
