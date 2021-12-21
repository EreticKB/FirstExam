using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Transform Head;
    public Player Snake;
    public float HeadDiameter;
    public GhostPlayer GhostPlayer;



    private LinkedListNode<Vector3> _linkedListLNode;
    private LinkedList<Transform> _segments = new LinkedList<Transform>();
    private LinkedList<Vector3> _positions = new LinkedList<Vector3>();

    private void Awake()
    {
        _positions.AddLast(Head.position);
    }

    private void Update()
    {
        float distance = (Head.position - _positions.First.Value).magnitude;//рассчет направления от старой позиции к новой
        if (distance > HeadDiameter) //если вышли за диаметр головы надо положить в список новую позицию головы и удалить устаревшую последнюю позицию
        {
            Vector3 direction = (Head.position - _positions.First.Value).normalized;
            _positions.AddFirst(_positions.First.Value + direction * HeadDiameter);
            _positions.RemoveLast();
            distance -= HeadDiameter; //чтобы не уйти далеко за 1 и не было рывка.
        }

        _linkedListLNode = _positions.First;

        if (Snake.Collide)
        {
            distance = GhostPlayer.Distance;
            if (distance / HeadDiameter >= 1f)
            {
                RetractSnake();
                Snake.Collide = false;
            }
        }

        foreach (Transform segment in _segments)
        {
            segment.position = Vector3.Lerp(_linkedListLNode.Next.Value, _linkedListLNode.Value, distance / HeadDiameter);
            _linkedListLNode = _linkedListLNode.Next;
        }
    }
    public void ExtendSnake()
    {
        Transform segment = Instantiate(Head, _positions.Last.Value, Quaternion.identity, transform);
        _segments.AddLast(segment);
        _positions.AddLast(segment.position);
    }

    public void RetractSnake()
    {
        if (_segments.First == null)
        {
            Debug.Log("emptysnake");
            return;
        }
        Destroy(_segments.First.Value.gameObject);
        _segments.RemoveFirst();
        _positions.Remove(_positions.First.Next);
    }
}
