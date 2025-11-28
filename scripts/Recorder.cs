using UnityEngine;

public class Recorder : MonoBehaviour
{
  private AudioSource audioSource;
  private string micName;
  private bool isRecording = false;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    if (Microphone.devices.Length > 0)
    {
      micName = Microphone.devices[0];
      Debug.Log("Micrófono detectado: " + micName);
    }
    else
    {
      Debug.LogError("No se ha detectado ningún micrófono en el dispositivo");
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.R))
    {
      if (!isRecording)
        StartRecording();
      else
        StopRecordingAndPlay();
    }
  }

  void StartRecording()
  {
    if (micName == null) return;
    Debug.Log("Iniciando grabación");
    audioSource.clip = Microphone.Start(micName, false, 10, 44100);
    isRecording = true;
  }

  void StopRecordingAndPlay()
  {
    Debug.Log("Finalizando grabación y reproduciendo...");
    Microphone.End(micName);
    isRecording = false;
    audioSource.Play();
  }
}
