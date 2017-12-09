using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Modified: 06/28/2015
/// Bob O'Dell
/// This class is used by Question and Option Editing pages
/// The id could be question id or option id and the numeric the sort order
/// </summary>
public class SortQuesOpt : IComparable<SortQuesOpt>
{
	private int id;    
    private double sortorder;
    private string questype;
    private string quesgrid;

    // if we did sort by these - usage would be TieredPersonnel.Sort(TieredPersonnel.JobCodeComparison)
    /*public static Comparison<TieredPersonnel> JobCodeComparison = delegate(TieredPersonnel p1, TieredPersonnel p2)  
        {
            return p1.jobcode.CompareTo(p2.jobcode);                                                        
        }; */

    /*public static Comparison<TieredPersonnel> JobDescriptionComparison = delegate(TieredPersonnel p1, TieredPersonnel p2)                                                     
        {                                                        
            return p1.jobdescription.CompareTo(p2.jobdescription);                                                     
        }; */

    public int Id    
    {
        get { return id; }
        set { id = value; }    
    }
    public double SortOrder    
    {
        get { return sortorder; }
        set { sortorder = value; }    
    }
    public string QuesType
    {
        get { return questype; }
        set { questype = value; }
    }
    public string QuesGrid
    {
        get { return quesgrid; }
        set { quesgrid = value; }
    }
    public SortQuesOpt(int id, double sortorder, string questype, string quesgrid)   
    {
        this.id = id;
        this.sortorder = sortorder;
        this.questype = questype;
        this.quesgrid = quesgrid;
    }

    #region IComparable<SortQuesOpt> Members
    public int CompareTo(SortQuesOpt other)    
    {        
        return SortOrder.CompareTo(other.SortOrder);    
    }    
    #endregion

}

/// <summary>
/// Modified: 06/28/2015
/// Bob O'Dell
/// This class is used by Grid and Likert Resequence page to sort subQuestions
/// The id could be question id or option id and the numeric the sort order
/// </summary>
public class SortQues : IComparable<SortQues>
{
    private int id;
    private double sortorder;
    private string questype;
    private string quesgrid;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public double SortOrder
    {
        get { return sortorder; }
        set { sortorder = value; }
    }
    public string QuesType
    {
        get { return questype; }
        set { questype = value; }
    }
    public string QuesGroup
    {
        get { return quesgrid; }
        set { quesgrid = value; }
    }
    public SortQues(int id, double sortorder, string questype, string quesgrid)
    {
        this.id = id;
        this.sortorder = sortorder;
        this.questype = questype;
        this.quesgrid = quesgrid;
    }

    #region IComparable<SortQues> Members
    public int CompareTo(SortQues other)
    {
        return SortOrder.CompareTo(other.SortOrder);
    }
    #endregion

}