using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    private Matrix4x4 ortho;
    private Matrix4x4 perspective;

    private MatrixBlender matrixBlender;

    public static Action toOrtho;
    public static Action toPerspec;

    void Start()
    {
        ortho = Camera.main.projectionMatrix;
        perspective = Matrix4x4.Perspective(60.0f, (float)Screen.width / (float)Screen.height, 0.01f, 1000.0f);
        matrixBlender = GetComponent<MatrixBlender>();
        toOrtho = () =>
        {
            Context.userInterface.setProjection(true);
            matrixBlender.BlendToMatrix(ortho, 1.0f, () =>
            {
                Camera.main.transform.SetParent(null);
                StartCoroutine(LerptoOrthoRoutine());
            });
        };
        toPerspec = () =>
        {
            Context.userInterface.setProjection(false);
            matrixBlender.BlendToMatrix(perspective, 1.0f, () =>
            {
                Camera.main.transform.SetParent(Context.Player);
                StartCoroutine(LerptoPerspecRoutine());
            });
        };
    }
    static public IEnumerator LerptoOrthoRoutine()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 1.0f)
        {
            yield return null;
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0, 10.0f, -10.0f), (Time.time - startTime));
            Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, Quaternion.Euler(45, 0, 0), (Time.time - startTime));
            Camera.main.transform.localScale = Vector3.Lerp(Camera.main.transform.localScale, new Vector3(1, 1, 1), (Time.time - startTime));
        }
    }
    static public IEnumerator LerptoPerspecRoutine()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 1.0f)
        {
            yield return null;
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0, 7.5f, -10.0f), (Time.time - startTime));
            Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, Quaternion.Euler(15, 0, 0), (Time.time - startTime));
            Camera.main.transform.localScale = Vector3.Lerp(Camera.main.transform.localScale, new Vector3(10, 10, 10), (Time.time - startTime));
        }
    }

}
