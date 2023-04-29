using GameLogic.Gravity.Components;
using Leopotam.EcsLite;

public sealed class InitGameSystem : IEcsInitSystem {

    public void Init(IEcsSystems systems) {
        var world = systems.GetWorld();
        var entity = world.NewEntity();
        var requestPool = world.GetPool<InitGravitySourceRequest>();
        requestPool.Add(entity);
    }
}