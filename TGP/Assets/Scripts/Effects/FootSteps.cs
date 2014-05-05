using UnityEngine;
using System.Collections;

public class FootSteps : MonoBehaviour 
{
    public float baseFootAudioVolume = 1.0f;
    public float soundEffectPitchRandomness = 0.05f;

    public GameObject leftFoot;
    public GameObject rightFoot;

    PlatformerController platControl;

    void Start()
    {
        platControl = GetComponent<PlatformerController>();
    }

    void LeftFootStep()
    {
        FootStep(leftFoot);
    }

    void RightFootStep()
    {
        FootStep(rightFoot);
    }

    void FootStep(GameObject foot)
    {
        if (platControl.IsGrounded())
        {
            Instantiate(platControl.groundParticle.effect, foot.transform.position, foot.transform.rotation);

            AudioSource footSounds = foot.GetComponent<AudioSource>();

            footSounds.clip = platControl.groundSound.audioClip;
            footSounds.volume = platControl.groundSound.volumeModifier * baseFootAudioVolume;
            footSounds.pitch = Random.Range(1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);
            footSounds.Play();
        }
    }
}