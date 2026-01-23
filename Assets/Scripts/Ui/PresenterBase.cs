using System;
using R3;

namespace Ui
{
    public abstract class PresenterBase : IDisposable
    {
        protected readonly CompositeDisposable Disposables = new();

        protected void Bind<T>(ReadOnlyReactiveProperty<T> source, Action<T> onNext)
        {
            source.Subscribe(onNext).AddTo(Disposables);
        }

        protected void BindClick(Observable<Unit> clickStream, Action onClick)
        {
            clickStream.Subscribe(_ => onClick()).AddTo(Disposables);
        }

        public virtual void Dispose()
        {
            Disposables.Dispose();
            Disposables.Clear();
        }
    }
}
