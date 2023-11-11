using System;
using LinAlgebraCore.DependencyInjection.Interfaces;

namespace LinAlgebraCore.DependencyInjection
{
	public class DeepCloner<T> : IDeepCloner<T>
	{
		public DeepCloner()
		{
		}

        public T DeepClone(T other)
        {
            throw new NotImplementedException();
        }
    }
}

