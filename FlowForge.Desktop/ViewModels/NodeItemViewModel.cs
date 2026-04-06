using System;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlowForge.Engine;
using FlowForge.Engine.Node;

namespace FlowForge.Desktop.ViewModels;

public partial class NodeItemViewModel(IEngine engine, Guid id, string name, NodeState state)
    : ViewModelBase
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActionText))]
    [NotifyPropertyChangedFor(nameof(BackgroundColor))]
    [NotifyPropertyChangedFor(nameof(ForegroundColor))]
    [NotifyCanExecuteChangedFor(nameof(ExecuteActionCommand))]
    private NodeState _state = state;

    public string ActionText => State switch
    {
        NodeState.WaitingForInitializing => "Waiting for Initialization",
        NodeState.Ready => "Start",
        NodeState.Running => "Stop",
        NodeState.Stopped => "Start",
        NodeState.Error => "Error",
        _ => "Unknown"
    };

    public IBrush BackgroundColor => State switch
    {
        NodeState.WaitingForInitializing => new SolidColorBrush(Color.Parse("#3A3D44")),
        NodeState.Ready => new SolidColorBrush(Color.Parse("#294436")),
        NodeState.Running => new SolidColorBrush(Color.Parse("#1F3E52")),
        NodeState.Stopped => new SolidColorBrush(Color.Parse("#4A4130")),
        NodeState.Error => new SolidColorBrush(Color.Parse("#5A2D33")),
        _ => new SolidColorBrush(Color.Parse("#353535"))
    };

    public static IBrush ForegroundColor => new SolidColorBrush(Color.Parse("#F3F4F6"));
    
    [RelayCommand(CanExecute = nameof(CanExecuteAction))]
    private async Task ExecuteActionAsync()
    {
        if (State == NodeState.Running)
            await engine.StopNodeAsync(Id);
        else
            await engine.StartNodeAsync(Id);
    }

    private bool CanExecuteAction() => State is not (NodeState.WaitingForInitializing or NodeState.Error);
}