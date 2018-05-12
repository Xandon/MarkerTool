using UnityEngine;

/*! \cond PRIVATE */
namespace DarkTonic.CoreGameKit {
	public static class CoreMonoHelper {
		public static Transform GetChildTransform(this Transform transParent, string childName) {
			#if UNITY_2017_1_OR_NEWER
			return transParent.Find(childName);
			#else
			return transParent.FindChild(childName);
			#endif
		}
	}
}
/*! \endcond */