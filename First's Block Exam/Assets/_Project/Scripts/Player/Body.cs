using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Transform Head;
    public Transform Tail;
    public TextMesh Text;
    private Transform _headTwo;
    public Player Snake;
    public float HeadDiameter;
    public GhostPlayer GhostPlayer;

    private LinkedListNode<Vector3> _linkedListLNode;
    private LinkedList<Transform> _segments = new LinkedList<Transform>();
    private LinkedList<Vector3> _positions = new LinkedList<Vector3>();

    [HideInInspector] public int Size { get; private set; } = 4;

    private void Awake()
    {
        _positions.AddLast(Head.position);
        _headTwo = GhostPlayer.transform;
    }

    private void Update()
    {
        float distance = (Head.position - _positions.First.Value).magnitude;//������� ����������� �� ������ ������� � �����
        if (distance > HeadDiameter) //���� ����� �� ������� ������ ���� �������� � ������ ����� ������� ������ � ������� ���������� ��������� �������
        {
            Vector3 direction = (Head.position - _positions.First.Value).normalized;
            _positions.AddFirst(_positions.First.Value + direction * HeadDiameter);
            _positions.RemoveLast();
            distance -= HeadDiameter; //����� �� ���� ������ �� 1 � �� ���� �����.
        }
        if (Snake.Collide) //����� ������� �����, ����� �������� � ����.
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
            RetractSnake(true);
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

    public void RetractSnake(bool isStay)
    {
        if (Size == 0f)
        {
            gameObject.GetComponent<Player>().Die();
            return;
        }
        Size--;
        Text.text = Size.ToString();
        Destroy(_segments.First.Value.gameObject);
        _segments.RemoveFirst();
        if (isStay) _positions.RemoveLast();
        else _positions.Remove(_positions.First.Next);
    }
}
