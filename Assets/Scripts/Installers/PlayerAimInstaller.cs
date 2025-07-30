using Gameplay.Aim;
using Gameplay.Aim.Impls;
using UnityEngine;
using UnityEngine.Splines;
using Zenject;

namespace Installers
{
    public class PlayerAimInstaller: MonoInstaller
    {
        [SerializeField] private Transform arrowTransform;
        [SerializeField] private SplineContainer splineContainer;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform bowTransform;
        [SerializeField] private Transform trajectoryTransform;

        public override void InstallBindings()
        {
            Container.Bind<IAimLogic>()
                .To<AimLogic>()
                .AsSingle();

            Container.Bind<IAimVisualizer>()
                .To<SplineAimVisualizer>()
                .AsSingle()
                .WithArguments(splineContainer, lineRenderer, bowTransform);

            Container.Bind<IArrowManipulator>()
                .To<ArrowManipulator>()
                .AsSingle()
                .WithArguments(arrowTransform);

            Container.BindInterfacesAndSelfTo<TrajectoryVisualizer>()
                .AsSingle()
                .WithArguments(trajectoryTransform);
        }
    }
}