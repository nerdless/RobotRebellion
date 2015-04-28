using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public void Reload ()
    {
       Application.LoadLevel(1);
    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
