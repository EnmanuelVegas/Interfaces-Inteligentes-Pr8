using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Camera : MonoBehaviour
{
  private Material tvMaterial;
  private WebCamTexture webcamTexture;
  private int captureCounter = 1;
  private string savePath;

  void Start()
  {
    savePath = Path.Combine(Application.dataPath, "images");
    Debug.Log(Application.dataPath);
    if (!Directory.Exists(savePath))
    {
      Directory.CreateDirectory(savePath);
      Debug.Log("Carpeta creada en: " + savePath);
    }

    tvMaterial = GetComponent<Renderer>().materials[1];
    tvMaterial.mainTextureScale = new Vector2(1, -1);
    if (WebCamTexture.devices.Length > 0)
    {
      string camName = WebCamTexture.devices[0].name;
      Debug.Log(camName); // Ejercicio 4
      webcamTexture = new WebCamTexture(camName);
      tvMaterial.mainTexture = webcamTexture;
      webcamTexture.Play();
      Debug.Log("Cámara iniciada: " + camName);
    }
    else
    {
      Debug.LogError("No se ha detectado ninguna cámara en el dispositivo.");
    }
  }

  void Update()
  {
    // Pausar
    if (Input.GetKeyDown(KeyCode.P))
    {
      if (webcamTexture != null && webcamTexture.isPlaying)
      {
        webcamTexture.Pause();
        Debug.Log("Cámara pausada");
      }
    }

    // Reanudar
    if (Input.GetKeyDown(KeyCode.R))
    {
      if (webcamTexture != null && !webcamTexture.isPlaying)
      {
        webcamTexture.Play();
        Debug.Log("Cámara reanudada");
      }
    }

    if (Input.GetKeyDown(KeyCode.X)) // ejercicio 5
    {
      CaptureFrame();
    }
  }

  void CaptureFrame()
  {
    if (webcamTexture == null || !webcamTexture.isPlaying)
    {
      Debug.LogWarning("No se puede capturar: la cámara no está activa.");
      return;
    }

    // Crear textura del tamaño del frame
    Texture2D snapshot = new Texture2D(webcamTexture.width, webcamTexture.height);

    // Copiar píxeles desde la webcam
    snapshot.SetPixels(webcamTexture.GetPixels());
    snapshot.Apply();

    // Guardar archivo PNG
    string fileName = "captura_" + captureCounter + ".png";
    string filePath = Path.Combine(savePath, fileName);
      
    try
    {
      File.WriteAllBytes(filePath, snapshot.EncodeToPNG());
      captureCounter++;
      Debug.Log("Fotograma capturado y guardado en: " + filePath);
    }
    catch (System.Exception e)
    {
      Debug.LogError("Error al guardar la imagen: " + e.Message);
    }
  }
}
