using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public Sprite wheatResource;
    public Sprite woodResource;
    public Sprite stoneResource;
    public Material resourceMaterial;

    private PigController _pigController;
    private GameObject _materialGO;
    private SpriteRenderer _materialSpriteRenderer;
    private ResourceFX _resourceFX;

    // Start is called before the first frame update
    void Start()
    {
        InitResource();
        _resourceFX = GetComponent<ResourceFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pigController != null)
        {
            if (_pigController.IsGettingResource())
            {
                _pigController.ChangeMaterial(resourceMaterial);
                _resourceFX.PlayTakeResource(resourceMaterial);
            }
        }
    }

    private void InitResource()
    {
        _materialGO = gameObject.transform.GetChild(0).gameObject;
        _materialSpriteRenderer = _materialGO.GetComponent<SpriteRenderer>();
        _materialSpriteRenderer.sprite = GetMaterialSprite();        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == GameConfiguration.PLAYER_TAG)
        {
            _pigController = other.GetComponent<PigController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == GameConfiguration.PLAYER_TAG)
        {
            _pigController = null;
        }
    }

    private Sprite GetMaterialSprite()
    {
        switch (resourceMaterial)
        {
            case Material.Wheat:
                return wheatResource;
            case Material.Wood:
                return woodResource;
            case Material.Stone:
                return stoneResource;
            default:
                return null;
        }
    }
}
