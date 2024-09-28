using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Huellas : MonoBehaviour
{
    private GameObject trampaAsignada;

    // M�todo para asignar una trampa a esta huella
    public void AsignarTrampa(GameObject trampa)
    {
        if (trampa != null)
        { 
            trampaAsignada = trampa;
        }
        Debug.Log(trampaAsignada.name);
        
    }

    // M�todo para verificar si esta huella est� asociada a una trampa espec�fica
    public bool TrampaAsignada(GameObject trampa)
    {
        return trampaAsignada == trampa;
    }

    // M�todo para desasociar la trampa de esta huella
    public void DesasociarTrampa()
    {
        trampaAsignada = null;
    }

    public string GetTrampaAsignada()
    {
        if (trampaAsignada != null)
        {
            return trampaAsignada.GetComponentInChildren<SpriteRenderer>().sprite.name; 
        }
        else
        {
            return "";
        }
          
    }

    public GameObject getTrampa()
    {
        return trampaAsignada;
    }
}

