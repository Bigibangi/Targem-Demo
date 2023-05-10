using GameLogic.Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Jobs;

namespace GameLogic.Core.Systems {

    public sealed class UpdateModelJobSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<Model, UpdateModelJob>> _proceduralModels;
        private readonly EcsPoolInject<Model> _models;
        private readonly EcsPoolInject<UpdateModelJob> _jobs;

        public void Run(IEcsSystems systems) {
            foreach (var proceduralModel in _proceduralModels.Value) {
                JobHandle jobHandle = default;
                var parts = _models.Value.Get(proceduralModel).parts;
                var matrices = _models.Value.Get(proceduralModel).matrices;
                jobHandle = new UpdateModelJob {
                    parts = parts,
                    matrices = matrices
                }.ScheduleParallel(parts.Length, 5, jobHandle);
                jobHandle.Complete();
            }
        }
    }
}