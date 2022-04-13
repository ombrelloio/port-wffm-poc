// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Providers.SqlFormsDataProvider
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Analytics.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sitecore.WFFM.Analytics.Providers
{
    public class SqlFormsDataProvider : IWffmDataProvider
    {
        private readonly IDbConnectionProvider _connectionProvider;
        private readonly string _connectionString;
        private readonly ISettings _settings;

        public SqlFormsDataProvider(
          string connectionStringName,
          ISettings settings,
          IDbConnectionProvider connectionProvider)
        {
            Assert.ArgumentNotNullOrEmpty(connectionStringName, nameof(connectionStringName));
            Assert.ArgumentNotNull((object)settings, nameof(settings));
            Assert.ArgumentNotNull((object)connectionProvider, nameof(connectionProvider));
            this._connectionString = settings.GetConnectionString(connectionStringName);
            this._settings = settings;
            this._connectionProvider = connectionProvider;
        }

        public virtual IEnumerable<Sitecore.WFFM.Abstractions.Analytics.FormData> GetFormData(
          Guid formId)
        {
            if (this._settings.IsRemoteActions)
                return (IEnumerable<Sitecore.WFFM.Abstractions.Analytics.FormData>)new List<Sitecore.WFFM.Abstractions.Analytics.FormData>();
            List<Sitecore.WFFM.Abstractions.Analytics.FormData> formDataList = new List<Sitecore.WFFM.Abstractions.Analytics.FormData>();
            bool flag = false;
            using (IDbConnection connection = this._connectionProvider.GetConnection(this._connectionString))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [Id],[FormItemId],[ContactId],[InteractionId],[TimeStamp],[Data] FROM [FormData] WHERE [FormItemId]=@p1";
                    command.Parameters.Add((object)new SqlParameter("p1", (object)formId));
                    command.CommandType = CommandType.Text;
                    IDataReader dataReader = command.ExecuteReader();
                    try
                    {
                        while (dataReader.Read())
                        {
                            Sitecore.WFFM.Abstractions.Analytics.FormData formData = new Sitecore.WFFM.Abstractions.Analytics.FormData()
                            {
                                Id = dataReader.GetGuid(0),
                                FormID = dataReader.GetGuid(1),
                                ContactId = dataReader.GetGuid(2),
                                InteractionId = dataReader.GetGuid(3),
                                Timestamp = dataReader.GetDateTime(4)
                            };
                            formDataList.Add(formData);
                        }
                    }
                    catch
                    {
                        flag = true;
                    }
                    finally
                    {
                        dataReader.Close();
                    }
                }
            }
            if (!flag && formDataList.Count > 0)
            {
                foreach (Sitecore.WFFM.Abstractions.Analytics.FormData formData in formDataList)
                {
                    List<Sitecore.WFFM.Abstractions.Analytics.FieldData> fieldDataList = new List<Sitecore.WFFM.Abstractions.Analytics.FieldData>();
                    using (IDbConnection connection = this._connectionProvider.GetConnection(this._connectionString))
                    {
                        connection.Open();
                        using (IDbCommand command = connection.CreateCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "SELECT [Id],[FieldItemId],[FieldName],[Value],[Data] FROM [FieldData] WHERE [FormId]=@p1";
                            command.Parameters.Add((object)new SqlParameter("p1", (object)formData.Id));
                            command.CommandType = CommandType.Text;
                            IDataReader dataReader = command.ExecuteReader();
                            try
                            {
                                while (dataReader.Read())
                                {
                                    Sitecore.WFFM.Abstractions.Analytics.FieldData fieldData = new Sitecore.WFFM.Abstractions.Analytics.FieldData()
                                    {
                                        Id = new Guid(dataReader["Id"].ToString()),
                                        FieldId = new Guid(dataReader["FieldItemId"].ToString()),
                                        FieldName = dataReader["FieldName"] as string,
                                        FormId = formData.Id,
                                        Value = dataReader["Value"] as string,
                                        Data = dataReader["Data"] as string
                                    };
                                    fieldDataList.Add(fieldData);
                                }
                            }
                            catch
                            {
                                flag = true;
                            }
                            finally
                            {
                                dataReader.Close();
                            }
                        }
                    }
                    if (fieldDataList.Count > 0)
                        formData.Fields = (IEnumerable<Sitecore.WFFM.Abstractions.Analytics.FieldData>)fieldDataList;
                }
            }
            return !flag ? (IEnumerable<Sitecore.WFFM.Abstractions.Analytics.FormData>)formDataList : (IEnumerable<Sitecore.WFFM.Abstractions.Analytics.FormData>)new List<Sitecore.WFFM.Abstractions.Analytics.FormData>();
        }

        public virtual void InsertFormData(Sitecore.WFFM.Abstractions.Analytics.FormData form)
        {
            if (_settings.IsRemoteActions)
            {
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            using (IDbConnection dbConnection = _connectionProvider.GetConnection(_connectionString))
            {
                dbConnection.Open();
                using (IDbTransaction dbTransaction = dbConnection.BeginTransaction())
                {
                    using (IDbCommand dbCommand = dbConnection.CreateCommand())
                    {
                        int num = 1;
                        dbCommand.Transaction = dbTransaction;
                        dbCommand.Connection = dbConnection;
                        Guid guid = Guid.NewGuid();
                        stringBuilder.AppendFormat("INSERT INTO [FormData] ([Id],[FormItemId],[ContactId],[InteractionId],[Timestamp]) VALUES ( @{0}, @{1}, @{2}, @{3}, @{4} ) ", AddParameter(dbCommand.Parameters, num++, guid), AddParameter(dbCommand.Parameters, num++, form.FormID), AddParameter(dbCommand.Parameters, num++, form.ContactId), AddParameter(dbCommand.Parameters, num++, form.InteractionId), AddParameter(dbCommand.Parameters, num++, form.Timestamp));
                        if (form.Fields != null)
                        {
                            foreach (Sitecore.WFFM.Abstractions.Analytics.FieldData field in form.Fields)
                            {
                                Guid guid2 = Guid.NewGuid();
                                stringBuilder.AppendFormat("INSERT INTO [FieldData] ([Id],[FormId],[FieldItemId],[FieldName],[Value],[Data]) VALUES ( @{0}, @{1}, @{2}, @{3}, @{4}, @{5} ) ", AddParameter(dbCommand.Parameters, num++, guid2), AddParameter(dbCommand.Parameters, num++, guid), AddParameter(dbCommand.Parameters, num++, field.FieldId), AddParameter(dbCommand.Parameters, num++, field.FieldName), AddParameter(dbCommand.Parameters, num++, field.Value), AddParameter(dbCommand.Parameters, num++, ((object)field.Data) ?? ((object)DBNull.Value)));
                            }
                        }
                        dbCommand.CommandText = stringBuilder.ToString();
                        dbCommand.CommandType = CommandType.Text;
                        dbCommand.ExecuteNonQuery();
                    }

                    dbTransaction.Commit();
                }
            }
        }

        public virtual IFormStatistics GetFormStatistics(Guid formId)
        {
            if (this._settings.IsRemoteActions)
                return (IFormStatistics)new FormStatistics();
            int result;
            using (IDbConnection connection = this._connectionProvider.GetConnection(this._connectionString))
            {
                connection.Open();
                using (IDbTransaction dbTransaction = connection.BeginTransaction())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = dbTransaction;
                        command.Connection = connection;
                        command.CommandText = "SELECT COUNT(Id) AS submit_count FROM [FormData] WHERE [FormItemId]=@p1";
                        command.Parameters.Add((object)new SqlParameter("p1", (object)formId));
                        command.CommandType = CommandType.Text;
                        if (!int.TryParse((command.ExecuteScalar() ?? (object)0).ToString(), out result))
                            result = 0;
                    }
                    dbTransaction.Commit();
                }
            }
            return (IFormStatistics)new FormStatistics()
            {
                FormId = formId,
                SuccessSubmits = result
            };
        }

        public virtual IEnumerable<IFormFieldStatistics> GetFormFieldsStatistics(
          Guid formId)
        {
            if (this._settings.IsRemoteActions)
                return (IEnumerable<IFormFieldStatistics>)new List<IFormFieldStatistics>();
            List<IFormFieldStatistics> formFieldStatisticsList = new List<IFormFieldStatistics>();
            using (IDbConnection connection = this._connectionProvider.GetConnection(this._connectionString))
            {
                connection.Open();
                using (IDbTransaction dbTransaction = connection.BeginTransaction())
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.Transaction = dbTransaction;
                        command.Connection = connection;
                        command.CommandText = "select FieldItemId as fieldid, max(FieldName) fieldname, COUNT(FormId) as submit_count \r\nfrom FieldData, FormData\r\nwhere FieldData.FormId=FormData.Id\r\nand FormItemId=@p1\r\ngroup by FieldItemId";
                        command.Parameters.Add((object)new SqlParameter("p1", (object)formId));
                        command.CommandType = CommandType.Text;
                        IDataReader dataReader = command.ExecuteReader();
                        try
                        {
                            while (dataReader.Read())
                            {
                                FormFieldStatistics formFieldStatistics = new FormFieldStatistics()
                                {
                                    FieldId = new Guid(dataReader["fieldid"].ToString()),
                                    FieldName = dataReader["fieldname"] as string,
                                    Count = System.Convert.ToInt32(dataReader["submit_count"])
                                };
                                formFieldStatisticsList.Add((IFormFieldStatistics)formFieldStatistics);
                            }
                        }
                        finally
                        {
                            dataReader.Close();
                        }
                    }
                    dbTransaction.Commit();
                }
            }
            return (IEnumerable<IFormFieldStatistics>)formFieldStatisticsList;
        }

        public virtual IEnumerable<IFormContactsResult> GetFormsStatisticsByContact(
          Guid formId,
          PageCriteria pageCriteria)
        {
            return (IEnumerable<IFormContactsResult>)new List<IFormContactsResult>();
        }

        private string AddParameter(
          IDataParameterCollection parameters,
          int parameterNumber,
          object parameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter("p" + (object)parameterNumber, parameterValue);
            parameters.Add((object)sqlParameter);
            return sqlParameter.ParameterName;
        }
    }
}
