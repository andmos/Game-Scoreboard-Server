using Topshelf.Nancy;
using Topshelf; 

namespace GameScoreboardServer
{
    public class Program
    {
        static void Main()
        {
            var host = HostFactory.New(x =>
            {
                x.UseLinuxIfAvailable(); 
                x.Service<GameScoreboardServerSelfHost>(s =>
                {
                    s.ConstructUsing(settings => new GameScoreboardServerSelfHost());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                    s.WithNancyEndpoint(x, c =>
                    {
                        c.AddHost(port: 5000);
                        c.CreateUrlReservationsOnInstall();
                        c.OpenFirewallPortsOnInstall(firewallRuleName: "GameScoreboardServer");
                    });
                });
                x.StartAutomatically();
                x.SetServiceName("GameScoreboardServer");
                x.SetDisplayName("GameScoreboardServer");
                x.SetDescription("GameScoreboardServer");
                x.RunAsNetworkService();
            });
            host.Run();
        }
    }
}
