using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public int id;
    public bool isClue;
    public bool isZoom;
    public bool isPanel;
    public int panelNum;

    public Vector3 savedPosition;
    public Quaternion savedRotation;

    // Unity callback method
    void OnValidate()
    {
        // Check if isZoom is false, reset savedPosition to Vector3.zero
        if (!isZoom)
        {
            savedPosition = Vector3.zero;
            savedRotation = Quaternion.identity;
        }

        if (!isPanel)
        {
            panelNum = 0;
        }
    }
}
