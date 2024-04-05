#region

using Imperium.Core;
using Imperium.MonoBehaviours.VisualizerObjects;
using UnityEngine;

#endregion

namespace Imperium.Visualizers;

internal class KnifeIndicators() : BaseVisualizer<Shovel>("Knife Indicators")
{
    internal void Refresh(KnifeItem knife, bool isActivelyHolding)
    {
        if (!indicatorObjects.TryGetValue(knife.GetInstanceID(), out var indicatorObject))
        {
            indicatorObject = new GameObject();
            indicatorObject.transform.SetParent(knife.transform);
            indicatorObject.AddComponent<KnifeIndicator>();

            indicatorObjects[knife.GetInstanceID()] = indicatorObject;
        }

        indicatorObject.GetComponent<KnifeIndicator>().Init(knife, isActivelyHolding);
        indicatorObject.gameObject.SetActive(ImpSettings.Visualizations.KnifeIndicators.Value);
    }
}