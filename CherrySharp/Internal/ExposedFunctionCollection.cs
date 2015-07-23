using System.Linq;
using System.Reflection;

namespace CherrySharp.Internal{
	public class ExposedFunctionCollection{
		private readonly MethodInfo[] _attributes;

		public ExposedFunctionCollection(object collectFrom){
			_attributes =
				collectFrom.GetType()
					.GetMethods()
					.Where(MethodHasExposeAttribute)
					.ToArray();
		}

		private static bool MethodHasExposeAttribute(MethodInfo methodInfo){
			return methodInfo.CustomAttributes.Any(attr => attr.AttributeType == typeof (ExposeAttribute));
		}

		public int Count{
			get { return _attributes.Length; }
		}

		public MethodInfo this[int index]{
			get { return _attributes[index]; }
		}
	}
}