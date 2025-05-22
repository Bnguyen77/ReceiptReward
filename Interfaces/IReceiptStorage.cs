namespace ReceiptReward.Interfaces
{
	public interface IReceiptStorage
	{
		void Save(string id, int points);
		bool TryGet(string id, out int points);
	}
}