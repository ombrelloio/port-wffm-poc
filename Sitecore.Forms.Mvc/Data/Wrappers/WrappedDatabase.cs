// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Data.Wrappers.WrappedDatabase
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Forms.Mvc.Data.Wrappers
{
  public class WrappedDatabase : IDatabase
  {
    private readonly Database sitecoreDatabase;

    public WrappedDatabase(Database database) => this.sitecoreDatabase = database;

    public Item GetItem(string id) => this.sitecoreDatabase.GetItem(id);

    public Item GetItem(ID id) => this.sitecoreDatabase.GetItem(id);
  }
}
