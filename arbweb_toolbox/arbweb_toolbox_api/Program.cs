namespace arbweb_toolbox_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var app = builder.Build();
            app.UseFileServer();
            app.MapControllers();
            app.Run();
        }
    }
}