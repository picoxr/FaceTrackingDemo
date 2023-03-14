
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

public class FTTest : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    public SkinnedMeshRenderer tongueBlendShape;
    public SkinnedMeshRenderer leftEyeExample;
    public SkinnedMeshRenderer rightEyeExample;

    private float[] blendShapeWeight = new float[52];

    private List<string> blendShapeList = new List<string>
    {
        "eyeLookDownLeft",
        "noseSneerLeft",
        "eyeLookInLeft",
        "browInnerUp",
        "browDownRight",
        "mouthClose",
        "mouthLowerDownRight",
        "jawOpen",
        "mouthUpperUpRight",
        "mouthShrugUpper",
        "mouthFunnel",
        "eyeLookInRight",
        "eyeLookDownRight",
        "noseSneerRight",
        "mouthRollUpper",
        "jawRight",
        "browDownLeft",
        "mouthShrugLower",
        "mouthRollLower",
        "mouthSmileLeft",
        "mouthPressLeft",
        "mouthSmileRight",
        "mouthPressRight",
        "mouthDimpleRight",
        "mouthLeft",
        "jawForward",
        "eyeSquintLeft",
        "mouthFrownLeft",
        "eyeBlinkLeft",
        "cheekSquintLeft",
        "browOuterUpLeft",
        "eyeLookUpLeft",
        "jawLeft",
        "mouthStretchLeft",
        "mouthPucker",
        "eyeLookUpRight",
        "browOuterUpRight",
        "cheekSquintRight",
        "eyeBlinkRight",
        "mouthUpperUpLeft",
        "mouthFrownRight",
        "eyeSquintRight",
        "mouthStretchRight",
        "cheekPuff",
        "eyeLookOutLeft",
        "eyeLookOutRight",
        "eyeWideRight",
        "eyeWideLeft",
        "mouthRight",
        "mouthDimpleLeft",
        "mouthLowerDownLeft",
        "tongueOut",
    };

    private int[] indexList = new int[52];
    private int tongueIndex;
    private int leftLookDownIndex;
    private int leftLookUpIndex;
    private int leftLookInIndex;
    private int leftLookOutIndex;

    private int rightLookDownIndex;
    private int rightLookUpIndex;
    private int rightLookInIndex;
    private int rightLookOutIndex;

    private PxrFaceTrackingInfo faceTrackingInfo;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < indexList.Length; i++)
        {
            indexList[i] = skin.sharedMesh.GetBlendShapeIndex(blendShapeList[i]);
        }
        tongueIndex = tongueBlendShape.sharedMesh.GetBlendShapeIndex("tongueOut");
        leftLookDownIndex = leftEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookDownLeft");
        leftLookUpIndex = leftEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookUpLeft");
        leftLookInIndex = leftEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookInLeft");
        leftLookOutIndex = leftEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookOutLeft");
        rightLookDownIndex = rightEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookDownRight");
        rightLookUpIndex = rightEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookUpRight");
        rightLookInIndex = rightEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookInRight");
        rightLookOutIndex = rightEyeExample.sharedMesh.GetBlendShapeIndex("eyeLookOutRight");
    }
    // Update is called once per frame
    void Update()
    {
        if (PXR_Plugin.System.UPxr_QueryDeviceAbilities(PxrDeviceAbilities.PxrTrackingModeFaceBit))
        {
            PXR_System.GetFaceTrackingData(0, GetDataType.PXR_GET_FACE_DATA, ref faceTrackingInfo);
            blendShapeWeight = faceTrackingInfo.blendShapeWeight;
            float[] data = blendShapeWeight;
            for (int i = 0; i < data.Length; ++i)
            {
                if (indexList[i] >= 0)
                {
                    skin.SetBlendShapeWeight(indexList[i], 100 * data[i]);
                }
            }
            tongueBlendShape.SetBlendShapeWeight(tongueIndex, 100 * data[51]);
            leftEyeExample.SetBlendShapeWeight(leftLookUpIndex, 100 * data[31]);
            leftEyeExample.SetBlendShapeWeight(leftLookDownIndex, 100 * data[0]);
            leftEyeExample.SetBlendShapeWeight(leftLookInIndex, 100 * data[2]);
            leftEyeExample.SetBlendShapeWeight(leftLookOutIndex, 100 * data[44]);
            rightEyeExample.SetBlendShapeWeight(rightLookUpIndex, 100 * data[35]);
            rightEyeExample.SetBlendShapeWeight(rightLookDownIndex, 100 * data[12]);
            rightEyeExample.SetBlendShapeWeight(rightLookInIndex, 100 * data[11]);
            rightEyeExample.SetBlendShapeWeight(rightLookOutIndex, 100 * data[45]);
        }
    }
}

