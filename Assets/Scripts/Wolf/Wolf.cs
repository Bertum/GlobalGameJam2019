using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public int MaxTimeToAppear;
    public int Duration;

    internal WolfManager _manager;

    private double _nextAppear;
    private double _duration;
    private Renderer _renderer;
    private WolfAlert _alert;

    // Start is called before the first frame update
    void Start()
    {
        if (MaxTimeToAppear <= 0) MaxTimeToAppear = 5;
        if (Duration <= 0) Duration = 3;
        _nextAppear = MaxTimeToAppear;
        _duration = Duration + _nextAppear;
        _renderer = GetComponentInChildren<Renderer>();
        _renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_nextAppear <= 0d)
        {
            var wallAppear = _manager.TryToAppear(this);
            if (wallAppear != null)
            {
                transform.SetParent(wallAppear.transform);
                transform.localPosition = new Vector3(-1, 0);
                var position = transform.position;
                transform.parent = null;
                transform.position = position;
                transform.rotation = wallAppear.transform.rotation;
                _nextAppear = double.MaxValue;
                _renderer.enabled = true;
                _alert = gameObject.AddComponent<WolfAlert>();
            }
        }
        else if (_duration <= 0d)
        {
            _nextAppear = Random.Range(1, MaxTimeToAppear);
            _duration = Duration + _nextAppear;
            transform.parent = null;
            _renderer.enabled = false;
            _manager.Disappear(this);
            Destroy(_alert);
            _alert = null;
        }
        else
        {
            _duration -= Time.deltaTime;
            _nextAppear -= Time.deltaTime;
        }
    }
}
