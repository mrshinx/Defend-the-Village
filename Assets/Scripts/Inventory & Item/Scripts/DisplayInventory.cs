using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour {

    public InventoryObject Inventory;
    Dictionary<GameObject, InventorySlot > itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    MouseItem mouseItem = new MouseItem();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

        }
    }

    void Start () {

        CreateDisplay();

	}
	
	// Update is called once per frame
	void Update () {

        UpdateDisplay();

	}

    void CreateDisplay()
    {

        ClearDisplay();

        for (int i=0; i < 139; i++)
        {

            var currentSlot = transform.GetChild(i);

            AddEvent(currentSlot.gameObject,  EventTriggerType.PointerEnter, delegate { OnEnter(currentSlot.gameObject); });
            AddEvent(currentSlot.gameObject, EventTriggerType.PointerExit, delegate { OnExit(currentSlot.gameObject); });
            AddEvent(currentSlot.gameObject, EventTriggerType.BeginDrag, delegate { DragBegin(currentSlot.gameObject); });
            AddEvent(currentSlot.gameObject, EventTriggerType.EndDrag, delegate { DragEnd(currentSlot.gameObject); });
            AddEvent(currentSlot.gameObject, EventTriggerType.Drag, delegate { OnDrag(currentSlot.gameObject); });

            
        }

    }

    void ClearDisplay()
    {
        for (int i = 0; i < 139; i++)
        {
            Image tempImage = transform.GetChild(i).transform.GetChild(0).GetComponent<Image>();
            tempImage.sprite = null;
            var tempColor = tempImage.color;
            tempColor.a = 0f;
            tempImage.color = tempColor;

            transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "";
        }
    }

    void UpdateDisplay()
    {
        ClearDisplay();

        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < Inventory.Container.Count; i++)
        {
            var currentSlot = transform.GetChild(i);

            Image tempImage = currentSlot.transform.GetChild(0).GetComponent<Image>();
            tempImage.sprite = Inventory.Container[i].item.itemSprite;
            var tempColor = tempImage.color;
            tempColor.a = 1f;
            tempImage.color = tempColor;

            transform.GetChild(i).GetChild(1).GetComponent<Text>().text = Inventory.Container[i].amount.ToString();
            itemsDisplayed.Add(currentSlot.gameObject, Inventory.Container[i]);
        }
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {

        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);

    }

    public void OnEnter(GameObject obj)
    {

    }
    public void OnExit(GameObject obj)
    {

    }
    public void DragBegin(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(32, 32);
        rt.transform.localScale = new Vector3(1, 1, 1);
        mouseObject.transform.SetParent(transform.parent.transform.parent);
        Debug.Log("Parent: " +mouseObject.transform.parent);

        if(itemsDisplayed[obj].ID>=0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = Inventory.database.GetItem[itemsDisplayed[obj].ID].itemSprite;
            img.raycastTarget = false;

        }
        mouseItem.obj = mouseObject;
        mouseItem.obj.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
        mouseItem.item = itemsDisplayed[obj];
    }
    public void DragEnd(GameObject obj)
    {

    }
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
        {
            Vector2 localPoint;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(mouseItem.obj.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out localPoint);
            mouseItem.obj.GetComponent<RectTransform>().anchoredPosition = localPoint;

            //mouseItem.obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
            Debug.Log("color: " +mouseItem.obj.GetComponent<Image>().color);
            Debug.Log("sprite: " +mouseItem.obj.GetComponent<Image>().sprite);
            Debug.Log("mouse: " +Input.mousePosition);
            Debug.Log("obj: " + mouseItem.obj.GetComponent<RectTransform>().position);
            Debug.Log("obj anchord: " + mouseItem.obj.GetComponent<RectTransform>().anchoredPosition);
        }
    }

}
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}
