using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Dialogue
{   
	[FormerlySerializedAs("_sentences")]
	[TextArea(3,10)]
	[SerializeField] private string[] sentences;
	[SerializeField] private int phase;

	#region GetSet
	public string[] Sentences
	{
		get => sentences;
		set => sentences = value;
	}

	public int Phase
	{
		get => phase;
		set => phase = value;
	}
	#endregion
}
