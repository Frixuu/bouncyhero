using Frixu.BouncyHero.Managers;
using System;
using TMPro;
using Unity.Entities;

namespace Frixu.BouncyHero.Scripts
{
    [Serializable]
    public struct UiViewComponent : IComponentData
    {
        public enum ViewType
        {
            CurrentTime,
            BestTime
        }

        public ViewType ViewMode;
    }

    public class UiView : ComponentDataProxy<UiViewComponent>
    {

    }

    public class UiViewSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var tm = World.Active.GetExistingManager<TimeManager>();
            Entities.WithAll<UiViewComponent, TextMeshProUGUI>()
            .ForEach((ref UiViewComponent determiner, TextMeshProUGUI text) =>
            {
                switch (determiner.ViewMode)
                {
                    case UiViewComponent.ViewType.CurrentTime:
                        text.SetText(TimeManager.TimeFormatter(tm.CurrentTime));
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
