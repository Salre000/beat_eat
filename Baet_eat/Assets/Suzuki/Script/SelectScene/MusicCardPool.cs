using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCardPool : MonoBehaviour
{
    [SerializeField] private Transform _upLimit;
    [SerializeField] private Transform _downLimit;
    private List<GameObject> _musicCards = new(MusicManager.CAPACITY);

    private void Start()
    {
        _musicCards = MusicManager.instance.GetMusicCards();
    }

    private void Update()
    {
        ActiveLimit();
    }

    private void ActiveLimit()
    {
        // limitの間にあるオブジェクトはActiveにする
        foreach (GameObject cards in _musicCards)
        {
            if (_upLimit.position.y >= cards.transform.position.y && _downLimit.position.y <= cards.transform.position.y)
            {
                cards.SetActive(true);
            }
            else
                cards.SetActive(false);
        }
    }
}
