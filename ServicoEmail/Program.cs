using Microsoft.Extensions.Hosting;
using Topshelf;

class Program
{
    static void Main(string[] args)
    {
        HostFactory.Run(config =>
        {
            config.Service<Bootstrap>(service =>
            {
                service.ConstructUsing(s => new Bootstrap());
                service.WhenStarted(s => s.Start());
                service.WhenStopped(s => s.Stop());
            });
            config.RunAsLocalSystem();
            config.SetServiceName("MyService");
            config.SetDisplayName("My Service");
            config.SetDescription("Example of service develped using Topshelf");
            config.StartAutomatically();
        });
    }
}