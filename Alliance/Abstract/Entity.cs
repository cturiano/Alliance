using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Alliance.Interfaces;
using Alliance.Models;
using Newtonsoft.Json;

namespace Alliance.Abstract
{
    public abstract class Entity<T> : IEquatable<Entity<T>> where T : IEntity
    {
        #region Constructors

        protected Entity(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        #endregion

        #region Properties

        public Address Address { get; }

        public string Id { get; set; }

        public string Name { get; }

        #endregion

        #region Public Methods

        public void Delete()
        {
            var objects = ReadFile();

            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    if (obj.Id == Id)
                    {
                        objects.Remove(obj);
                        Id = string.Empty;
                        break;
                    }
                }
            }

            WriteToFile(objects);
        }

        public bool Equals(Entity<T> other)
        {
            return other != null && (ReferenceEquals(this, other) || Equals(Address, other.Address) && string.Equals(Id, other.Id) && string.Equals(Name, other.Name));
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<T>;
            if (other == null)
            {
                return false;
            }

            return ReferenceEquals(this, obj) || Equals((Entity<T>)obj);
        }

        public static T Find(string id)
        {
            var objects = ReadFile();

            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    if (obj.Id == id)
                    {
                        return (T)Convert.ChangeType(obj, typeof(T));
                    }
                }
            }

            return default(T);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Address != null ? Address.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !Equals(left, right);
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                // the item has already been saved
                return;
            }

            Id = Guid.NewGuid().ToString();
            var objects = ReadFile() ?? new HashSet<T>();
            if (objects.Add((T)Convert.ChangeType(this, typeof(T))))
            {
                WriteToFile(objects);
            }
        }

        #endregion

        #region Private Methods

        private static string GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        private static string GetPathToSaveFile()
        {
            var type = typeof(T);
            return Path.Combine(GetAssemblyDirectory(), $"{type.Name}.json");
        }

        private static HashSet<T> ReadFile()
        {
            var path = GetPathToSaveFile();
            using (var sr = new StreamReader(File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)))
            {
                var json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<HashSet<T>>(json);
            }
        }

        private static void WriteToFile(HashSet<T> objects)
        {
            using (var sw = new StreamWriter(GetPathToSaveFile()))
            {
                sw.Write(JsonConvert.SerializeObject(objects));
            }
        }

        #endregion
    }
}