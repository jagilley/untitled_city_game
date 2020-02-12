<<<<<<< HEAD
﻿using System.Runtime.CompilerServices;

// Allow internal visibility for testing purposes.
[assembly: InternalsVisibleTo("Unity.TextCore")]

[assembly: InternalsVisibleTo("Unity.FontEngine.Tests")]

#if UNITY_EDITOR
[assembly: InternalsVisibleTo("Unity.TextCore.Editor")]
[assembly: InternalsVisibleTo("Unity.TextMeshPro.Editor")]
#endif
=======
﻿using System.Runtime.CompilerServices;

// Allow internal visibility for testing purposes.
[assembly: InternalsVisibleTo("Unity.TextCore")]

[assembly: InternalsVisibleTo("Unity.FontEngine.Tests")]

#if UNITY_EDITOR
[assembly: InternalsVisibleTo("Unity.TextCore.Editor")]
[assembly: InternalsVisibleTo("Unity.TextMeshPro.Editor")]
#endif
>>>>>>> 711d5e49af469ce061ba97343ef1560d9c22cb45
