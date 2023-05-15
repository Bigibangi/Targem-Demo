using Leopotam.EcsLite;
using UnityEngine;

namespace GameLogic.Core {

    internal class GravitySourceViewFactory : AbstractViewFactory {

        public GravitySourceViewFactory(EcsPackedEntityWithWorld packedEntity, GameObject prefab) : base(packedEntity, prefab) {
        }

        public override void InstatiateView() {
            base.InstatiateView();
        }
    }
}