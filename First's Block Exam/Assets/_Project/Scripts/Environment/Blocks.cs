using System.Collections;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public Material[] colors = new Material[6];
    Renderer _renderer;
    public TextMesh Text;
    private int HP = 27;

    private void Start()
    {
        Text.text = (HP-1).ToString();
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
        Destroy(gameObject);
    }

    private void _setColor(Renderer renderer)
    {
        if (HP < 6) renderer.sharedMaterial = colors[0];
        else if (HP < 11) renderer.sharedMaterial = colors[1];
        else if (HP < 16) renderer.sharedMaterial = colors[2];
        else if (HP < 21) renderer.sharedMaterial = colors[3];
        else if (HP < 26) renderer.sharedMaterial = colors[4];
        else renderer.sharedMaterial = colors[5];
    }
}
