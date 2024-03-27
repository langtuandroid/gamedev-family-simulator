using UnityEngine;

public class StoreHandler : MonoBehaviour {
	
	[Header("Total Coins Earned")]
	[SerializeField] private int totalEarnedCoins;
	
	[Header("Rewards Per Levels")]
	[SerializeField] private int[] rewardPerLevels;
	
	[Header("Total Cost of the Product")]
	[SerializeField] private int[] costOfProduct;
	
	public void FirstTimeComing(int temp)
	{
		if (PlayerPrefs.GetInt("FirstTimeComing_1", 0) != 0) return;
		PlayerPrefs.SetInt ("FirstTimeComing_1", 1);
		SetTotalEarnedCoins (temp);
	}

	public void SetTotalEarnedCoins(int temp)
	{
		totalEarnedCoins = temp;
		PlayerPrefs.SetInt ("TotalCoinsEarned", totalEarnedCoins);
	}

	public int GetTotalEarnedCoins()
	{
		totalEarnedCoins = PlayerPrefs.GetInt ("TotalCoinsEarned",0);
		return totalEarnedCoins;
	}

	public int GetRewardOfLevel(int temp)
	{
		return rewardPerLevels [temp];
	}

	public bool BuyCurrentProduct(int temp)
	{
		totalEarnedCoins = PlayerPrefs.GetInt ("TotalCoinsEarned",0);
		if (totalEarnedCoins >= costOfProduct[temp]) {
			return true;
		}
		return false;
	}

	public int ReturnCostOfProduct(int temp)
	{
		return costOfProduct [temp];
	}

}
