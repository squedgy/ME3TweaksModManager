﻿using System;
using System.IO;
using LegendaryExplorerCore.TLK.ME2ME3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ME3TweaksModManager.Tests
{
    [TestClass]
    public class TLKTests
    {
        [TestMethod]
        public void TestTLKs()
        {
            GlobalTest.Init();

            Console.WriteLine(@"Testing TLK operations");
            var tlksDir = Path.Combine(GlobalTest.GetTestingDataDirectoryFor("tlk"), "me3");

            var tlksToTestOn = Directory.GetFiles(tlksDir, "*.tlk", SearchOption.AllDirectories);
            foreach (var tlk in tlksToTestOn)
            {
                var talkFileMe2 = new ME2ME3TalkFile(tlk);
                var tlkStream = LegendaryExplorerCore.TLK.ME2ME3.HuffmanCompression.SaveToTlkStream(talkFileMe2.StringRefs);
                tlkStream.Position = 0;
                var reloadedTlk = new ME2ME3TalkFile(tlkStream);

                foreach (var v in talkFileMe2.StringRefs)
                {
                    var fd = reloadedTlk.FindDataById(v.StringID);

                    if (fd == "\"Male\"") continue; //Male/Female, we don't have way to distinguish these
                    Assert.AreEqual($"\"{v.Data}\"", fd);
                }
            }
        }
    }
}
