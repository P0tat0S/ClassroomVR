using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//Code for the animations and initialisation of VR hands to provide presence.
public class HandPresence : MonoBehaviour {
    // Global variables for the controller and models of the hands.
    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject handModelPrefab;
    private InputDevice targetDevice;
    private GameObject handModel;
    private Animator handAnimator;

    void Start() {
        TryInitialise();
    }

    // Function that updates the hand animations by setting the animator to trigger or grip mode for the hand to change state animations.
    void UpdateHandAnimation() {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) handAnimator.SetFloat("Trigger", triggerValue);
        else handAnimator.SetFloat("Trigger", 0);

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) handAnimator.SetFloat("Grip", gripValue);
        else handAnimator.SetFloat("Grip", 0);
    }

    //Function that gets the controller device and adds the hand models into the controller
    void TryInitialise() {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0) {
            targetDevice = devices[0];
            handModel = Instantiate(handModelPrefab, transform);
            handAnimator = handModel.GetComponent<Animator>();
        }
    }

    // This functions is called every frame and tries to initialise the controller if it does not found one, but if it finds one it updates the animations constantly
    void Update() {
        if (!targetDevice.isValid) TryInitialise();
        else UpdateHandAnimation();
    }
}
