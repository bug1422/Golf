using UnityEngine;

public class SetWorldBound : MonoBehaviour
{
    private void Awake()
    {
        var bounds = GetComponent<Collider2D>().bounds;
        Globals.WorldBounds = bounds;
    }
}
