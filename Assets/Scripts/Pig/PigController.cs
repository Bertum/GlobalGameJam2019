using UnityEngine;
using UnityEngine.UI;

public class PigController : MonoBehaviour
{
    public int wheat, wood, stone;
    public Material currentMaterial;
    public bool useKeys;
    private JoystickController joystickController;

    public Sprite wheatResource;
    public Sprite woodResource;
    public Sprite stoneResource;

    private GameObject _materialGO;
    private SpriteRenderer _materialSpriteRenderer;
    private PigFX _pigFX;
    private Animator _hammerAnimator;
    private GameObject _hammerGO;

    private InputKeyController inputKeyController;

    public void Start()
    {
        this.joystickController = GetComponent<JoystickController>();
        _materialGO = gameObject.transform.GetChild(1).gameObject;
        _hammerGO = gameObject.transform.GetChild(2).gameObject;
        _materialSpriteRenderer = _materialGO.GetComponent<SpriteRenderer>();
        _materialSpriteRenderer.sprite = null;
        _pigFX = GetComponent<PigFX>();
        this.inputKeyController = GetComponent<InputKeyController>();
        _hammerAnimator = _hammerGO.GetComponent<Animator>();
    }

    private void Awake()
    {
        ResetMaterials();
    }

    private void Update()
    {
        if (this.useKeys && !this.inputKeyController.enabled)
        {
            this.inputKeyController.enabled = true;
            this.joystickController.enabled = false;
        }
        else if (!this.useKeys && !this.joystickController.enabled)
        {
            this.inputKeyController.enabled = false;
            this.joystickController.enabled = true;
        }
    }

    public void UseMaterial()
    {
        _hammerAnimator.SetTrigger("Hit");
        if (currentMaterial == Material.Wheat && wheat > 0)
        {
            wheat--;
            if (wheat == 0)
            {
                //No more resources
                _materialSpriteRenderer.sprite = null;
            }
        }
        else if (currentMaterial == Material.Wood && wood > 0)
        {
            wood--;
            if (wood == 0)
            {
                //No more resources
                _materialSpriteRenderer.sprite = null;
            }
        }
        else if (currentMaterial == Material.Stone && stone > 0)
        {
            stone--;
            if(stone == 0)
            {
                //No more resources
                _materialSpriteRenderer.sprite = null;
            }
        }
    }

    public bool TryUseMaterial()
    {
        if (wheat > 0 || wood > 0 || stone > 0)
        {
            return true;
        }
        else
        {
            _pigFX.PlayNoMaterial();
            return false;
        }
    }

    public void ChangeMaterial(Material newMaterial)
    {
        currentMaterial = newMaterial;
        ResetMaterials();
        switch (currentMaterial)
        {
            case Material.Wheat:
                wheat = GameConfiguration.WHEATCAPACITY;
                _materialSpriteRenderer.sprite = wheatResource;
                break;
            case Material.Wood:
                wood = GameConfiguration.WOODCAPACITY;
                _materialSpriteRenderer.sprite = woodResource;
                break;
            case Material.Stone:
                stone = GameConfiguration.STONECAPACITY;
                _materialSpriteRenderer.sprite = stoneResource;
                break;
        }
    }

    public bool IsRepairingWall()
    {
        if (useKeys)
        {
            return this.inputKeyController.IsKeySpaceDown() && TryUseMaterial();
        }
        else
        {
            return joystickController.IsPressAnyButton() && TryUseMaterial();
        }
    }

    public bool IsGettingResource()
    {
        if (useKeys)
        {
            return this.inputKeyController.IsKeySpaceDown();
        }
        else
        {
            return joystickController.IsPressAnyButton();
        }
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
