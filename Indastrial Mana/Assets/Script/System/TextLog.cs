using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLog : SingletonMonoBehaviour<TextLog>
{
    [SerializeField]
    private Text _text = null;
    [SerializeField]
    private GameObject _window = null;
    private const byte _MAXLINE = 5;
    private List<string> _body = new List<string>();
    private bool _isActive = false;

    void Start()
    {
        _text.text = string.Empty;
    }

    private void Write()
    {
        _text.text = string.Empty;

        foreach(string s in _body)
        {
            //Debug.Log(s);
            _text.text += s + '\n';
        }
    }
    public void Insert(string s)
    {
        _body.Add(s);
        if (_body.Count > _MAXLINE)
            _body.RemoveAt(0);

        Write();
    }

    public void Onclick()
    {
        if (_isActive)
            _isActive = false;
        else
            _isActive = true;

        _window.SetActive(_isActive);
    }
}
