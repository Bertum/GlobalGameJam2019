using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared : MonoBehaviour
{
    public enum TipoPared
    {
        Paja,
        Madera,
        Piedra
    }

    public int hits = 1;
    public Sprite noPared;
    public Sprite paredPaja;
    public Sprite paredMadera;
    public Sprite paredPiedra;
    public Sprite grietas;
    public TipoPared tipoPared = TipoPared.Paja;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = GetPared();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if()
    }

    private Sprite GetPared()
    {
        switch (tipoPared)
        {
            case TipoPared.Paja:
                return paredPaja;
            case TipoPared.Madera:
                return paredMadera;
            case TipoPared.Piedra:
                return paredPiedra;
            default:
                return noPared;
        }
    }
}
