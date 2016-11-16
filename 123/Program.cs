using System;

namespace Application
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			StringBuilder sb = new StringBuilder ();
			string chars = "abcdabe";
			string oldValue = "c";
			string newstr = "x";
			sb.Append (chars);
			sb.Append (newstr);
			sb.Append (oldValue);
			sb.Insert (0, newstr);
			sb.Replace (oldValue, newstr);
			Console.WriteLine (sb.ToString ());
            Console.ReadKey();
		}
	}
}

public class StringBuilder
{
	public StringBuilder ()
	{
		Capacity = 0;
		Length = 0;
		MaxCapacity = 1024;
		chars = new char[Capacity];
	}

	public char[] chars;

	public int Capacity;

	public int Length;

	public int MaxCapacity;

	public void ExpandCapacity (int newCapacity)                            //增大容量
	{
		try {
			if (newCapacity > MaxCapacity) {
				throw (new ArgumentOutOfRangeException ());
			}
		} 
		catch (ArgumentOutOfRangeException e) {
			Console.WriteLine ("Exception caught: {0}", e);	
			return;
		}
		char[] copy = new char[newCapacity * 2];  
		System.Array.Copy (chars, copy, Length);
		chars = copy;
		Capacity = newCapacity;
	}

	public StringBuilder Append (string value)
	{
		try {
			if (value.Length + Length > MaxCapacity) {
				throw(new ArgumentOutOfRangeException ());
			}				
		} 
		catch (ArgumentOutOfRangeException e) {
			Console.WriteLine ("Exception caught: {0}", e);	
			return this;
		}
		if (value.Length + Length > Capacity) {
			ExpandCapacity (value.Length + Length);
		}
		for (int i = Length; i < value.Length + Length; i++) {
			chars [i] = value [i - Length];
		}
		Length = Length + value.Length;
		return this;
	}

	public StringBuilder Insert (int index, string value)
	{
		try {
			if (index < 0 || index >= Length || value.Length + Length > MaxCapacity) {
				throw(new ArgumentOutOfRangeException ());
			}				
		} 
		catch (ArgumentOutOfRangeException e) {
			Console.WriteLine ("Exception caught: {0}", e);	
			return this;
		}
		if (value.Length + Length > Capacity) {
			ExpandCapacity (value.Length + Length);
		}
		for (int i = value.Length + Length - 1; i >= index; i--) {
			if (i > index + value.Length - 1) {
				chars [i] = chars [i - value.Length];
			} else {
				chars [i] = value [i - index];
			}
		}
		Length = Length + value.Length;	
		return this;
	}

	public StringBuilder Replace (string oldValue, string newValue)
	{
		try {
			if (oldValue == null) {
				throw (new ArgumentNullException ());
			}
			if (oldValue.Length == 0) {
				throw (new ArgumentException ());
			}
		} 
		catch (ArgumentNullException e) {
			Console.WriteLine ("Exception caught: {0}", e);	
			return this;
		} 
		catch (ArgumentException e) {
			Console.WriteLine ("Exception caught: {0}", e);	
			return this;
		}
		if (oldValue.Length > Length) {
			return this;
		}
		int i = 0;
		int j = 0;
		int m = oldValue.Length;
		int matchPosition = 0;

        while (i < chars.Length && j < oldValue.Length)
        {
            if (chars[i] == oldValue[j])
            {
                i++;
                j++;
            }
            else
            {
                if (m == chars.Length - 1) break;

                int k = oldValue.Length - 1;
                while (k >= 0 && chars[m] != oldValue[k])
                {
                    k--;
                }
                int gap = oldValue.Length - k;
                i += gap;
                m = i + oldValue.Length;
                if (m > chars.Length) m = chars.Length - 1;
                matchPosition = i;
                j = 0;
            }
        }

        if (i < Length)
            {

                if (newValue.Length - oldValue.Length + Length > Capacity)
                {
                    ExpandCapacity(newValue.Length - oldValue.Length + Length);
                }
                if (newValue.Length >= oldValue.Length)
                {
                    for (int v = newValue.Length - oldValue.Length + Length - 1; v >= matchPosition + newValue.Length; v--)
                    {
                        chars[v] = chars[v - newValue.Length + oldValue.Length];
                    }
                }
                else
                {
                    for (int v = matchPosition + newValue.Length; v < newValue.Length - oldValue.Length + Length; v++)
                    {
                        chars[v] = chars[v - newValue.Length + oldValue.Length];
                    }
                    for (int v = newValue.Length - oldValue.Length + Length; v < Length; v++)
                    {
                        chars[v] = '\0';
                    }
                }
                for (int h = matchPosition; h < matchPosition + newValue.Length; h++)
                {
                    chars[h] = newValue[h - matchPosition];
                }
                Length = newValue.Length - oldValue.Length + Length;
            }
            
      
        
		return this;
	}

	public override string ToString ()
	{
		string s = new string (chars);
		return s;
	}
}
