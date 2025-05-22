using ReceiptReward.Interfaces;
using System.Collections.Concurrent;

namespace ReceiptReward.Components
{
	public class InMemoryStorage : IReceiptStorage
	{
		private readonly ConcurrentDictionary<string, int> _store = new();

		public void Save(string id, int points)
		{
			_store[id] = points;
		}

		public bool TryGet(string id, out int points)
		{
			return _store.TryGetValue(id, out points);
		}
	}
}
