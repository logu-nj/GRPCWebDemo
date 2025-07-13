using Grpc.Net.Client;

namespace Service2.GRPCApps
{
    public class GRPCClient
    {
        public readonly SayHello.SayHelloClient client;
        public GRPCClient()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5001");
            client = new SayHello.SayHelloClient(channel);
        }
    }
}
