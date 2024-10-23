using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

    public  void SearchLiveHealthcare(){ SearchCategory(HereCategoryID.LIVE_HEALTHCARE); }

    public void SearchWorship() { SearchCategory(HereCategoryID.WORSHIP); }

    public void SearchHigherEducation() { SearchCategory(HereCategoryID.HIGHER_EDUCATION); }

    public void SearchBusienssHealthcare(){ SearchCategory(HereCategoryID.BUSINESS_HEALTHCARE); }

    public void SearchFood() { SearchCategory($"{HereCategoryID.FINE_DINING},{HereCategoryID.TAKE_OUT},{HereCategoryID.COFFE_TEA}"); }

    public void SearchHotels() { SearchCategory(HereCategoryID.HOTEL); }

    public void SearchMuseums() { SearchCategory(HereCategoryID.MUSEUMS); }

    private async void SearchCategory(string category)
    {
        places.Clear();
        List<Item> items = new List<Item>();
        await SearchCategoryRecursive(items, category, 0);
        if (items != null)
        {
            foreach (Item item in items)
            {
                Debug.Log($"Creating marker for {item.title} at {item.position.lat}, {item.position.lng}.");
                places.Add(new Place((float)item.position.lat, (float)item.position.lng, item.title));
            }
        }
        PlaceMarkers(places);
    }

    private async Task<List<Item>> SearchCategoryRecursive(List<Item> items, string category, int offset)
    {
        List<Item> results = await HereSearch(category, offset);
        Debug.Log($"Here Browse call for category of {category} with offset of {offset}");
        if (results != null)
        {
            Debug.Log($"Received results: " + results.Count);
            // TODO - filter results for only city == Katy results
            items.AddRange(results);
            if (results.Count == 100 - offset)
            {
                offset = offset + 1;
                await SearchCategoryRecursive(items, category, offset);
            }
        }
        return items;
    }

    private async Task<List<Item>> HereSearch(string category, int offset)
    {
        List<Item> results = await HereAPIManager.GetBrowsePlaces(apiCancellationToken.Token, keyManager.hereApiKey, category, offset, limit, latitude, longitude, radius);
        return results;
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
