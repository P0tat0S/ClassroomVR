using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DrawFromController : MonoBehaviour {

    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject drawTrace;
    private InputDevice targetDevice;

    // Start is called before the first frame update
    void Start() {
        TryInitialise();
    }

    void TryInitialise() {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0) {
            targetDevice = devices[0];
        }
    }

    /*
        Function that Instantiates Objects on trigger press that will remain
        in the space giving the ability to draw, it also destroys all of the
        objects placed on the screen with the grip button press.
    */
    void Draw() {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f) {
            Instantiate(drawTrace, transform.position, Quaternion.identity);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.1f) {
            GameObject[] traces = GameObject.FindGameObjectsWithTag("DrawTrace");
            foreach (var trace in traces) {
                Destroy(trace);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!targetDevice.isValid) TryInitialise();
        else Draw();
    }
}
