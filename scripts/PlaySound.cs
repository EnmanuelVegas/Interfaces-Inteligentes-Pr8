using UnityEngine;

public class PlaySound : MonoBehaviour
{

  // Audio
  public AudioSource audioSource;
  public AudioClip reachClip;
  private bool soundPlayed = false;

  void Start()
  {
    if (audioSource == null)
      audioSource = GetComponent<AudioSource>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (!soundPlayed && other.gameObject.tag == "escudoSonido") {
      Debug.Log("Sonido!");
      PlayReachSound();
      soundPlayed = true;
    }
  }

  private void PlayReachSound()
  {
    if (audioSource != null && reachClip != null)
      audioSource.PlayOneShot(reachClip);
    else
      Debug.LogWarning("AudioSource o ReachClip no asignado.");
  }

}