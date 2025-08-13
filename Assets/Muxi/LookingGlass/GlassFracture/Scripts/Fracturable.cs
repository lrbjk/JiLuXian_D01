using UnityEngine;

namespace GlassSystem
{
    public abstract class Fracturable : MonoBehaviour
    {
        [Tooltip("The fracture patterns to be used when this object breaks.")]
        public Mesh[] FracturePatterns;

        public abstract void TriggerFracture(Vector3 impactPoint, Vector3 impactForce, int patternIndex = -1, float rotation = float.NaN);
    }
}