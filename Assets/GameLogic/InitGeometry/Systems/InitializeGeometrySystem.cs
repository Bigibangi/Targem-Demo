using GameLogic.InitGeometry.Components;
using Leopotam.EcsLite;

namespace GameLogic.InitGeometry.Systems {

    public sealed class InitializeGeometrySystem : IEcsRunSystem {

        public void Run(IEcsSystems systems) {
            var world = systems.GetWorld();
            var initGeometryFilter = world.Filter<InitRequest>().End();
            var initRequests = world.GetPool<InitRequest>();
            foreach (var entity in initGeometryFilter) {
                ref var initRequest = ref initRequests.Get(entity);
            }
        }
    }
}