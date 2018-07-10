using Mapbox.Unity.Utilities;
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

	void Start ()
    {
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

        ////Check if the left Mouse button is clicked
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && APPManager.Instance.CurrStatus != StatusAPP.EditObjectLocation)
        //{

        //    //Set up the new Pointer Event
        //    m_PointerEventData = new PointerEventData(m_EventSystem);
        //    //Set the Pointer Event Position to that of the mouse position
        //    m_PointerEventData.position = Input.GetTouch(0).position;

        //    //Create a list of Raycast Results
        //    List<RaycastResult> results = new List<RaycastResult>();

        //    //Raycast using the Graphics Raycaster and mouse click position
        //    m_Raycaster.Raycast(m_PointerEventData, results);

        //    //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        //    foreach (RaycastResult result in results)
        //    {
        //        APPManager.Instance.CurrStatus = StatusAPP.EditObjectLocation;
        //        isEditing = true;
        //        _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        //    }
        //}
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && APPManager.Instance.CurrStatus != StatusAPP.EditObjectLocation 
        //    && _camController.IsHitARObject)
        //{
        //    edit();
        //}

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
