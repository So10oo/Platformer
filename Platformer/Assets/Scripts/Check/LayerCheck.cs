using UnityEngine;

public class LayerCheck : Check
{
    [SerializeField] LayerMask Layer;
    protected Collider2D colliderCheck;
    protected ContactFilter2D contactFilter;

    protected override void AwakeHundler()
    {
        colliderCheck = GetComponent<Collider2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(Layer);
    }

    Collider2D[] result = new Collider2D[1];

    protected override bool CheckAllObject()
    {
        return colliderCheck.OverlapCollider(contactFilter, result) > 0;
    }

    protected override bool CheckObject(Collider2D collision)
    {
        return (Layer.value & (1 << collision.gameObject.layer)) != 0;
    }

}
