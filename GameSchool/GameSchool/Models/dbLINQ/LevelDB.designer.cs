﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameSchool.Models.dbLINQ
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="verk2012_hopur07")]
	public partial class LevelDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertLevelModel(LevelModel instance);
    partial void UpdateLevelModel(LevelModel instance);
    partial void DeleteLevelModel(LevelModel instance);
    partial void InsertLevelAmountCompletion(LevelAmountCompletion instance);
    partial void UpdateLevelAmountCompletion(LevelAmountCompletion instance);
    partial void DeleteLevelAmountCompletion(LevelAmountCompletion instance);
    #endregion
		
		public LevelDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["verk2012_hopur07ConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public LevelDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LevelDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LevelDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LevelDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<LevelModel> LevelModels
		{
			get
			{
				return this.GetTable<LevelModel>();
			}
		}
		
		public System.Data.Linq.Table<LevelCompletion> LevelCompletions
		{
			get
			{
				return this.GetTable<LevelCompletion>();
			}
		}
		
		public System.Data.Linq.Table<LevelAmountCompletion> LevelAmountCompletions
		{
			get
			{
				return this.GetTable<LevelAmountCompletion>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LevelModel")]
	public partial class LevelModel : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _CourseID;
		
		private int _NumberInCourse;
		
		private string _Name;
		
		private string _Description;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnCourseIDChanging(int value);
    partial void OnCourseIDChanged();
    partial void OnNumberInCourseChanging(int value);
    partial void OnNumberInCourseChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
		
		public LevelModel()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CourseID", DbType="Int NOT NULL")]
		public int CourseID
		{
			get
			{
				return this._CourseID;
			}
			set
			{
				if ((this._CourseID != value))
				{
					this.OnCourseIDChanging(value);
					this.SendPropertyChanging();
					this._CourseID = value;
					this.SendPropertyChanged("CourseID");
					this.OnCourseIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumberInCourse", DbType="Int NOT NULL")]
		public int NumberInCourse
		{
			get
			{
				return this._NumberInCourse;
			}
			set
			{
				if ((this._NumberInCourse != value))
				{
					this.OnNumberInCourseChanging(value);
					this.SendPropertyChanging();
					this._NumberInCourse = value;
					this.SendPropertyChanged("NumberInCourse");
					this.OnNumberInCourseChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(MAX)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LevelCompletion")]
	public partial class LevelCompletion
	{
		
		private int _ID;
		
		private string _StudentName;
		
		private int _LevelID;
		
		public LevelCompletion()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StudentName", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string StudentName
		{
			get
			{
				return this._StudentName;
			}
			set
			{
				if ((this._StudentName != value))
				{
					this._StudentName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LevelID", DbType="Int NOT NULL")]
		public int LevelID
		{
			get
			{
				return this._LevelID;
			}
			set
			{
				if ((this._LevelID != value))
				{
					this._LevelID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LevelAmountCompletion")]
	public partial class LevelAmountCompletion : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _StudentName;
		
		private System.Nullable<int> _LevelsCompleted;
		
		private int _CourseID;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnStudentNameChanging(string value);
    partial void OnStudentNameChanged();
    partial void OnLevelsCompletedChanging(System.Nullable<int> value);
    partial void OnLevelsCompletedChanged();
    partial void OnCourseIDChanging(int value);
    partial void OnCourseIDChanged();
    #endregion
		
		public LevelAmountCompletion()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StudentName", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string StudentName
		{
			get
			{
				return this._StudentName;
			}
			set
			{
				if ((this._StudentName != value))
				{
					this.OnStudentNameChanging(value);
					this.SendPropertyChanging();
					this._StudentName = value;
					this.SendPropertyChanged("StudentName");
					this.OnStudentNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LevelsCompleted", DbType="Int")]
		public System.Nullable<int> LevelsCompleted
		{
			get
			{
				return this._LevelsCompleted;
			}
			set
			{
				if ((this._LevelsCompleted != value))
				{
					this.OnLevelsCompletedChanging(value);
					this.SendPropertyChanging();
					this._LevelsCompleted = value;
					this.SendPropertyChanged("LevelsCompleted");
					this.OnLevelsCompletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CourseID", DbType="Int NOT NULL")]
		public int CourseID
		{
			get
			{
				return this._CourseID;
			}
			set
			{
				if ((this._CourseID != value))
				{
					this.OnCourseIDChanging(value);
					this.SendPropertyChanging();
					this._CourseID = value;
					this.SendPropertyChanged("CourseID");
					this.OnCourseIDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
