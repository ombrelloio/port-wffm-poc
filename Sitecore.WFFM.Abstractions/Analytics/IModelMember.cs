using Sitecore.Analytics.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.WFFM.Abstractions.Analytics
{
    //TODO: must be reworked
    /// <summary>
    ///   Allows a class to implement a moder member in the model hierarchy.
    /// </summary>
    [Obsolete("Will be removed in the next version.")]
    public interface IModelMember
    {
        /// <summary>Gets the element containing the current member.</summary>
        /// <value>
        ///   The <see cref="T:Sitecore.Analytics.Model.Framework.IElement" /> object that contains the current member; or <c>null</c> if the current member is
        ///   the root element.
        /// </value>
        IElement Parent { get; }

        /// <summary>Gets the name of the current member.</summary>
        /// <value>
        ///   A <see cref="T:System.String" /> value that contains the name of the current member.
        /// </value>
        string Name { get; }

        /// <summary>
        ///   Gets a value that indicates whether the current member is set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the current member has a value set; otherwise, <c>false</c>.
        /// </value>
        bool IsEmpty { get; }

        /// <summary>Resets the current node.</summary>
        void Reset();
    }
}
