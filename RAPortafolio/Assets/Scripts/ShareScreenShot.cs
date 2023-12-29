using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShareScreenShot : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    private ARPointCloudManager aRPointCloudManager;

    // Start is called before the first frame update
    void Start()
    {
        // Encuentra el ARPointCloudManager
        aRPointCloudManager = FindAnyObjectByType<ARPointCloudManager>();

        // Inicializa la visibilidad
        SetARPointCloudsActive(true);
        SetMainMenuVisibility(true);
    }

    // Método para llamar al método de tomar la captura de pantalla
    public void TakeScreenShot()
    {
        // Oculta los elementos antes de tomar la captura
        SetARPointCloudsActive(false);
        SetMainMenuVisibility(false);

        StartCoroutine(TakeScreenshotAndShare());
    }

    // Desactiva o activa el contenido AR
    private void SetARPointCloudsActive(bool active)
    {
        if (aRPointCloudManager != null)
        {
            foreach (var point in aRPointCloudManager.trackables)
            {
                point.gameObject.SetActive(active);
            }
        }
    }

    // Desactiva o activa el menú principal
    private void SetMainMenuVisibility(bool active)
    {
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(active);
        }
    }

    // Método para tomar la captura de pantalla y compartirla
    private IEnumerator TakeScreenshotAndShare()
    {
        // Espera al final del frame actual
        yield return new WaitForEndOfFrame();

        // Captura de pantalla
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Ruta del archivo
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // Evita las fugas de memoria
        Destroy(ss);

        // Comparte la captura de pantalla
        new NativeShare().AddFile(filePath)
            .SetSubject("Subject goes here").SetText("RAPortafolio: Visualice su Mueble en Realidad Aumentada!!!")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Restaura la visibilidad después de tomar la captura de pantalla
        SetARPointCloudsActive(true);
        SetMainMenuVisibility(true);
    }


    
}