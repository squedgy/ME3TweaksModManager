﻿using System.IO;
using LegendaryExplorerCore.Helpers;
using Newtonsoft.Json;

namespace ME3TweaksModManager.modmanager.objects.mod.merge
{
    /// <summary>
    /// Asset file packaged in a merge mod
    /// </summary>
    public class MergeAsset
    {
        /// <summary>
        /// Filename of the asset. Only used during serialization and for logging errors
        /// </summary>
        [JsonProperty(@"filename")]
        public string FileName { get; set; }

        /// <summary>
        /// Size of the asset
        /// </summary>
        [JsonProperty(@"filesize")] 
        public int FileSize { get; set; }

        /// <summary>
        /// Asset binary data. This is only loaded when needed - mods loaded from archive will have this populated, mods loaded from disk will not.
        /// </summary>
        [JsonIgnore]
        public byte[] AssetBinary;

        /// <summary>
        /// Where the data for this asset begins in the stream (post magic). Used when loading from disk on demand. Mods loaded from archive will load it when the file is parsed
        /// </summary>
        [JsonIgnore] 
        public int FileOffset;

        /// <summary>
        /// Reads the asset binary data into the <see cref="AssetBinary"/> byte array. Seeks and reads from the specified stream.
        /// </summary>
        /// <param name="mergeFileStream">The stream to read from.</param>
        public void ReadAssetBinary(Stream mergeFileStream)
        {
            if (FileOffset != 0)
                mergeFileStream.Seek(FileOffset, SeekOrigin.Begin);
            AssetBinary = mergeFileStream.ReadToBuffer(FileSize);
        }
    }
}
