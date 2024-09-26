using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

public static class RestAPI
{
    #region Public Methods
    public static async Task<UnityWebRequest> GetRequest(string uri, (string, string)[] headers = null, (string, string)[] parameters = null)
    {
        var cert = new ForceAcceptAll();
        if (parameters != null)
        {
            uri += "?";
            foreach (var parameter in parameters)
            {
                uri += $"{parameter.Item1}={parameter.Item2}&";
            }
        }
        if (uri[uri.Length-1] == '&')
            uri = uri.Remove(uri.Length - 1);
        Debug.Log(uri);

        UnityWebRequest request = UnityWebRequest.Get(uri);

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.SetRequestHeader($"{header.Item1}", $"{header.Item2}");
            }
        }
        request.certificateHandler = cert;
        await request.SendWebRequest();
        cert?.Dispose();
        return request;
    }

    public static async Task<UnityWebRequest> PostRequest(string uri, string data, (string, string)[] headers = null, (string, string)[] parameters = null)
    {
        var cert = new ForceAcceptAll();
        if (parameters != null)
        {
            uri += "?";
            foreach (var parameter in parameters)
            {
                uri += $"{parameter.Item1}={parameter.Item2}&";
            }
        }

        UnityWebRequest request = UnityWebRequest.Put(uri, data);
        request.method = UnityWebRequest.kHttpVerbPOST;

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.SetRequestHeader($"{header.Item1}", $"{header.Item2}");
            }
        }
        request.certificateHandler = cert;
        await request.SendWebRequest();
        cert?.Dispose();
        return request;
    }

    public static async Task<UnityWebRequest> PutRequest(string uri, string data, (string, string)[] headers = null, (string, string)[] parameters = null)
    {
        var cert = new ForceAcceptAll();
        if (parameters != null)
        {
            uri += "?";
            foreach (var parameter in parameters)
            {
                uri += $"{parameter.Item1}={parameter.Item2}&";
            }
        }

        UnityWebRequest request = UnityWebRequest.Put(uri, data);

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.SetRequestHeader($"{header.Item1}", $"{header.Item2}");
            }
        }
        request.certificateHandler = cert;
        await request.SendWebRequest();
        cert?.Dispose();
        return request;
    }

    public static async Task<UnityWebRequest> DeleteRequest(string uri, (string, string)[] headers = null, (string, string)[] parameters = null)
    {
        var cert = new ForceAcceptAll();
        if (parameters != null)
        {
            uri += "?";
            foreach (var parameter in parameters)
            {
                uri += $"{parameter.Item1}={parameter.Item2}&";
            }
        }

        UnityWebRequest request = UnityWebRequest.Delete(uri);

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.SetRequestHeader($"{header.Item1}", $"{header.Item2}");
            }
        }
        request.certificateHandler = cert;
        await request.SendWebRequest();
        cert?.Dispose();
        return request;
    }
    #endregion
}