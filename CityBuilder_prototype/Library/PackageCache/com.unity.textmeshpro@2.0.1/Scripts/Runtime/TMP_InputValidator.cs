<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;


namespace TMPro
{
    /// <summary>
    /// Custom text input validator where user can implement their own custom character validation.
    /// </summary>
    [System.Serializable]
    public abstract class TMP_InputValidator : ScriptableObject
    {
        public abstract char Validate(ref string text, ref int pos, char ch);
    }
}
=======
﻿using UnityEngine;
using System.Collections;


namespace TMPro
{
    /// <summary>
    /// Custom text input validator where user can implement their own custom character validation.
    /// </summary>
    [System.Serializable]
    public abstract class TMP_InputValidator : ScriptableObject
    {
        public abstract char Validate(ref string text, ref int pos, char ch);
    }
}
>>>>>>> 711d5e49af469ce061ba97343ef1560d9c22cb45
