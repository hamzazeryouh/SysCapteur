using Sys.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Entities.BaseEntities
{
    /// <summary>
    /// Entity class that implement <see cref="IEntity"/>, which is the base entity class
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// the creation time of the model
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// the last time the model has been modified
        /// </summary>
        public DateTimeOffset? LastModifiedOn { get; set; }

    }

    /// <summary>
    /// Entity class that implement <see cref="IEntity"/> and inherit from the <see cref="Entity"/> base class
    /// </summary>
    /// <typeparam name="Tkey">type of key</typeparam>
    public abstract class Entity<Tkey> : Entity, IEntity<Tkey>
    {
        /// <summary>
        /// the id of the entity
        /// </summary>
        public Tkey Id { get; set; }
    }
}
