
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using TMPro;

public class FTTest : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    public SkinnedMeshRenderer tongueBlendShape;
    public SkinnedMeshRenderer leftEyeExample;
    public SkinnedMeshRenderer rightEyeExample;

    public GameObject text;
    public Transform TextParent;

    private List<TMP_Text> texts = new List<TMP_Text>();

    private float[] blendShapeWeight = new float[72];

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
        "viseme_PP",
        "viseme_CH",
        "viseme_o",
        "viseme_O",
        "viseme_i",
        "viseme_I",
        "viseme_RR",
        "viseme_XX",
        "viseme_aa",
        "viseme_FF",
        "viseme_u",
        "viseme_U",
        "viseme_TH",
        "viseme_kk",
        "viseme_SS",
        "viseme_e",
        "viseme_DD",
        "viseme_E",
        "viseme_nn",
        "viseme_sil",
    };

    private int[] indexList = new int[72];
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
        // 开始面部追踪
        PXR_MotionTracking.WantFaceTrackingService();
        FaceTrackingStartInfo info = new FaceTrackingStartInfo();
        info.mode = FaceTrackingMode.PXR_FTM_FACE_LIPS_BS;
        PXR_MotionTracking.StartFaceTracking(ref info);

        for (int i = 0; i < indexList.Length; i++)
        {
            indexList[i] = skin.sharedMesh.GetBlendShapeIndex(blendShapeList[i]);
            GameObject textGO = GameObject.Instantiate(text,TextParent);
            texts.Add(textGO.GetComponent<TMP_Text>());
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
            switch (PXR_Manager.Instance.trackingMode)
            {
                case FaceTrackingMode.PXR_FTM_FACE_LIPS_BS:
                    PXR_System.GetFaceTrackingData(0, GetDataType.PXR_GET_FACELIP_DATA, ref faceTrackingInfo);

                    break;
                case FaceTrackingMode.PXR_FTM_FACE:
                    PXR_System.GetFaceTrackingData(0, GetDataType.PXR_GET_FACE_DATA, ref faceTrackingInfo);

                    break;
                case FaceTrackingMode.PXR_FTM_LIPS:
                    PXR_System.GetFaceTrackingData(0, GetDataType.PXR_GET_LIP_DATA, ref faceTrackingInfo);

                    break;
            }
            //blendShapeWeight = faceTrackingInfo.blendShapeWeight;
            unsafe
            {
                fixed (float* source = faceTrackingInfo.blendShapeWeight)
                {
                    for (int i = 0; i < 72; i++)
                    {
                        blendShapeWeight[i] = source[i];

                        texts[i].text = $"{blendShapeList[i]}\n{(int)(blendShapeWeight[i] * 120)}";

                        if (indexList[i] >= 0)
                        {
                            skin.SetBlendShapeWeight(indexList[i], 100 * blendShapeWeight[i]);
                        }


                    }
                }
            }
            

            
            tongueBlendShape.SetBlendShapeWeight(tongueIndex, 100 * blendShapeWeight[51]);
            
            leftEyeExample.SetBlendShapeWeight(leftLookUpIndex, 100 * blendShapeWeight[31]);
            leftEyeExample.SetBlendShapeWeight(leftLookDownIndex, 100 * blendShapeWeight[0]);
            leftEyeExample.SetBlendShapeWeight(leftLookInIndex, 100 * blendShapeWeight[2]);
            leftEyeExample.SetBlendShapeWeight(leftLookOutIndex, 100 * blendShapeWeight[44]);
            rightEyeExample.SetBlendShapeWeight(rightLookUpIndex, 100 * blendShapeWeight[35]);
            rightEyeExample.SetBlendShapeWeight(rightLookDownIndex, 100 * blendShapeWeight[12]);
            rightEyeExample.SetBlendShapeWeight(rightLookInIndex, 100 * blendShapeWeight[11]);
            rightEyeExample.SetBlendShapeWeight(rightLookOutIndex, 100 * blendShapeWeight[45]);
            
        }
    }

    public void ToggleDebugUI()
    {
        TextParent.gameObject.SetActive(!TextParent.gameObject.activeSelf);
    }
}

