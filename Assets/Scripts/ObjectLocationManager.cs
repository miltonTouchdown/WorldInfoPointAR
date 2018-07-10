using Mapbox.Unity.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityARInterface;
using UnityEngine;

public class ObjectLocationManager : MonoBehaviour {

    private ARFocusSquare _ARFocus;

    public GameObject prefabMessage;

    private static ObjectLocationManager _instance;
    public static ObjectLocationManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        _ARFocus = GameObject.FindObjectOfType<ARFocusSquare>();
    }

    void Update ()
    {
		
	}

    public void createInterestPoint()
    {
        Transform currLocation = _ARFocus.GetTransformLocation();
        //Console.Instance.Log(currLocation.transform.position.ToString(), "aqua");
        if (currLocation != null)
        {
            GameObject message = Instantiate(prefabMessage, transform);
            message.transform.position = currLocation.position;
            message.transform.rotation = currLocation.localRotation;
        }
    }
}
