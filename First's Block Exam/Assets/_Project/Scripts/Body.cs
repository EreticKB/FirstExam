using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public GameObject Snake;
    private Transform _head;
    private Player _player;
    public float HeadDiameter;
    public GhostPlayer GhostPlayer;

   

    private LinkedListNode<Vector3> _linkedListLNode;
    private LinkedList<Transform> _segments = new LinkedList<Transform>();
    private LinkedList<Vector3> _positions = new LinkedList<Vector3>();

    private void Awake()
    {
        _player = Snake.GetComponent<Player>();
        _head = Snake.GetComponent<Transform>();
        _positions.AddLast(_head.position);
    }

    private void Update()
    {
        float distance = (_head.position - _positions.First.Value).magnitude;//рассчет направления от старой позиции к новой
        if (distance > HeadDiameter) //если вышли за диаметр головы надо положить в список новую позицию головы и удалить устаревшую последнюю позицию
        {
            Vector3 direction = (_head.position - _positions.First.Value).normalized;
            _positions.AddFirst(_positions.First.Value + direction * HeadDiameter);
            _positions.RemoveLast();
            distance -= HeadDiameter; //чтобы не уйти далеко за 1 и не было рывка.
        }

        _linkedListLNode = _positions.First;

        if (_player.Collide)
        {
            distance = GhostPlayer.Distance;
            if (distance / HeadDiameter >= 0.9f) RetractSnake();
        }
            
        foreach (Transform segment in _segments)
        {
            segment.position = Vector3.Lerp(_linkedListLNode.Next.Value, _linkedListLNode.Value, distance / HeadDiameter);
            _linkedListLNode = _linkedListLNode.Next;
        }
    }
    public void ExtendSnake()
    {
        Transform segment = Instantiate(_head, _positions.Last.Value, Quaternion.identity, transform);
        _segments.AddLast(segment);
        _positions.AddLast(segment.position);
    }

    public void RetractSnake()
    {
        Destroy(_segments.First.Value.gameObject);
        _segments.RemoveFirst();
        _positions.Remove(_positions.First.Next);
    }
}
