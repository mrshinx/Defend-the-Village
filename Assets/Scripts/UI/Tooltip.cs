using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public enum tooltipTypes
    {
        UI,
        Monster,
        Tower,
        Passive,
        ResearchNode
    }
    public tooltipTypes tooltipType;

    [SerializeField] string Name;
    [TextArea(5,10)][SerializeField] string Description;
    public GameObject tooltip;
    public GameObject auraGroup;
    public static GameObject lastSelected;
    public static GameObject nowSelected;
    public string hardTooltip;
    float tooltipHeight;
    bool tooltipHidden = true;
    bool selecting = false;

    Text tooltiptext;
    Text tooltipname;
    Image tooltipImage;

    public AuraInformation[] Auras;
    [SerializeField] GameObject AuraPrefab;

    private void Awake()
    {
        switch (tooltipType)
        {
            case tooltipTypes.UI:
                tooltip = GameObject.FindWithTag("Tooltip");
                tooltiptext = tooltip.GetComponentInChildren<Text>();
                tooltipImage = tooltip.GetComponent<Image>();
                break;
            case tooltipTypes.Monster:
                tooltip = GameObject.FindWithTag("Tooltip Monster");
                tooltiptext = tooltip.GetComponentInChildren<Text>();
                tooltipImage = tooltip.GetComponent<Image>();
                break;
            case tooltipTypes.Passive:
                tooltip = GameObject.FindWithTag("Tooltip Passives");
                tooltiptext = tooltip.GetComponentInChildren<Text>();
                tooltipImage = tooltip.GetComponent<Image>();
                break;
            case tooltipTypes.ResearchNode:
                tooltip = GameObject.Find("Tooltip Research");
                tooltipname = tooltip.transform.GetChild(0).GetComponent<Text>();
                tooltiptext = tooltip.transform.GetChild(1).GetComponent<Text>();
                tooltipImage = tooltip.GetComponent<Image>();
                break;
        }
        

        auraGroup = GameObject.Find("Aura Group");
    }

    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(1))
        {
            selecting = false;
            HideToolTip();
        }
        if (selecting)
            ShowToolTip();
    }

    private void OnMouseDown()
    {

        if (tooltipType == tooltipTypes.Monster)
        {
            if (lastSelected != null)
            {
                lastSelected.GetComponent<Tooltip>().selecting = false;
                lastSelected.GetComponent<Tooltip>().HideToolTip();
            }
            selecting = true;
            lastSelected = gameObject;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowToolTip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!selecting)
        HideToolTip();
    }
    public void OnMouseOver()
    {
        ShowToolTip();
    }
    public void OnMouseExit()
    {
        if(!selecting)
        HideToolTip();
    }

    public void ShowToolTip()
    {
        tooltipHidden = false;
        switch (tooltipType)
        {
            case tooltipTypes.UI:
                tooltiptext.text = Name + "\n\n" + Description;
                tooltip.transform.position = transform.parent.transform.position;
                break;
            case tooltipTypes.Monster:
                Rarity rarity = gameObject.GetComponent<Monster>().rarity.rarity;
                float HP = (float)System.Math.Round(gameObject.GetComponent<Monster>().currentHP,1);
                float maxHP = gameObject.GetComponent<Monster>().maxHP;
                float armor = gameObject.GetComponent<Defense>().armor;
                float coldResistance = gameObject.GetComponent<Defense>().coldResist;
                float lightningResistance = gameObject.GetComponent<Defense>().lightningResist;
                float fireResistance = gameObject.GetComponent<Defense>().fireResist;
                tooltiptext.text = rarity + " " +Name + "\n\n" + "HP: " + HP + "/" + maxHP + "\n" + "Armor: " + armor + "\n" + "Cold Resistance: " + coldResistance*100 +"%" + "\n" + "Lightning Resistance: " + lightningResistance*100 + "%" + "\n" + "Fire Resistance: " + fireResistance * 100 + "%";
                tooltip.transform.position = transform.position;
                Invoke("DisplayAura", 0.02f);
                break;
            case tooltipTypes.Passive:
                tooltiptext.text = hardTooltip;
                tooltip.transform.position = transform.position + new Vector3(0.25f,0.25f,0);
                break;
            case tooltipTypes.ResearchNode:
                tooltipname.text = Name;
                tooltiptext.text = "\n" + Description;
                tooltip.transform.position = transform.position;
                break;

        }
        if (tooltiptext.text != "")
        {
            tooltipImage.enabled = true;
        }

    }

    public void HideToolTip()
    {
        tooltipHidden = true;
        if ((tooltipname!=null)&&(tooltipname.text != null)) tooltipname.text = null;
        tooltiptext.text = null;
        tooltipImage.enabled = false;
        auraGroup.transform.position = new Vector3(-500, 0, 0);
    }
    void DisplayAura()
    {
        if (!tooltipHidden)
        {
            tooltipHeight = tooltip.GetComponent<RectTransform>().rect.height;
            if (!tooltipHidden)
            {
                auraGroup.transform.position = tooltip.transform.position - new Vector3(0, tooltipHeight / 60, 0);
                GetAuras();
            }

        }
    }

    void GetAuras()
    {
        foreach (Transform child in auraGroup.transform)
        {
            Destroy(child.gameObject);
        }

        Auras = gameObject.GetComponentsInChildren<AuraInformation>();
        if (Auras != null)
        {
            foreach (AuraInformation Aura in Auras)
            {
                GameObject newAura = Instantiate(AuraPrefab, transform.position, Quaternion.identity);
                newAura.GetComponent<AuraTooltipInfo>().AuraID = Aura.AuraID;
                newAura.GetComponent<AuraTooltipInfo>().AuraBaseDuration = Aura.baseDuration;
                newAura.GetComponent<AuraTooltipInfo>().AuraDuration = Aura.duration;
                newAura.GetComponent<AuraTooltipInfo>().stacks = Aura.stacks;
                newAura.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Aura.sprite;
                newAura.transform.SetParent(auraGroup.transform, false);
            }
        }
    }
}
