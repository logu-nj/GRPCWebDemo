using Microsoft.AspNetCore.Mvc;
using Service2.GRPCApps;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        public readonly GRPCClient grpcClient;
        public DemoController(GRPCClient gRPCClient)
        {
            grpcClient = gRPCClient;
        }
        // GET api/<DemoController>/5
        [HttpGet("{name}")]
        public async Task<ResponseMessage> Get(string name)
        {
            Detail d = new()
            {
                Name = name
            };
            return await grpcClient.client.GetHelloMessageAsync(d);
        }
        [HttpGet("/info/{name}")]
        public Information Getinfo(string name)
        {
            Detail d = new()
            {
                Name = name
            };
            return grpcClient.client.GetInfo(d);
        }
        [HttpGet("[Action]")]
        public Students GetStudents()
        {
            return grpcClient.client.GetStudents(new Google.Protobuf.WellKnownTypes.Empty());
        }
        [HttpGet("[Action]")]
        public Marks GetMarks()
        {
            return grpcClient.client.GetMarks(new Google.Protobuf.WellKnownTypes.Empty());
        }

    }
}
