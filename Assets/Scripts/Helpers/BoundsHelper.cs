using UnityEngine;

namespace Helpers
{
    public static class BoundsHelper
    {
        public static Vector3 ClampToScreenBounds(this Vector3 point, float padding)
        {
            Vector2 bounds = ResolutionHelper.cameraWorldBounds;

            float minX = 0 + padding;
            float minY = 0 + padding;
            float maxX = bounds.x - padding;
            float maxY = bounds.y - padding;

            if (point.x < minX) point.x = minX;
            if (point.y < minY) point.y = minY;
            if (point.x > maxX) point.x = maxX;
            if (point.y > maxY) point.y = maxY;

            return point;
        }
    }
}
