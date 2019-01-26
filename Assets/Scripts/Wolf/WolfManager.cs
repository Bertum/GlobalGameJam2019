using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfManager : MonoBehaviour
{
    public float TimeToAdd;
    public GameObject WolfPrefab;

    private GameObject[] _walls;

    private readonly Dictionary<Wolf, GameObject> _enemies = new Dictionary<Wolf, GameObject>();
    private readonly List<GameObject> _freeWalls = new List<GameObject>();
    private float _counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (TimeToAdd == 0f) TimeToAdd = 5;
        _walls = GameObject.FindGameObjectsWithTag(GameConfiguration.WALL);
        _freeWalls.AddRange(_walls);
    }

    // Update is called once per frame
    void Update()
    {
        if (_counter <= 0)
        {
            var wolf = Instantiate(WolfPrefab).GetComponentInChildren<Wolf>();
            wolf._manager = this;
            _enemies.Add(wolf, null);
            _counter += TimeToAdd;
        }
        else
        {
            _counter -= Time.deltaTime;
        }
    }

    public GameObject TryToAppear(Wolf w)
    {
        if (_freeWalls.Count == 0) return null;

        var i = Mathf.RoundToInt(Random.Range(0, _freeWalls.Count));
        var wallAppear = _freeWalls[i];
        _freeWalls.RemoveAt(i);
        _enemies[w] = wallAppear;
        return wallAppear;
    }

    public void Disappear(Wolf w)
    {
        var wall = _enemies[w];
        if (wall == null) return;

        _freeWalls.Add(wall);
        _enemies[w] = null;
    }
}
