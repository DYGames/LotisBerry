using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUISizeSetter : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta.Set(transform.childCount * (transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x + gameObject.GetComponent<HorizontalLayoutGroup>().spacing), gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }
}
