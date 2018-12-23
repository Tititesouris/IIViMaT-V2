using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class InitScene : MonoBehaviour
{
    private void Start()
    {
        DisableVR();
    }

    private void Update()
    {
    }

    private IEnumerator LoadDevice(string newDevice, bool enable)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = enable;
    }

    private void EnableVR()
    {
        StartCoroutine(LoadDevice("SteamVR", true));
    }

    private void DisableVR()
    {
        StartCoroutine(LoadDevice("None", false));
    }
}