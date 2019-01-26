using UnityEngine;
using UnityEngine.UI;

public class PigController : MonoBehaviour
{
    private int wheat, wood, stone;
    public Material currentMaterial;
    private JoystickController joystickController;

    private Image imageCurrentResource;
    private Text countCurrentReource;
    public Sprite wheatResource;
    public Sprite woodResource;
    public Sprite stoneResource;

    public void Start()
    {
        this.joystickController = GetComponent<JoystickController>();
        this.imageCurrentResource = GameObject.Find("ImageCurrentResource").GetComponent<Image>();
        this.countCurrentReource = GameObject.Find("CountCurrentResource").GetComponent<Text>();
        this.imageCurrentResource.color = new Color(0, 0, 0, 0);
        this.countCurrentReource.color = new Color(0, 0, 0, 0);
    }

    private void Awake()
    {
        ResetMaterials();

    }

    private void Update()
    {

    }


    private bool UseMaterial()
    {
        bool used = false;
        if (currentMaterial == Material.Wheat && wheat > 0)
        {
            // TODO reproduce use wheat sound
            wheat--;
            used = true;
        }
        else if (currentMaterial == Material.Wood && wood > 0)
        {
            // TODO reproduce use wood sound
            wood--;
            used = true;
        }
        else if (currentMaterial == Material.Stone && stone > 0)
        {
            // TODO reproduce use stone sound
            stone--;
            used = true;
        }
        if (!used)
        {
            // TODO sound pig without materials?
        }
        return used;
    }

    public void ChangeMaterial(Material newMaterial)
    {
        currentMaterial = newMaterial;
        ResetMaterials();
        switch (currentMaterial)
        {
            case Material.Wheat:
                // TODO reproduce get wheat sound
                wheat = GameConfiguration.WHEATCAPACITY;
                this.imageCurrentResource.sprite = wheatResource;
                this.countCurrentReource.text = string.Concat(wheat);
                break;
            case Material.Wood:
                // TODO reproduce get wood sound
                wood = GameConfiguration.WOODCAPACITY;
                this.imageCurrentResource.sprite = woodResource;
                this.countCurrentReource.text = string.Concat(wood);
                break;
            case Material.Stone:
                // TODO reproduce get stone sound
                stone = GameConfiguration.STONECAPACITY;
                this.imageCurrentResource.sprite = stoneResource;
                this.countCurrentReource.text = string.Concat(stone);
                break;
        }

        this.imageCurrentResource.color = new Color(255, 255, 255, 255);
        this.countCurrentReource.color = new Color(255, 201, 0, 255);
    }

    public bool IsRepairingWall()
    {
        return joystickController.IsPressAnyButton();
        //return Input.GetKeyDown(KeyCode.R);
    }

    public bool IsGettingResource()
    {
        return joystickController.IsPressAnyButton();
        //return Input.GetKeyDown(KeyCode.R);
    }

    public Material GetCurrentMaterial()
    {
        return currentMaterial;
    }

    private void ResetMaterials()
    {
        wheat = 0;
        wood = 0;
        stone = 0;
    }
}
