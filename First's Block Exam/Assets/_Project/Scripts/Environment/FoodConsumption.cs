using UnityEngine;

public class FoodConsumption : MonoBehaviour
{
    public GameObject[] forms;
    public int _value = 3;
    public TextMesh Text;
    

    private void Awake()
    {
        forms[Random.Range(0, 6)].SetActive(true);
    }
    private void Start()
    {
        Text.text = _value.ToString();   
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Body body)) return;
        for (int i = 0; i < _value; i++) body.ExtendSnake();
        Destroy(gameObject);
    }

}
