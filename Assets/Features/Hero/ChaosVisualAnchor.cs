using UnityEngine;

namespace Features.Hero
{
    public class ChaosVisualAnchor : MonoBehaviour
    {
        [SerializeField] private GameObject _pentagram;

        public GameObject Pentagram => _pentagram;
    }
}