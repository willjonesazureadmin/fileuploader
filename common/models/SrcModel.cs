using System;
using System.Collections.Generic;

namespace fileuplaoder.common.Models

{
    public class SrcModel
    {
        public string Path { get; private set; }

        public SrcModel(string path)
        {
            this.Path = path;
        }
    }

}