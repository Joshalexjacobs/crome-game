using System;
using System.Collections.Generic;

public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable {
	#region IComparer<TKey> Members

	public int Compare(TKey x, TKey y) {
		int result = y.CompareTo(x);

		if (result == 0)
			return 1;   // Handle equality as beeing greater
		else
			return result;
	}

	#endregion
}