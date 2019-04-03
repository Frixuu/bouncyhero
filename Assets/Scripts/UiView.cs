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
            var pdm = World.Active.GetExistingManager<PlayerDataManager>();

            Entities.WithAll<UiViewComponent, TextMeshProUGUI>()
            .ForEach((ref UiViewComponent determiner, TextMeshProUGUI text) =>
            {
                switch (determiner.ViewMode)
                {
                    case UiViewComponent.ViewType.CurrentTime:
                        text.SetText(TimeManager.TimeFormatter(tm.CurrentTime));
                        break;
                    case UiViewComponent.ViewType.BestTime:
                        var current = tm.CurrentTime;
                        var saved = pdm.Data.BestTime;
                        text.SetText(TimeManager.TimeFormatter(current > saved ? current : saved));
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
