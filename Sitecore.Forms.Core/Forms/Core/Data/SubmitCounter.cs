// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.SubmitCounter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Form.Core.Utility;
using System;
using System.Collections;
using System.Web;

namespace Sitecore.Forms.Core.Data
{
  [Serializable]
  public class SubmitCounter : ISubmitCounter
  {
    private readonly Hashtable allCounter = new Hashtable();
    private readonly object lockObject = new object();
    private static SubmitCounter server;

    internal SubmitCounter()
    {
    }

    public static SubmitCounter Server
    {
      get
      {
        if (SubmitCounter.server == null && HttpContext.Current != null)
          SubmitCounter.server = HttpContext.Current.Application.GetSubmitCounter();
        return SubmitCounter.server;
      }
    }

    public static SubmitCounter Session => HttpContext.Current != null ? HttpContext.Current.Session.GetSubmitCounter() : (SubmitCounter) null;

    public virtual int GetSubmitCount(ID formID) => this.GetCounter(formID).Total;

    public virtual void AddSubmit(ID form, uint keepItMinutes)
    {
      if (keepItMinutes == 0U)
        return;
      SubmitCounter.Counter counter = this.GetCounter(form);
      counter.UpperBound = keepItMinutes;
      counter.IncCounter();
    }

    private SubmitCounter.Counter GetCounter(ID formID)
    {
      if (this.allCounter.Contains((object) formID))
        return (SubmitCounter.Counter) this.allCounter[(object) formID];
      lock (this.lockObject)
      {
        if (this.allCounter.Contains((object) formID))
          return (SubmitCounter.Counter) this.allCounter[(object) formID];
        SubmitCounter.Counter counter = new SubmitCounter.Counter();
        this.allCounter[(object) formID] = (object) counter;
        return counter;
      }
    }

    [Serializable]
    public class Counter
    {
      protected int[] array;
      private uint upperBound;

      public Counter()
      {
        this.array = new int[60];
        this.LastIncTime = DateTime.Now;
        this.upperBound = 60U;
        DateTime now = DateTime.Now;
        int year = now.Year;
        now = DateTime.Now;
        int month = now.Month;
        now = DateTime.Now;
        int day = now.Day;
        this.BenchMark = new DateTime(year, month, day, 0, 0, 0);
      }

      public DateTime LastIncTime { get; protected set; }

      public int Total
      {
        get
        {
          this.ClearObsolete(DateTime.Now);
          int num = 0;
          for (int index = 0; (long) index < (long) this.UpperBound; ++index)
            num += this.array[index];
          return num;
        }
      }

      public uint UpperBound
      {
        get => this.upperBound;
        set
        {
          if ((int) this.upperBound == (int) value)
            return;
          this.upperBound = value;
          if (value > 60U)
            this.array = new int[(int) this.upperBound];
          else
            this.Clear(0, this.array.Length);
        }
      }

      protected DateTime BenchMark { get; set; }

      protected internal void IncCounter()
      {
                DateTime now = DateTime.Now;
                ClearObsolete(now);
                array[(long)(int)(now - BenchMark).TotalMinutes % (long)UpperBound]++;
                LastIncTime = now;
      }

      protected void ClearObsolete(DateTime time)
      {
        int totalMinutes = (int) (time - this.LastIncTime).TotalMinutes;
        if (totalMinutes == 0)
          return;
        if ((long) totalMinutes >= (long) this.UpperBound)
          this.Clear(0, (int) this.UpperBound);
        else
          this.Clear((int) ((this.LastIncTime - this.BenchMark).TotalMinutes % (double) this.UpperBound + 1.0), (int) ((time - this.BenchMark).TotalMinutes % (double) this.UpperBound));
      }

      private void Clear(int start, int end)
      {
        if (start <= end)
        {
          this.Zero(start, end);
        }
        else
        {
          this.Zero(0, end);
          this.Zero(start, this.array.Length);
        }
      }

      private void Zero(int start, int end)
      {
        for (int index = start; index < end; ++index)
          this.array[index] = 0;
      }
    }
  }
}
