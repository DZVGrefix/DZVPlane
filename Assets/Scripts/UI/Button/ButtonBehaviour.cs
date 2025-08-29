using UnityEngine;
using UnityEngine.UI;

/*
    gomb kattintáshoz ősosztály
*/
[RequireComponent(typeof(Button))]
public abstract class ButtonBehaviour<T> : MonoBehaviour where T : Object
{
    protected Button MyButton;
    protected T Manager;


    // Hívatkozások begyüjtése
    private void Awake()
    {
        MyButton = GetComponent<Button>();
        Manager = FindFirstObjectByType<T>();
    }

    // felíratkozás az onClick eseményre
    private void Start()
    {
        MyButton.onClick.AddListener(OnClick);
    }

    // leíratkozás az onClick eseményről
    private void OnDestroy()
    {
        MyButton.onClick.RemoveListener(OnClick);
    }

    // Ezt minden osztálynak meg kell csinálnia.
    protected abstract void OnClick();
}
