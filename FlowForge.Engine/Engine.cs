namespace FlowForge.Engine;

public class Engine(Runner runner, List<Node.Node> nodes)
{
    public async Task InitializeAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task StartAsync()
    {
        await runner.Run(nodes);
    }
    
    public async Task StopAsync()
    {
        await runner.Stop(nodes);
    }
}