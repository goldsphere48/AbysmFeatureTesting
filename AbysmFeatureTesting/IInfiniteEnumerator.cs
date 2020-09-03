using System;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting
{
    interface IInfiniteEnumerator<T>
    {
        T Next(LayerName layerName);
    }
}