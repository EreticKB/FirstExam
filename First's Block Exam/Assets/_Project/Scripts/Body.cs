using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Transform Head;
    public float HeadDiameter;
   

    private LinkedListNode<Vector3> _linkedListLNode;
    private LinkedList<Transform> _segments = new LinkedList<Transform>();
    private LinkedList<Vector3> _positions = new LinkedList<Vector3>();

    private void Awake()
    {
        _positions.AddLast(Head.position);
    }

    private void Update()
    {
        float distance = (Head.position - _positions.First.Value).magnitude;//??????? ??????????? ?? ?????? ??????? ? ?????
        if (distance > HeadDiameter) //???? ????? ?? ??????? ?????? ???? ???????? ? ?????? ????? ??????? ?????? ? ??????? ?????????? ????????? ???????
        {
            Vector3 direction = (Head.position - _positions.First.Value).normalized;
            _positions.AddFirst(_positions.First.Value + direction * HeadDiameter);
            _positions.RemoveLast();
            distance -= HeadDiameter; //????? ?? ???? ?????? ?? 1 ? ?? ???? ?????.
        }

        _linkedListLNode = _positions.First;
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
        Destroy(_segments.First.Value.gameObject);
        _segments.RemoveFirst();
        _positions.Remove(_positions.First.Next);
    }
}
