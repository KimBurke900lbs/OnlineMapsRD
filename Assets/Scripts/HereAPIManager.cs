using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class HereAPIManager
{
    // https://www.here.com/docs/bundle/geocoding-and-search-api-v7-api-reference/page/index.html#/paths/~1discover/get
    private static string DISCOVERURL = "https://discover.search.hereapi.com/v1/discover";
    private static string GEOCODEURL = "https://geocode.search.hereapi.com/v1/geocode";
    private static string APIKEY = "4wI9joyzSSpXUy2bvfhctyVibXR27HyBn-dGFBGqiW8";

    #region Public Methods
    /*public static async Task<List<Datum>> GetAllData(CancellationToken ct, string apiLocation)
        {
            List<Datum> dataList = new List<Datum>();
            (string, string)[] headers = { ("Authorization", "Basic " + AUTHORIZATION) };
            var request = await RestAPI.GetRequest(apiLocation + CONFIG_ENDPOINT, headers);
            if (ct.IsCancellationRequested)
                return null;
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error while sending: " + request.error);
                return null;
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                string response = request.downloadHandler.text;
                Root root = null;
                try
                {
                    root = JsonUtility.FromJson<Root>(response.ToString());
                }
                catch (Exception e)
                {
                    Debug.Log("ERROR: Could not parse CMS data - " + e.Message);
                    return null;
                }
                dataList = new List<Datum>(root.data);
            }
            return dataList;
        }
    }*/

    public static async Task<List<Place>> GetDiscoverPlaces(CancellationToken ct, string category, int offset, int limit, float latitude, float longitude, float radius)
    {
        List<Place> places = new List<Place>();
        (string, string)[] parameters = { 
            ("apiKey", APIKEY),
            ("q", category),
            ("offset", offset.ToString()),
            ("limit", limit.ToString()),
            ("in", $"circle:{latitude},{longitude};r={radius}")
        };
        var request = await RestAPI.GetRequest(DISCOVERURL, null, parameters);

        if (ct.IsCancellationRequested)
            return null;
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error while sending: " + request.error);
            return null;
            ;
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);
            string response = request.downloadHandler.text;
            Root root = null;
            try
            {
                root = JsonUtility.FromJson<Root>(response.ToString());
                places = new List<Place>(root.places);
            }
            catch (Exception e)
            {
                Debug.Log("ERROR: Could not parse CMS data - " + e.Message);
                return null;
            }
        }
        return places;
    }
    #endregion
}
