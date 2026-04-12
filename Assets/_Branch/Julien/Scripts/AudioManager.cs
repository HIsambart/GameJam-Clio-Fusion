using UnityEngine;
using Utils;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    public AudioClip BruitDebrit;
    public AudioClip BruitDefaite;
    public AudioClip BruitDeFeu;
    public AudioClip BruitDuTokamak;
    public AudioClip BruitJeuPression;
    public AudioClip BruitJeuRotation;
    public AudioClip BruitJeuxRecette;
    public AudioClip BruitUI;
    public AudioClip BruitVictoire;
    public AudioClip BruitWarning;
    public AudioClip MusiqueInGame;
    public AudioClip MusiqueMenuDELancement;
    public AudioClip Perso_Pas1;
    public AudioClip Perso_Pas2;
    
    
    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
