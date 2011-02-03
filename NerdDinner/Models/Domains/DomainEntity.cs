using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdDinner.Models.Domains
{
    public abstract class DomainEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public interface IRolePlayer
    {

    }

    public interface IRole
    {
    }

    public interface IPlayRole
    {
        IRole Role { get; set; }
    }

    public abstract class PlayedBy<T> : IPlayRole where T : IRole
    {
        public T Self
        {
            get
            {
                return (T)Role;
            }
        }

        public IRole Role { get; set; }
    }


    public static class WithRole
    {
        public static T PlayRole<T>(this IRole role) where T : IPlayRole, new()
        {
            var t = new T();
            t.Role = role;
            return t;
        }

        public static T2 With<T2>(this object role)
        {
            var dynamic = new LinFu.Reflection.DynamicObject(role);

            return (T2)dynamic.CreateDuck(typeof(T2));
        }

        
    }
}