using Sitecore.Analytics.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.WFFM.Abstractions.Analytics
{
    //TODO: must be reworked
    /// <summary>Allows a class to implement an dictionary accessor.</summary>
    [Obsolete("Will be removed in the next version.")]
    public interface IModelCollectionMember : IModelMember
    {
        /// <summary>Gets the collection of elements.</summary>
        /// <value>
        ///   An <see cref="T:Sitecore.Analytics.Model.Framework.IElementCollection`1" /> object that contains the elements within the collection.
        /// </value>
        IElementCollection<IElement> Elements { get; }
    }
}
