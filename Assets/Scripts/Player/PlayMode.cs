using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayMode : MonoBehaviour
{
    public enum Mode
    {
        EDIT,
        TPS,
    }
    static private List<System.Type> types;

    static private Mode _mode;

    static public Mode mode
    {
        get
        {
            return _mode;
        }
        set
        {
            if (value == _mode)
                return;
            switch (value)
            {
                case Mode.EDIT:
                    for (int i = 0; i < types.Count; i++)
                    {
                        (Context.Player.GetComponent(types[i]) as MonoBehaviour).enabled = false;
                    }
                    CameraLerp.toOrtho.Invoke();
                    TileInput.CursorActive.Invoke(true);
                    break;
                case Mode.TPS:
                    for (int i = 0; i < types.Count; i++)
                    {
                        (Context.Player.GetComponent(types[i]) as MonoBehaviour).enabled = true;
                    }
                    CameraLerp.toPerspec.Invoke();
                    TileInput.CursorActive.Invoke(false);
                    break;
            }
            _mode = value;
        }
    }


    private void Start()
    {
        mode = Mode.EDIT;

        types = new List<System.Type>();
        types.Add(typeof(ThirdPersonCharacter));
        types.Add(typeof(ThirdPersonUserControl));
        types.Add(typeof(PlayerAttack));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mode == Mode.EDIT)
                mode = Mode.TPS;
            else if (mode == Mode.TPS)
                mode = Mode.EDIT;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Context.gameData.Money += 100;
        }
    }
}
