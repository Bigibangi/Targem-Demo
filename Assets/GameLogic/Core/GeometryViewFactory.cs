using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Core {

    internal class GeometryViewFactory : AbstractViewFactory {

        public GeometryViewFactory(EcsPackedEntityWithWorld packedEntity, GameObject prefab) : base(packedEntity, prefab) {
        }

        public void InitializeEntityWithProceduralDraw() {
            base.InstatiateView();
        }
    }
}