using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMng : MonoBehaviour
{
    static public CursorMng cursorMng;
    [SerializeField]
    private Texture2D cursor;

    static public Vector2 cursorSize;
    bool isRoutine;
    void Start()
    {
        cursorMng = this;
        Cursor.visible = false;
        cursorSize = new Vector2(55, 55);
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - cursorSize.x / 2,
            Event.current.mousePosition.y - cursorSize.y / 2, cursorSize.x, cursorSize.y), cursor);
    }
    public void CursorSizeTo(float s, bool lerp = false)
    {
        if (lerp && !isRoutine)
             StartCoroutine(CursorSizeLerpRoutine(s));
        else
        {
            StopAllCoroutines();
            isRoutine = false;
            cursorSize = new Vector2(s, s);
        }
    }

    private IEnumerator CursorSizeLerpRoutine(float s)
    {
        isRoutine = true;
        while (true)
        {
            cursorSize = Vector2.Lerp(cursorSize, new Vector2(s, s), 0.06f);
            if (Vector2.Distance(cursorSize, new Vector2(s, s)) < 0.1f)
            {
                isRoutine = false;
                yield break;
            }
            yield return null;
        }
    }
}
