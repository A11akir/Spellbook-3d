using Features.GameBootstrap;
using Features.Menu;
using Zenject;

public class StartGameCommand : AbstractButton, IMenuCommand
{
    private LevelBootstrap _bootstrap;

    [Inject]
    public void Construct(LevelBootstrap bootstrap)
    {
        _bootstrap = bootstrap;
    }

    protected override void OnExecute()
    {
        _bootstrap.StartLevel();
    }
}