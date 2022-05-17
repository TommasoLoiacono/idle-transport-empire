using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
	private List<GameObject> _pooler;
	private GameObject _pooledPrefab;
	private Transform _parent;
	private int _increment;

	/// <summary>
	/// Instantiate a new pooler contained zero Object clones.
	/// </summary>
	/// <param name="zeroObject">The original object.</param>
	/// <param name="amountInPooler">The number of clones in the pooler.</param>
	/// <param name="increment">The increment in the pooler when no object is available.</param>
	/// <param name="parent">The parent of the clones.</param>
	public ObjectPooler(GameObject zeroObject, int amountInPooler, int increment, Transform parent = null)
	{
		_increment = increment;
		_parent = parent;

		_pooledPrefab = zeroObject;

		_pooler = new List<GameObject>();

		InstantiateNewClones(amountInPooler);
	}

	/// <summary>
	/// Returns an available clone.
	/// </summary>
	/// <param name="getActivated">If true the clone returned is already active, otherwise not. Default is true. (If false, if the object doesn't become active it remains available for the pooler).</param>
	/// <returns>An available clone.</returns>
	public GameObject GetObject(bool getActivated = true)
	{
		foreach (GameObject clone in _pooler)
		{
			if (CheckAvailability(clone))
			{
				clone.SetActive(getActivated);
				return clone;
			}
		}

		if (_increment == 0)
		{
			return null;
		}

		InstantiateNewClones(_increment);

		return GetObject(getActivated);
	}

	/// <summary>
	/// Returns an available clone.
	/// </summary>
	/// <param name="getActivated">If true the clone returned is already active, otherwise not. Default is true. (If false, if the object doesn't become active it remains available for the pooler).</param>
	/// <param name="transform">Sets a specific parent for the object.</param>
	/// <returns>An available clone.</returns>
	public GameObject GetObject(Transform parent, bool getActivated = true)
	{
		foreach (GameObject clone in _pooler)
		{
			if (CheckAvailability(clone))
			{
				if (parent != null)
				{
					clone.transform.SetParent(parent);
				}
				clone.SetActive(getActivated);
				return clone;
			}
		}

		if (_increment == 0)
		{
			return null;
		}

		InstantiateNewClones(_increment);

		return GetObject(parent, getActivated);
	}

	/// <summary>
	/// Adds new clones to the pooler
	/// </summary>
	/// <param name="amount">The amount of new clones (amount >= 0).</param>
	/// <exception cref="UnityException">If amount is less then 0.</exception>  
	public void AddInPooler(int amount)
	{
		if (amount < 0)
		{
			throw new UnityException("You cannot add a negative number of object to the pooler.");
		}

		InstantiateNewClones(amount);
	}

	/// <summary>
	/// Removes clones from the pooler.
	/// </summary>
	/// <param name="amount">The number of clones to remove (amount >= 0).</param>
	/// <param name="onlyNotAvailable">If true only the available clones are removed, otherwise all clones can be removed.</param>
	/// <returns>The effective number of clones removed.</returns>
	/// <exception cref="UnityException">If amount is less then 0.</exception>  
	public int RemoveFromPooler(int amount, bool onlyNotAvailable = true)
	{
		int amountRemoved;
		bool exit;

		amountRemoved = 0;

		if (amount < 0)
		{
			throw new UnityException("You cannot remove a negative number of object to the pooler.");
		}

		if (amount > _pooler.Count)
		{
			amount = _pooler.Count;
		}

		if (amount == _pooler.Count && !onlyNotAvailable)
		{
			_pooler.Clear();
			amountRemoved = amount;
		}
		else
		{
			exit = false;
			for (int i = 0; i < _pooler.Count && !exit; i++)
			{
				if (!onlyNotAvailable)
				{
					_pooler.RemoveAt(i);
					amountRemoved++;
					i--;
				}
				else if (CheckAvailability(_pooler[i]) && onlyNotAvailable)
				{
					_pooler.RemoveAt(i);
					amountRemoved++;
					i--;
				}
				if (amountRemoved >= amount)
				{
					exit = true;
				}
			}
		}

		return amountRemoved;
	}

	/// <summary>
	/// Add a specific object in the pooler.
	/// </summary>
	/// <param name="clone">The new object.</param>
	public void ForceObjectInPooler(GameObject clone)
	{
		_pooler.Add(clone);
	}

	/// <summary>
	/// Returns the number of available clones.
	/// </summary>
	/// <returns>Number of available clones.</returns>
	public int CountAvailable
	{
		get
		{
			int n;
			n = 0;
			foreach (GameObject clone in _pooler)
			{
				if (CheckAvailability(clone))
				{
					n++;
				}
			}
			return n;
		}
	}

	/// <summary>
	/// Sets to active = false all objects inside the pooler, and sets their parent transform to that of the pooler.
	/// </summary>
	public void GiveBackAllObjects()
	{
		foreach (GameObject clone in _pooler)
		{
			clone.SetActive(false);
			clone.transform.parent = _parent;
			clone.transform.position = _parent.position;
		}
	}

	/// <summary>
	/// Returns the total number of clones.
	/// </summary>
	/// <returns>Total number of clones.</returns>
	public int CountClones
	{
		get
		{
			return _pooler.Count;
		}
	}

	/// <summary>
	/// Destroys all game objects in the pooler and cleans the list. Then it repopulates the pool at the start conditions.
	/// </summary>
	public void ResetPooler(bool instantiateNewClones)
	{
		int amountInPooler;

		amountInPooler = _pooler.Count;

		foreach (GameObject clone in _pooler)
		{
			GameObject.Destroy(clone);
		}

		_pooler.Clear();

		if (instantiateNewClones)
			InstantiateNewClones(amountInPooler);
	}

	/// <summary>
	/// Creates new clones and it adds them to the pooler.
	/// </summary>
	/// <param name="amount">The amount of new clones.</param>
	private void InstantiateNewClones(int amount)
	{
		GameObject newClone;
		for (int i = 0; i < amount; i++)
		{
			if (_parent != null)
			{
				newClone = GameObject.Instantiate(_pooledPrefab, Vector3.zero, Quaternion.identity, _parent);
			}
			else
			{
				newClone = GameObject.Instantiate(_pooledPrefab, Vector3.zero, Quaternion.identity);
			}

			newClone.SetActive(false);

			_pooler.Add(newClone);
		}
	}

	private bool CheckAvailability(GameObject clone)
	{
		return !clone.activeSelf && (clone.transform.parent.Equals(_parent));
	}
}
