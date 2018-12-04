using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParams {

    public Vector2 position { get; set; }
    public int numberOfExplosions { get; set; }
    public float min { get; set; }
    public float max { get; set; }

    public ExplosionParams(Vector2 position, int numberOfExplosions, float min, float max) {
        this.position = position;
        this.numberOfExplosions = numberOfExplosions;
        this.min = min;
        this.max = max;
    }
}
