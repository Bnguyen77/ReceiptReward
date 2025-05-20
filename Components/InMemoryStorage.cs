using System.Collections.Concurrent;

namespace ReceiptPoints.Components
{
	public class InMemoryStorage
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
