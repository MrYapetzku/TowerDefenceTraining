using UnityEngine;

public abstract class Tower : GameTileContent
{
    [SerializeField, Range(1.5f, 10.5f)] protected float TargetingRange = 1.5f;

    public abstract TowerType Type { get; }

    protected bool IsAcquireTarget(out TargetPoint target)
    {
        if (TargetPoint.FilleBuffer(transform.localPosition, TargetingRange))
        {
            target = TargetPoint.GetBuffered(0);
            return true;
        }

        target = null;
        return false;
    }

    protected bool IsTargetTracked(ref TargetPoint target)
    {
        if (target == null)
        {
            return false;
        }

        Vector3 myPos = transform.localPosition;
        Vector3 targetPos = target.Position;
        if (Vector3.Distance(myPos, targetPos) > TargetingRange + target.ColliderSize * target.Enemy.Scale)
        {
            target = null;
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = transform.localPosition;
        position.y += 0.01f;
        Gizmos.DrawWireSphere(position, TargetingRange);
    }
}