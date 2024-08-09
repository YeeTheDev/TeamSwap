using UnityEngine;

namespace TSwap.Stats
{
    [CreateAssetMenu(fileName = "New Damage Stats", menuName = "Damage Stats")]
    public class DamageStats : ScriptableObject
    {
        [SerializeField] Vector3 damageForce;
        [SerializeField] float invulnerabilityTime = 1.5f;
        [SerializeField] float pushTime = 0.5f;
        [SerializeField] string damagerTag;
        [SerializeField] string itemTag;

        public Vector3 DamageForce => damageForce;
        public float PushTime => pushTime;
        public float InvulnerabilityTime => invulnerabilityTime;
        public string DamagerTag => damagerTag;
        public string ItemTag => itemTag;
    }
}