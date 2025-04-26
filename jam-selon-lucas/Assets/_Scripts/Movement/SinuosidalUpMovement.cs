using UnityEngine;

public class SinuosidalUpMovement : PhysicalObject
{
    [SerializeField] float frequency, magnitude, randomMagnitudeOffset;

    float phaseOffset;

    void Awake()
    {
        float baseMagnitude = magnitude;
        magnitude = 0;
        //magnitude.Do(baseMagnitude,1f);

        magnitude += randomMagnitudeOffset * Random.value;

        phaseOffset = Random.value * 2f * Mathf.PI;
    }

    protected override void Update()
    {
        float f = Mathf.Sin(frequency * Time.time + phaseOffset) * magnitude;
        Velocity = transform.up * f;
        base.Update();
    }
}
