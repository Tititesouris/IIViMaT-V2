using System.Collections;
using System.Runtime;
using UnityEngine;
using UnityEngine.VR;
using Valve.VR;

public class InitScene : MonoBehaviour {
    
	void Start () {
        DisableVR();
	}
	
	void Update () {
		
	}

    IEnumerator LoadDevice(string newDevice, bool enable)
    {
        VRSettings.LoadDeviceByName(newDevice);
        yield return null;
        VRSettings.enabled = enable;
    }

    void EnableVR()
    {
        StartCoroutine(LoadDevice("SteamVR", true));
    }

    void DisableVR()
    {
        StartCoroutine(LoadDevice("None", false));
    }
}
