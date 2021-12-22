using UnityEngine;
using Assets._Project.Scripts.Environment;

public class FoodConsumption : MonoBehaviour, IObjectsInteractable
{
    public GameObject[] Forms = new GameObject[7];
    public int _value = 3;
    public TextMesh Text;


    private void Awake()
    {
        Forms[Random.Range(0, 6)].SetActive(true);
    }
    private void Start()
    {
        Text.text = _value.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Body body)) return;
        for (int i = 0; i < _value; i++) body.ExtendSnake();
        gameObject.SetActive(false);
    }

    public void Refresh(int quality)
    {
        if (quality == 1) _value = Random.Range(1, 10);
        if (quality == 2) _value = Random.Range(1, 18);
        if (quality == 3) _value = Random.Range(8, 15);
        Text.text = _value.ToString();
        foreach (GameObject form in Forms) form.SetActive(false);
        Forms[Random.Range(0, 6)].SetActive(true);
    }
}
