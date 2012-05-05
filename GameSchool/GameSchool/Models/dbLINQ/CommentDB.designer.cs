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
	public partial class CommentDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCommentModel(CommentModel instance);
    partial void UpdateCommentModel(CommentModel instance);
    partial void DeleteCommentModel(CommentModel instance);
    partial void InsertCommentsInNewsFeed(CommentsInNewsFeed instance);
    partial void UpdateCommentsInNewsFeed(CommentsInNewsFeed instance);
    partial void DeleteCommentsInNewsFeed(CommentsInNewsFeed instance);
    #endregion
		
		public CommentDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["verk2012_hopur07ConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CommentDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CommentDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CommentDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CommentDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CommentModel> CommentModels
		{
			get
			{
				return this.GetTable<CommentModel>();
			}
		}
		
		public System.Data.Linq.Table<CommentsInNewsFeed> CommentsInNewsFeeds
		{
			get
			{
				return this.GetTable<CommentsInNewsFeed>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CommentModel")]
	public partial class CommentModel : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _CommentText;
		
		private string _UserName;
		
		private System.DateTime _CommentDate;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnCommentTextChanging(string value);
    partial void OnCommentTextChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnCommentDateChanging(System.DateTime value);
    partial void OnCommentDateChanged();
    #endregion
		
		public CommentModel()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CommentText", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string CommentText
		{
			get
			{
				return this._CommentText;
			}
			set
			{
				if ((this._CommentText != value))
				{
					this.OnCommentTextChanging(value);
					this.SendPropertyChanging();
					this._CommentText = value;
					this.SendPropertyChanged("CommentText");
					this.OnCommentTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CommentDate", DbType="SmallDateTime NOT NULL")]
		public System.DateTime CommentDate
		{
			get
			{
				return this._CommentDate;
			}
			set
			{
				if ((this._CommentDate != value))
				{
					this.OnCommentDateChanging(value);
					this.SendPropertyChanging();
					this._CommentDate = value;
					this.SendPropertyChanged("CommentDate");
					this.OnCommentDateChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CommentsInNewsFeed")]
	public partial class CommentsInNewsFeed : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _NewsFeedID;
		
		private int _CommentID;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNewsFeedIDChanging(int value);
    partial void OnNewsFeedIDChanged();
    partial void OnCommentIDChanging(int value);
    partial void OnCommentIDChanged();
    #endregion
		
		public CommentsInNewsFeed()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NewsFeedID", DbType="Int NOT NULL")]
		public int NewsFeedID
		{
			get
			{
				return this._NewsFeedID;
			}
			set
			{
				if ((this._NewsFeedID != value))
				{
					this.OnNewsFeedIDChanging(value);
					this.SendPropertyChanging();
					this._NewsFeedID = value;
					this.SendPropertyChanged("NewsFeedID");
					this.OnNewsFeedIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CommentID", DbType="Int NOT NULL")]
		public int CommentID
		{
			get
			{
				return this._CommentID;
			}
			set
			{
				if ((this._CommentID != value))
				{
					this.OnCommentIDChanging(value);
					this.SendPropertyChanging();
					this._CommentID = value;
					this.SendPropertyChanged("CommentID");
					this.OnCommentIDChanged();
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