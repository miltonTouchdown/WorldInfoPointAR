using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityARInterface;
using UnityEngine;

public class ObjectLocationManager : MonoBehaviour {

    public bool isDataDump = false;

    private ARFocusSquare _ARFocus;

    public List<ARObjectData> lstARObject = new List<ARObjectData>();
    public GameObject prefabMessage;

    [SerializeField]
    private AbstractMap _map;
    bool _isInitialized;

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
        _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
        if (_locationProvider != null)
        {
            _locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }

        _map.OnInitialized += () => _isInitialized = true;
        _map.OnInitialized += LoadARObject;

        //LoadARObject();
        _ARFocus = GameObject.FindObjectOfType<ARFocusSquare>();
    }

    void Update ()
    {
		
	}

    public void createInterestPoint(ARObjectData ArObject = null)
    {
        Debug.Log(ArObject + " - " + _isInitialized);
        if (ArObject != null && _isInitialized)
        {
            GameObject message = Instantiate(prefabMessage, transform);
            Vector2d latitudLongitude = new Vector2d(ArObject.Lat, ArObject.Long);
            message.transform.position = _map.GeoToWorldPosition(latitudLongitude);

            setInfo(message, ArObject.Lat, ArObject.Long);
        }
        else
        {
            Transform currLocation = _ARFocus.GetTransformLocation();
            //Console.Instance.Log(currLocation.transform.position.ToString(), "aqua");
            if (currLocation != null)
            {
                GameObject message = Instantiate(prefabMessage, transform);
                message.transform.position = currLocation.position;
                message.transform.rotation = currLocation.localRotation;

                setInfo(message, _targetPosition.x, _targetPosition.y);
            }
        }
    }

    private void setInfo(GameObject ArObject, double lat , double longi)
    {
        // TODO: Guardar nombre, id, latitud, longitud
        MessageLocation messLoc = ArObject.GetComponent<MessageLocation>();
        messLoc.Lat = lat;
        messLoc.Long = longi;
        messLoc.Message = "Escribir descripcion...";

        messLoc.latitud.text = messLoc.Lat.ToString();
        messLoc.longitud.text = messLoc.Long.ToString();
        messLoc.setDescription(messLoc.Message);
        
        // Agregar a la lista
        ARObject baseClassAR = (ARObject)messLoc;
        addToList(messLoc);
    }

    private void addToList(ARObject arObject)
    {
        if (APPManager.Instance.CurrStatus == StatusAPP.Init)
            return;

        ARObjectData data = new ARObjectData();
        data.Id = 0;
        data.Lat = arObject.Lat;
        data.Long = arObject.Long;
        data.Message = arObject.Message;

        lstARObject.Add(data);
    }

    public void LoadARObject()
    {
        //TODO: Agregar Callback para creacion de objetos de manera secuencial y para aumento de performance

        if (isDataDump)
            StartCoroutine(loadDumpData());
        else
            Instance.lstARObject = DataManager.Load().ToList();

        foreach (ARObjectData item in Instance.lstARObject)
        {
            createInterestPoint(item);
        }

        APPManager.Instance.CurrStatus = StatusAPP.CreateObjectLocation;
    }

    public void UpdateAROBject(ARObjectData arObject)
    {

    }

    /***BEGIN TEST***/

    IEnumerator loadDumpData()
    {
        Debug.Log("load Dump Data");
        ARObjectData arObj_0 = new ARObjectData();
        ARObjectData arObj_1 = new ARObjectData();
        ARObjectData arObj_2 = new ARObjectData();
        ARObjectData arObj_3 = new ARObjectData();
        arObj_0.Id = 0;
        arObj_0.Lat = -33.3912288;
        arObj_0.Long = -70.6199788;
        arObj_0.Message = "id: 0";

        arObj_1.Id = 1;
        arObj_1.Lat = -33.39102276920846;
        arObj_1.Long = -70.61898960052952;
        arObj_1.Message = "id: 1";

        arObj_2.Id = 2;
        arObj_2.Lat = -33.39113205514006;
        arObj_2.Long = -70.64487280000003;
        arObj_2.Message = "id: 2";

        arObj_3.Id = 3;
        arObj_3.Lat = -33.422517;
        arObj_3.Long = -70.6443128;
        arObj_3.Message = "id: 3";

        Instance.lstARObject.Add(arObj_0);
        Instance.lstARObject.Add(arObj_1);
        Instance.lstARObject.Add(arObj_2);
        Instance.lstARObject.Add(arObj_3);
        //Instance.lstARObject.Add(arObj_0);

        yield return new WaitForSeconds(.5f);

        foreach (ARObjectData item in Instance.lstARObject)
        {
            Debug.Log("item: " + item);
            createInterestPoint(item);
        }

        APPManager.Instance.CurrStatus = StatusAPP.CreateObjectLocation;
    }

    /***END TEST***/
}
