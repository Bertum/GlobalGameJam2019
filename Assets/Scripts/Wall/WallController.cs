using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public int hits;
    public Sprite noWall;
    public Sprite wheatWall;
    public Sprite woodWall;
    public Sprite stoneWall;
    public Material wallMaterial;

    public int _life;
    private GameObject _wallTypeGO;
    private SpriteRenderer _wallTypeSpriteRenderer;
    private SpriteRenderer _crackSpriteRenderer;
    private GameObject _wallCrackGO;
    private PigController _pigController;
    private WolfController _wolfController;
    private WallFX _wallFX;

    // Start is called before the first frame update
    void Start()
    {
        InitWall();
        _wallFX = GetComponent<WallFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_pigController != null)
        {
            if (_pigController.IsRepairingWall())
            {
                if (RepairWall(_pigController.GetCurrentMaterial()))
                {
                    _pigController.UseMaterial();
                }                
            }            
        }

        if(_wolfController != null)
        {
            if (_wolfController.IsDestroyingWall())
            {
                DestroyWall();
            }            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == GameConfiguration.PLAYER_TAG)
        {
            _pigController = other.GetComponent<PigController>();
            
        }
        else if (other.tag == GameConfiguration.ENEMY_TAG)
        {
            _wolfController = other.GetComponent<WolfController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == GameConfiguration.PLAYER_TAG)
        {
            _pigController = null;
        }
        else if (other.tag == GameConfiguration.ENEMY_TAG)
        {
            _wolfController = null;
        }
    }

    private bool RepairWall(Material material)
    {
        bool wallRepaired = false;
        if(IsNoWall)
        {
            wallMaterial = material;
            InitHits();
        }

        if (_life < hits && wallMaterial == material)
        {
            _life++;
            SetupWall();
            _wallFX.PlayRepair(material);
            wallRepaired = true;
        }
        return wallRepaired;
    }

    public void DestroyWall()
    {
        if (_life > 0)
        {
            _life--;
            if(_life == 0)
            {
                _wallFX.PlayExplode(wallMaterial);
                wallMaterial = Material.None;                
            }
            SetupWall();
        }        
    }

    public void InitWall()
    {
        InitHits();
        _life = hits;
        _wallTypeGO = gameObject.transform.GetChild(0).gameObject;
        _wallCrackGO = gameObject.transform.GetChild(1).gameObject;
        _wallTypeSpriteRenderer = _wallTypeGO.GetComponent<SpriteRenderer>();
        _crackSpriteRenderer = _wallCrackGO.GetComponent<SpriteRenderer>();
        _crackSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        SetupWall();
    }

    private void InitHits()
    {
        hits = GetHits();
    }

    public bool IsWallCracked
    {
        get { return !IsNoWall && _life <= hits / 2; }
    }

    public bool IsWallNotWell
    {
        get { return !IsWallCracked && !IsNoWall && _life < hits; }
    }

    public bool IsNoWall
    {
        get { return _life == 0; }
    }

    private Sprite GetWallSprite()
    {
        switch (wallMaterial)
        {
            case Material.Wheat:
                return wheatWall;
            case Material.Wood:
                return woodWall;
            case Material.Stone:
                return stoneWall;
            default:
                return noWall;
        }
    }

    private void SetupWall()
    {
        if (IsWallNotWell)
        {
            _crackSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if (IsWallCracked)
        {
            _crackSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            _crackSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
        _wallTypeSpriteRenderer.sprite = GetWallSprite();
    }

    private int GetHits()
    {
        switch (wallMaterial)
        {
            case Material.Wheat:
                return GameConfiguration.WHEAT_HITS;
            case Material.Wood:
                return GameConfiguration.WOOD_HITS;
            case Material.Stone:
                return GameConfiguration.STONE_HITS;
            default:
                return 0;
        }
    }
}
