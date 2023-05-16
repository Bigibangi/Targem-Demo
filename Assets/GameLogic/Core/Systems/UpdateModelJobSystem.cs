using GameLogic.Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace GameLogic.Core.Systems {

    public sealed class UpdateModelJobSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<Model, UpdateModelJobTag>> _proceduralModels;
        private readonly EcsPoolInject<Model> _models;

        public void Run(IEcsSystems systems) {
            foreach (var proceduralModel in _proceduralModels.Value) {
                JobHandle jobHandle = default;
                ref var model = ref _models.Value.Get(proceduralModel);
                ref var parts = ref model.parts;
                ref var matrices = ref model.matrices;
                parts[0][0] = model.root;
                var objectScale = Vector3.one;
                matrices[0][0] = float4x4.TRS(model.root.worldPosition, model.root.worldRotation, new float3(objectScale));
                for (int i = 1; i < parts.Length; i++) {
                    jobHandle = new UpdateModelJob {
                        parents = parts[i - 1],
                        parts = parts[i],
                        matrices = matrices[i]
                    }.ScheduleParallel(parts.Length, 5, jobHandle);
                }
                jobHandle.Complete();
            }
        }
    }
}