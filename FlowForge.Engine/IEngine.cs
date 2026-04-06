using FlowForge.Engine.Node;

namespace FlowForge.Engine;

public interface IEngine
{
    IReadOnlyList<Node.Node> Nodes { get; }

    event EventHandler<NodeEventArgs>? NodeAdded;
    event EventHandler<NodeEventArgs>? NodeRemoved;
    event EventHandler<NodeStateChangedEventArgs>? NodeStateChanged;

    Task InitializeAsync(CancellationToken ct = default);

    Task StartNodeAsync(Guid nodeId, CancellationToken ct = default);
    Task StopNodeAsync(Guid nodeId, CancellationToken ct = default);
}