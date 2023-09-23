using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResolutionHelper
{
    public static Vector2 cameraWorldBounds => _cameraWorldBounds ??= Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    private static Vector2? _cameraWorldBounds;
}
