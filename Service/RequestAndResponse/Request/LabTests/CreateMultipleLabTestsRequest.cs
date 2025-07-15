using System.Collections.Generic;
using Service.RequestAndResponse.Request.LabTests;

namespace Service.RequestAndResponse.Request.LabTests
{
    public class CreateMultipleLabTestsRequest
    {
        public List<CreateLabTestRequest> LabTests { get; set; }
    }
} 