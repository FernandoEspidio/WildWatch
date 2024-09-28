using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject objectToInstantiate;
    public Tilemap tilemap;
    public TileBase validTile;

    private bool canDrag = true;
    
    [SerializeField] private Item item; // El ScriptableObject original
    [SerializeField] private Button button;
    [SerializeField] private Text textdescription;
    [SerializeField] private GameObject Description;

    private GameObject draggedObject;
    private Camera mainCamera;


    void Awake()
    {
        mainCamera = Camera.main;
        Description.SetActive(false);
        item.quantity = 3;

    }

    void Update()
    {
        button.GetComponent<ItemDescription>().ActualizarCantidad();
        ObtenerCantidadObjeto();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {   
        if (item.quantity == 0 && !canDrag) 
        {   
            Description.SetActive(true);
            textdescription.text = "Ya no cuentas con m�s objetos de ese tipo, usa el desarmador de tu inventario" +
                " para poder recolocar las camaras";
            return;
        }

        if (item.quantity > -1 && canDrag) // Verifica si hay cantidad y si no hay un objeto actualmente arrastrado
        {
            if (item.quantity >= 1)
            {
                draggedObject = Instantiate(objectToInstantiate, transform.position, Quaternion.identity);
                draggedObject.GetComponentInChildren<SpriteRenderer>().sprite = item.imagenItem;
            }
          
            item.quantity --;
            // Decrementa la cantidad
        }
        
    }



    public void OnDrag(PointerEventData eventData)
    {

        if (item.quantity > -1 && canDrag)
        {
            if (draggedObject != null)
            {
                Vector3 position = mainCamera.ScreenToWorldPoint(eventData.position);
                position.z = 0; // Aseg�rate de que el objeto se mantiene en la capa correcta
                draggedObject.transform.position = position;
            }
        }
        else
        {
            return;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item.quantity > -1 && canDrag)
        {
            if (draggedObject != null)
            {
                Vector3 worldPosition = draggedObject.transform.position;
                Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
                TileBase tileAtPosition = tilemap.GetTile(cellPosition);

                // Utilizamos OverlapBox para verificar si hay objetos con el tag "PlaceableObject" en la posici�n final
                Collider2D[] results = Physics2D.OverlapBoxAll(worldPosition, new Vector2(0.5f, 0.5f), 0); // Ajusta el tama�o seg�n la escala de tus objetos
                bool isOccupied = false;
                foreach (var result in results)
                {
                    if (result.gameObject.CompareTag("PlaceableObject") && result.gameObject != draggedObject) // Verifica el tag y excluye el propio objeto arrastrado
                    {
                        isOccupied = true;
                        break;
                    }
                }

                if (tileAtPosition == validTile && !isOccupied)
                {

                    // Si el objeto se suelta en una posici�n v�lida y no est� ocupada por otro objeto con el tag
                    // "PlaceableObject", puede permanecer ah�.
                }
                else
                {
                    // Si no es una posici�n v�lida o est� ocupada por un objeto con el tag "PlaceableObject", destruye el objeto.
                    Destroy(draggedObject);
                    item.quantity++;
                }
            }
        }
        else
        {
            return;
        }

    }

    public void ObtenerCantidadObjeto()
    {
        if (item.quantity == -1)
        {
            item.quantity = 0;
            canDrag = false;
        }
        else if(item.quantity > 0 && canDrag == false)
        {
            canDrag = true;
        }
    }

}
