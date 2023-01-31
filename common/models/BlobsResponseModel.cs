using System;
using System.Collections.Generic;
using fileuploader.common.models;

namespace fileuplaoder.common.Models

{
    public class BlobsResponseModel : ResponseModel
    {

        public List<BlobModel>? blob {get; set;}

    }

}
