using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera mainCamera; // Asigna esto en el Inspector
    public Camera otherCamera; // Asigna esto en el Inspector
    public GameObject overlayBin; // Asigna el canvas OverlayBin aqu� en el Inspector
    public GameObject readyB;
    public GameObject readyC;

    public BirdSpawner birdSpawner; // Asigna esto en el Inspector
    public static SwitchCamera instance;
    public UIController uiController;

    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private Canvas PausedCanvas;
    private void Start()
    {
        // Activa la c�mara principal y desactiva la otra c�mara al inicio.
        mainCamera.gameObject.SetActive(false);
        otherCamera.gameObject.SetActive(true);
        readyC.SetActive(false);

        // Aseg�rate de que el overlay est� activo al inicio si la c�mara principal est� activa.
        overlayBin.SetActive(false);

        // Inicia la corrutina para cambiar de c�mara despu�s de un tiempo.
        //StartCoroutine(SwitchCameraAfterDelay(5f));
    }

    void Awake()
    {
        // Si la instancia no existe, la establece a this
        if (instance == null)
        {
            instance = this;
        }
        // Si la instancia existe y no es this, destruye el GameObject
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // ... resto del c�digo de Awake ...
    }

    public void ready()
    {
        // Cambia de la c�mara principal a la otra c�mara.
        mainCamera.gameObject.SetActive(true);
        otherCamera.gameObject.SetActive(false);
        MainCanvas.GetComponent<Canvas>().worldCamera = mainCamera;
        PausedCanvas.GetComponent<Canvas>().worldCamera = mainCamera;


        // Desactiva el overlay cuando la c�mara principal est� desactivada.
        overlayBin.SetActive(true);
        readyB.SetActive(false);

        // Activa el spawn de aves
        if (birdSpawner != null)
        {
            birdSpawner.StartSpawning(); 
        }
        else
        {
            Debug.LogError("BirdSpawner no est� asignado en SwitchCamera.");
        }

        UIController.instance.hideButton();

    }



    public void continueButton()
    {
        // Activa el spawn de aves
        if (birdSpawner != null)
        {
            birdSpawner.StartSpawning();
        }
        else
        {
            Debug.LogError("BirdSpawner no est� asignado en SwitchCamera.");
        }
        readyC.SetActive(false);
        if (avesController.instance != null)
        {
            avesController.instance.increaseRound();
            //Debug.Log(avesController.instance.getRound());
            avesController.instance.setDetected();
        }

        UIController.instance.UpdateRound(avesController.instance.getRound());
        UIController.instance.hideButton();
    }

    public void ShowReadyC()
    {
        readyC.SetActive(true);
    }

    public void showContinue()
    {
        readyC.SetActive(true);
    }

    public void hideContinue()
    {
        readyC.SetActive(false);
    }

    /*
    private IEnumerator SwitchCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Cambia de la c�mara principal a la otra c�mara.
        mainCamera.gameObject.SetActive(true);
        otherCamera.gameObject.SetActive(false);

        // Desactiva el overlay cuando la c�mara principal est� desactivada.
        overlayBin.SetActive(true);
    }
    */
}
