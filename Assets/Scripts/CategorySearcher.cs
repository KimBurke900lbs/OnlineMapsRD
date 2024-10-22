using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CategorySearcher : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField, Range(-90.0f, 90.0f)] private float latitude;
    [SerializeField, Range(-180.0f, 180.0f)] private float longitude;
    [SerializeField, Range(20, 100)] private int limit = 100;
    [SerializeField, Tooltip("In meters.")] private int radius = 500;
    [Header("References")]
    [SerializeField] private OnlineMaps map;
    [SerializeField] private OnlineMapsKeyManager keyManager;
    [SerializeField] private GameObject prefab;

    private CancellationTokenSource apiCancellationToken = new CancellationTokenSource();
    private List<Place> places = new List<Place>();
    private List<OnlineMapsMarker> markers = new List<OnlineMapsMarker>();

    // Categories link - https://www.here.com/docs/bundle/places-search-api-developer-guide/page/topics/place_categories/places-category-system.html

    private void OnDestroy()
    {
        apiCancellationToken.Cancel();
        apiCancellationToken.Dispose();
    }

    public void SearchHealthcare()
    {
        places.Clear();
        PlaceMarkers(places);
    }

    public void SearchSchools()
    {
        places.Clear();
        PlaceMarkers(places);
    }

    public void SearchFood()
    {
        places.Clear();
        PlaceMarkers(places);
    }

    private async void HereSearch(string category)
    {
        List<Item> items = await HereAPIManager.GetDiscoverPlaces(apiCancellationToken.Token, keyManager.hereApiKey, category, 0, limit, latitude, longitude, radius);
        Debug.Log($"Found {items.Count} items...");
        if (items != null)
        {
            foreach (Item item in items)
            {
                Debug.Log($"Creating marker for {item.title} at {item.position.lat}, {item.position.lng}.");
                places.Add(new Place((float)item.position.lat, (float)item.position.lng, item.title));
                
            }
        }
        
    }

    private void PlaceMarkers(List<Place> places)
    {
        // delete the old markers
        map.markerManager.RemoveAll();
        markers.Clear();

        foreach (Place place in places)
        {
            OnlineMapsMarker marker = map.markerManager.Create(new Vector2((float)place.longitude, (float)place.latitude), place.title);
            markers.Add(marker);
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
