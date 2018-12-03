using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CromeActions : PlayerActionSet {

    public PlayerAction fire;
    public PlayerAction cancel;

    public PlayerAction left;
    public PlayerAction right;
    public PlayerAction up;
    public PlayerAction down;

    public PlayerOneAxisAction moveX;
    public PlayerOneAxisAction moveY;

    public CromeActions() {
        fire = CreatePlayerAction("Fire");
        cancel = CreatePlayerAction("Cancel");

        left = CreatePlayerAction("Left");
        right = CreatePlayerAction("Right");
        up = CreatePlayerAction("Up");
        down = CreatePlayerAction("Down");

        moveX = CreateOneAxisPlayerAction(left, right);
        moveY = CreateOneAxisPlayerAction(down, up);
    }
}
