using UnityEngine;

namespace UI.Animations
{
    public interface IAnimatableShow
    {
        void Show();
        GameObject GameObject { get; }
    }
}