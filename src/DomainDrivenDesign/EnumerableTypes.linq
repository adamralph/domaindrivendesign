<Query Kind="Program">
  <Namespace>System.Collections</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	typeof(int[]).GetInterfaces().Dump();
	typeof(Bar).GetInterfaces().Where(type => typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType).Dump();
}

		class Foo : IEnumerable
		{

			public IEnumerator GetEnumerator()
			{
				throw new NotImplementedException();
			}
		}

		class Bar : IEnumerable<int>
		{

			public IEnumerator<int> GetEnumerator()
			{
				throw new NotImplementedException();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}
		}

		class Baz : IEnumerable<int>, IEnumerable<string>
		{
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				throw new NotImplementedException();
			}
		}

		class Gum : IEnumerable<int>, IEnumerable<string>, IEnumerable
		{

			public IEnumerator<int> GetEnumerator()
			{
				throw new NotImplementedException();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				throw new NotImplementedException();
			}
		}