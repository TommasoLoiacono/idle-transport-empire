using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Threading;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
	public const string GENERAL_SAVE_FILE = "/GeneralSave.save";

	public const string GENERAL_FILE_VERSION = "0.0.1";

	private string _pathDirectoryFiles = "";

	[NonSerialized]
	public SaveData MySaveData;

	private Thread _IOThread;
	private Semaphore _semaphoreThread;
	private int _threadWaiting;
	private Coroutine _hideLoadSaveCoroutine;

	private void Awake()
	{
		_pathDirectoryFiles = Application.persistentDataPath;

		MySaveData = null;
		_semaphoreThread = new Semaphore(1, 1);
		_threadWaiting = 0;
		_hideLoadSaveCoroutine = null;
	}

	///--------------------------------------------------------------------------
	///-------------------------------GENERAL FILE-------------------------------
	///--------------------------------------------------------------------------

	public void CreateSaveDataFile(bool saveImmediatly = false)
	{
		SaveData generalSave = new SaveData();

		MySaveData = generalSave;

		if (saveImmediatly)
			SaveSaveDataFile();
	}

	public void DeleteSaveDataFile()
	{
		DeleteFile(_pathDirectoryFiles + GENERAL_SAVE_FILE);
	}

	public void SaveSaveDataFile()
	{
		SaveData generalSave = MySaveData.GetClone();

		_IOThread = new Thread(delegate ()
		{
				SaveOnFile(_pathDirectoryFiles + GENERAL_SAVE_FILE,
				generalSave);
			});
		_threadWaiting++;
		_IOThread.Start();

		if (_hideLoadSaveCoroutine == null)
			_hideLoadSaveCoroutine = StartCoroutine(HideSaveLoadIcon());
	}

	public void LoadSaveDataFile()
	{
		object data;

		data = LoadFromFile(_pathDirectoryFiles + GENERAL_SAVE_FILE);

		if (data == null)
		{
			Debug.Log("No general file found.");
		}
		try
		{
			MySaveData = (SaveData)data;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Error on opening general file: " + ex.Message);
			MySaveData = null;
		}
	}

	///--------------------------------------------------------------------------
	///-----------------------------GENERAL FUNCTION-----------------------------
	///--------------------------------------------------------------------------

	private void SaveOnFile(string path, object data)
	{
		BinaryFormatter bf = new BinaryFormatter();
		_semaphoreThread.WaitOne();
		try
		{
			FileStream file;
			file = File.Create(path);
			bf.Serialize(file, data);
			file.Close();
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Error in saving " + path + ": " + ex.Message);
		}

		_semaphoreThread.Release();
		_threadWaiting = 0;
	}

	private object LoadFromFile(string path)
	{
		BinaryFormatter bf = new BinaryFormatter();
		object data = null;

		FileStream file;
		if (File.Exists(path))
		{
			try
			{
				file = File.Open(path, FileMode.Open);
				data = bf.Deserialize(file);
				file.Close();
			}
			catch (Exception)
			{
				return data;
			}
		}
		else
		{
			return data;
		}

		return data;
	}

	private IEnumerator HideSaveLoadIcon()
	{
		while (_threadWaiting > 0)
			yield return null;

		yield return null;

		_hideLoadSaveCoroutine = null;
	}

	private void DeleteFile(string path)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}
}
