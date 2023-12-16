using UnityEngine;

namespace GraphicsManagement
{
    public abstract class MonoGraphicsService : MonoBehaviour, IGraphicsService
    {
        public abstract void Apply(GraphicsType type);
    }
}
