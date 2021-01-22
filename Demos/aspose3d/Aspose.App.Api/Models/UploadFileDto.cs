using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspose.App.Api.Models
{
    public class UploadFileDto
    {
        public string folderName { get; set; }
        public string fileName { get; set; }

        public UploadFileDto()
        {
        }

        public UploadFileDto(string folderName, string fileName)
        {
            this.folderName = folderName;
            this.fileName = fileName;
        }
    }
}
