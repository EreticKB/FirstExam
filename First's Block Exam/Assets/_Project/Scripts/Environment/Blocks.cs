using System.Collections;
using UnityEngine;
using Assets._Project.Scripts.Environment;

public class Blocks : MonoBehaviour, IObjectsInteractable
{
    public Material[] colors = new Material[6];
    public Renderer Renderer;
    public TextMesh Text;
    private int HP;

    private void Start()
    {
        Text.text = HP.ToString();
        setColor(Renderer);
    }
    public bool GetDamage()
    {
        HP--;
        setColor(Renderer);
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

    private void setColor(Renderer renderer)
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
        Text.text = HP.ToString();
        setColor(Renderer);
    }
    public void Refresh(int quality)
    {
        Refresh(quality, 4);
    }
}
