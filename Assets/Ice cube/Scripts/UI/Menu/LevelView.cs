using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(Image))]
public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _color;
    [SerializeField] private float _scale;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Init(int level)
    {
        _text.text = level.ToString();
    }

    public void Select()
    {
        _image.color = _color;
        transform.localScale *= _scale;
    }
}
