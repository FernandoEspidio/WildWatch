using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    List<string> NombresMinijuegos = new List<string>();
    List<string> DescripcionMinijuegos = new List<string>();
    public List<Sprite> ImagesMinigame = new List<Sprite>();

    public Text title;
    public Text description;
    public Image gameImage;

    private int typeOfGame;

    public void enterMinigame()
    {
        //int typeOfGame = PlayerPrefs.GetInt("TypeOfGame");

        if (typeOfGame == 1)
        {
            SceneManager.LoadScene("AvesScene");
        }
        else if(typeOfGame == 2)
        {
            SceneManager.LoadScene("AnfibiosScene");
        }else if(typeOfGame == 3)
        {
            SceneManager.LoadScene("MamiferosCentral");
        }
        else if(typeOfGame==4)
        {
            SceneManager.LoadScene("InsectosScene");
        }
        else if(typeOfGame==5)
        {
            SceneManager.LoadScene("PlantasScene");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        typeOfGame = PlayerPrefs.GetInt("TypeOfGame");
        NombresMinijuegos.Add("Alas y Eco");
        NombresMinijuegos.Add("Secretos Subrocosos");
        NombresMinijuegos.Add("Huellas Misteriosas");
        NombresMinijuegos.Add("Recuerdo de un Zumbido a oscuras");
        NombresMinijuegos.Add("Jard�n Secreto");

        /*
        DescripcionMinijuegos.Add("El objetivo del juego es lograr ver el ave que aparece. Tienes tres rondas para intentar ver tres aves. Al finalizar cada ronda, recuerda intentar identificar el sonido para obtener m�s puntos. RECOMENDACI�N: Escucha de donde sale el sonido\r\n");
        DescripcionMinijuegos.Add("El objetivo del juego es encontrar el m�ximo n�mero de anfibios que puedas. Tienes un tiempo l�mite para encontrarlos a todos, cada que encuentres uno, p�cale para registrarlo. Recuerda que los anfibios suelen estar debajo de las piedras, ser�a buena idea picarles a �stas.\r\n");
        DescripcionMinijuegos.Add("Parece que han pasado animales por ciertas �reas en el mapa y han dejado huellas y heces. Anal�zalas y elige el tipo de trampa correcta para identificar al animal. CONSEJO: Revisa el tama�o de las evidencias para determinar qu� tipo de animal es. \r\n");
        DescripcionMinijuegos.Add("A los insectos les gusta la luz, el objetivo del juego es saber si puedes identificar al grupo de insectos correcto. P�cale a todos los insectos que son del mismo grupo, aunque cuidado, una vez se va la luz al principio no podr�s verlos de nuevo. �Ser�s capaz de recordarlos?\r\n");
        DescripcionMinijuegos.Add("El objetivo del juego es recolectar el mayor n�mero de plantas que puedas. Mu�vete a trav�s del mapa y busca la mayor cantidad que puedas, nada m�s pon atenci�n por donde caminas, en cualquier momento podr�as encontrarte con alg�n obst�culo. \r\n");
        */

        DescripcionMinijuegos.Add(" REGLAS \r\n \r\n 1.- El objetivo del juego es ver tres aves diferentes en tres rondas. \r\n \r\n 2.- Al final de cada ronda, escucha y adivina el sonido del ave para puntos extra.\r\n \r\n 3.- Al final se sumar�n todos los puntos por cada ave vista y por identificar correctamente el sonido. \r\n \r\n �Disfruta el juego! \r\n");
        DescripcionMinijuegos.Add("REGLAS \r\n \r\n 1.- El objetivo del juego es encontrar el m�ximo n�mero de anfibios dentro del tiempo l�mite. \r\n \r\n 2.- Tienes un tiempo determinado para encontrar todos los anfibios. \r\n \r\n 3.- Busca debajo de las piedras, donde suelen esconderse los anfibios. \r\n \r\n 4.-  Interact�a con cada anfibio que encuentres para registrarlos. \r\n \r\n �Buena suerte encontrando esos anfibios! \r\n");
        DescripcionMinijuegos.Add("REGLAS \r\n \r\n 1.- Analiza las huellas dejadas por animales en ciertas �reas del mapa para identificar el tipo de animal. \r\n \r\n 2.- Observa el tama�o y la forma de las huellas para decidir que tipo de trampa usar. \r\n \r\n 3.- Presta atenci�n al tama�o de las evidencias, ya que te ayudar� a determinar qu� tipo de animal podr�a ser. \r\n \r\n �Buena suerte analizando las evidencias y atrapando al animal correcto!\r\n");
        DescripcionMinijuegos.Add("REGLAS \r\n \r\n 1.- El objetivo del juego es memorizar la posici�n de los insectos antes de que se apague la luz. \r\n \r\n 2.- Una vez se vaya la luz, debes identificar y seleccionar todos los insectos que pertenezcan al mismo grupo. \r\n \r\n 3.- Una vez que se apague la luz al principio, no podr�s ver los insectos de nuevo. Debes recordar su ubicaci�n y grupo para seleccionarlos correctamente. \r\n \r\n �Buena suerte recordando los insectos!\r\n");
        DescripcionMinijuegos.Add("REGLAS \r\n \r\n 1.- El objetivo del juegos es recolectar la mayor cantidad de plantas posible mientras te mueves por el mapa. \r\n \r\n 2.- Navega por el mapa en busca de plantas. Ten cuidado, ya que pueden aparecer obst�culos en tu camino en cualquier momento. \r\n \r\n �Divi�rtete explorando el mapa y recolectando plantas! \r\n");

        title.text = NombresMinijuegos[typeOfGame - 1];
        description.text = DescripcionMinijuegos[typeOfGame - 1];
        gameImage.sprite = ImagesMinigame[typeOfGame - 1];


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
