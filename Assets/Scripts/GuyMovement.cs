using System.Collections.Generic;
using UnityEngine;

public class GuyMovement : MonoBehaviour
{
    public MoverID MoverID;
    public float Speed = 5f;
    public Vector3 DirVector;

    IMover Mover;
    private Camera mainCam;

    public void Awake()
    {
        mainCam = Camera.main;
    }

    public void Init(MoverID moverID, float speed, Vector3 dirVector)
    {
        MoverID = moverID;
        Speed = speed;
        DirVector = dirVector;

        Mover = MoverLookUp.MoverByID[moverID];
    }

    private void Update()
    {
        Move();
        Wrap();
    }

    public void Move()
    {
        Mover.Move(transform, Speed, DirVector);
    }

    void Wrap()
    {
        Vector3 spritePosition = transform.position;

        float screenLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenTop = mainCam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        float screenBottom = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        if (spritePosition.x < screenLeft)
        {
            spritePosition.x = screenRight;
        }
        else if (spritePosition.x > screenRight)
        {
            spritePosition.x = screenLeft;
        }

        if (spritePosition.y < screenBottom)
        {
            spritePosition.y = screenTop;
        }
        else if (spritePosition.y > screenTop)
        {
            spritePosition.y = screenBottom;
        }

        transform.position = spritePosition;
    }
}

public enum MoverID
{
    None = 0,
    Linear = 1
}

public static class MoverLookUp
{
    public static readonly Dictionary<MoverID, IMover> MoverByID = new()
    {
        {
            MoverID.None, new NoneMover()
        },
        {
            MoverID.Linear, new LinearMover()
        },
    };
}