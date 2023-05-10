using GameLogic.Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Linq;
using UnityEngine;

namespace GameLogic.Core.Systems {

    public sealed class UpdateViewSystem : IEcsRunSystem {
        private static readonly int matricesID = Shader.PropertyToID("_Matrices");
        private readonly EcsFilterInject<Inc<Model, ProceduralView>> _proceduralModels;
        private readonly EcsPoolInject<Model> _models;
        private readonly EcsPoolInject<ProceduralView> _views;

        public void Run(IEcsSystems systems) {
            foreach (var entity in _proceduralModels.Value) {
                var model = _models.Value.Get(entity);
                var view = _views.Value.Get(entity);
                var bound = new Bounds(model.parts.First().worldPosition, Vector3.one);
                for (var i = 0; i < view._matricesBuffers.Length; i++) {
                    var buffer = view._matricesBuffers[i];
                    buffer.SetData(model.matrices);
                    view.propertyBlock.SetBuffer(matricesID, buffer);
                    Graphics.DrawMeshInstancedProcedural(
                        view.mesh,
                        0,
                        view.material,
                        bound,
                        buffer.count,
                        view.propertyBlock);
                }
            }
        }
    }
}