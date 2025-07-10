using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void Game()
	{
		SceneManager.LoadScene("Basket"); // в двойных кавычках пишите имя вашей сцены(где ваша игра)
	}

	public void Back()
	{
		SceneManager.LoadScene("Menu"); // в двойных кавычках пишите имя вашей сцены(где ваше меню)
	}

	public void ExitGame() //выход из игры 
	{
		Application.Quit();
		Debug.Log("Exit");
	}
}
