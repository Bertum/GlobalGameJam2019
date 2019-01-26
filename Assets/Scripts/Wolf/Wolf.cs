using System;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public int MaxTimeToAppear;
    public int BlowDuration;
    public int WalkDuration;
    public int Offset;

    internal GameManager Manager;
    internal GameObject Wall;
    internal Renderer Renderer;
    internal WolfAlert Alert;
    internal Vector3 InitPosition;

    private Dictionary<Type, WolfState> _mapping;
    private WolfState _current;

    // Start is called before the first frame update
    void Start()
    {
        if (MaxTimeToAppear <= 0) MaxTimeToAppear = 5;
        if (BlowDuration <= 0) BlowDuration = 3;
        if (WalkDuration <= 0) WalkDuration = 3;
        if (Offset == 0) Offset = -1;
        else Offset = -Math.Abs(Offset);

        Renderer = GetComponentInChildren<Renderer>();
        Renderer.enabled = false;

        _mapping = new Dictionary<Type, WolfState>
        {
            { typeof(AppearingState), new AppearingState(MaxTimeToAppear, this) },
            { typeof(BlowingState), new BlowingState(BlowDuration, this)},
            { typeof(MovingState), new MovingState(WalkDuration, this)}
        };
        _current = _mapping[typeof(AppearingState)];
    }

    // Update is called once per frame
    void Update()
    {
        _current.Update();
    }

    internal void Progress<T>() where T : WolfState
    {
        _current = _mapping[typeof(T)];
        _current.ResetState();
    }
}

public abstract class WolfState
{
    protected readonly float _duration;
    protected float _remaining;

    protected WolfState(float stateDuration, Wolf wolf)
    {
        _duration = _remaining = stateDuration;
        Wolf = wolf;
    }

    public Wolf Wolf { get; }

    protected abstract void TriggerState();

    public virtual void Update()
    {
        _remaining -= Time.deltaTime;
        if (_remaining <= 0)
        {
            TriggerState();
        }
    }

    public virtual void ResetState()
    {
        _remaining = _duration;
    }
}

public class AppearingState : WolfState
{
    public AppearingState(float stateDuration, Wolf wolf) : base(stateDuration, wolf)
    {

    }

    protected override void TriggerState()
    {
        Wolf.Wall = Wolf.Manager.TryToAppear(Wolf);
        if (Wolf.Wall != null)
        {
            Wolf.transform.rotation = Quaternion.Euler(Vector3.zero);
            Wolf.transform.SetParent(Wolf.Wall.transform);
            Wolf.transform.localPosition = new Vector3(Wolf.Offset, 0);
            Wolf.InitPosition = Wolf.transform.position;
            Wolf.transform.parent = null;
            Wolf.transform.position = Wolf.InitPosition;
            Wolf.transform.Rotate(0, 0, Wolf.Wall.transform.rotation.eulerAngles.z-180);
            Wolf.Renderer.enabled = true;
            Wolf.Alert = Wolf.gameObject.AddComponent<WolfAlert>();
            Wolf.GetComponentInChildren<ParticleSystem>().Play();
            Wolf.Progress<BlowingState>();
        }
    }

    public override void ResetState()
    {
        _remaining = UnityEngine.Random.Range(1, _duration);
    }
}

public class BlowingState : WolfState
{
    public BlowingState(float stateDuration, Wolf wolf) : base(stateDuration, wolf)
    {
    }

    protected override void TriggerState()
    {
        Wolf.GetComponentInChildren<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmitting);
        var wallCtrl = Wolf.Wall.GetComponent<WallController>();
        wallCtrl.DestroyWall();
        if (!wallCtrl.IsNoWall)
        {
            Wolf.Renderer.enabled = false;
            Wolf.Manager.Disappear(Wolf);
            UnityEngine.Object.Destroy(Wolf.Alert);
            Wolf.Alert = null;
            Wolf.Progress<AppearingState>();
        }
        else
        {
            Wolf.Progress<MovingState>();
        }
    }
}

public class MovingState : WolfState
{
    public MovingState(float stateDuration, Wolf wolf) : base(stateDuration, wolf)
    {
    }

    public override void Update()
    {
        _remaining -= Time.deltaTime;
        if (_remaining >= 0)
        {
            TriggerState();
        }
        else
        {
            // TODO: Loose
            Debug.Log("You looser");
        }
    }

    protected override void TriggerState()
    {
        var nextPosition = Vector3.Lerp(Wolf.Wall.transform.position, Wolf.InitPosition, _remaining / _duration);
        Wolf.transform.position = nextPosition;
    }
}