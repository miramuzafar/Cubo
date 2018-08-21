using System;

[Serializable]
public class AppDetails {

	// Use this for initialization
	public int componentQuantity;
	public int isExpanded;
	public decimal price;
	public int code;
	public AppDetails()
	{
		componentQuantity = 0;
		isExpanded = 0;
		price = 0.0M;
		code = 0;
	}
}
