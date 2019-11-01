# SampleScene

## Overview:

This scene contains the VR interactions and the placeholder NPC.

---

## Gameobjects VR:

This object houses all the VR related objects.

### VRInput:

This group of objects house the VR input objects.

#### UnityXRCameraRig:

This object is deactivated by default, because the SimulatedCameraRig is activated in place of it.

This object should be activated when a real headset is wanting to be used (deactivate SimulatedCameraRig as well).

https://academy.vrtk.io/Documentation/HowToGuides/CameraRigs/AddingTheUnityXRCameraRig/


#### SimulatedCameraRig:

This object is a mock Camera Rig set up that can be used to develop with VRTK without the need for VR Hardware.

Disable it when the UnityXRCameraRig is activated.

https://vrtoolkit.readme.io/v3.0.0/docs/vr-simulator


#### Controllers:

These objects are the Boolean actions when the button is pressed.

### VRListeners:

Each of these objects are a list of sources (boolean actions) that activate the described object.

### VRInteractors:

These are VR game objects that have scripts to do interactions in the scene.

---

## Interactions:

### Teleporting:

Activating the Curved Pointer object and once it is deactivated, the player will be teleported to the location.
