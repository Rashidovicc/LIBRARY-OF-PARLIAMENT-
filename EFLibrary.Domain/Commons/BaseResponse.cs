using System.Collections;
using System.Text.Json.Serialization;

namespace EFLibrary.Domain.Commons
{
    public class BaseResponse<TSorse>
    {
        public ErrorResponse Error { get; set; } = new ErrorResponse(200, "Success");
        public TSorse Data { get; set; }

        
    }
}