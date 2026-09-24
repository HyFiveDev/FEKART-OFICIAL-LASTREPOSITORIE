using UnityEngine;

public class gizmocont : MonoBehaviour
{
    [SerializeField] AraraAnim anim;
    private bool virou=false;

    public float posicaoFrente = 0.5f;
    public float posicaoTras = -0.5f;
    // Update is called once per frame
    void Update()
    {
        if (anim.flipped==true && virou==false)
        {
            transform.localPosition = new Vector2(posicaoTras, transform.localPosition.y);
            virou = true;

        }
        else if (anim.flipped==false && virou==true)
        {
            transform.localPosition = new Vector2(posicaoFrente, transform.localPosition.y);
            virou = false;
        }
    }
}
