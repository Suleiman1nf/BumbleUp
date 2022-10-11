using System.Collections;
using UnityEngine;

namespace Suli.Bumble
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);

        public void StopCoroutine(Coroutine coroutine);
    }
}