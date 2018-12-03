using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CromeController : MonoBehaviour {

    CromeActions cromeActions;

	// Use this for initialization
	void Start () {
        cromeActions = new CromeActions();

        cromeActions.fire.AddDefaultBinding(Key.Space);
        cromeActions.fire.AddDefaultBinding(InputControlType.Action1);
        cromeActions.fire.AddDefaultBinding(InputControlType.Action3);

        cromeActions.cancel.AddDefaultBinding(Key.Escape);
        cromeActions.cancel.AddDefaultBinding(InputControlType.Action2);

        cromeActions.left.AddDefaultBinding(Key.A);
        cromeActions.left.AddDefaultBinding(Key.LeftArrow);
        cromeActions.left.AddDefaultBinding(InputControlType.DPadLeft);
        cromeActions.left.AddDefaultBinding(InputControlType.LeftStickLeft);

        cromeActions.right.AddDefaultBinding(Key.D);
        cromeActions.right.AddDefaultBinding(Key.RightArrow);
        cromeActions.right.AddDefaultBinding(InputControlType.DPadRight);
        cromeActions.right.AddDefaultBinding(InputControlType.LeftStickRight);

        cromeActions.up.AddDefaultBinding(Key.W);
        cromeActions.up.AddDefaultBinding(Key.UpArrow);
        cromeActions.up.AddDefaultBinding(InputControlType.DPadUp);
        cromeActions.up.AddDefaultBinding(InputControlType.LeftStickUp);

        cromeActions.down.AddDefaultBinding(Key.S);
        cromeActions.down.AddDefaultBinding(Key.DownArrow);
        cromeActions.down.AddDefaultBinding(InputControlType.DPadDown);
        cromeActions.down.AddDefaultBinding(InputControlType.LeftStickDown);
    }
	
    public bool CromeIsFiring() {
        return cromeActions.fire.IsPressed;
    }

    public bool CromeIsCanceling() {
        return cromeActions.cancel.WasPressed;
    }

    public float CromeHorizontal() {
        return cromeActions.moveX.Value;
    }

    public float CromeVertical() {
        return cromeActions.moveY.Value;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
