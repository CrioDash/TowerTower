using UnityEngine;

namespace Gameplay.Tower.Impls
{
    public class TowerBlock: MonoBehaviour, ITowerBlock
    {
        public Transform Transform => transform;
    }
}