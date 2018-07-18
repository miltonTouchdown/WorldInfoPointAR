using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APPManager : MonoBehaviour {

    private StatusAPP _currStatus = StatusAPP.Init;
    public StatusAPP CurrStatus {
        get
        {
            return _currStatus;
        }
        set
        {
            if(_currStatus != value)
            {
                _currStatus = value;
                changeStatusApp(_currStatus);
            }          
        }
    }

    private static APPManager _instance;
    public static APPManager Instance
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

    void Start ()
    {
        Instance.CurrStatus = StatusAPP.Init;
	}
	
	void Update ()
    {
		
	}

    private void changeStatusApp(StatusAPP status)
    {
        switch (status)
        {
            case StatusAPP.Init:
                {
                    Debug.Log(CurrStatus);
                    break;
                }
            case StatusAPP.CreateObjectLocation:
                {
                    Debug.Log(CurrStatus);
                    break;
                }
            case StatusAPP.EditObjectLocation:
                {
                    break;
                }
        }
    }

    public void saveData()
    {
        DataManager.Save(ObjectLocationManager.Instance.lstARObject);
    }

}

public enum StatusAPP {Init, EditObjectLocation, CreateObjectLocation}