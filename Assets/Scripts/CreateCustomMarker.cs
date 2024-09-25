using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCustomMarker : MonoBehaviour
{
    public OnlineMaps map;

    public GameObject prefab;
    public Vector2 startingCoord;

    private List<OnlineMapsMarker> markers2D = new List<OnlineMapsMarker>();
    private List<OnlineMapsMarker3D> markers3D = new List<OnlineMapsMarker3D>();

    public void CreateHundred2DMarkers()
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log("Creating 2D marker number: " + i);
            // Create a marker at the location of the result.
            OnlineMapsMarker marker = map.markerManager.Create(new Vector2(startingCoord.x + i, startingCoord.y + i), i.ToString());
            markers2D.Add(marker);
        }
    }

    public void CreateHundred3DMarkers()
    {
        for (int i = 0; i < 100; i++)
        {
            // Create a marker at the location of the result.
            OnlineMapsMarker3D marker = map.marker3DManager.Create(new Vector2(startingCoord.x - i, startingCoord.y - i), prefab);
            markers3D.Add(marker);
        }
    }

    public void Clear3DMarkers()
    {
        map.marker3DManager.RemoveAll();
        markers3D.Clear();
    }

    public void Clear2DMarkers()
    {
        map.markerManager.RemoveAll();
        markers2D.Clear();
    }
}
