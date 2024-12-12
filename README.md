# Face Tracking Unity Demo

- If you have any questions/comments, please visit [**Pico Developer Support Portal**](https://picodevsupport.freshdesk.com/support/home) and raise your question there.

## Environment：

- PUI 5.11.0
- Unity 2021.3.13f1
- Pico Unity Integration SDK 3.0.5

## Applicable devices:

- PICO 4 Pro

## Description：
To enable Face Tracking, you need to pick a Face Tracking Mode on PXR_Manager.
![Screenshot](https://github.com/picoxr/FaceTrackingDemo/blob/e93d29d63e8311e7a11fd95d38a0a33a10201aae/Screenshots/Setting.png)

Hybrid: Enable face tracking and lipsync. Uses all 52 blend shapes and 20 visemes.

Face Only: Enable face tracking only. Uses all 52 blend shapes.

Lipsync Only: Enable lipsync only. Uses all 20 visemes.


In this demo, you will learn how to get Face Tracking data and apply the data to an ARKit standard avatar.

![Screenshot](https://github.com/picoxr/FaceTrackingDemo/blob/e93d29d63e8311e7a11fd95d38a0a33a10201aae/Screenshots/Face.jpeg)

To check the specific data, enable the debug UI.

![Screenshot](https://github.com/picoxr/FaceTrackingDemo/blob/e93d29d63e8311e7a11fd95d38a0a33a10201aae/Screenshots/UI.jpeg)

Method explanation:

Get device Face Tracking ability: PXR_MotionTracking.GetFaceTrackingSupported

Get Face Tracking data based on the mode and store in faceTrackingInfo: PXR_System.GetFaceTrackingData(0, GetDataType.PXR_GET_FACELIP_DATA, ref faceTrackingInfo);

For detailed Face Tracking Data explanation, please visit [**Face Tracking Document**](https://developer-global.pico-interactive.com/document/unity/face-tracking) .


