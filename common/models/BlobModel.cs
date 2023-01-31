using System;
using System.Collections.Generic;

namespace fileuplaoder.common.Models

{
    public class BlobModel
    {

        public Guid Id { get; set; }

        public SrcModel? Src { get; set; }

        public  BlobFileTypes FileType { get; set; }

        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

    }

        public enum BlobFileTypes
    {
        jpeg,
        png,
        doc,
        pdf

    }
}