using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.WFFM.Abstractions.Analytics
{
    //TODO: must be reworked
    /// <summary>
    ///   Allows a class to implement a collection of <see cref="T:Sitecore.Analytics.Model.Framework.IModelMember" /> objects.
    /// </summary>
    [Obsolete("Will be removed in the next version.")]
    public interface IModelMemberCollection : IEnumerable<IModelMember>, IEnumerable
    {
        /// <summary>
        ///   Gets the number of elements contained in the current collection.
        /// </summary>
        /// <value>
        ///   An <see cref="T:System.Int32" /> value that indicates the number of nodes contained in the current collection.
        /// </value>
        int Count { get; }

        /// <summary>Gets the member with the specified name.</summary>
        /// <param name="name">
        ///   The case-sensitive name of the member the return.
        /// </param>
        /// <returns>
        ///   The <see cref="T:Sitecore.Analytics.Model.Framework.IModelMember" /> object with the specified name.
        /// </returns>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
        ///   The collection does not contain a member with the specified name.
        /// </exception>
        IModelMember this[string name] { get; }

        /// <summary>
        ///   Determines whether a member is contained within the current collection.
        /// </summary>
        /// <param name="name">The name of the member to locate.</param>
        /// <returns>
        ///   <c>true</c> if a member with the specified name is found in the current collection; otherwise,
        ///   <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///   Argument <paramref name="name" /> is a <c>null</c> reference.
        /// </exception>
        bool Contains(string name);
    }
}
