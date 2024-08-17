using UnityEngine;

[System.Serializable]
public struct TransformSave
{
    public Vector3 position;
    public Quaternion rotation;

    public TransformSave(Transform transform)
    {
        this.position = transform.position;
        this.rotation = transform.rotation;
    }

    public TransformSave(Vector3 posititon)
    {
        this.position = posititon;
        this.rotation = Quaternion.identity;
    }
}
