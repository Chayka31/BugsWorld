using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Juk_controller : MonoBehaviour
{
    public int _lvl;
    public float _maxhp;
    public float _hp;
    public float _damage;
    public int _score;

    public float _Speed;
    public float _RotationSpeed;
    public PlayerMovement pl_mov;
    public bool is_stuned;
    private bool is_dead;

    [SerializeField]private TextMeshPro textMeshPro;
    [SerializeField]private Image imag_hp;
    public GameObject Telce;
    public Animator StunImage;
    public GameObject[] bodyparts;

    public AudioClip EatBugSound;
    public AudioClip StunSound;
    public AudioSource audio;

    void Start()
    {
        textMeshPro =  transform.GetComponentInChildren<TextMeshPro>();
        _maxhp = _hp;
        Image[] imgs = GetComponentsInChildren<Image>();
        imag_hp = imgs[1];
        TryGetComponent<PlayerMovement>(out pl_mov);
        StunImage = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();

        bodyparts = Telce.transform.GetChild(0).GetComponentsInChildren<Transform>()
            .Where(child => child.CompareTag("body_part"))
            .Select(child => child.gameObject)
            .ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Уровень: " + _lvl;
        imag_hp.fillAmount = _hp / _maxhp;
        if(_hp <= 0 && is_dead == false) 
        {
            JukDeath();
            is_dead = true;
        }
    }

    public void SoundBeat() 
    {
        audio.clip = StunSound;
        audio.Play();
    }

    public void SoundEat() 
    {
        audio.clip = EatBugSound;
        audio.Play();
    }
    public void JukDeath() 
    {
        bodyparts.ToList().ForEach(part => part.AddComponent<MeshCollider>().convex = true);
        bodyparts.ToList().ForEach(part => part.AddComponent<Rigidbody>());
        Invoke("DestroingComponents", 0.1f);
        Destroy(gameObject, 3f);
    }

    public void DestroingComponents() 
    {
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<PlayerMovement>());
        Destroy(Telce.transform.GetChild(0).GetComponentInChildren<Hp_Script>().gameObject);
        Destroy(Telce.transform.GetChild(0).GetComponentInChildren<Hit_script>().gameObject);
        Destroy(GetComponent<FsmExample>());
        Destroy(textMeshPro.gameObject);
        Destroy(this);
    }
}
