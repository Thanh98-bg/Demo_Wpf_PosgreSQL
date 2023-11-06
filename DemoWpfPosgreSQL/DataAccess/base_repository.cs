using DemoWpfPosgreSQL.ViewModel;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWpfPosgreSQL.DataAccess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbProviderFactory provider_factory_;
        protected DbConnection connection_;
        protected DbCommand command_;
        protected string table_name_;
        public BaseRepository(DbProviderFactory provider_factory)
        {
            provider_factory_ = provider_factory;
            connection_ = provider_factory_.CreateConnection();
            connection_.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        #region ProtectedMethods
        protected abstract ICollection<T> ExecuteReader();
        protected abstract void AddItem(T item);
        protected abstract void UpdateItem(T item);
        protected abstract void DeleteItem(T item);
        #endregion
        #region PrivateMethods
        private void Open()
        {
            connection_.Open();
            command_ = provider_factory_.CreateCommand();
            command_.Connection = connection_;
        }
        private void Close()
        {
            connection_.Close();
        }
        #endregion
        #region IRepository
        public void Add(T item)
        {
            Open();
            AddItem(item);
            Close();
        }

        public void Delete(T item)
        {
            Open();
            DeleteItem(item);
            Close();
        }

        public ICollection<T> GetAll()
        {
            Open();
            ICollection<T> items_ = new List<T>();
            using (command_)
            {
                command_.CommandText = $"SELECT * FROM  {table_name_};";
                items_ = ExecuteReader();
            }
            Close();
            return items_;
        }

        public void Update(T new_item)
        {
            Open();
            UpdateItem(new_item);
            Close();
        }
        #endregion
    }
}
