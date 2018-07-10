using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageLocation : MonoBehaviour {

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public Text description;
    private Camera _camera;
    private bool isEditing;
    private TouchScreenKeyboard _keyboard = null;
    private CameraController _camController;

    //---INIT TEST---

    private ILocationProvider _locationProvider;
    Vector2d _targetPosition;

    void OnDestroy()
    {
        if (_locationProvider != null)
        {
            _locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
        }
    }


    void LocationProvider_OnLocationUpdated(Location location)
    {
        _targetPosition = location.LatitudeLongitude;
    }
    //---END TEST----

    void Start ()
    {
        //---INIT TEST---

        _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
        if (_locationProvider != null)
        {
            _locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }

        //---END TEST----

        isEditing = false;

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = FindObjectOfType<EventSystem>();

        _camera = Camera.main;
        _camController = FindObjectOfType<CameraController>();
    }
	
	void Update ()
    {
        transform.LookAt(_camera.transform);

        //---INIT TEST---
        string location = "Latitud: " + _targetPosition.x +"; Longitud: " + _targetPosition.y;
        setDescription(location);

        //---END TEST----

        if (isEditing && _keyboard != null)
        {
            if (!_keyboard.active)
            {
                setDescription(_keyboard.text);
                _keyboard = null;
                isEditing = false;
                APPManager.Instance.CurrStatus = StatusAPP.CreateObjectLocation;
            }
        }
    }
    public void edit()
    {
        APPManager.Instance.CurrStatus = StatusAPP.EditObjectLocation;
        isEditing = true;
        _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void setDescription(string message)
    {
        description.text = message;
    }
}
