using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HereTestManager : MonoBehaviour
{
    [SerializeField] private float latitude;
    [SerializeField] private float longitude;
    [SerializeField] private int limit;
    [SerializeField, Tooltip("In meters.")] private int radius;
    [SerializeField] private string category;
    [SerializeField] private OnlineMaps map;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
