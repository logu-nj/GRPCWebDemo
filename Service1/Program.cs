using Microsoft.AspNetCore.Server.Kestrel.Core;
using Service1.GRPCApps;

namespace Service1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();

            // Configure Kestrel with different ports
            builder.WebHost.ConfigureKestrel(options =>
            {
                // Web API (HTTP 1.1)
                options.ListenLocalhost(5000, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                });

                // gRPC (HTTP/2)
                options.ListenLocalhost(5001, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();             // Web API
                endpoints.MapGrpcService<GRPCServer>(); // gRPC
                endpoints.MapGrpcReflectionService(); // ✅ Enables grpcurl list
            });

            app.MapControllers();

            app.Run();
        }
    }
}
