using UnityEngine;

public class ShatterDeath : CubeDeathAnimation
{
    [SerializeField] private MeshRenderer _renderer;

    private ShatterPool _pool;

    protected override void OnPlay()
    {
        _renderer.enabled = false;
        _pool.PlayAt(transform.position);
    }

    public void Init(ShatterPool pool)
    {
        _pool = pool;
    }
}
