using UnityEngine;

public interface IMover
{
    public void Move(Transform transform, float speed, Vector3 dirVector);
}

public class NoneMover : IMover
{
    public void Move(Transform transform, float speed, Vector3 dirVector)
    {
        return;
    }
}

public class LinearMover : IMover
{
    public void Move(Transform transform, float speed, Vector3 dirVector)
    {
        transform.position += dirVector * speed * Time.deltaTime;
    }
}
