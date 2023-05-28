using UnityEngine;

[System.Serializable]
public abstract class MouseLightStateClass
{
    public abstract void EnterState(MouseLightController element);

    public abstract void UpdateState(MouseLightController element);
}
