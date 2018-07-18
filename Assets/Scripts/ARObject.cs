using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARObject : MonoBehaviour
{
    public int Id { get; /*private*/ set; }
    public double Lat { get; /*private*/ set; }
    public double Long { get; /*private*/ set; }
    public string Message { get; /*private*/ set; }

    private AbstractMap _map;
    bool _isInitialized;

    ILocationProvider _locationProvider;
    public ILocationProvider LocationProvider
    {
        private get
        {
            if (_locationProvider == null)
            {
                _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
            }

            return _locationProvider;
        }
        set
        {
            if (_locationProvider != null)
            {
                _locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;

            }
            _locationProvider = value;
            _locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }
    }

    Vector3 _targetPosition;
    Vector2d latitudLongitude;

    protected virtual void Start()
    {
        _map = FindObjectOfType<AbstractMap>();

        LocationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
        _map.OnInitialized += () => _isInitialized = true;
    }

    void OnDestroy()
    {
        if (LocationProvider != null)
        {
            LocationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
        }
    }

    void LocationProvider_OnLocationUpdated(Location location)
    {
        //if (_isInitialized && location.IsLocationUpdated)
        //{
        //    latitudLongitude = new Vector2d(Lat, Long);
        //    _targetPosition = _map.GeoToWorldPosition(latitudLongitude);
        //}
    }

    protected virtual void Update()
    {
        latitudLongitude = new Vector2d(Lat, Long);
        _targetPosition = _map.GeoToWorldPosition(latitudLongitude);
        transform.localPosition = _targetPosition;
    }
}
