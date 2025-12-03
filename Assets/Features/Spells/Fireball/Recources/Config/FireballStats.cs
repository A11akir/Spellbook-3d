using UnityEngine;

namespace Features.Spells.Fireball.Recources.Config
{
    [CreateAssetMenu(
        fileName = "Fireball",
        menuName = "Configs/Spell/Fireball",
        order = 1)]
    public class FireballStats : ScriptableObject
    {
       [SerializeField] private int _damage = 25;
       [SerializeField] private int _lifeTime = 3;
       [SerializeField] private int _missleSpeed = 100;
       [SerializeField] private int _cooldown = 2;
       [SerializeField] private int _cost = 12;
    }
}