using System;
namespace LinAlgebraCore.DependencyInjection.Interfaces
{
	public interface IDeepCloner<T>
	{
		T DeepClone(T other);
	}
}

