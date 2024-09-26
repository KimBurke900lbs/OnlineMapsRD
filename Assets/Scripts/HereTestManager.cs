using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class HereTestManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField, Range(-90.0f, 90.0f)] private float latitude;
    [SerializeField, Range(-180.0f, 180.0f)] private float longitude;
    [SerializeField, Range(20,100)] private int limit = 100;
    [SerializeField, Tooltip("In meters.")] private int radius = 500;
    [SerializeField] private string category = "restaurant";
    [Header("References")]
    [SerializeField] private OnlineMaps map;
    [SerializeField] private OnlineMapsKeyManager keyManager;
    [SerializeField] private TMP_InputField apiAppField;
    [SerializeField] private TMP_InputField apiKeyField;
    [SerializeField] private TMP_InputField googleKeyField;
    [SerializeField] private GameObject prefab;

    private CancellationTokenSource apiCancellationToken = new CancellationTokenSource();
    private List<Place> places = new List<Place>();
    private List<OnlineMapsMarker> markers = new List<OnlineMapsMarker>();

    private void OnDestroy()
    {
        apiCancellationToken.Cancel();
        apiCancellationToken.Dispose();
    }

    public async void OnDiscoverButton()
    {
        if (keyManager != null)
        {
            keyManager.hereApiKey = apiKeyField.text;
            keyManager.hereAppID = apiAppField.text;
            keyManager.googleMaps = googleKeyField.text;
        }
        List<Item> items = await HereAPIManager.GetDiscoverPlaces(apiCancellationToken.Token, keyManager.hereApiKey, category, 0, limit, latitude, longitude, radius);
        Debug.Log($"Found {items.Count} items...");
        if (items != null)
        {
            foreach (Item item in items)
            {
                Debug.Log($"Creating marker for {item.title} at {item.position.lat}, {item.position.lng}.");
                places.Add(new Place((float)item.position.lat, (float)item.position.lng, item.title));
                OnlineMapsMarker marker = map.markerManager.Create(new Vector2((float)item.position.lng, (float)item.position.lat), item.title);
                markers.Add(marker);
            }
        }
        // Get center point and best zoom for markers
        Debug.Log($"Created {markers.Count} markers.");
        Vector2 center;
        int zoom;
        OnlineMapsUtils.GetCenterPointAndZoom(markers.ToArray(), out center, out zoom);

        // Set map position and zoom.
        map.position = center;
        map.zoom = zoom + 1;
    }
}

public class Place
{
    public float latitude;
    public float longitude;
    public string title;

    public Place(float latitude, float longitude, string title)
    {
        this.latitude = latitude;
        this.longitude = longitude;
        this.title = title;
    }
}