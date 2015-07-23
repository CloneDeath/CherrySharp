using System.Linq;
using System.Reflection;

namespace CherrySharp.Internal{
	public class ExposedFunctionCollection{
		private readonly MethodInfo[] _attributes;

		public ExposedFunctionCollection(object collectFrom){
			_attributes =
				collectFrom.GetType()
					.GetMethods()
					.Where(f => f.CustomAttributes.Any(attr => attr.AttributeType == typeof (ExposeAttribute)))
					.ToArray();
		}

		public int Count{
			get { return _attributes.Length; }
		}
	}
}