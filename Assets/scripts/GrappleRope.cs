using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRope : MonoBehaviour
{
    private LineRenderer lr;
    private Spring spring;
    private Vector3 currentGrapplePosition;
    public Grapple grapple;
    public int quality;
    public float damper;
    public float strength;
    public float velocity;
    public float waveCount;
    public float waveHeight;
    public AnimationCurve affectCurve;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        spring = new Spring();
        spring.SetTarget(0);
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        if (!grapple.IsGrappling())
        {
            currentGrapplePosition = grapple.grappleTip.position;
            spring.Reset();
            if (lr.positionCount > 0)
                lr.positionCount = 0;
            return;
        }

        if (lr.positionCount == 0)
        {
            spring.SetVelocity(velocity);
            lr.positionCount = quality + 1;
        }

        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);

        var grapplePoint = grapple.GetGrapplePoint();
        var grappleTipPos = grapple.grappleTip.position;
        var up = Quaternion.LookRotation((grapplePoint - grappleTipPos).normalized) * Vector3.up;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        for (int i = 0; i < quality +1; i++)
        {
            var delta = i / (float)quality;
            var right = Quaternion.LookRotation((grapplePoint - grappleTipPos).normalized) * Vector3.right;

            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                                     affectCurve.Evaluate(delta) +
                                     right * waveHeight * Mathf.Cos(delta * waveCount * Mathf.PI) * spring.Value *
                                     affectCurve.Evaluate(delta);
            lr.SetPosition(i, Vector3.Lerp(grappleTipPos, currentGrapplePosition, delta)+offset);
        }

    }
}
