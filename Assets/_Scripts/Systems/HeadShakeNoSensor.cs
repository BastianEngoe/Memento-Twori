using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @kurtdekker - head shake no sensor
//
// To use:
//	Put this on a mouse-controlled FPS camera and it can detect:
//		- left/right "shake head no"
//	Optionally give it an audio to play
//
public class HeadShakeNoSensor : MonoBehaviour
{
	public AudioSource AudioNo;

	// how many left-rights until we say "you said no"
	const float ShakeCountRequired = 6;

	// how much left/right constitutes half of a shape
	const float ShakeAngularRequirement = 3;

	// each phase of "shake only latches for this long
	const float ShakeTimingRequirement = 0.50f;

	// track if we think you are nodding
	float ShakeInProgress;

	float LastSignificantShakeAngle;
	int LastDigitalShake;
	int ShakeCount;

	void UpdateShakeNo()
	{
		// time out
		if (ShakeInProgress > 0)
		{
			ShakeInProgress -= Time.deltaTime;
			if (ShakeInProgress <= 0)
			{
				//Debug.Log( "Timed out - SHAKE NO");
				ShakeCount = 0;
				LastDigitalShake = 0;
			}
		}

		// how far up/down is your head?
		float angle = transform.eulerAngles.y;

		// quantize and study this shake
		int shake = 0;		// neutral, -1 is right, +1 is left
		float deltaAngle = Mathf.DeltaAngle( angle, LastSignificantShakeAngle);
		if (deltaAngle < -ShakeAngularRequirement)
		{
			//Debug.Log( "Right");
			shake = -1;
			LastSignificantShakeAngle = angle;
		}
		else
		{
			if (deltaAngle > +ShakeAngularRequirement)
			{
				//Debug.Log( "Left");
				shake = +1;
				LastSignificantShakeAngle = angle;
			}
		}

		// we've gone left / right enough?
		if (shake != 0)
		{
			// and it was in a different direction than before
			if (shake != LastDigitalShake)
			{
				LastDigitalShake = shake;

				ShakeCount++;

				// reset timing, we think you might still be nodding
				ShakeInProgress = ShakeTimingRequirement;

				if (ShakeCount >= ShakeCountRequired)
				{
					ShakeCount = 0;

					GameManager.instance.isShaking = true;

					if (AudioNo) AudioNo.Play();
				}
			}
		}
	}

	void Update ()
	{
		UpdateShakeNo();
	}

	void ResetShake()
	{
		
	}
}