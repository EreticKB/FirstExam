using System.Collections;
using UnityEngine;
using Assets._Project.Scripts.Environment;

public class Blocks : MonoBehaviour, IObjectsInteractable
{
    public Material[] colors = new Material[6];
    Renderer _renderer;
    public TextMesh Text;
    private int HP = 27;

    private void Start()
    {
        Text.text = (HP - 1).ToString();
        _renderer = GetComponent<Renderer>();
        _setColor(_renderer);
    }
    public bool GetDamage()
    {
        HP--;
        _setColor(_renderer);
        Text.text = HP.ToString();
        if (HP > 0) return true;
        StartCoroutine(Death());
        return false;
    }
    IEnumerator Death()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }

    private void _setColor(Renderer renderer)
    {
        if (HP < 11) renderer.sharedMaterial = colors[0];
        else if (HP < 21) renderer.sharedMaterial = colors[1];
        else if (HP < 31) renderer.sharedMaterial = colors[2];
        else if (HP < 41) renderer.sharedMaterial = colors[3];
        else if (HP < 51) renderer.sharedMaterial = colors[4];
        else renderer.sharedMaterial = colors[5];
    }

    public void Refresh(int quality, int size)
    {

        if (quality == 1) HP = Random.Range(size / 4, size / 2);
        if (quality == 2) HP = Random.Range(size / 4, size);
        if (quality == 3) HP = Random.Range(size / 2, size * 3 / 2);
        if (quality == 4) HP = Random.Range(1, size * 2);
        HP += 3;
    }
    public void Refresh(int quality)
    {
        Refresh(quality, 4);
    }
}
