using System;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Transform Head;
    public Transform Tail;
    public TextMesh Text;
    public Player Snake;
    public float HeadDiameter;
    public GhostPlayer GhostPlayer;

    private Transform _headTwo;
    private LinkedListNode<Vector3> _linkedListLNode;
    private LinkedList<Transform> _segments = new LinkedList<Transform>();
    private LinkedList<Vector3> _positions = new LinkedList<Vector3>();

    [HideInInspector] public int Size { get; private set; }

    private void Awake()
    {
        _positions.AddLast(Head.position);
        _headTwo = GhostPlayer.transform;
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
        if (Snake.Collide) //чтобы двигать хвост, когда уперлись в блок.
        {
            distance = (_headTwo.position - GhostPlayer.Position).magnitude;
            if (distance > HeadDiameter) distance = HeadDiameter;
        }

        _linkedListLNode = _positions.First;
        foreach (Transform segment in _segments)
        {
            segment.position = Vector3.Lerp(_linkedListLNode.Next.Value, _linkedListLNode.Value, distance / HeadDiameter);
            _linkedListLNode = _linkedListLNode.Next;
        }
        if (distance / HeadDiameter >= 1f && Snake.Collide)
        {
            Snake.Collide = false;
        }
    }
    public void ExtendSnake()
    {
        
        Transform segment = Instantiate(Tail, _positions.Last.Value, Quaternion.identity, transform);
        _segments.AddLast(segment);
        _positions.AddLast(segment.position);
        Size++;
        Text.text = Size.ToString();
    }

    public void RetractSnake()
    {
        if (Size < 1f)
        {
            gameObject.GetComponent<Player>().Die();
            return;
        }
        Size--;
        Text.text = Size.ToString();
        Destroy(_segments.First.Value.gameObject);
        _segments.RemoveFirst();
        _positions.RemoveLast();
        
    }

    public void DisableHead()
    {
        Head.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
