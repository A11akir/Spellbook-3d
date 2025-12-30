using UnityEngine;

[CreateAssetMenu(
    fileName = "HeroStatsData",
    menuName = "Configs/ScaleDebuff/Chaos",
    order = 1)]
public class ChaosDebuffStatsData : ScriptableObject, IScaleDebuffStatsData
{
    [SerializeField] private int _timeDebuff;

    public int TimeDebuff
    {
        get => _timeDebuff;
        set => _timeDebuff = value;
    }
}