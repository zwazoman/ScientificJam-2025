using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System.Collections;

public class SinuosuidalUpMovement : PhysicalObject
{
    [SerializeField] float frequency, magnitude, randomMagnitudeOffset;

    float phaseOffset;

    void OnInstantiatedByPool()
    {
        float baseMagnitude = magnitude;
        magnitude = 0;
        DOTween.To(
            () => magnitude,
            (val) => magnitude = val,
            baseMagnitude + randomMagnitudeOffset,
            .2f);

        phaseOffset = Random.value * 2f * Mathf.PI;
    }

    protected override void Update()
    {
        float f = Mathf.Sin(frequency * Time.time + phaseOffset) * magnitude;
        Velocity = transform.up * f;
        base.Update();
    }
}
